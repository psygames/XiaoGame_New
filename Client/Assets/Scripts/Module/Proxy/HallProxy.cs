using UnityEngine;
using System.Collections;
using Message;

namespace RedStone
{
    public class HallProxy : HallProxyBase
    {
        public Data.PlayerData playerData { get; private set; }
        public BattleServerInfo battleServerInfo { get; private set; }

        public HallProxy()
        {
            playerData = new Data.PlayerData();
        }

        public override void OnInit()
        {
            network.socket.onConnected = () =>
            {
                Debug.Log("Network Connect Success (Main Server).");
                Login();
            };

            network.RegisterNetwork<CMMatchSuccess>(OnMatchSuccess);
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

        public void BeginMatch(int gameID, int gameMode)
        {
            CMMatchRequest msg = new CMMatchRequest();
            msg.GameID = gameID;
            msg.GameMode = gameMode;
            msg.GroupID = -1;
            SendMessage<CMMatchRequest, CMMatchReply>(msg
            , (rep) =>
            {
                Debug.Log(rep.Status);
            });
        }

        void OnMatchSuccess(CMMatchSuccess msg)
        {
            Debug.Log(msg.BattleServerInfo.Address);
        }
    }
}