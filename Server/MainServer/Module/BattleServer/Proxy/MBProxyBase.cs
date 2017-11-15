using System;
namespace RedStone
{
    public class MBProxyBase : ProxyBase
    {
        public override Plugins.ServerNetworkManager network { get { return NetworkManager.instance.serverForBattle; } }
    }
}
