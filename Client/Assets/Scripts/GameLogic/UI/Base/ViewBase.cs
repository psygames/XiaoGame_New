using System;
using UnityEngine;
namespace RedStone
{
	public class ViewBase : MonoBehaviour
	{
		public bool isBottom = false;

		public T GetProxy<T>() where T : ProxyBase
		{
			return ProxyManager.instance.GetProxy<T>();
		}

		public void Register<T>(string eventName, Action<T> callback)
		{
			EventManager.instance.Register<T>(eventName, callback);
		}

		public void Register(string eventName, Action callback)
		{
			EventManager.instance.Register(eventName, callback);
		}

		public void UnRegister(string eventName, Action callback)
		{
			EventManager.instance.UnRegister(eventName, callback);
		}

		public void UnRegister<T>(string eventName, Action<T> callback)
		{
			EventManager.instance.UnRegister<T>(eventName, callback);
		}

		public virtual void OnInit()
		{

		}

		public virtual void OnDestory()
		{

		}

		public virtual void OnOpen()
		{ 
		
		}

		public virtual void OnClose()
		{ 
		
		}
	}
}
