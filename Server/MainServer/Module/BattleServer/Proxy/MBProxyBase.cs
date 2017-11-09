using System;
namespace RedStone
{
    public class MBProxyBase : ProxyBase
    {
        public Plugins.ServerNetworkManager network { get { return NetworkManager.instance.serverForBattle; } }

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
    }
}
