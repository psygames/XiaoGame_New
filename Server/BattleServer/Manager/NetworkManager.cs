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
        public ClientNetworkManager client { get; private set; }
        /// <summary>
        /// 监听客户端
        /// </summary>
        public ServerNetworkManager server { get; private set; }

        public void Init()
        {
            InitServer();
            InitClient();
        }

        private void InitClient()
        {
            client = new ClientNetworkManager();
            var socket = new Plugins.Network.WebSocketClient();
            socket.Setup("127.0.0.1", 8730);
            var serializer = new Plugins.ProtoSerializer();
            serializer.getTypeFunc = (name) => { return Type.GetType(name); };
            serializer.LoadProtoNum(typeof(Message.ProtoNum));
            client.Init(socket, serializer);
            Debug.LogInfo("初始化网络连接（主服） [{0}]".FormatStr(socket.address));
        }

        private void InitServer()
        {
            server = new ServerNetworkManager();
            var listener = new Plugins.Network.WebSocketServer();
            int port = NetTool.GetAvailablePort(8740);
            listener.Setup("0.0.0.0", port);
            var serializer = new Plugins.ProtoSerializer();
            serializer.getTypeFunc = (name) => { return Type.GetType(name); };
            serializer.LoadProtoNum(typeof(Message.ProtoNum));
            server.Init(listener, serializer);
            Debug.LogInfo("初始化网络监听（客户端） [{0}]".FormatStr(listener.address));
        }
    }
}

