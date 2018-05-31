using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Message;
using NetworkLib;

namespace RedStone
{
    public class MainServerProxy : ProxyBaseClient
    {
        public override void OnInit()
        {
            base.OnInit();

            network.onConnected = () =>
            {
                Logger.LogInfo("连接主服成功！");
                Login();
            };

            RegisterMessage<BMCreateRoomRequest>(OnCreateRoom);
        }

        public void Connenct()
        {
            Logger.LogInfo("开始连接主服...");
            network.Connect();
        }

        public void Login()
        {
            BMLoginRequest msg = new BMLoginRequest();
            msg.ListenerAddress = NetTool.GetAddress(NetConfig.LISTENER_IP, NetConfig.LISTENER_PORT);
            SendMessage<BMLoginRequest, BMLoginReply>(msg, (reply) =>
            {
                Logger.Log($"登录主服成功, 战场名: {reply.Name}");
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

        public void BattleEnd(int roomID)
        {
            Message.BMBattleResult msg = new Message.BMBattleResult();
            msg.RoomID = roomID;
            foreach (var p in GetProxy<BattleServerProxy>().GetSosLogic(roomID).GetPlayers())
            {
                var pinfo = new Message.BMBattleResultUserInfo();
                pinfo.PlayerName = p.name;
                pinfo.PlayerID = p.id;
                pinfo.Uid = p.uid;
                pinfo.IsAI = p.isAI;
                pinfo.IsOut = p.state == SOS.Player.State.Out;
                pinfo.Score = p.score;
                msg.RankUsers.Add(pinfo);
            }
            SendMessage(msg);
        }
    }
}
