using System;
using System.Collections.Generic;
using System.Linq;

public class EventManager
{
    private Dictionary<string, Dictionary<int, Action<object>>> m_handlers = new Dictionary<string, Dictionary<int, Action<object>>>();
    private Dictionary<int, Action<object>> m_handlerAlls = new Dictionary<int, Action<object>>();

    public void RegisterAll(Action<object> handler)
    {
        m_handlerAlls.Add(handler.Target.GetHashCode(), handler);
    }

    public void UnRegisterAll(Action<object> handler)
    {
        if (m_handlerAlls.ContainsKey(handler.Target.GetHashCode()))
            m_handlerAlls.Remove(handler.Target.GetHashCode());
    }


    public void UnRegister(string eventName, int handleTargetHashCode)
    {
        if (!m_handlers.ContainsKey(eventName))
            return;

        m_handlers[eventName].Remove(handleTargetHashCode);
        if (m_handlers[eventName].Count == 0)
            m_handlers.Remove(eventName);
    }

    public void Register(string eventName, Action handler)
    {
        if (!m_handlers.ContainsKey(eventName))
            m_handlers.Add(eventName, new Dictionary<int, Action<object>>());

        m_handlers[eventName].Add(handler.Target.GetHashCode(), (obj) =>
        {
            handler.Invoke();
        });
    }

    public void UnRegister(string eventName, Action handler)
    {
        if (!m_handlers.ContainsKey(eventName))
            return;

        m_handlers[eventName].Remove(handler.Target.GetHashCode());
        if (m_handlers[eventName].Count == 0)
            m_handlers.Remove(eventName);
    }

    public void Register<T>(string eventName, Action<T> handler)
    {
        if (!m_handlers.ContainsKey(eventName))
            m_handlers.Add(eventName, new Dictionary<int, Action<object>>());

        m_handlers[eventName].Add(handler.Target.GetHashCode(), (obj) =>
        {
            object[] objArray = obj as object[];
            handler.Invoke((T)objArray[0]);
        });
    }

    public void UnRegister<T>(string eventName, Action<T> handler)
    {
        if (!m_handlers.ContainsKey(eventName))
            return;

        m_handlers[eventName].Remove(handler.Target.GetHashCode());
        if (m_handlers[eventName].Count == 0)
            m_handlers.Remove(eventName);
    }

    public void Register<T1, T2>(string eventName, Action<T1, T2> handler)
    {
        if (!m_handlers.ContainsKey(eventName))
            m_handlers.Add(eventName, new Dictionary<int, Action<object>>());

        m_handlers[eventName].Add(handler.Target.GetHashCode(), (obj) =>
        {
            object[] objArray = obj as object[];
            handler.Invoke((T1)objArray[0], (T2)objArray[1]);
        });
    }

    public void UnRegister<T1, T2>(string eventName, Action<T1, T2> handler)
    {
        if (!m_handlers.ContainsKey(eventName))
            return;

        m_handlers[eventName].Remove(handler.Target.GetHashCode());
        if (m_handlers[eventName].Count == 0)
            m_handlers.Remove(eventName);
    }

    public void Send(string eventName, params object[] param)
    {
        foreach (var item in m_handlerAlls)
        {
            item.Value.Invoke(param);
        }

        if (!m_handlers.ContainsKey(eventName))
            return;
        List<int> keys = m_handlers[eventName].Keys.ToList();
        for (int i = 0; i < keys.Count; i++)
        {
            m_handlers[eventName][keys[i]].Invoke(param);
        }
    }

    public void ClearAll()
    {
        m_handlerAlls.Clear();
        m_handlers.Clear();
    }
}
