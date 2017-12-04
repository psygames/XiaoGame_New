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
            network.server.onConnected = OnClientConnected;
            network.server.onClosed = OnClientClosed;
            network.RegisterNetworkAll(OnClientMessage);
        }

        private Dictionary<string, UserMessageHandle> m_clientHandles = new Dictionary<string, UserMessageHandle>();
        private Dictionary<string, UserData> m_userDatas = new Dictionary<string, UserData>();

        private void OnClientClosed(string sessionID)
        {
            if (m_userDatas[sessionID].state != UserState.Offline)
            {
                m_clientHandles[sessionID].Logout();
            }

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
            return m_userDatas.Values.First(a => a.uid == uid);
        }

        public UserMessageHandle GetHandle(long uid)
        {
            return m_clientHandles[GetData(uid).sessionID];
        }
    }
}
