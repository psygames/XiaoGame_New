using System;
namespace NetworkLib
{
    public interface IClient
    {
        void Setup(string ip, int port);
        void Connect();
        void Close();
        void Send(byte[] data);
        Action onConnected { get; set; }
        Action onClosed { get; set; }
        Action<byte[]> onReceived { get; set; }
    }
}