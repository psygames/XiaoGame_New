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
            return m_datas.Values.First();
        }


        public override void OnInit()
        {
            base.OnInit();

            network.server.onConnected = OnConnected;
            network.server.onClosed = OnClosed;
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
            Debug.Log($"战场断链, {m_datas[sessionID].name}");
            m_datas.Remove(sessionID);
        }

        void OnLogin(string sessionID, BMLoginRequest msg)
        {
            GetData(sessionID).SetData("Hip-Hop", msg.ListenerAddress);
            BMLoginReply reply = new BMLoginReply();
            reply.Name = GetData(sessionID).name;
            SendMessage(sessionID, reply);
            Debug.Log($"战场登录成功, 战场名:{reply.Name}");
        }




        //TODO: BATTLE END Remove Room
        public RoomData CreateRoom(string battleSessionID, List<long> users)
        {
            RoomData data = new RoomData();
            BMCreateRommRequest req = new BMCreateRommRequest();

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

            SendMessage<BMCreateRommRequest, BMCreateRoomReply>(battleSessionID, req,
            (sessionID, rep) =>
            {
                data.SetData(rep.RoomID, rep.Token, rep.Name);
                GetData(sessionID).AddRoom(data);
            });

            data.SetUsers(users);
            return data;
        }
    }
}
