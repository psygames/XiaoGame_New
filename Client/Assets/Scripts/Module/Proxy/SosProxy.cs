using UnityEngine;
using System.Collections;
using Message;
using Plugins;
using RedStone.Data.SOS;
using System.Collections.Generic;

namespace RedStone
{
    public class SosProxy : BattleProxyBase
    {
        public BattleServerInfo serverInfo { get { return GetProxy<HallProxy>().battleServerInfo; } }
        public RoomData room { get; private set; }

        public bool isConnected { get; private set; }
        public bool isLogin { get; private set; }

        public void Reset()
        {
            room = new RoomData();
            isConnected = false;
            isLogin = false;
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
        }

        private void InitSocket()
        {
            //Init
            string ip = NetTool.GetIP(serverInfo.Address);
            int port = NetTool.GetPort(serverInfo.Address);
            var socket = new Plugins.Network.WebSocketClient();
            socket.Setup(ip, port);
            var serializer = new ProtoSerializer();
            serializer.getTypeFunc = (name) => { return System.Type.GetType(name); };
            serializer.LoadProtoNum(typeof(ProtoNum));
            network.Init(socket, serializer);

            Debug.Log("Init Network (Battle) [{0}]".FormatStr(serverInfo.Address));

            network.onConnected = () =>
            {
                isConnected = true;
                Debug.Log("Network Connect Success (Battle Server).");
                Login();
            };


        }

        public void Connect()
        {
            Reset();
            if (serverInfo != null)
            {
                InitSocket();
                network.socket.Connect();
            }
            else
            {
                Debug.LogError("BattleServerInfo is NULL");
            }
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

        public void PlayCard(int cardId, int target = 0,int extend = 0)
        {
            CBPlayCard msg = new CBPlayCard();
            msg.CardID = cardId;
            msg.TargetID = target;
            msg.Extend = extend;
            SendMessage(msg);
        }


        // On Network Message
        void OnJoined(CBJoinBattleReply msg)
        {
            Toast.instance.ShowNormal("加入战场成功！");

            room = new RoomData();
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


            if (cardTableID != 1 && msg.TargetID != room.mainPlayer.id)
            {
                Debug.LogError("{0}技能目标错误：{1}".FormatStr(fromCard.name, msg.TargetID));
                return;
            }

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

            Debug.LogError("DROP=> " + player.name + ": " + card.name);
        }

        void OnPlayerOutSync(CBPlayerOutSync msg)
        {
            var player = room.GetPlayer(msg.PlayerID);
            var card = room.GetCard(msg.HandCardID);
            player.RemoveCard(card);
            player.Out();
            SendEvent(EventDef.SOS.PlayerOut, msg);
            Debug.LogError("OUT=> " + player.name + ": " + card.name);
        }
    }
}