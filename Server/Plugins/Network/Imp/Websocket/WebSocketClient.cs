using System;
using System.Collections.Generic;

namespace Plugins.Network
{
    public class WebSocketClient : ClientBase
    {
        public WebSocketSharp.WebSocket _socket = null;
        public override void Setup(string ip, int port)
        {
            _socket = new WebSocketSharp.WebSocket(WebSocketTool.GetAddress(ip, port) + "/default");
            _socket.OnOpen += (a, b) => { base.OnConnected(); };
            _socket.OnClose += (a, b) => { base.OnClosed(); };
            _socket.OnMessage += (a, b) => { base.OnReceived(b.RawData); };
        }

        public override void Send(byte[] data)
        {
            _socket.Send(data);
        }

        public override void Connect()
        {
            _socket.Connect();
        }

        public override void Close()
        {
            _socket.Close();
        }

    }
}
