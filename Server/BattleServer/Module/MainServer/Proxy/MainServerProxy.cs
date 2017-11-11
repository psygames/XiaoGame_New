using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Message;

namespace RedStone
{
    public class MainServerProxy : BMProxyBase
    {
        public override void OnInit()
        {
            base.OnInit();

            network.socket.onConnected = () =>
            {
                Debug.LogInfo("连接主服成功！");
                Login();
            };
        }

        public void Connenct()
        {
            Debug.LogInfo("开始连接主服...");
            network.socket.Connect();
        }

        public void Login()
        {
            BMLoginRequest msg = new BMLoginRequest();
            var serv = NetworkManager.instance.server.server as Plugins.Network.WebSocketServer;
            msg.ListenerAddress = serv.address;
            SendMessage<BMLoginRequest, BMLoginReply>(msg, (reply) =>
            {
                Debug.Log(reply.Name);
            });
        }
    }
}
