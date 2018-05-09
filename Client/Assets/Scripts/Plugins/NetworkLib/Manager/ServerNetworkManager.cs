using System;
using Core;

namespace NetworkLib
{
    public class ServerNetworkManager
    {
        protected EventManager m_eventMgr = null;
        protected ISerializer m_serializer = null;
        protected IServer m_server = null;

        public Action<string> onConnected { get; set; }
        public Action<string> onClosed { get; set; }
        public ServerBase server { get { return m_server as ServerBase; } }

        public ServerNetworkManager(IServer server, ISerializer serializer)
        {
            m_eventMgr = new EventManager();
            m_server = server;
            m_serializer = serializer;

            m_server.onReceived += OnReceived;
            m_server.onConnected += OnConnected;
            m_server.onClosed += OnClosed;
        }

        ~ServerNetworkManager()
        {
            m_server.onReceived -= OnReceived;
            m_server.onConnected -= OnConnected;
            m_server.onClosed -= OnClosed;
        }

        public void Init(string ip, int port)
        {
            m_serializer.Init();
            m_server.Setup(ip, port);
        }

        public void Start()
        {
            m_server.Start();
        }

        public void Stop()
        {
            m_server.Stop();
        }

        void OnConnected(string sessionID)
        {
            if (onConnected != null)
                onConnected.Invoke(sessionID);
        }

        void OnClosed(string sessionID)
        {
            if (onClosed != null)
                onClosed.Invoke(sessionID);
        }

        void OnReceived(string sessionID, byte[] data)
        {
            object obj = m_serializer.Deserialize(data);
            m_eventMgr.Send(obj.GetType().Name, sessionID, obj);
        }

        public void RegisterNetworkAll(Action<string, object> action)
        {
            m_eventMgr.RegisterAll(a =>
            {
                object[] items = a as object[];
                action.Invoke(items[0].ToString(), items[1]);
            });
        }

        public void RegisterNetwork<T>(Action<string, T> action)
        {
            m_eventMgr.Register(typeof(T).Name, action);
        }

        private void RegisterNetworkOnce<T>(string sessionID, Action<string, T> action)
        {
            Action<string, T> _action = null;
            _action = (_sessionID, t) =>
            {
                if (_sessionID == sessionID)
                {
                    action.Invoke(sessionID, t);
                    m_eventMgr.UnRegister(typeof(T).Name, _action);
                }
            };
            RegisterNetwork(_action);
        }

        public void Send<T>(string sessionID, T msg)
        {
            var data = m_serializer.Serialize(msg as Google.Protobuf.IMessage);
            m_server.Send(sessionID, data);
        }

        public void Send<TSend, TReply>(string sessionID, TSend msg, Action<string, TReply> action)
        {
            RegisterNetworkOnce(sessionID, action);
            Send(sessionID, msg);
        }
    }
}
