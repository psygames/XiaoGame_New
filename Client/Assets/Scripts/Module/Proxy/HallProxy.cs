using UnityEngine;
using System.Collections;
using Message;
using System;

namespace RedStone
{
    public class HallProxy : HallProxyBase
    {
        public Data.PlayerData playerData { get; private set; }
        public BattleServerInfo battleServerInfo { get; private set; }


        public bool isLogin { get; private set; }
        public bool isConnected { get; private set; }

        public HallProxy()
        {
            playerData = new Data.PlayerData();
        }

        public override void OnInit()
        {
            isConnected = false;
            isLogin = false;

            network.onConnected = () =>
            {
                isConnected = true;
                Debug.Log("Network Connect Success (Main Server).");
                Login();
            };

            network.RegisterNetwork<CMMatchSuccess>(OnMatchSuccess);
        }

        public void Connect()
        {
            network.socket.Connect();
        }

        public void Login()
        {
            CMLoginRequest req = new CMLoginRequest();
            req.DeviceID = DeviceID.UUID;
            network.Send<CMLoginRequest, CMLoginReply>(req
            , (reply) =>
             {
                 //TODO:重连Or 退出
                 if (reply.IsInBattle)
                 {
                     playerData.SetData(reply.PlayerInfo);

                     MessageBox.Show("战斗提示", "已经在战场中，是否重连！", MessageBoxStyle.OKCancelClose, (result) =>
                     {
                         if (result.result == MessageBoxResultType.OK)
                         {
                             CancelReconnect(1, 1); //TODO:重连
                             isLogin = true;
                         }
                         else
                         {
                             CancelReconnect(1, 1);
                             isLogin = true;
                         }
                     });
                 }
                 else
                 {
                     isLogin = true;
                     playerData.SetData(reply.PlayerInfo);
                 }
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
                if (rep.Status == 1)
                {
                    SendEvent(EventDef.MatchBegin);

                }
                else if (rep.Status == 0)
                {
                    MessageBox.Show("匹配失败", "匹配失败，没有可用的战场服务器！", MessageBoxStyle.OK);
                }
                else if (rep.Status == -1)
                {
                    // 重连Or强退
                    MessageBox.Show("匹配失败", "已经在房间中！", MessageBoxStyle.OK, (result) =>
                    {
                        CancelReconnect(gameID, gameMode);
                    });
                }
            });
        }

        public void CancelReconnect(int gameID, int gameMode)
        {
            CMCancelReconnect msg = new CMCancelReconnect();
            msg.GameID = gameID;
            msg.GameMode = gameMode;
            SendMessage(msg);
        }

        public void CancelMatch()
        {
            CMMatchCancel msg = new CMMatchCancel();
            SendMessage(msg);
            SendEvent(EventDef.MatchCancel);
        }

        void OnMatchSuccess(CMMatchSuccess msg)
        {
            battleServerInfo = msg.BattleServerInfo;
            SendEvent(EventDef.MatchSuccess);
            GF.ChangeState<BattleLoadingState>();
        }

    }
}