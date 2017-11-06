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

        public void Start()
        {
            timer = new Timer(new TimerCallback(Update), null, 0, interval);
        }

        private void Update(object obj)
        {
            foreach (var item in m_items)
                item.Update();
            foreach (var item in m_items)
                item.LateUpdate();
        }


        public void Add(IUpdateable item)
        {
            lock (m_items)
            {
                if (!m_items.Contains(item))
                    Add(item);
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
