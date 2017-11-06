using System;
using System.Collections.Generic;
using System.Linq;

namespace RedStone
{
	public class EventManager : Core.Singleton<EventManager>
	{
		private Dictionary<string, Dictionary<int, Action<object>>> m_handlers = new Dictionary<string, Dictionary<int, Action<object>>>();

		public void Register(string eventName, Action handler)
		{
			Register<object>(eventName, (obj) => { handler.Invoke(); });
		}

		public void UnRegister(string eventName, Action handler)
		{
			UnRegister<object>(eventName, (obj) => { handler.Invoke(); });
		}

		public void Register<T>(string eventName, Action<T> handler)
		{
			if (!m_handlers.ContainsKey(eventName))
				m_handlers.Add(eventName, new Dictionary<int, Action<object>>());

			m_handlers[eventName].Add(handler.GetHashCode(), (obj) => { handler.Invoke((T)obj); });
		}

		public void UnRegister<T>(string eventName, Action<T> handler)
		{
			if (!m_handlers.ContainsKey(eventName))
				return;

			m_handlers[eventName].Remove(handler.GetHashCode());
			if (m_handlers[eventName].Count == 0)
				m_handlers.Remove(eventName);
		}

		public void Send(string eventName, object param = null)
		{
			if (!m_handlers.ContainsKey(eventName))
				return;
			List<int> keys = m_handlers[eventName].Keys.ToList();
			for (int i = 0; i < keys.Count; i++)
			{
				m_handlers[eventName][keys[i]].Invoke(param);
			}
		}

		public void ClearAll()
		{
			m_handlers.Clear();
		}
	}
}
