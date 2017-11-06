using System;
using message;
namespace RedStone
{
	public class ProxyBase
	{
		public Net.Network network
		{
			get { return NetworkManager.instance.Get(netType); }
		}

		public T GetProxy<T>() where T : ProxyBase
		{
			return ProxyManager.instance.GetProxy<T>();
		}

		public NetType netType = NetType.Hall;

		public void SendEvent(string eventName, object obj = null)
		{
			EventManager.instance.Send(eventName, obj);
		}

		public ProxyBase()
		{

		}

		public virtual void OnInit()
		{

		}

		public virtual void OnUpdate()
		{

		}

		public virtual void OnDestroy()
		{

		}
	}
}
