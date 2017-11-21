using System;
using System.Collections.Generic;
using Plugins;
using UnityEngine;

namespace RedStone
{
    public class NetworkManager : Core.Singleton<NetworkManager>
    {
        /// <summary>
        /// 连接主服
        /// </summary>
        public ClientNetworkManager battle { get; private set; }
        /// <summary>
        /// 监听客户端
        /// </summary>
        public ClientNetworkManager main { get; private set; }

        public void Init()
        {
            InitMainServer();
            InitBattleServer();
        }

        private void InitBattleServer()
        {
            battle = new ClientNetworkManager();
            var socket = new Plugins.Network.WebSocketClient();
            socket.Setup("192.168.1.103", 8740);
            var serializer = new Plugins.ProtoSerializer();
            serializer.getTypeFunc = (name) => { return Type.GetType(name); };
            serializer.LoadProtoNum(typeof(Message.ProtoNum));
            battle.Init(socket, serializer);
            Debug.Log("初始化网络连接（主服） [{0}]".FormatStr(socket.address));
        }

        private void InitMainServer()
        {
            main = new ClientNetworkManager();
            var socket = new Plugins.Network.WebSocketClient();
            socket.Setup("127.0.0.1", 8730);
            var serializer = new Plugins.ProtoSerializer();
            serializer.getTypeFunc = (name) => { return Type.GetType(name); };
            serializer.LoadProtoNum(typeof(Message.ProtoNum));
            main.Init(socket, serializer);
            Debug.Log("初始化网络连接（主服） [{0}]".FormatStr(socket.address));
        }

        public void Close()
        {
            battle.socket.Close();
            main.socket.Close();
        }
    }
}

