using System;
using System.Collections.Generic;
using System.Threading;
using Core;

namespace NetworkLib
{
    public class ServerNetworkManager : IDisposable
    {
        protected EventManager m_eventMgr = null;
        protected ISerializer m_serializer = null;
        protected IServer m_server = null;
        protected HeartbeatServer m_heartbeat = null;

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

        public void Dispose()
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

        protected virtual void OnConnected(string sessionID)
        {
            if (onConnected != null)
                onConnected.Invoke(sessionID);
        }

        protected virtual void OnClosed(string sessionID)
        {
            if (onClosed != null)
                onClosed.Invoke(sessionID);
        }

        protected virtual void OnReceived(string sessionID, byte[] data)
        {
            object obj = null;
            var protocolNum = GetProtocolNum(data);
            if (protocolNum == HeartbeatRequest.PROTOCOL_NUM)
            {
                obj = DeserialzeHeartbeat(data);
                OnReceivedHandle(sessionID, obj);
                HandleHeartbeatRequest(sessionID, obj as HeartbeatRequest);
            }
            else
            {
                obj = m_serializer.Deserialize(data);
                OnReceivedHandle(sessionID, obj);
            }
        }

        private void HandleHeartbeatRequest(string sessionID, HeartbeatRequest msg)
        {
            m_sessionHeartbeatTime[sessionID] = time;
            ReplyHeartbeat(sessionID, msg.number);
        }

        private ushort GetProtocolNum(byte[] data)
        {
            byte[] head = new byte[2];
            Array.Copy(data, head, 2);
            return BitConverter.ToUInt16(head, 0);
        }

        protected virtual void OnReceivedHandle(string sessionID, object data)
        {
            m_eventMgr.Send(data.GetType().Name, sessionID, data);
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
            var data = m_serializer.Serialize(msg);
            m_server.Send(sessionID, data);
        }

        public void Send<TSend, TReply>(string sessionID, TSend msg, Action<string, TReply> action)
        {
            RegisterNetworkOnce(sessionID, action);
            Send(sessionID, msg);
        }

        private void OnTimerCallback(object _sta)
        {
            lock (this)
            {
                if (server.state != ServerBase.State.Start)
                    return;
                CheckTimeout();
            }
        }

        private void ReplyHeartbeat(string sessionID, int number)
        {
            HeartbeatReply msg = new HeartbeatReply();
            msg.number = number;
            m_server.Send(sessionID, msg.Serialize());
        }

        private double time { get { return DateTime.Now.Ticks * 0.0000001d; } }
        private Dictionary<string, double> m_sessionHeartbeatTime = new Dictionary<string, double>();
        private HashSet<string> m_timeoutSessions = new HashSet<string>();

        private void CheckTimeout()
        {
            foreach (var se in server.sessions)
            {
                if (m_sessionHeartbeatTime.ContainsKey(se.Key)
                    && !m_timeoutSessions.Contains(se.Key)
                    && time - m_sessionHeartbeatTime[se.Key] > m_timeoutDuration)
                {
                    m_timeoutSessions.Add(se.Key);
                    OnTimeout(se.Key);
                }
            }
        }

        protected void OnTimeout(string sessionID)
        {
            if (onTimeout != null)
                onTimeout.Invoke(sessionID);
        }
    }
}
