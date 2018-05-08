using System;
using Core;

namespace NetworkLib
{
    public class ClientNetworkManager
    {
        protected EventManager m_eventMgr = null;
        protected ISerializer m_serializer = null;
        protected IClient m_socket = null;

        public bool isInit { get; private set; }
        public IClient socket { get { return m_socket; } }
        public Action onConnected { get; set; }
        public Action onClosed { get; set; }

        public ClientNetworkManager()
        {
            m_eventMgr = new EventManager();
            m_serializer = new ProtoSerializer();
        }

        public void Init(IClient socket)
        {
            m_serializer.Init();
            if (isInit && m_socket != null)
            {
                m_socket.onReceived -= OnReceived;
                m_socket.onConnected -= OnConnected;
                m_socket.onClosed -= OnClosed;
            }
            m_socket = socket;
            m_socket.onReceived += OnReceived;
            m_socket.onConnected += OnConnected;
            m_socket.onClosed += OnClosed;
            isInit = true;
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
