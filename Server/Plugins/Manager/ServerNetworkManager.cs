using System;
using Plugins.Network;

namespace Plugins
{
    public class ServerNetworkManager
    {
        private EventManager m_eventMgr = null;
        private IProtoSerializer m_serializer = null;
        private ServerBase m_server = null;

        public IServer server { get { return m_server; } }

        public ServerNetworkManager()
        {
            m_eventMgr = new EventManager();
            m_serializer = new ProtoSerializer();
        }

        public void Init(IServer server, IProtoSerializer serializer)
        {
            m_serializer = serializer;
            m_server = server as ServerBase;
            m_server.onReceived = OnReceived;
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

        private void RegisterNetworkOnce<T>(Action<string, T> action)
        {
            RegisterNetwork<T>((sessionID, msg) =>
            {
                action.Invoke(sessionID, msg);
                m_eventMgr.UnRegister(typeof(T).Name, action);
            });
        }

        public void Send<T>(string sessionID, T msg)
        {
            var data = m_serializer.Serialize(msg as Google.Protobuf.IMessage);
            m_server.Send(sessionID, data);
        }

        public void Send<TSend, TReply>(string sessionID, TSend msg, Action<string, TReply> action)
        {
            Send(sessionID, msg);
            RegisterNetworkOnce(action);
        }
    }
}
