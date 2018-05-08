using System;
using NetworkLib;

namespace RedStone
{
    public class MCProxyBase : ProxyBaseServer
    {
        public override ServerNetworkManager network { get { return NetworkManager.instance.serverForClient; } }
    }
}
