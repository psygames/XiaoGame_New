using System;
using System.Collections.Generic;
using Plugins;
using NetworkLib;

namespace RedStone
{
    public class NetworkManager : Core.Singleton<NetworkManager>
    {
        public ServerNetworkManager serverForBattle { get; private set; }
        public ServerNetworkManager serverForClient { get; private set; }

        public void Init()
        {
            InitForBattle();
            InitForClient();
        }

        private void InitForBattle()
        {
            serverForBattle = new ServerNetworkManager();
            var server = new NetworkLib.WebSocketServer();
            server.Setup("0.0.0.0", 8731);
            var serializer = new ProtoSerializer();
            serializer.getTypeFunc = (name) => { return Type.GetType(name); };
            serializer.LoadProtoNum(typeof(Message.ProtoNum));
            serverForBattle.Init(server, serializer);
            Logger.LogInfo("初始化网络监听（战场） [{0}]", server.address);
        }

        private void InitForClient()
        {
            serverForClient = new ServerNetworkManager();
            var server = new NetworkLib.WebSocketServer();
            server.Setup("0.0.0.0", 8730);
            var serializer = new ProtoSerializer();
            serializer.getTypeFunc = (name) => { return Type.GetType(name); };
            serializer.LoadProtoNum(typeof(Message.ProtoNum));
            serverForClient.Init(server, serializer);
            Logger.LogInfo("初始化网络监听（客户端） [{0}]", server.address);
        }
    }
}

