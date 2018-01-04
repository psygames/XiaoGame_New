using System;
using System.Collections.Generic;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Plugins.Network
{
    public class WebSocketServer : ServerBase
    {
        const string defaultServicePath = "/default";
        public WebSocketSharp.Server.WebSocketServer _server = null;
        public override string address { get { return "ws://{0}{1}:{2}".FormatStr(_server.Address, defaultServicePath, _server.Port); } }
        private WebSocketServiceHost host
        {
            get { return _server.WebSocketServices[defaultServicePath]; }
        }
        public override void Setup(string ip, int port)
        {
            base.Setup(ip, port);
            _server = new WebSocketSharp.Server.WebSocketServer(NetTool.GetAddress(ip, port));
            _server.Log.Level = WebSocketSharp.LogLevel.Error;
            _server.AddWebSocketService(defaultServicePath, () =>
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
