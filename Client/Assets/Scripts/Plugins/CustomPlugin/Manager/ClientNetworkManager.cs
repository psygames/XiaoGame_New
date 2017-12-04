using System;
using Plugins.Network;

namespace Plugins
{
    public class ClientNetworkManager
    {
        protected EventManager m_eventMgr = null;
        protected IProtoSerializer m_serializer = null;
        protected IClient m_socket = null;

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
