using System;
using Plugins;
namespace RedStone
{
    public class ProxyBase
    {
        public virtual EventManager eventManager { get { return GameManager.instance.eventManager; } }

        public T GetProxy<T>() where T : ProxyBase
        {
            return ProxyManager.instance.GetProxy<T>();
        }

        public void SendEvent(string eventName, object obj = null)
        {
            eventManager.Send(eventName, obj);
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
