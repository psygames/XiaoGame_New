using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RedStone.Core
{
    public abstract class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T s_instance;

        public static T instance { get { return s_instance; } }

        public static T CreateInstance(GameObject go)
        {
            if (s_instance != null)
            {
                throw new InvalidOperationException(typeof(T).ToString() + "is not Destory before Create");
            }
            s_instance = go.AddComponent<T>();
            return instance;
        }

        public static void DestoryInstance()
        {
            if (s_instance == null)
            {
                throw new InvalidOperationException(typeof(T).ToString() + "is not Create before Destory");
            }
            GameObject.Destroy(s_instance);
            s_instance = null;
        }

        protected virtual void Awake()
        {
            if (s_instance != null)
            {
                throw new InvalidOperationException("Already has a " + typeof(T) + " instance");
            }
            s_instance = this as T;
        }

        protected virtual void OnDestory()
        {
            s_instance = null;
        }
    }
}
