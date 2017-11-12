using UnityEngine;
using System.Collections;
using Message;

namespace RedStone
{
    public class HallProxy : HallProxyBase
    {
        public override void OnInit()
        {
            network.socket.onConnected = () =>
            {
                Login();
            };
        }

        public void Login()
        {
            CMLoginRequest req = new CMLoginRequest();
            req.DeviceID = "ndioaidwoai";
            network.Send<CMLoginRequest, CMLoginReply>(req
            , (reply) =>
             {
                 Debug.Log(reply.PlayerInfo.Name);
             });
        }
    }
}