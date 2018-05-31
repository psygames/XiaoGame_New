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
            RegisterMessage<BMBattleResult>(OnBattleResult);
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

        void OnBattleResult(string sessionID, BMBattleResult msg)
        {
            //Calculate
            foreach (var uinfo in msg.RankUsers)
            {
                if (uinfo.IsAI)
                    continue;
                int incrGold = uinfo.Score * 10;
                int incrExp = uinfo.Score - 50;
                GetProxy<UserDaoProxy>().CalReuslt(uinfo.Uid, incrGold, incrExp);
                GetProxy<UserProxy>().InfoSync(uinfo.Uid);
            }

            //REMOVE ROOM
            var bs = GetData(sessionID);
            bs.RemoveRoom(msg.RoomID);
            Logger.Log($"战场（{bs.name}) 房间（{msg.RoomID}）战斗结束，移除房间列表！");
        }

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
