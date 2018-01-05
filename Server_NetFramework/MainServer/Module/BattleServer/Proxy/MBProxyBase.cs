using System;
namespace RedStone
{
    public class MBProxyBase : ProxyBaseServer
    {
        public override Plugins.ServerNetworkManager network { get { return NetworkManager.instance.serverForBattle; } }
    }
}
