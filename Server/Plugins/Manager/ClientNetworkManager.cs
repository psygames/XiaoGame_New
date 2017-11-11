using System;
using Plugins.Network;

namespace Plugins
{
    public class ClientNetworkManager
    {
        private EventManager m_eventMgr = null;
        private IProtoSerializer m_serializer = null;
        private IClient m_socket = null;

        public IClient socket { get { return m_socket; } }

        public ClientNetworkManager()
        {
            m_eventMgr = new EventManager();
            m_serializer = new ProtoSerializer();
        }

        public void Init(IClient socket, IProtoSerializer serializer)
        {
            m_serializer = serializer;
            m_socket = socket;
            m_socket.onReceived = OnReceived;
        }

        public void RegisterNetwork<T>(Action<T> action)
        {
            m_eventMgr.Register(typeof(T).Name, action);
        }

        private void RegisterNetworkOnce<T>(Action<T> action)
        {
            RegisterNetwork<T>((t) =>
            {
                action.Invoke(t);
                m_eventMgr.UnRegister(typeof(T).Name, action);
            });
        }
        void OnReceived(byte[] data)
        {
            object obj = m_serializer.Deserialize(data);
            m_eventMgr.Send(obj.GetType().Name, obj);
        }

        public void Send<T>(T msg) 
        {
            var data = m_serializer.Serialize(msg as Google.Protobuf.IMessage);
            m_socket.Send(data);
        }

        public void Send<TSend, TReply>(TSend msg, Action<TReply> action)
        {
            Send(msg);
            RegisterNetworkOnce(action);
        }
    }
}
