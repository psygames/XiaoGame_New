using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace NetworkLib
{
    public class HeartbeatClient
    {
        private Timer m_timerHeartbeat = null;
        private Timer m_timerCheckTimeout = null;
        private ClientBase m_client = null;
        private float m_timeoutDuration;
        private float m_sendInterval;

        public bool isTimeout { get; protected set; }
        public Action onTimeout { get; set; }

        public HeartbeatClient(float interval, float timeout, ClientBase client)
        {
            m_timeoutDuration = timeout;
            m_sendInterval = interval;
            m_client = client;

            m_timerHeartbeat = new Timer(new TimerCallback(OnTimerHeartbeatCallback));
            m_timerCheckTimeout = new Timer(new TimerCallback(OnTimerCheckTimeoutCallback));
        }


        public void Start()
        {
            m_heartbeatNumber = 1;
            isTimeout = false;
            m_heartbeatReplyTime = time;

            m_timerHeartbeat.Change(0, (int)(m_sendInterval * 1000));
            m_timerCheckTimeout.Change(0, (int)(m_timeoutDuration * 1000 * 0.5f));
        }


        public void Stop()
        {
            m_timerHeartbeat.Change(-1, -1);
            m_timerCheckTimeout.Change(-1, -1);
        }

        public void Dispose()
        {
            m_timerCheckTimeout.Dispose();
            m_timerHeartbeat.Dispose();
        }

        private void OnTimerHeartbeatCallback(object _sta)
        {
            lock (this)
            {
                if (m_client.state != ClientBase.State.Connected)
                    return;
                SendHeartbeat();
            }
        }

        private int m_heartbeatNumber = 1;
        private void SendHeartbeat()
        {
            HeartbeatRequest msg = new HeartbeatRequest();
            msg.number = m_heartbeatNumber;
            m_client.Send(msg.Serialize());
            m_heartbeatNumber++;
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
                    Timeout();
                }
            }
        }

        public static HeartbeatReply Deserialze(byte[] data)
        {
            byte[] body = new byte[data.Length - 2];
            Array.Copy(data, 2, body, 0, data.Length - 2);
            HeartbeatReply msg = HeartbeatReply.Parse(body);
            return msg;
        }

        public HeartbeatReply HandleMessage(byte[] data)
        {
            m_heartbeatReplyTime = time;
            var msg = Deserialze(data);
            return msg;
        }

        protected void Timeout()
        {
            isTimeout = true;
            if (onTimeout != null)
                onTimeout.Invoke();
        }

        public void TestTriggerTimeout()
        {
            Timeout();
        }
    }
}
