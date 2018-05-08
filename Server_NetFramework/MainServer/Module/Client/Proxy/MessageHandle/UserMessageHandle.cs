using System;
using System.Collections.Generic;
using System.Text;
using Message;
using RedStone.Data;
using System.Linq;

namespace RedStone
{

    public class UserMessageHandle
    {
        public string sessionID { get; private set; }
        private EventManager m_eventMgr = new EventManager();

        public UserDaoProxy dao { get { return ProxyManager.instance.GetProxy<UserDaoProxy>(); } }
        public UserProxy userProxy { get { return ProxyManager.instance.GetProxy<UserProxy>(); } }
        public BattleServerProxy battleProxy { get { return ProxyManager.instance.GetProxy<BattleServerProxy>(); } }
        public MatchPoolProxy matchProxy { get { return ProxyManager.instance.GetProxy<MatchPoolProxy>(); } }
        private UserData data { get { return userProxy.GetData(sessionID); } }


        public void Init(string sessionID)
        {
            this.sessionID = sessionID;
            RegisterMsg<CMLoginRequest>(OnLogin);
            RegisterMsg<CMMatchRequest>(OnBeginMatch);
            RegisterMsg<CMMatchCancel>(OnCancelMatch);
            RegisterMsg<CMCancelReconnect>(OnCancelReconnect);
        }

        public void Logout()
        {
            data.Logout();
            dao.Logout(data.uid);
            Logger.Log($"{data.uid} logout");
        }

        private void OnLogin(CMLoginRequest msg)
        {
            var db = dao.Login(msg.DeviceID);
            data.SetData(sessionID, db);
            data.Login();

            Logger.Log($"{data.uid} login");

            CMLoginReply reply = new CMLoginReply();
            reply.PlayerInfo = new PlayerInfo();
            reply.PlayerInfo.Exp = data.exp;
            reply.PlayerInfo.Gold = data.gold;
            reply.PlayerInfo.Level = data.level;
            reply.PlayerInfo.Name = data.name;
            reply.PlayerInfo.Uid = data.uid;
            reply.IsInBattle = false;


            var room = battleProxy.GetUserRoom(data.uid);
            if (room != null)
            {
                reply.IsInBattle = true;
                reply.BattleServerInfo = new BattleServerInfo();
                reply.BattleServerInfo.Address = room.battleServer.address;
                reply.BattleServerInfo.Name = room.battleServer.name;
                reply.BattleServerInfo.Token = room.userTokens.First(a=>a.Uid == data.uid).Token;
                reply.BattleServerInfo.State = room.battleServer.state.ToString();
            }

            SendMessage(reply);

            if(reply.IsInBattle)
                data.SetState(UserState.Game);
            else
                data.SetState(UserState.Hall);
        }


        private void OnBeginMatch(CMMatchRequest req)
        {
            //TODO: Game Id Game Mode...

            CMMatchReply rep = new CMMatchReply();
            if (battleProxy.GetBestBattleServer() == null)
            {
                rep.Status = 0; //failed
                rep.WaitTime = 0;
            }
            else
            {
                rep.Status = 1;
                rep.WaitTime = 3; //TODO: WaitTime
                data.SetState(UserState.Matching);
                matchProxy.Add(data.uid, req.GameID, req.GameMode, req.GroupID);
            }

            SendMessage(rep);
        }

        private void OnCancelMatch(CMMatchCancel req)
        {
            if (data.state == UserState.Matching)
                data.SetState(UserState.Hall);
            matchProxy.Remove(data.uid);
        }


        private void OnCancelReconnect(CMCancelReconnect msg)
        {
            var room = battleProxy.GetUserRoom(data.uid);
            if (room != null)
                room.RemoveUser(data.uid);

            if (data.state == UserState.Game)
                data.SetState(UserState.Hall);
        }


        public void MactchSuccess(BattleServerData server, RoomData room)
        {
            CMMatchSuccess msg = new CMMatchSuccess();
            msg.BattleServerInfo = new BattleServerInfo();
            msg.BattleServerInfo.Address = server.address;
            msg.BattleServerInfo.Name = server.name;
            msg.BattleServerInfo.State = server.state.ToString();
            msg.BattleServerInfo.Token = room.userTokens.First(a => a.Uid == data.uid).Token;
            SendMessage(msg);
            data.SetState(UserState.Game);
        }


        private void RegisterMsg<T>(Action<T> action)
        {
            m_eventMgr.Register(typeof(T).Name, action);
        }

        public void OnMessage(object message)
        {
            m_eventMgr.Send(message.GetType().Name, message);
        }

        public void SendMessage<T>(T msg)
        {
            userProxy.SendMessage(sessionID, msg);
        }
    }
}
