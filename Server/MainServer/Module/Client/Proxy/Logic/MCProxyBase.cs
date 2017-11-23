using System;
namespace RedStone
{
    public class MCProxyBase : ProxyBaseServer
    {
        public override Plugins.ServerNetworkManager network { get { return NetworkManager.instance.serverForClient; } }
    }
}
