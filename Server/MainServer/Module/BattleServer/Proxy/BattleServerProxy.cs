using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using message;

namespace RedStone
{
    public class BattleServerProxy : MBProxyBase
    {
        public override void OnInit()
        {
            base.OnInit();

            network.server.onConnected = (sessionID) =>
            {
                Debug.Log(sessionID + " connencted");
            };
            RegisterMessage<LoginRequest>(OnPlayerLogin);
        }

        void OnPlayerLogin(string sessionID, LoginRequest msg)
        {
            Debug.Log(msg.deviceUID);
            LoginReply reply = new LoginReply();
            reply.name = "World!";
            SendMessage(sessionID, reply);
        }
    }
}
