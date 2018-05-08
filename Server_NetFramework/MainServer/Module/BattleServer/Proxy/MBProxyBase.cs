using System;
using NetworkLib;

namespace RedStone
{
    public class MBProxyBase : ProxyBaseServer
    {
        public override ServerNetworkManager network { get { return NetworkManager.instance.serverForBattle; } }
    }
}
