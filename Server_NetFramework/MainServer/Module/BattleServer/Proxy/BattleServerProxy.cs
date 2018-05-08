using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Message;
using RedStone.Data;

namespace RedStone
{
    public class BattleServerProxy : MBProxyBase
    {
        private Dictionary<string, BattleServerData> m_datas = new Dictionary<string, BattleServerData>();


        public BattleServerData GetData(string sessionID)
        {
            return m_datas[sessionID];
        }

        public BattleServerData GetBestBattleServer()
        {
            // TODO: Best Battle Server , using ping or status
            return m_datas.Values.FirstOrDefault();
        }


        public override void OnInit()
        {
            base.OnInit();

            network.onConnected = OnConnected;
            network.onClosed = OnClosed;
            RegisterMessage<BMLoginRequest>(OnLogin);
        }

        void OnConnected(string sessionID)
        {
            if (!m_datas.ContainsKey(sessionID))
            {
                var data = new BattleServerData();
                data.SetSessionID(sessionID);
                m_datas.Add(sessionID, data);
            }
        }

        void OnClosed(string sessionID)
        {
            Logger.Log($"战场断链, {m_datas[sessionID].name}");
            m_datas.Remove(sessionID);
        }

        void OnLogin(string sessionID, BMLoginRequest msg)
        {
            GetData(sessionID).SetData("Hip-Hop", msg.ListenerAddress);
            BMLoginReply reply = new BMLoginReply();
            reply.Name = GetData(sessionID).name;
            SendMessage(sessionID, reply);
            Logger.Log($"战场登录成功, 战场名:{reply.Name}");
        }




        //TODO: BATTLE END Remove Room
        public void CreateRoom(string battleSessionID, List<long> users, Action<RoomData> createdCallback)
        {
            RoomData data = new RoomData();
            BMCreateRoomRequest req = new BMCreateRoomRequest();

            foreach (var uid in users)
            {
                var user = GetProxy<UserProxy>().GetData(uid);
                PlayerInfo player = new PlayerInfo();
                player.Uid = user.uid;
                player.Exp = user.exp;
                player.Gold = user.gold;
                player.Level = user.level;
                player.Name = user.name;
                req.Users.Add(player);
            }

            SendMessage<BMCreateRoomRequest, BMCreateRoomReply>(battleSessionID, req,
            (sessionID, rep) =>
            {
                data.SetData(sessionID, rep.RoomID, rep.PlayerTokens, rep.RoomName);
                data.SetUsers(users);
                GetData(sessionID).AddRoom(data);
                createdCallback.Invoke(data);
            });
        }

        public RoomData GetUserRoom(long uid)
        {
            foreach (var sv in m_datas)
            {
                var room = sv.Value.rooms.FirstOrDefault(a => a.users.Contains(uid));
                if (room != null)
                    return room;
            }
            return null;
        }
    }
}
