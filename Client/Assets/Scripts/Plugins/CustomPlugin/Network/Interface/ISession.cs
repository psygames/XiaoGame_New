namespace Plugins.Network
{

    public interface ISession
    {
        void Send(byte[] data);

        void OnConnected();
        void OnClosed();
        void OnReceived(byte[] data);
        string ID { get; }
    }}