using System;
namespace RedStone
{
    public class MBProxyBase : ProxyBaseExt
    {
        public override Plugins.ServerNetworkManager network { get { return NetworkManager.instance.serverForBattle; } }
    }
}
