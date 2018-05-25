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
        public bool isConnected { get { return network.isConneted; } }

        private bool m_needReconnnect = false;

        public HallProxy()
        {
            playerData = new Data.PlayerData();
        }

        public override void OnInit()
        {
            isLogin = false;

            network.onConnected = () =>
            {
                Logger.Log("Network Connect Success (Main Server).");
                if (m_needReconnnect)
                {
                    SendReconnectMsg();
                }
                else
                {
                    Login();
                }
            };

            network.heartbeat.onTimeout = () =>
            {
                Logger.LogError("Main Server Timeout!!!");
                Reconnect();
            };

            network.RegisterNetwork<CMMatchSuccess>(OnMatchSuccess);
            network.RegisterNetwork<NetworkLib.HeartbeatReply>(OnHeartbeatReply);
        }

        public void Reconnect(int retryNum = 1, int retryCount = 5)
        {
            // Close 处理
            if (network.isConneted)
            {
                network.Close();
                network.onClosed = () =>
                {
                    Reconnect();
                    network.onClosed = null;
                };
                return;
            }


            Logger.Log("Try to reconnect({0}).".FormatStr(retryNum));
            m_needReconnnect = true;
            network.socket.Connect();

            Task.WaitFor(3f, () =>
            {
                if (!isConnected && retryNum < retryCount)
                {
                    Reconnect(retryNum + 1);
                }
                else
                {
                    Logger.LogError("Reconnect Failed.");
                    network.Close();
                }
            });
        }

        public void Connect()
        {
            m_needReconnnect = false;
            network.socket.Connect();
        }

        public void SendReconnectMsg()
        {
            CMLoginRequest req = new CMLoginRequest();
            req.DeviceID = DeviceID.UUID;
            network.Send<CMLoginRequest, CMLoginReply>(req
            , (reply) =>
            {
                playerData.SetData(reply.PlayerInfo);
            });
        }

        public void Login()
        {
            CMLoginRequest req = new CMLoginRequest();
            req.DeviceID = DeviceID.UUID;
            network.Send<CMLoginRequest, CMLoginReply>(req
            , (reply) =>
             {
                 playerData.SetData(reply.PlayerInfo);
                 isLogin = true;

                 if (reply.IsInBattle)
                 {
                     MessageBox.Show("战斗提示", "已经在战场中，是否重连！"
                         , MessageBoxStyle.OKCancelClose, (result) =>
                     {
                         if (result.result == MessageBoxResultType.OK)
                         {
                             //TODO:重连
                             CancelReconnect(1, 1);
                         }
                         else
                         {
                             CancelReconnect(1, 1);
                         }
                     });
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
        void OnHeartbeatReply(NetworkLib.HeartbeatReply msg)
        {
            // Logger.Log("heartbeat reply num: {0}", msg.number);
        }

    }
}