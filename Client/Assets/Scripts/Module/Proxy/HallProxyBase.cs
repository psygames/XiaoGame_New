using UnityEngine;
using System.Collections;
using NetworkLib;

namespace RedStone
{
    public class HallProxyBase : ProxyBase
    {
        public override ClientNetworkManager network
        {
            get
            {
                return NetworkManager.instance.main;
            }
        }
    }
}