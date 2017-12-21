using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plugins;

namespace RedStone
{
    public class GF
    {
        public static T GetProxy<T>() where T : ProxyBase
        {
            return ProxyManager.instance.GetProxy<T>();
        }

        public static void ShowView<T>() where T : ViewBase
        {
            UIManager.instance.Show<T>();
        }

        public static void ChangeState<T>() where T : AbstractState, new()
        {
            GameManager.instance.ChangeGameState<T>();
        }

        public static void StartCoroutine(System.Collections.IEnumerator enumerator)
        {
            GameManager.instance.StartCoroutine(enumerator);
        }


















        // Event
        public static EventManager eventManager { get { return GameManager.instance.eventManager; } }

        public static void Register<T>(string eventName, Action<T> callback)
        {
            eventManager.Register(eventName, callback);
        }

        public static void Register(string eventName, Action callback)
        {
            eventManager.Register(eventName, callback);
        }

        public static void UnRegister(string eventName, Action callback)
        {
            eventManager.UnRegister(eventName, callback);
        }

        public static void UnRegister<T>(string eventName, Action<T> callback)
        {
            eventManager.UnRegister(eventName, callback);
        }

        public static void Send<T>(string eventName, T msg)
        {
            eventManager.Send(eventName, msg);
        }

        public static void Send(string eventName)
        {
            eventManager.Send(eventName);
        }

    }
}
