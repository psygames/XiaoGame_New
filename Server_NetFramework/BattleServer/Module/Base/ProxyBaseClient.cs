using System;
using Plugins;
namespace RedStone
{
    public class ProxyBaseClient : ProxyBase
    {
        public virtual EventManager eventManager { get { return GameManager.instance.eventManager; } }
        public virtual ClientNetworkManager network { get { return NetworkManager.instance.forServer; } }

        public void SendMessage<T>(T msg)
        {
            network.Send(msg);
        }

        public void SendMessage<T1, T2>(T1 msg, Action<T2> reply)
        {
            network.Send(msg, reply);
        }
        public void RegisterMessage<T>(Action<T> callback)
        {
            network.RegisterNetwork(callback);
        }

        public void RegisterMessageAll(Action<object> callback)
        {

        }

        public void SendEvent(string eventName, object obj = null)
        {
            eventManager.Send(eventName, obj);
        }
    }
}
