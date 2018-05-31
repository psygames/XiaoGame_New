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
            forClient = new ServerNetworkManager(new WebSocketServer(), new ProtoSerializer());
            forServer = new ClientNetworkManager(new WebSocketClient(), new ProtoSerializer());
        }

        public void Init()
        {
            InitForClient();
            InitForServer();
        }

        private void InitForServer()
        {
            forServer.Init(NetConfig.SERVER_IP, NetConfig.SERVER_PORT);
            Logger.LogInfo("初始化网络连接（主服） [{0}]".FormatStr(forServer.socket.address));
        }

        private void InitForClient()
        {
            forClient.Init("0.0.0.0", NetConfig.LISTENER_PORT);
            var addr = NetTool.GetAddress("0.0.0.0", NetConfig.LISTENER_PORT);
            Logger.LogInfo("初始化网络监听（客户端） [{0}]".FormatStr(addr));
        }
    }
}

