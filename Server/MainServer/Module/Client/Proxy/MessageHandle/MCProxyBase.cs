using System;
namespace RedStone
{
    public class MCProxyBase : ProxyBaseExt
    {
        public override Plugins.ServerNetworkManager network { get { return NetworkManager.instance.serverForClient; } }
    }
}
