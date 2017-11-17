using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Message;


namespace RedStone
{
    public class ClientProxy : MCProxyBase
    {
        public override void OnInit()
        {
            base.OnInit();
            network.server.onConnected = OnClientLogin;
            network.RegisterNetworkAll(OnClientMessage);
        }

        private Dictionary<string, ClientMessageHandle> m_clientHandles = new Dictionary<string, ClientMessageHandle>();

        private void OnClientLogin(string sessionID)
        {
            if (!m_clientHandles.ContainsKey(sessionID))
            {
                var handle = new ClientMessageHandle();
                handle.Init();
                m_clientHandles.Add(sessionID, handle);
            }
        }


        private void OnClientMessage(string sessionID, object msg)
        {
            ClientMessageHandle handle = null;
            if (m_clientHandles.TryGetValue(sessionID, out handle))
            {
                handle.OnMessage(msg);
            }
        }


        private void OnClientForceQuit(long pid)
        {
        }


    }
}
