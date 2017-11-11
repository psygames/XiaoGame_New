using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Message;

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
            RegisterMessage<BMLoginRequest>(OnLogin);
        }

        void OnLogin(string sessionID, BMLoginRequest msg)
        {
            Debug.Log(msg.ListenerAddress);
            BMLoginReply reply = new BMLoginReply();
            reply.Name = msg.ListenerAddress;
            SendMessage(sessionID, reply);
        }
    }
}
