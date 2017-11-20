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
            network.RegisterNetworkAll(OnClientMessage);
        }

        private Dictionary<string, UserMessageHandle> m_clientHandles = new Dictionary<string, UserMessageHandle>();
        private Dictionary<string, UserData> m_userDatas = new Dictionary<string, UserData>();


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

        private void OnClientDisconnected(string sessionID)
        {
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

    }
}
