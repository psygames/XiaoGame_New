using System;
using UnityEngine;

namespace RedStone.Core
{
    public abstract class Singleton<T>
    {
        private static T s_instance;
        public static T instance { get { return s_instance; } }

        public static T CreateInstance(params object[] args)
        {
            if (s_instance != null)
            {
                throw new InvalidOperationException(typeof(T).ToString() + "is not Destory before Create");
            }
            s_instance = (T)Activator.CreateInstance(typeof(T), args);
            return instance;
        }
        public static T CreateInstance()
        {
            if (s_instance != null)
            {
				DestoryInstance ();
               // throw new InvalidOperationException(typeof(T).ToString() + "is not Destory before Create");
            }
            s_instance = Activator.CreateInstance<T>();
            return instance;
        }

        public static void DestoryInstance()
        {
            if (s_instance == null)
            {
                throw new InvalidOperationException(typeof(T).ToString() + "is not Create before Destory");
            }
            s_instance = default(T);
        }
    }
}
