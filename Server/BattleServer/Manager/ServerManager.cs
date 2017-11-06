using System;
using System.Collections.Generic;
using Plugins;

namespace RedStone
{
    public class ServerManager : Core.Singleton<ServerManager>
    {
        public ClientNetworkManager BM { get; private set; }

        public void Init()
        {
            BM = new ClientNetworkManager();
            var socket = new Plugins.Network.WebSocketClient();
            socket.Setup("127.0.0.1", 8730);
            var serializer = new Plugins.ProtoSerializer();
            serializer.getTypeFunc = (name) => { return Type.GetType(name); };
            serializer.LoadNumFile(MyPath.RES_PROTO_NUM);
            BM.Init(socket, serializer);
        }
    }
}

