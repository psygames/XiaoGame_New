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
        public Action<string> onTimeout { get; set; }

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

        private Dictionary<string, double> m_sessionHeartbeatTime = new Dictionary<string, double>();
        private HashSet<string> m_timeoutSessions = new HashSet<string>();

        private void OnTimerCheckTimeoutCallback(object _sta)
        {
            lock (this)
            {
                foreach (var se in m_server.sessions)
                {
                    if (m_sessionHeartbeatTime.ContainsKey(se.Key)
                        && !m_timeoutSessions.Contains(se.Key)
                        && time - m_sessionHeartbeatTime[se.Key] > m_timeoutDuration)
                    {
                        Timeout(se.Key);
                    }
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
            m_sessionHeartbeatTime[sessionID] = time;
            var msg = Deserialze(data);
            ReplyHeartbeat(sessionID, msg.number);
            return msg;
        }

        private void ReplyHeartbeat(string sessionID, int number)
        {
            HeartbeatReply msg = new HeartbeatReply();
            msg.number = number;
            m_server.Send(sessionID, msg.Serialize());
        }

        protected void Timeout(string sessionID)
        {
            m_timeoutSessions.Add(sessionID);
            if (onTimeout != null)
                onTimeout.Invoke(sessionID);
        }

        public void TestTriggerTimeout(string sessionID)
        {
            Timeout(sessionID);
        }
    }
}
