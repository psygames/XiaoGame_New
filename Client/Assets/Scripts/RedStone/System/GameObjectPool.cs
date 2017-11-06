using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace Coolfish.System
{
    public class GameObjectPool
    {
        private UnityEngine.Object m_Prefab;
        private readonly Stack<GameObject> m_Stack = new Stack<GameObject>();
        private readonly UnityAction<GameObject> m_ActionOnGet;
        private readonly UnityAction<GameObject> m_ActionOnRelease;

        public int countAll { get; private set; }
        public int countActive { get { return countAll - countInactive; } }
        public int countInactive { get { return m_Stack.Count; } }

        public GameObjectPool(UnityEngine.Object prefab, UnityAction<GameObject> actionOnGet = null, UnityAction<GameObject> actionOnRelease = null)
        {
            m_Prefab = prefab;
            m_ActionOnGet = actionOnGet;
            m_ActionOnRelease = actionOnRelease;
        }

        public GameObject Get()
        {
            GameObject element;
            if (m_Stack.Count == 0)
            {
                element = (GameObject)UnityEngine.Object.Instantiate(m_Prefab);
                countAll++;
            }
            else
            {
                element = m_Stack.Pop();
            }
            if (m_ActionOnGet != null)
                m_ActionOnGet(element);

            element.SetActive(true);
            return element;
        }

        public void Release(GameObject element)
        {
            if (m_Stack.Count > 0 && ReferenceEquals(m_Stack.Peek(), element))
                Debug.LogError("Internal error. Trying to destroy object that is already released to pool.");
            if (m_ActionOnRelease != null)
                m_ActionOnRelease(element);

            element.SetActive(false);
            m_Stack.Push(element);
        }
    }
}
