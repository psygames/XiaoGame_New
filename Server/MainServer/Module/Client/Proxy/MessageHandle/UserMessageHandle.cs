using System;
using System.Collections.Generic;
using System.Text;
using Message;
using RedStone.Data;

namespace RedStone
{

    public class UserMessageHandle
    {
        public string sessionID { get; private set; }
        private Plugins.EventManager m_eventMgr = new Plugins.EventManager();

        public ClientDaoProxy dao
        {
            get
            {
                return ProxyManager.instance.GetProxy<ClientDaoProxy>();
            }
        }

        public UserProxy userProxy
        {
            get { return ProxyManager.instance.GetProxy<UserProxy>(); }
        }
        private UserData data { get { return userProxy.GetData(sessionID); } }


        public void Init(string sessionID)
        {
            this.sessionID = sessionID;
            RegisterMsg<CMLoginRequest>(OnLogin);
            RegisterMsg<CMStartGameRequest>(OnStartGame);
        }

        public void Logout()
        {
            data.SetState(UserState.Offline);
            dao.Logout(data.uid);
            Debug.Log($"{data.uid} logout");
        }

        private void OnLogin(CMLoginRequest msg)
        {
            var db = dao.Login(msg.DeviceID);
            data.SetData(sessionID, db);

            Debug.Log($"{data.uid} login");

            CMLoginReply reply = new CMLoginReply();
            reply.PlayerInfo = new PlayerInfo();
            reply.PlayerInfo.Exp = data.exp;
            reply.PlayerInfo.Gold = data.gold;
            reply.PlayerInfo.Level = data.level;
            reply.PlayerInfo.Name = data.name;
            reply.PlayerInfo.Uid = data.uid;
            SendMessage(reply);
        }


        private void OnStartGame(CMStartGameRequest req)
        {
            Debug.Log(req.GameID);

            CMStartGameReply rep = new CMStartGameReply();
            rep.BattleServer = new BattleServerInfo();

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
