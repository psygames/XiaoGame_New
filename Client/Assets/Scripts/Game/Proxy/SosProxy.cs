using UnityEngine;
using System.Collections;
using Message;
using RedStone.Data.SOS;
using System.Collections.Generic;
using NetworkLib;

namespace RedStone
{
    public class SosProxy : BattleProxyBase
    {
        public BattleServerInfo serverInfo { get { return GetProxy<HallProxy>().battleServerInfo; } }
        public RoomData room { get; private set; }

        public bool isConnected { get { return network.isConneted; } }
        public bool isLogin { get; private set; }
        private bool m_needReconnnect = false;

        public void Reset()
        {
            room = new RoomData();
            isLogin = false;
            m_needReconnnect = false;
        }

        public SosProxy()
        {
            room = new RoomData();
        }

        public override void OnInit()
        {
            RegisterMessage<CBReadySync>(OnReadySync);
            RegisterMessage<CBRoomSync>(OnRoomSync);
            RegisterMessage<CBSendCardSync>(OnSendCardSync);
            RegisterMessage<CBCardInfoSync>(OnCardInfoSync);
            RegisterMessage<CBPlayCardSync>(OnPlayCardSync);
            RegisterMessage<CBBattleResultSync>(OnBattleResultSync);
            RegisterMessage<CBCardEffectSync>(OnCardEffectSync);
            RegisterMessage<CBPlayerDropCardSync>(OnDropCardSync);
            RegisterMessage<CBPlayerOutSync>(OnPlayerOutSync);
            RegisterMessage<CBSendMessageSync>(OnSendMessageSync);
        }

        public void InitSocket()
        {
            network.Init(NetTool.GetIP(serverInfo.Address), NetTool.GetPort(serverInfo.Address));

            Logger.Log("Init Network (Battle) [{0}]", serverInfo.Address);

            network.heartbeat.onTimeout = OnTimeout;
            network.onConnected = () =>
            {
                Logger.Log("Network Connect Success (Battle Server).");
                if (m_needReconnnect)
                    SendReconnect();
                else
                    Login();
            };
        }

        public void Connect()
        {
            if (serverInfo == null)
            {
                Logger.LogError("BattleServerInfo is NULL");
                return;
            }

            network.Connect();

            Task.WaitFor(3, () =>
            {
                if (isConnected)
                    return;

                MessageBox.Show("连接失败", "连接战场失败，是否重新连接？", MessageBoxStyle.OKClose
                , (result) =>
                {
                    if (result.result == MessageBoxResultType.OK)
                    {
                        Connect();
                    }
                });
            });
        }

        public void Reconnect(int retryNum = 1, int retryCount = 5)
        {
            // CLOSE
            if (network.isConneted)
            {
                Logger.Log("Close current socket (BattleServer).");

                network.onClosed = () =>
                {
                    network.onClosed = null;
                    Logger.Log("Current socket Closed (BattleServer)");
                    Reconnect();
                };
                network.Close();
                return;
            }

            Logger.Log("Try to reconnect BattleServer({0}).", retryNum);
            m_needReconnnect = true;
            network.Connect();

            Task.WaitFor(3f, () =>
            {
                if (isConnected)
                    return;

                if (!isConnected && retryNum < retryCount)
                {
                    Reconnect(retryNum + 1);
                }
                else
                {
                    Logger.LogError("Reconnect BattleServer Failed.");
                    network.Close();
                    MessageBox.Show("重连战场失败", "是否继续尝试重连？", MessageBoxStyle.OKCancelClose, (result) =>
                    {
                        if (result.result == MessageBoxResultType.OK)
                        {
                            Reconnect();
                        }
                    });
                }
            });
        }

        public void Close()
        {
            if (isConnected && network != null && network.socket != null)
                network.Close();
        }

        private void OnTimeout()
        {
            SendEvent(EventDef.SOS.HeartbeatTimeout);
            Reconnect();
        }

        private void SendReconnect()
        {
            CBReconnectRequest msg = new CBReconnectRequest();
            msg.SessionID = "";//TODO:LAST SESSION ID
            msg.Token = serverInfo.Token;
            SendMessage<CBReconnectRequest, CBReconnectReply>(msg, (rep) =>
            {
                if (rep.RoomState == (int)RoomData.State.Dismiss)
                {
                    room.SetState(0, rep.RoomState, 0);
                    MessageBox.Show("房间已解散", "点击确认，退出房间！", MessageBoxStyle.OK, (res) =>
                    {
                        GF.ChangeState<HallState>();
                    });
                    return;
                }

                Reset();
                isLogin = true;

                room.SetData(rep.RoomInfo);
                room.SetCards(rep.Cards);
                room.SetPlayers(rep.PlayerInfos);
                room.SetState(rep.WhoseTurn, rep.RoomState, rep.LeftCardCount);

                SendEvent(EventDef.SOS.Reconnected);
            });
        }

        public void Login()
        {
            CBLoginRequest req = new CBLoginRequest();
            req.Token = serverInfo.Token;
            SendMessage<CBLoginRequest, CBLoginReply>(req
            , (rep) =>
            {
                isLogin = true;
                // Toast.instance.ShowNormal("登录战场服务器成功！");
                Task.WaitFor(1f, () =>
                {
                    JoinBattle();//自动加入战场
                });
            });
        }

        public void JoinBattle()
        {
            CBJoinBattleRequest req = new CBJoinBattleRequest();
            SendMessage<CBJoinBattleRequest, CBJoinBattleReply>(req,
            (rep) =>
            {
                OnJoined(rep);
            });
        }

        public void Ready()
        {
            CBReady req = new CBReady();
            SendMessage(req);
        }

        public void PlayCard(int cardId, int target = 0, int extend = 0)
        {
            CBPlayCard msg = new CBPlayCard();
            msg.CardID = cardId;
            msg.TargetID = target;
            msg.Extend = extend;
            SendMessage(msg);
        }

        public void SendChatMessage(string content)
        {
            CBSendMessage msg = new CBSendMessage();
            msg.Content = content;
            SendMessage(msg);
        }

        // On Network Message
        void OnJoined(CBJoinBattleReply msg)
        {
            Toast.instance.ShowNormal("加入战场成功！");

            room.SetData(msg.Info);
            room.SetPlayers(msg.PlayerInfos);

            SendEvent(EventDef.SOS.Joined);
        }

        void OnReadySync(CBReadySync msg)
        {
            room.GetPlayer(msg.FromID).SetReady();
            SendEvent(EventDef.SOS.Ready, msg.FromID);
        }

        void OnCardInfoSync(CBCardInfoSync msg)
        {
            room.SetCards(msg.Cards);
        }

        void OnRoomSync(CBRoomSync msg)
        {
            room.RoomSync(msg);
            SendEvent(EventDef.SOS.RoomSync);
        }

        void OnSendCardSync(CBSendCardSync msg)
        {
            var card = room.GetCard(msg.CardID);
            var player = room.GetPlayer(msg.TargetID);
            player.AddCard(card);
            SendEvent(EventDef.SOS.SendCard, msg);
        }

        void OnPlayCardSync(CBPlayCardSync msg)
        {
            var card = room.GetCard(msg.CardID);
            var player = room.GetPlayer(msg.FromID);
            player.RemoveCard(card);

            SendEvent(EventDef.SOS.PlayCard, msg);
        }

        void OnBattleResultSync(CBBattleResultSync msg)
        {
            SendEvent(EventDef.SOS.BattleResult, msg);
        }

        void OnCardEffectSync(CBCardEffectSync msg)
        {
            var fromCard = room.GetCard(msg.FromCardID);
            int cardTableID = fromCard.tableID;

            if (cardTableID == 1) // 侦察
            {

            }
            else if (cardTableID == 2) //混乱
            {
                var p = room.GetPlayer(msg.TargetID);
                p.ChangeCard(room.GetCard(msg.TargetCardID));
            }
            else if (cardTableID == 3) // 变革
            {
                var p = room.GetPlayer(msg.TargetID);
                p.ChangeCard(room.GetCard(msg.TargetCardID));
            }
            else if (cardTableID == 4) // 壁垒
            {
                var p = room.GetPlayer(msg.FromPlayerID);
                p.SetEffect(PlayerData.Effect.InvincibleOneRound);
            }
            else if (cardTableID == 5) // 猜卡牌TableID
            {

            }
            else if (cardTableID == 6) // 决斗
            {

            }
            else if (cardTableID == 7) // 霸道 太阳
            {

            }
            else if (cardTableID == 8) // 交换
            {
                var p = room.GetPlayer(msg.TargetID);
                p.ChangeCard(room.GetCard(msg.TargetCardID));
            }
            else if (cardTableID == 9) // 开溜（只限制出牌阶段，出牌类型，出牌后无效果）
            {

            }
            else if (cardTableID == 10)
            {

            }

            SendEvent(EventDef.SOS.CardEffect, msg);
        }

        void OnDropCardSync(CBPlayerDropCardSync msg)
        {
            var player = room.GetPlayer(msg.PlayerID);
            var card = room.GetCard(msg.CardID);
            player.RemoveCard(card);

            SendEvent(EventDef.SOS.DropCard, msg);
        }

        void OnPlayerOutSync(CBPlayerOutSync msg)
        {
            var player = room.GetPlayer(msg.PlayerID);
            if (msg.HandCardID > 0 && player.oneCard != null)
            {
                var card = room.GetCard(msg.HandCardID);
                player.RemoveCard(card);
            }
            player.Out();
            SendEvent(EventDef.SOS.PlayerOut, msg);
        }

        void OnSendMessageSync(CBSendMessageSync msg)
        {
            SendEvent(EventDef.SOS.SendMessageSync, msg);
        }
    }
}