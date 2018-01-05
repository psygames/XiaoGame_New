using System;
using System.Collections.Generic;
using Plugins;

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

        public void Init()
        {
            InitForClient();
            InitForServer();
        }

        private void InitForServer()
        {
            forServer = new ClientNetworkManager();
            var socket = new Plugins.Network.WebSocketClient();
            socket.Setup(NetConfig.SERVER_IP, NetConfig.SERVER_PORT);
            var serializer = new Plugins.ProtoSerializer();
            serializer.getTypeFunc = (name) => { return Type.GetType(name); };
            serializer.LoadProtoNum(typeof(Message.ProtoNum));
            forServer.Init(socket, serializer);
            Debug.LogInfo("初始化网络连接（主服） [{0}]".FormatStr(socket.address));
        }

        private void InitForClient()
        {
            forClient = new ServerNetworkManager();
            var listener = new Plugins.Network.WebSocketServer();
            listener.Setup("0.0.0.0", NetConfig.LISTENER_PORT);
            var serializer = new Plugins.ProtoSerializer();
            serializer.getTypeFunc = (name) => { return Type.GetType(name); };
            serializer.LoadProtoNum(typeof(Message.ProtoNum));
            forClient.Init(listener, serializer);
            Debug.LogInfo("初始化网络监听（客户端） [{0}]".FormatStr(listener.address));
        }
    }
}

