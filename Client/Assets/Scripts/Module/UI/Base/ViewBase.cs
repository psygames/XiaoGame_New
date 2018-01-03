using System;
using UnityEngine;
using RedStone.UI;
using System.Collections.Generic;

namespace RedStone
{
    public class ViewBase : EventHandleItem
    {
        public bool isBottom = false;

        public T GetProxy<T>() where T : ProxyBase
        {
            return ProxyManager.instance.GetProxy<T>();
        }

        List<RegistedEvent> registedEvents = new List<RegistedEvent>();

        class RegistedEvent
        {
            public RegistedEvent(string evtName, int hashCode)
            {
                this.eventName = evtName;
                this.handleTargetHashCode = hashCode;
            }

            public string eventName;
            public int handleTargetHashCode;
        }

        private void UnRegisterAll()
        {
            foreach (var re in registedEvents)
            {
                GF.eventManager.UnRegister(re.eventName, re.handleTargetHashCode);
            }
            registedEvents.Clear();
        }


        public void Register<T>(string eventName, Action<T> callback)
        {
            GF.Register(eventName, callback);
            registedEvents.Add(new RegistedEvent(eventName, callback.Target.GetHashCode()));
        }

        public void Register(string eventName, Action callback)
        {
            GF.Register(eventName, callback);
            registedEvents.Add(new RegistedEvent(eventName, callback.Target.GetHashCode()));
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

        public virtual void OnDestroy()
        {
            UnRegisterAll();
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
