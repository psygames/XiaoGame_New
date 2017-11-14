using UnityEngine;
using System.Collections;

namespace RedStone
{
    public class BattleProxyBase : ProxyBase
    {
        public override Plugins.ClientNetworkManager network
        {
            get
            {
                return NetworkManager.instance.main;
            }
        }
    }
}