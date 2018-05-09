using System;
using System.Collections.Generic;
using System.Threading;

namespace Core
{
    public class Updater
    {
        const int interval = 15;//ms
        private Timer timer = null;
        private List<IUpdateable> m_items = new List<IUpdateable>();

        private long m_startTicks = -1;
        private long m_lastTicks = -1;
        private long m_curTicks = -1;

        public float time { get { return (m_curTicks - m_startTicks) * 0.0000001f; } }
        public float deltaTime { get { return (m_curTicks - m_lastTicks) * 0.0000001f; } }

        public void Start()
        {
            timer = new Timer(new TimerCallback(Update), null, 0, interval);
            m_curTicks = m_lastTicks = m_startTicks = DateTime.UtcNow.Ticks;
        }

        private void Update(object obj)
        {
            m_curTicks = DateTime.UtcNow.Ticks;
            try
            {
                lock (m_items)
                {
                    foreach (var item in m_items)
                        item.Update();
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message + "\n" + e.StackTrace);
            }
            m_lastTicks = m_curTicks;
        }


        public void Add(IUpdateable item)
        {
            lock (m_items)
            {
                if (!m_items.Contains(item))
                    m_items.Add(item);
            }
        }

        public void Clear()
        {
            lock (m_items)
            {
                m_items.Clear();
            }
        }
    }
}