using System;
using System.Collections.Generic;
using UnityEngine;
using NetworkLib;

namespace RedStone
{
    public class NetworkManager : Core.Singleton<NetworkManager>
    {
        public ClientNetworkMangerEx battle { get; private set; }
        public ClientNetworkMangerEx main { get; private set; }

        public NetworkManager()
        {
            main = new ClientNetworkMangerEx(new WebSocketClient(), new ProtoSerializer());

            battle = new ClientNetworkMangerEx(new WebSocketClient(), new ProtoSerializer());
        }

        public void Init()
        {
            InitMainServer();
        }

        private void InitMainServer()
        {
            main.Init(GameManager.instance.serverAddress, 8730);
            Debug.Log("初始化网络连接（主服） [{0}]".FormatStr(main.socket.address));
        }

        public void Close()
        {
            if (battle.socket != null && battle.socket.state != ClientBase.State.None)
                battle.socket.Close();
            if (main.socket != null && main.socket.state != ClientBase.State.None)
                main.socket.Close();
        }

        public void Dispose()
        {
            Close();
            battle.Dispose();
            main.Dispose();
        }

        public void Update()
        {
            battle.Update();
            main.Update();
        }


        public class ClientNetworkMangerEx : ClientNetworkManager
        {
            public ClientNetworkMangerEx(IClient client, ISerializer serializer)
                : base(client, serializer)
            {

            }

            protected override void OnReceivedHandle(object data)
            {
                m_msgQueue.Enqueue(data);
            }

            private bool m_triggerConnected = false;
            protected override void OnConnected()
            {
                m_triggerConnected = true;
            }

            private bool m_triggerClosed = false;
            protected override void OnClosed()
            {
                m_triggerClosed = true;
            }

            private Queue<object> m_msgQueue = new Queue<object>();

            public void Update()
            {
                HandlePoolMsg();
            }

            private void HandlePoolMsg()
            {
                while (m_msgQueue.Count > 0)
                {
                    var obj = m_msgQueue.Dequeue();
                    m_eventMgr.Send(obj.GetType().Name, obj);
                }

                if (m_triggerConnected)
                {
                    base.OnConnected();
                    m_triggerConnected = false;
                }

                if (m_triggerClosed)
                {
                    base.OnClosed();
                    m_triggerClosed = false;
                }
            }
        }
    }
}

