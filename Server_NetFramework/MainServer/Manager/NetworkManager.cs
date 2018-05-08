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

        public NetworkManager()
        {
            serverForBattle = new ServerNetworkManager();
            serverForClient = new ServerNetworkManager();
        }

        public void Init()
        {
            InitForBattle();
            InitForClient();
        }

        private void InitForBattle()
        {
            var server = new WebSocketServer();
            server.Setup("0.0.0.0", 8731);
            serverForBattle.Init(server, new ProtoSerializer());
            Logger.LogInfo("初始化网络监听（战场） [{0}]", server.address);
        }

        private void InitForClient()
        {
            var server = new WebSocketServer();
            server.Setup("0.0.0.0", 8730);
            serverForClient.Init(server, new ProtoSerializer());
            Logger.LogInfo("初始化网络监听（客户端） [{0}]", server.address);
        }
    }
}

