using System;
using System.Collections.Generic;

namespace Plugins.Network
{
    public class ClientBase : IClient
    {
        public virtual void Setup(string ip, int port){}
        public virtual void Send(byte[] data){}
        public virtual void Connect(){}
        public virtual void Close(){}


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

        public Action<byte[]> onReceived { get; set; }
        public Action onClosed { get; set; }
        public Action onConnected { get; set; }
    }
}
