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
            room = null;
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

        public int Roomda { get; private set; }
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

        public void PlayCard(int cardId, int target = 0)
        {
            CBPlayCard msg = new CBPlayCard();
            msg.CardID = cardId;
            msg.TargetID = target;
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
            room.GetPlayer(msg.FromID).SetReady(true);
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
    }
}