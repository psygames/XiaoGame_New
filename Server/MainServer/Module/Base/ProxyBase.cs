using System;
using Plugins;
namespace RedStone
{
    public class ProxyBase
    {
        public T GetProxy<T>() where T : ProxyBase
        {
            return ProxyManager.instance.GetProxy<T>();
        }

        public virtual void OnInit() { }

        public virtual void OnUpdate() { }

        public virtual void OnDestroy() { }
    }
}
