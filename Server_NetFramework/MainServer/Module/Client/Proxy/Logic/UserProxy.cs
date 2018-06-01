using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Message;
using RedStone.Data;


namespace RedStone
{
    public class UserProxy : MCProxyBase
    {
        public override void OnInit()
        {
            base.OnInit();
            network.onConnected = OnClientConnected;
            network.onClosed = OnClientClosed;
            network.heartbeat.onTimeout = OnTimeout;
            network.RegisterNetworkAll(OnClientMessage);
        }

        private Dictionary<string, UserMessageHandle> m_clientHandles = new Dictionary<string, UserMessageHandle>();
        private Dictionary<string, UserData> m_userDatas = new Dictionary<string, UserData>();

        private void OnTimeout(string sessionID)
        {
            if (m_clientHandles.ContainsKey(sessionID))
            {
                //TODO:这里需要考虑心跳超时后的，断连，数据清理，Session缓存交换，
                Logger.LogError($"{sessionID} heartbeat timeout!!!");
            }
        }

        private void OnClientClosed(string sessionID)
        {
            m_clientHandles[sessionID].Logout();
            m_clientHandles.Remove(sessionID);
            m_userDatas.Remove(sessionID);
        }

        private void OnClientConnected(string sessionID)
        {
            if (!m_clientHandles.ContainsKey(sessionID))
            {
                var handle = new UserMessageHandle();
                handle.Init(sessionID);
                m_clientHandles.Add(sessionID, handle);
            }

            if (!m_userDatas.ContainsKey(sessionID))
            {
                var user = new UserData();
                user.SetSessionID(sessionID);
                m_userDatas.Add(sessionID, user);
            }
        }








        // Update
        public override void OnUpdate()
        {
            base.OnUpdate();

            UpdateMatch();
        }

        private void UpdateMatch()
        {
            var matchedUsers = GetProxy<MatchPoolProxy>().GetMatched();
            if (matchedUsers == null)
                return;

            // Match Success Create Room and Send Back
            var server = GetProxy<BattleServerProxy>().GetBestBattleServer();
            GetProxy<BattleServerProxy>().CreateRoom(server.sessionID, matchedUsers, (roomData) =>
            {
                foreach (var uid in matchedUsers)
                {
                    GetHandle(uid).MactchSuccess(server, roomData);
                }
            });
        }






        private void OnClientMessage(string sessionID, object msg)
        {
            UserMessageHandle handle = null;
            if (m_clientHandles.TryGetValue(sessionID, out handle))
            {
                handle.OnMessage(msg);
            }
        }


        public UserData GetData(string sessionID)
        {
            return m_userDatas[sessionID];
        }

        public UserData GetData(long uid)
        {
            return m_userDatas.Values.FirstOrDefault(a => a.uid == uid);
        }

        public UserMessageHandle GetHandle(long uid)
        {
            return m_clientHandles[GetData(uid).sessionID];
        }


        public void InfoSync(long uid)
        {
            var info = GetProxy<UserDaoProxy>().GetUserDB(uid);
            CMPlayerInfoSync msg = new CMPlayerInfoSync();
            msg.Info = new PlayerInfo();
            msg.Info.Uid = info.uid;
            msg.Info.Name = info.name;
            msg.Info.Level = info.level;
            msg.Info.Exp = info.exp;
            msg.Info.Gold = info.gold;
            var user = GetProxy<UserProxy>().GetData(uid);
            if (user != null)
                SendMessage(user.sessionID, msg);
        }
    }
}
