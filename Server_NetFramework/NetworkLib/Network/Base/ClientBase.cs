using System;
using System.Collections.Generic;

namespace NetworkLib
{
    public class ClientBase : IClient
    {
        public virtual void Setup(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
        }
        public virtual void Send(byte[] data) { }
        public virtual void Connect() { }
        public virtual void Close() { }


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

        public virtual string ip { get; protected set; }
        public virtual int port { get; protected set; }
        public virtual string address { get { return "{0}:{1}".FormatStr(ip, port); } }
        public Action<byte[]> onReceived { get; set; }
        public Action onClosed { get; set; }
        public Action onConnected { get; set; }
    }
}
