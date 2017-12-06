using UnityEngine;
using System.Collections;
using Message;
using Plugins;

namespace RedStone
{
    public class BattleProxy : BattleProxyBase
    {
        public BattleServerInfo serverInfo { get { return GetProxy<HallProxy>().battleServerInfo; } }

        public BattleProxy()
        {
        }

        public override void OnInit()
        {

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
            });
        }

        public void JoinBattle()
        {
            CBJoinBattleRequest req = new CBJoinBattleRequest();
            SendMessage<CBJoinBattleRequest, CBJoinBattleReply>(req,
            (rep) =>
            {

            });
        }
    }
}