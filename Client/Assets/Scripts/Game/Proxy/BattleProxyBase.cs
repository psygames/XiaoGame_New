using UnityEngine;
using System.Collections;
using NetworkLib;

namespace RedStone
{
    public class BattleProxyBase : ProxyBase
    {
        public override ClientNetworkManager network
        {
            get
            {
                return NetworkManager.instance.battle;
            }
        }
    }
}