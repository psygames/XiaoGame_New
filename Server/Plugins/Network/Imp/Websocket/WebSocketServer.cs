using System;
using System.Collections.Generic;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Plugins.Network
{
    public class WebSocketServer : ServerBase
    {
        public WebSocketSharp.Server.WebSocketServer _server = null;
        private WebSocketServiceHost host
        {
            get { return _server.WebSocketServices["/default"]; }
        }
        public override void Setup(string ip, int port)
        {
            _server = new WebSocketSharp.Server.WebSocketServer(WebSocketTool.GetAddress(ip, port));
            _server.Log.Level = WebSocketSharp.LogLevel.Error;
            _server.WaitTime = TimeSpan.FromSeconds(1);
            _server.AddWebSocketService("/default", () =>
            {
                WebSocketSession session = new WebSocketSession(SendAction);
                session.onConnected = () => { OnSessionConnected(session); };
                session.onClosed = () => { OnSessionClosed(session); };
                session.onReceived = (data) => { OnSessionReceived(session, data); };
                return session.behavior;
            });
        }

        private void SendAction(string sessionID, byte[] data)
        {
            host.Sessions.SendTo(data, sessionID);
        }

        public override void Start()
        {
            _server.Start();
        }

        public override void Stop()
        {
            _server.Stop();
        }
    }
}
