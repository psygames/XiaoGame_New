using System;
using UnityEngine;
using RedStone.UI;
namespace RedStone
{
    public class ViewBase : EventHandleItem
    {
        public bool isBottom = false;

        public T GetProxy<T>() where T : ProxyBase
        {
            return ProxyManager.instance.GetProxy<T>();
        }

        public void Register<T>(string eventName, Action<T> callback)
        {
            GF.Register(eventName, callback);
        }

        public void Register(string eventName, Action callback)
        {
            GF.Register(eventName, callback);
        }

        public void UnRegister(string eventName, Action callback)
        {
            GF.UnRegister(eventName, callback);
        }

        public void UnRegister<T>(string eventName, Action<T> callback)
        {
            GF.UnRegister(eventName, callback);
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

        public virtual void OnUpdate()
        {

        }

        private void Update()
        {
            OnUpdate();
        }
    }
}
