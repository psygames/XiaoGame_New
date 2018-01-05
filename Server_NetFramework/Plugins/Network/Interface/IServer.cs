using System;
namespace Plugins.Network
{
    public interface IServer
    {
        void Setup(string ip, int port);
        void Start();
        void Stop();
        void Send(string sessionId, byte[] content);
        Action<string> onConnected { get; set; }
        Action<string> onClosed { get; set; }
        Action<string, byte[]> onReceived { get; set; }

        ISession GetSession(string sessionID);
    }
}
