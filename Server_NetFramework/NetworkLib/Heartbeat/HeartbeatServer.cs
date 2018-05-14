using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace NetworkLib
{
    public class HeartbeatServer
    {
        private Timer m_timerCheckTimeout = null;
        private ServerBase m_server = null;
        private float m_timeoutDuration;

        public bool isTimeout { get; protected set; }
        public Action onTimeout { get; set; }

        public HeartbeatServer(float timeout, ServerBase server)
        {
            m_timeoutDuration = timeout;
            m_server = server;

            m_timerCheckTimeout = new Timer(new TimerCallback(OnTimerCheckTimeoutCallback));
            m_timerCheckTimeout.Change(0, (int)(timeout * 1000 * 0.5f));
        }


        public void Dispose()
        {
            m_timerCheckTimeout.Dispose();
        }

        private double time { get { return DateTime.Now.Ticks * 0.0000001d; } }
        private double m_heartbeatReplyTime = 0;
        private void OnTimerCheckTimeoutCallback(object _sta)
        {
            lock (this)
            {
                if (!isTimeout && m_heartbeatReplyTime != 0
                    && time - m_heartbeatReplyTime > m_timeoutDuration)
                {
                    isTimeout = true;
                    OnTimeout();
                }
            }
        }


        public static HeartbeatRequest Deserialze(byte[] data)
        {
            byte[] body = new byte[data.Length - 2];
            Array.Copy(data, 2, body, 0, data.Length - 2);
            HeartbeatRequest msg = HeartbeatRequest.Parse(body);
            return msg;
        }

        public HeartbeatRequest HandleMessage(string sessionID, byte[] data)
        {
            m_heartbeatReplyTime = time;
            var msg = Deserialze(data);
            return msg;
        }

        protected void OnTimeout()
        {
            if (onTimeout != null)
                onTimeout.Invoke();
        }
    }
}
