using UnityEngine;
using System.Collections;
using Message;
using Plugins;
using RedStone.Data;
using System.Collections.Generic;

namespace RedStone
{
    public class BattleProxy : BattleProxyBase
    {
        public BattleServerInfo serverInfo { get { return GetProxy<HallProxy>().battleServerInfo; } }

        public RoomData room { get; private set; }
        public bool isLogin { get; private set; }

        public void Reset()
        {

        }

        public BattleProxy()
        {
            room = new RoomData();
            players = new Dictionary<int, BattlePlayerData>();
        }

        public override void OnInit()
        {
            RegisterMessage<CBReadySync>(OnReadySync);
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

            network.socket.onConnected = () =>
            {
                Login(); // 连接成功后，LOGIN
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

        public int roomID { get; private set; }
        public void Login()
        {
            CBLoginRequest req = new CBLoginRequest();
            req.Token = serverInfo.Token;
            SendMessage<CBLoginRequest, CBLoginReply>(req
            , (rep) =>
            {
                roomID = rep.RoomID;
                Toast.instance.ShowNormal("登录战场服务器成功！");
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
                Toast.instance.ShowNormal("加入战场成功！");
                room.SetData(rep.Info);
                SetPlayers(rep.PlayerInfos);
            });
        }

        public void Ready()
        {
            CBReady req = new CBReady();
            SendMessage(req);
        }

        public void OnReadySync(CBReadySync msg)
        {
            GetPlayer(msg.FromID).SetReady(true);
            SendEvent(EventDef.PlayerReady, msg.FromID);
        }

        public BattlePlayerData GetPlayer(int id)
        {
            BattlePlayerData data = null;
            players.TryGetValue(id, out data);
            return data;
        }

        public Dictionary<int, BattlePlayerData> players { get; private set; }
        public void SetPlayers(IList<Message.BattlePlayerInfo> playerInfos)
        {
            players.Clear();
            foreach (var info in playerInfos)
            {
                BattlePlayerData data = new BattlePlayerData();
                data.SetData(info);
                players.Add(info.Id, data);
            }
        }
    }
}