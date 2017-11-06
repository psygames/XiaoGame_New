using System;
using System.Collections.Generic;
using Plugins;

namespace RedStone
{
    public class ServerManager : Core.Singleton<ServerManager>
    {
        public ServerNetworkManager MB { get; private set; }
        public ServerNetworkManager MC { get; private set; }

        public void Init()
        {
            MB = new ServerNetworkManager();
            var server = new Plugins.Network.WebSocketServer();
            server.Setup("127.0.0.1", 8730);
            var serializer = new Plugins.ProtoSerializer();
            serializer.getTypeFunc = (name) => { return Type.GetType(name); };
            serializer.LoadNumFile(MyPath.RES_PROTO_NUM);
            MB.Init(server, serializer);
        }
    }
}

