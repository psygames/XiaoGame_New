using System;
using NetworkLib;

namespace RedStone
{
    public class NetworkManager : Core.Singleton<NetworkManager>
    {
        /// <summary>
        /// 连接主服
        /// </summary>
        public ClientNetworkManager forServer { get; private set; }
        /// <summary>
        /// 监听客户端
        /// </summary>
        public ServerNetworkManager forClient { get; private set; }

        public NetworkManager()
        {
            forClient = new ServerNetworkManager();
            forServer = new ClientNetworkManager();
        }

        public void Init()
        {
            InitForClient();
            InitForServer();
        }

        private void InitForServer()
        {
            var socket = new WebSocketClient();
            socket.Setup(NetConfig.SERVER_IP, NetConfig.SERVER_PORT);
            forServer.Init(socket);
            Logger.LogInfo("初始化网络连接（主服） [{0}]".FormatStr(socket.address));
        }

        private void InitForClient()
        {
            var listener = new WebSocketServer();
            listener.Setup("0.0.0.0", NetConfig.LISTENER_PORT);
            var serializer = new ProtoSerializer();
            forClient.Init(listener, serializer);
            Logger.LogInfo("初始化网络监听（客户端） [{0}]".FormatStr(listener.address));
        }
    }
}

