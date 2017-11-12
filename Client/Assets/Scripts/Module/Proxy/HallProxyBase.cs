using UnityEngine;
using System.Collections;

namespace RedStone
{
    public class HallProxyBase : ProxyBase
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