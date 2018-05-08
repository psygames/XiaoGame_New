using System;
using Core;
using NetworkLib;

namespace RedStone
{
    public class ProxyBaseServer : ProxyBase
    {
        public virtual EventManager eventManager { get { return GameManager.instance.eventManager; } }
        public virtual ServerNetworkManager network { get { return null; } }

        public void SendMessage<T>(string sessionID, T msg)
        {
            network.Send(sessionID, msg);
        }

        public void SendMessage<T1, T2>(string sessionID, T1 msg, Action<string, T2> reply)
        {
            network.Send(sessionID, msg, reply);
        }
        public void RegisterMessage<T>(Action<string, T> callback)
        {
            network.RegisterNetwork(callback);
        }

        public void RegisterMessageAll(Action<string, object> callback)
        {

        }

        public void SendEvent(string eventName, object obj = null)
        {
            eventManager.Send(eventName, obj);
        }
    }
}
