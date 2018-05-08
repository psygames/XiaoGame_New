using System;
using Core;

namespace NetworkLib
{
    public class ClientNetworkManager
    {
        protected EventManager m_eventMgr = null;
        protected ISerializer m_serializer = null;
        protected IClient m_socket = null;

        public Action onConnected { get; set; }
        public Action onClosed { get; set; }
        public ClientBase socket { get { return m_socket as ClientBase; } }

        public ClientNetworkManager(IClient client, ISerializer serializer)
        {
            m_eventMgr = new EventManager();
            m_serializer = serializer;
            m_socket = client;

            m_socket.onReceived += OnReceived;
            m_socket.onConnected += OnConnected;
            m_socket.onClosed += OnClosed;
        }

        ~ClientNetworkManager()
        {
            m_socket.onReceived -= OnReceived;
            m_socket.onConnected -= OnConnected;
            m_socket.onClosed -= OnClosed;
        }

        public void Init(string ip, int port)
        {
            m_serializer.Init();
            m_socket.Setup(ip, port);
        }

        public void Connect()
        {
            m_socket.Connect();
        }
        public void Close()
        {
            m_socket.Close();
        }

        public void RegisterNetwork<T>(Action<T> action)
        {
            m_eventMgr.Register(typeof(T).Name, action);
        }

        private void RegisterNetworkOnce<T>(Action<T> action)
        {
            Action<T> _action = null;
            _action = (t) =>
            {
                action.Invoke(t);
                m_eventMgr.UnRegister(typeof(T).Name, _action);
            };
            RegisterNetwork(_action);
        }

        protected virtual void OnReceived(byte[] data)
        {
            object obj = m_serializer.Deserialize(data);
            m_eventMgr.Send(obj.GetType().Name, obj);
        }

        protected virtual void OnConnected()
        {
            if (onConnected != null)
                onConnected.Invoke();
        }

        protected virtual void OnClosed()
        {
            if (onClosed != null)
                onClosed.Invoke();
        }

        public void Send<T>(T msg)
        {
            var data = m_serializer.Serialize(msg as Google.Protobuf.IMessage);
            m_socket.Send(data);
        }

        public void Send<TSend, TReply>(TSend msg, Action<TReply> action)
        {
            RegisterNetworkOnce(action);
            Send(msg);
        }
    }
}
