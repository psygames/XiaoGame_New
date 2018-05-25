using System;

namespace NetworkLib
{
    public class ClientNetworkManager : IDisposable
    {
        protected EventManager m_eventMgr = null;
        protected ISerializer m_serializer = null;
        protected IClient m_socket = null;
        protected HeartbeatClient m_heartbeat = null;

        public Action onConnected { get; set; }
        public Action onClosed { get; set; }
        public ClientBase socket { get { return m_socket as ClientBase; } }
        public HeartbeatClient heartbeat { get { return m_heartbeat; } }
        public bool isConneted { get { return socket.state == ClientBase.State.Connected; } }

        public ClientNetworkManager(IClient client, ISerializer serializer)
        {
            m_eventMgr = new EventManager();
            m_serializer = serializer;
            m_socket = client;

            m_socket.onReceived += OnReceived;
            m_socket.onConnected += OnConnected;
            m_socket.onClosed += OnClosed;

            UseHeartbeat();
        }

        public void UseHeartbeat(float interval = 1f, float timeout = 10f)
        {
            m_heartbeat = new HeartbeatClient(interval, timeout, socket);
        }

        public void Dispose()
        {
            m_socket.onReceived -= OnReceived;
            m_socket.onConnected -= OnConnected;
            m_socket.onClosed -= OnClosed;

            if (m_heartbeat != null)
                m_heartbeat.Dispose();
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
            if (GetProtocolNum(data) == HeartbeatReply.PROTOCOL_NUM)
            {
                if (m_heartbeat != null)
                {
                    object obj = m_heartbeat.HandleMessage(data);
                    OnReceivedHandle(obj);
                }
            }
            else
            {
                object obj = m_serializer.Deserialize(data);
                OnReceivedHandle(obj);
            }
        }

        private ushort GetProtocolNum(byte[] data)
        {
            byte[] head = new byte[2];
            Array.Copy(data, head, 2);
            return BitConverter.ToUInt16(head, 0);
        }

        protected virtual void OnReceivedHandle(object data)
        {
            m_eventMgr.Send(data.GetType().Name, data);
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
            var data = m_serializer.Serialize(msg);
            m_socket.Send(data);
        }

        public void Send<TSend, TReply>(TSend msg, Action<TReply> action)
        {
            RegisterNetworkOnce(action);
            Send(msg);
        }
    }
}
