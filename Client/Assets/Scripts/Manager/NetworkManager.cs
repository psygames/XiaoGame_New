using System;
using System.Collections.Generic;
using UnityEngine;
using NetworkLib;

namespace RedStone
{
    public class NetworkManager : Core.Singleton<NetworkManager>
    {
        /// <summary>
        /// 连接主服
        /// </summary>
        public ClientNetworkMangerEx battle { get; private set; }
        /// <summary>
        /// 监听客户端
        /// </summary>
        public ClientNetworkMangerEx main { get; private set; }

        public void Init()
        {
            InitMainServer();
            InitBattleServer();
        }

        private void InitBattleServer()
        {
            battle = new ClientNetworkMangerEx();
        }

        private void InitMainServer()
        {
            main = new ClientNetworkMangerEx();
            var socket = new WebSocketClient();
            socket.Setup(GameManager.instance.serverAddress, 8730);
            var serializer = new ProtoSerializer();
            serializer.getTypeFunc = (name) => { return Type.GetType(name); };
            serializer.LoadProtoNum(typeof(Message.ProtoNum));
            main.Init(socket, serializer);
            Debug.Log("初始化网络连接（主服） [{0}]".FormatStr(socket.address));
        }

        public void Close()
        {
            if (battle.socket != null)
                battle.socket.Close();
            if (main.socket != null)
                main.socket.Close();
        }

        public void Update()
        {
            battle.Update();
            main.Update();
        }


        public class ClientNetworkMangerEx : ClientNetworkManager
        {
            protected override void OnReceived(byte[] data)
            {
                object obj = m_serializer.Deserialize(data);

                m_msgQueue.Enqueue(obj);
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

