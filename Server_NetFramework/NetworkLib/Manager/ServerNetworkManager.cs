using System;
using Core;

namespace NetworkLib
{
    public class ServerNetworkManager
    {
        private EventManager m_eventMgr = null;
        private ISerializer m_serializer = null;
        private IServer m_server = null;

        public bool isInit { get; private set; }
        public IServer server { get { return m_server; } }

        public ServerNetworkManager(IServer server, ISerializer serializer)
        {
            m_eventMgr = new EventManager();
            m_server = server;
            m_serializer = serializer;
        }

        public void Init()
        {
            m_serializer.Init();

            if (isInit && m_server != null)
            {
                m_server.onReceived -= OnReceived;
                m_server.onConnected -= OnConnected;
                m_server.onClosed -= OnClosed;
            }
            m_socket = socket;
            m_socket.onReceived += OnReceived;
            m_socket.onConnected += OnConnected;
            m_socket.onClosed += OnClosed;
            isInit = true;
        }

        void OnConnected(string sessionID) {

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
