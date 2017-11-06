using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using message;

namespace RedStone
{
    public class MainServerProxy : BMProxyBase
    {
        public override void OnInit()
        {
            base.OnInit();

            network.socket.onConnected = () =>
            {
                Login();
            };
        }

        public void Connenct()
        {
            network.socket.Connect();
        }

        public void Login()
        {
            LoginRequest msg = new LoginRequest();
            msg.deviceUID = "Hello";
            SendMessage<LoginRequest, LoginReply>(msg, (reply) =>
            {
                Debug.Log(reply.name);
            });
        }
    }
}
