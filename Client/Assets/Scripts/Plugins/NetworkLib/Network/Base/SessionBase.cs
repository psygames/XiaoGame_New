using System;

namespace NetworkLib
{
    public class SessionBase : ISession
    {
        public virtual string ID { get; protected set; }
        public Action onConnected { get; set; }
        public Action onClosed { get; set; }
        public Action<byte[]> onReceived { get; set; }

        public virtual void Send(byte[] data) { }

        public virtual void OnConnected()
        {
            if (onConnected != null)
                onConnected.Invoke();
        }

        public virtual void OnClosed()
        {
            if (onClosed != null)
                onClosed.Invoke();
        }

        public virtual void OnReceived(byte[] data)
        {
            if (onReceived != null)
                onReceived.Invoke(data);
        }
    }
}
