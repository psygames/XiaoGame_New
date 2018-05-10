using System;
using System.Collections.Generic;

namespace NetworkLib
{
    public class ServerBase : IServer
    {
        public Dictionary<string, ISession> sessions { get; private set; }
        public virtual State state { get; private set; }
        public virtual string ip { get; protected set; }
        public virtual int port { get; protected set; }
        public virtual string address { get { return "{0}:{1}".FormatStr(ip, port); } }

        public Action<string> onConnected { get; set; }
        public Action<string> onClosed { get; set; }
        public Action<string, byte[]> onReceived { get; set; }

        public virtual void Setup(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
        }

        public virtual void Start()
        {
            state = State.Start;
        }

        public virtual void Stop()
        {
            state = State.Stop;
        }

        public ServerBase()
        {
            sessions = new Dictionary<string, ISession>();
        }

        public virtual void OnSessionConnected(ISession session)
        {
            if (sessions.ContainsKey(session.ID))
            {
                //TODO: session reconnect???
            }
            else
            {
                sessions.Add(session.ID, session);
            }
            if (onConnected != null)
                onConnected.Invoke(session.ID);
        }

        public virtual void OnSessionClosed(ISession session)
        {
            if (sessions.ContainsKey(session.ID))
            {
                //TODO: NEED REMOVE ???
                if (onClosed != null)
                    onClosed.Invoke(session.ID);
            }
        }

        public virtual void OnSessionReceived(ISession session, byte[] data)
        {
            if (sessions.ContainsKey(session.ID))
            {
                if (onReceived != null)
                    onReceived.Invoke(session.ID, data);
            }
        }

        public virtual void Send(string sessionID, byte[] data)
        {
            var session = GetSession(sessionID);
            if (session != null)
                session.Send(data);
        }

        public ISession GetSession(string sessionID)
        {
            ISession session = null;
            sessions.TryGetValue(sessionID, out session);
            return session;
        }

        public enum State
        {
            None = 0,
            Start,
            Stop,
        }
    }
}
