using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Message;

namespace RedStone
{
    public class MainServerProxy : ProxyBaseClient
    {
        public override void OnInit()
        {
            base.OnInit();

            network.socket.onConnected = () =>
            {
                Debug.LogInfo("连接主服成功！");
                Login();
            };

            RegisterMessage<BMCreateRoomRequest>(OnCreateRoom);
        }

        public void Connenct()
        {
            Debug.LogInfo("开始连接主服...");
            network.socket.Connect();
        }

        public void Login()
        {
            BMLoginRequest msg = new BMLoginRequest();
            var serv = NetworkManager.instance.forClient.server as Plugins.Network.WebSocketServer;
            msg.ListenerAddress = Plugins.NetTool.GetAddress(NetConfig.LISTENER_IP, NetConfig.LISTENER_PORT);
            SendMessage<BMLoginRequest, BMLoginReply>(msg, (reply) =>
            {
                Debug.Log($"登录主服成功, 战场名: {reply.Name}");
            });
        }

        void OnCreateRoom(BMCreateRoomRequest req)
        {
            var room = GetProxy<BattleServerProxy>().MainServerRequsetCreateRoom(req.Users);
            BMCreateRoomReply rep = new BMCreateRoomReply();
            rep.RoomName = room.name;
            rep.RoomID = room.id;
            foreach (var user in room.users)
            {
                var ptoken = new PlayerTokenInfo();
                ptoken.Uid = user.uid;
                ptoken.Token = user.token;
                rep.PlayerTokens.Add(ptoken);
            }
            SendMessage(rep);
        }
    }
}
