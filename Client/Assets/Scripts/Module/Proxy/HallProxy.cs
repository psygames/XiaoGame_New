using UnityEngine;
using System.Collections;
using Message;

namespace RedStone
{
    public class HallProxy : HallProxyBase
    {
        public Data.PlayerData playerData { get; private set; }
        public Message.BattleServerInfo battleServerInfo { get; private set; }

        public HallProxy()
        {
            playerData = new Data.PlayerData();
        }

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
            req.DeviceID = DeviceID.UUID;
            network.Send<CMLoginRequest, CMLoginReply>(req
            , (reply) =>
             {
                 playerData.SetData(reply.PlayerInfo);
             });
        }

        public void StartGame(int gameID, int gameMode)
        {
            CMStartGameRequest msg = new CMStartGameRequest();
            msg.GameID = gameID;
            msg.GameMode = gameMode;
            msg.GroupID = -1;
            SendMessage<CMStartGameRequest, CMStartGameReply>(msg
            , (rep) =>
            {
                battleServerInfo = rep.BattleServer;
            });
        }

    }
}