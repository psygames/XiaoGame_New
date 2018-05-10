using System;
using Core;
using System.Threading;

namespace NetworkLib
{
    public class ClientNetworkManager : IDisposable
    {
        protected EventManager m_eventMgr = null;
        protected ISerializer m_serializer = null;
        protected IClient m_socket = null;

        public Action onConnected { get; set; }
        public Action onClosed { get; set; }
        public Action onTimeout { get; set; }
        public ClientBase socket { get { return m_socket as ClientBase; } }
        public bool isTimeout { get; protected set; }

        private Timer m_timerHeartbeat = null;
        private Timer m_timerCheckTimeout = null;
        private float m_timeoutDuration;

        public ClientNetworkManager(IClient client, ISerializer serializer
            , float heartbeatInterval = 1f, float timeout = 2f)
        {
            m_eventMgr = new EventManager();
            m_serializer = serializer;
            m_socket = client;

            m_timeoutDuration = timeout;

            m_timerHeartbeat = new Timer(new TimerCallback(OnTimerHeartbeatCallback));
            m_timerHeartbeat.Change(0, (int)(heartbeatInterval * 1000));

            m_timerCheckTimeout = new Timer(new TimerCallback(OnTimerCheckTimeoutCallback));
            m_timerCheckTimeout.Change(0, (int)(timeout * 1000 * 0.5f));

            m_socket.onReceived += OnReceived;
            m_socket.onConnected += OnConnected;
            m_socket.onClosed += OnClosed;
        }

        public void Dispose()
        {
            m_socket.onReceived -= OnReceived;
            m_socket.onConnected -= OnConnected;
            m_socket.onClosed -= OnClosed;
            m_timerCheckTimeout.Dispose();
            m_timerHeartbeat.Dispose();
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
            object obj = null;
            if (GetProtocolNum(data) == HeartbeatReply.PROTOCOL_NUM)
            {
                obj = DeserialzeHeartbeat(data);
                HandleHeartbeatReply(obj as HeartbeatReply);
            }
            else
            {
                obj = m_serializer.Deserialize(data);
            }

            OnReceivedHandle(obj);
        }

        protected void HandleHeartbeatReply(HeartbeatReply msg)
        {
            m_heartbeatReplyTime = time;
        }

        private ushort GetProtocolNum(byte[] data)
        {
            byte[] head = new byte[2];
            Array.Copy(data, head, 2);
            return BitConverter.ToUInt16(head, 0);
        }

        private HeartbeatReply DeserialzeHeartbeat(byte[] data)
        {
            byte[] body = new byte[data.Length - 2];
            Array.Copy(data, 2, body, 0, data.Length - 2);
            HeartbeatReply msg = HeartbeatReply.Parse(body);
            return msg;
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

        private void OnTimerHeartbeatCallback(object _sta)
        {
            lock (this)
            {
                if (socket.state != ClientBase.State.Connected)
                    return;
                SendHeartbeat();
            }
        }

        private int m_heartbeatNumber = 1;
        private void SendHeartbeat()
        {
            HeartbeatRequest msg = new HeartbeatRequest();
            msg.number = m_heartbeatNumber;
            m_socket.Send(msg.Serialize());
            m_heartbeatNumber++;
        }


        private double time { get { return DateTime.Now.Ticks * 0.0000001d; } }
        private double m_heartbeatReplyTime = 0;
        private void OnTimerCheckTimeoutCallback(object _sta)
        {
            lock (this)
            {
                if (!isTimeout && m_heartbeatReplyTime != 0
                    && time - m_heartbeatReplyTime > m_timeoutDuration)
                {
                    isTimeout = true;
                    OnTimeout();
                }
            }
        }

        protected void OnTimeout()
        {
            if (onTimeout != null)
                onTimeout.Invoke();
        }
    }
}
