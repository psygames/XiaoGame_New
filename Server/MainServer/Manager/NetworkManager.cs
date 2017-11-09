using System;
using System.Collections.Generic;
using Plugins;

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
            var server = new Plugins.Network.WebSocketServer();
            server.Setup("0.0.0.0", 8730);
            var serializer = new Plugins.ProtoSerializer();
            serializer.getTypeFunc = (name) => { return Type.GetType(name); };
            serializer.LoadNumFile(MyPath.RES_PROTO_NUM);
            serverForBattle.Init(server, serializer);
            Debug.LogInfo("初始化网络监听（战场） [{0}]", server.address);
        }

        private void InitForClient()
        {
            serverForClient = new ServerNetworkManager();
            var server = new Plugins.Network.WebSocketServer();
            server.Setup("0.0.0.0", 8731);
            var serializer = new Plugins.ProtoSerializer();
            serializer.getTypeFunc = (name) => { return Type.GetType(name); };
            serializer.LoadNumFile(MyPath.RES_PROTO_NUM);
            serverForClient.Init(server, serializer);
            Debug.LogInfo("初始化网络监听（客户端） [{0}]", server.address);
        }
    }
}

