using System;
namespace RedStone
{
    public class BMProxyBase : ProxyBase
    {
        public Plugins.ClientNetworkManager network { get { return NetworkManager.instance.client; }}

        public void SendMessage<T>(T proto)
        {
            network.Send(proto);
        }

        public void SendMessage<T1,T2>(T1 msg,Action<T2> reply)
        {
            network.Send(msg,reply);
        }

        public void RegisterMessage<T>(Action<T> callback)
        {
            network.RegisterNetwork(callback);
        }
    }
}
