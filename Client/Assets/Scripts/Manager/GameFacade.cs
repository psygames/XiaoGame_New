using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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



















        // Event

        public static void Register<T>(string eventName, Action<T> callback)
        {
            EventManager.instance.Register(eventName, callback);
        }

        public static void Register(string eventName, Action callback)
        {
            EventManager.instance.Register(eventName, callback);
        }

        public static void UnRegister(string eventName, Action callback)
        {
            EventManager.instance.UnRegister(eventName, callback);
        }

        public static void UnRegister<T>(string eventName, Action<T> callback)
        {
            EventManager.instance.UnRegister(eventName, callback);
        }

        public static void Send<T>(string eventName, T msg)
        {
            EventManager.instance.Send(eventName, msg);
        }

        public static void Send(string eventName)
        {
            EventManager.instance.Send(eventName);
        }
    }
}
