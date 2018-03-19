using UnityEngine;
using System.Collections;
 
using System.Collections.Generic;
public class Statistics
{
    private static Statistics s_instance;
    public static Statistics instance
    {
        get
        {
            if (s_instance == null)
                s_instance = new Statistics();
            return s_instance;
        }
    }

    private const string defaultListName = "___JUST_4_DEFAULT___";
    public void Add(float num)
    {
        Add(defaultListName, num);
    }

    public void Add(Vector2 num)
    {
        Add(defaultListName, num);
    }

    Dictionary<string, List<float>> m_data = new Dictionary<string, List<float>>();

    public void Add(string name, float num)
    {
        if (!m_data.ContainsKey(name))
            m_data.Add(name, new List<float>());
        m_data[name].Add(num);
        m_data[name].Sort();
    }

    public void Add(string name, Vector2 vec)
    {
        Add(name + "x", vec.x);
        Add(name + "y", vec.y);
    }

    public float GetMedian(string name = defaultListName)
    {
        List<float> lst = m_data[name];
        return lst[lst.Count / 2];
    }

    public float GetMax(string name = defaultListName)
    {
        List<float> lst = m_data[name];
        return lst[lst.Count - 1];
    }

    public float GetMin(string name = defaultListName)
    {
        List<float> lst = m_data[name];
        return lst[0];
    }

    public Vector2 GetMedianV2(string name = defaultListName)
    {
        return new Vector2(GetMedian(name + "x"), GetMedian(name + "y"));
    }

    public Vector2 GetMaxV2(string name = defaultListName)
    {
        return new Vector2(GetMax(name + "x"), GetMax(name + "y"));

    }

    public Vector2 GetMinV2(string name = defaultListName)
    {
        return new Vector2(GetMin(name + "x"), GetMin(name + "y"));
    }
}
