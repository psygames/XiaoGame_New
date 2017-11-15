using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Message;


namespace RedStone
{
    public class PlayerProxy : MCProxyBase
    {
        public override void OnInit()
        {
            base.OnInit();
            RegisterMessage<CMLoginRequest>()
        }

        public void OnPlayerLogin()
        {

        }

        public void OnPlayerForceQuit(long pid)
        {
        }


        public class PlayerHandle
        {
            public string sessionID { get; private set; }
            public PlayerData data = new PlayerData();

            public void OnLogin(CMLoginRequest login)
            {

            }
        }
    }
}
