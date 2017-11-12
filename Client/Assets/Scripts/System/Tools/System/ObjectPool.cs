using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace RedStone
{
    public class ObjectPool<T> where T : new()
    {
		private readonly Stack<T> m_pool = new Stack<T>();
        private readonly UnityAction<T> m_ActionOnGet;
        private readonly UnityAction<T> m_ActionOnRelease;

        public int countAll { get; private set; }
        public int countActive { get { return countAll - countInactive; } }
        public int countInactive { get { return m_pool.Count; } }

		private object m_lock = new object();
        public ObjectPool(UnityAction<T> actionOnGet = null, UnityAction<T> actionOnRelease = null)
        {
            m_ActionOnGet = actionOnGet;
            m_ActionOnRelease = actionOnRelease;
        }

        public T Get()
        {
            T element;

			lock (m_lock)
			{
				if (m_pool.Count == 0)
				{
					element = new T ();
					countAll++;
				}
				else
				{
					element = m_pool.Pop ();
				}
			}
            if (m_ActionOnGet != null)
                m_ActionOnGet(element);
            return element;
        }

        public void Release(T element)
        {
			lock (m_lock)
			{
				if (m_pool.Count > 0 && ReferenceEquals (m_pool.Peek (), element))
					Debug.LogError ("Internal error. Trying to destroy object that is already released to pool.");
			}
			if (m_ActionOnRelease != null)
                m_ActionOnRelease(element);
			lock (m_lock)
			{
				m_pool.Push (element);
			}
		}
	}	
	public static class CommonPool<T> where T : new()
	{
		// Object pool to avoid allocations.
		private static readonly ObjectPool<T> s_pool = new ObjectPool<T> (null, null);

		public static T Get ()
		{
			return s_pool.Get ();
		}

		public static void Release (T toRelease)
		{
			s_pool.Release (toRelease);
		}
	}
}
