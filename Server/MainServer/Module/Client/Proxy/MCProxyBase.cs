using System;
namespace RedStone
{
    public class MCProxyBase : ProxyBase
    {
        public override Plugins.ServerNetworkManager network { get { return NetworkManager.instance.serverForClient; } }
    }
}
