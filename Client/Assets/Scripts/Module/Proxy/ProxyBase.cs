using System;
using Plugins;
namespace RedStone
{
    public class ProxyBase
    {
        public virtual ClientNetworkManager network
        {
            get { return null; }
        }

        public T GetProxy<T>() where T : ProxyBase
        {
            return ProxyManager.instance.GetProxy<T>();
        }

        public void SendEvent(string eventName, object obj = null)
        {
            EventManager.instance.Send(eventName, obj);
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
