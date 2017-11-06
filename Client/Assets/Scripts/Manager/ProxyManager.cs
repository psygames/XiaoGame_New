using System;
using System.Collections.Generic;
namespace RedStone
{
	public class ProxyManager : Core.Singleton<ProxyManager>
	{
		private Dictionary<Type, ProxyBase> m_proxys = new Dictionary<Type, ProxyBase>();

		public ProxyManager()
		{
			AddProxy<HallProxy>();
			AddProxy<GomukuProxy>();
		}

		private void AddProxy<T>() where T : ProxyBase, new()
		{
			m_proxys.Add(typeof(T), new T());
		}

		public T GetProxy<T>() where T : ProxyBase
		{
			return (T)m_proxys[typeof(T)];
		}

		public void Init()
		{
			var itr = m_proxys.GetEnumerator();
			while (itr.MoveNext())
			{
				itr.Current.Value.OnInit();
			}
		}

		public void Destroy()
		{
			var itr = m_proxys.GetEnumerator();
			while (itr.MoveNext())
			{
				itr.Current.Value.OnDestroy();
			}
		}

		public void Update()
		{
			var itr = m_proxys.GetEnumerator();
			while (itr.MoveNext())
			{
				itr.Current.Value.OnUpdate();
			}
		}
	}
}
