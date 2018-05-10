using System;
using System.Collections.Generic;
using Plugins;
using NetworkLib;

namespace RedStone
{
    public class NetworkManager : Core.Singleton<NetworkManager>, IDisposable
    {
        public ServerNetworkManager serverForBattle { get; private set; }
        public ServerNetworkManager serverForClient { get; private set; }

        public NetworkManager()
        {
            serverForBattle = new ServerNetworkManager(new WebSocketServer(), new ProtoSerializer());
            serverForClient = new ServerNetworkManager(new WebSocketServer(), new ProtoSerializer());
        }

        public void Init()
        {
            InitForBattle();
            InitForClient();
        }

        private void InitForBattle()
        {
            serverForBattle.Init("0.0.0.0", 8731);
            Logger.LogInfo("初始化网络监听（战场） [{0}]", serverForBattle.server.address);
        }

        private void InitForClient()
        {
            serverForClient.Init("0.0.0.0", 8730);
            Logger.LogInfo("初始化网络监听（客户端） [{0}]", serverForClient.server.address);
        }


        public void Dispose()
        {
            serverForBattle.Dispose();
            serverForClient.Dispose();
        }
    }
}

