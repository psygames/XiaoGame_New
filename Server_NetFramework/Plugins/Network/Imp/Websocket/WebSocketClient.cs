using System;
using System.Collections.Generic;

namespace Plugins.Network
{
    public class WebSocketClient : ClientBase
    {
        public WebSocketSharp.WebSocket _socket = null;
        public override void Setup(string ip, int port)
        {
            base.Setup(ip, port);
            _socket = new WebSocketSharp.WebSocket(NetTool.GetAddress(ip, port) + "/default");
            _socket.Log.Level = WebSocketSharp.LogLevel.Error;
            // _socket.WaitTime = TimeSpan.FromSeconds(1);
            _socket.OnOpen += (a, b) => { base.OnConnected(); };
            _socket.OnClose += (a, b) => { base.OnClosed(); };
            _socket.OnMessage += (a, b) => { base.OnReceived(b.RawData); };
        }
        public override string address { get { return _socket.Url.AbsoluteUri; } }
        public override void Send(byte[] data)
        {
            _socket.SendAsync(data, null);
        }

        public override void Connect()
        {
            _socket.ConnectAsync();
        }

        public override void Close()
        {
            _socket.CloseAsync();
        }

    }
}
