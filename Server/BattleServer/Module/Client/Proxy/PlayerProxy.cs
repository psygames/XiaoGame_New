using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedStone.Data;


namespace RedStone
{
    public class BattleServerProxy : MBProxyBase
    {
        private Dictionary<long, UserData> m_users = new Dictionary<long, UserData>();


        public override void OnInit()
        {
            base.OnInit();

        }


    }
}
