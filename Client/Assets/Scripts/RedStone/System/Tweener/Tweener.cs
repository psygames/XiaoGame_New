using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Hotfire
{
    public class Tweener
    {
        private delegate float MethodDelegate(float t, float b, float c, float d);

        private static Dictionary<Method, MethodDelegate> s_tweenDictionaryCache;
        private static Dictionary<Method, MethodDelegate> s_tweenDictionary
        {
            get
            {
                if (s_tweenDictionaryCache == null)
                {
                    s_tweenDictionaryCache = new Dictionary<Method, MethodDelegate>();
                    InitTweenDictionary();
                }
                return s_tweenDictionaryCache;
            }
        }

        private static void InitTweenDictionary()
        {
            s_tweenDictionaryCache.Add(Method.Linear, TweenMethodLibrary.Linear);
            s_tweenDictionaryCache.Add(Method.QuadEaseOut, TweenMethodLibrary.QuadEaseOut);
            s_tweenDictionaryCache.Add(Method.QuadEaseIn, TweenMethodLibrary.QuadEaseIn);
            s_tweenDictionaryCache.Add(Method.QuadEaseInOut, TweenMethodLibrary.QuadEaseInOut);
            s_tweenDictionaryCache.Add(Method.QuadEaseOutIn, TweenMethodLibrary.QuadEaseOutIn);
            s_tweenDictionaryCache.Add(Method.ExpoEaseOut, TweenMethodLibrary.ExpoEaseOut);
            s_tweenDictionaryCache.Add(Method.ExpoEaseIn, TweenMethodLibrary.ExpoEaseIn);
            s_tweenDictionaryCache.Add(Method.ExpoEaseInOut, TweenMethodLibrary.ExpoEaseInOut);
            s_tweenDictionaryCache.Add(Method.ExpoEaseOutIn, TweenMethodLibrary.ExpoEaseOutIn);
            s_tweenDictionaryCache.Add(Method.CubicEaseOut, TweenMethodLibrary.CubicEaseOut);
            s_tweenDictionaryCache.Add(Method.CubicEaseIn, TweenMethodLibrary.CubicEaseIn);
            s_tweenDictionaryCache.Add(Method.CubicEaseInOut, TweenMethodLibrary.CubicEaseInOut);
            s_tweenDictionaryCache.Add(Method.CubicEaseOutIn, TweenMethodLibrary.CubicEaseOutIn);
            s_tweenDictionaryCache.Add(Method.QuartEaseOut, TweenMethodLibrary.QuartEaseOut);
            s_tweenDictionaryCache.Add(Method.QuartEaseIn, TweenMethodLibrary.QuartEaseIn);
            s_tweenDictionaryCache.Add(Method.QuartEaseInOut, TweenMethodLibrary.QuartEaseInOut);
            s_tweenDictionaryCache.Add(Method.QuartEaseOutIn, TweenMethodLibrary.QuartEaseOutIn);
            s_tweenDictionaryCache.Add(Method.QuintEaseOut, TweenMethodLibrary.QuintEaseOut);
            s_tweenDictionaryCache.Add(Method.QuintEaseIn, TweenMethodLibrary.QuintEaseIn);
            s_tweenDictionaryCache.Add(Method.QuintEaseInOut, TweenMethodLibrary.QuintEaseInOut);
            s_tweenDictionaryCache.Add(Method.QuintEaseOutIn, TweenMethodLibrary.QuintEaseOutIn);
            s_tweenDictionaryCache.Add(Method.CircEaseOut, TweenMethodLibrary.CircEaseOut);
            s_tweenDictionaryCache.Add(Method.CircEaseIn, TweenMethodLibrary.CircEaseIn);
            s_tweenDictionaryCache.Add(Method.CircEaseInOut, TweenMethodLibrary.CircEaseInOut);
            s_tweenDictionaryCache.Add(Method.CircEaseOutIn, TweenMethodLibrary.CircEaseOutIn);
            s_tweenDictionaryCache.Add(Method.SineEaseOut, TweenMethodLibrary.SineEaseOut);
            s_tweenDictionaryCache.Add(Method.SineEaseIn, TweenMethodLibrary.SineEaseIn);
            s_tweenDictionaryCache.Add(Method.SineEaseInOut, TweenMethodLibrary.SineEaseInOut);
            s_tweenDictionaryCache.Add(Method.SineEaseOutIn, TweenMethodLibrary.SineEaseOutIn);
            s_tweenDictionaryCache.Add(Method.ElasticEaseOut, TweenMethodLibrary.ElasticEaseOut);
            s_tweenDictionaryCache.Add(Method.ElasticEaseIn, TweenMethodLibrary.ElasticEaseIn);
            s_tweenDictionaryCache.Add(Method.ElasticEaseInOut, TweenMethodLibrary.ElasticEaseInOut);
            s_tweenDictionaryCache.Add(Method.ElasticEaseOutIn, TweenMethodLibrary.ElasticEaseOutIn);
            s_tweenDictionaryCache.Add(Method.BounceEaseOut, TweenMethodLibrary.BounceEaseOut);
            s_tweenDictionaryCache.Add(Method.BounceEaseIn, TweenMethodLibrary.BounceEaseIn);
            s_tweenDictionaryCache.Add(Method.BounceEaseInOut, TweenMethodLibrary.BounceEaseInOut);
            s_tweenDictionaryCache.Add(Method.BounceEaseOutIn, TweenMethodLibrary.BounceEaseOutIn);
            s_tweenDictionaryCache.Add(Method.BackEaseOut, TweenMethodLibrary.BackEaseOut);
            s_tweenDictionaryCache.Add(Method.BackEaseIn, TweenMethodLibrary.BackEaseIn);
            s_tweenDictionaryCache.Add(Method.BackEaseInOut, TweenMethodLibrary.BackEaseInOut);
            s_tweenDictionaryCache.Add(Method.BackEaseOutIn, TweenMethodLibrary.BackEaseOutIn);
        }

        public enum Method
        {
            Linear,
            QuadEaseOut, QuadEaseIn, QuadEaseInOut, QuadEaseOutIn,
            ExpoEaseOut, ExpoEaseIn, ExpoEaseInOut, ExpoEaseOutIn,
            CubicEaseOut, CubicEaseIn, CubicEaseInOut, CubicEaseOutIn,
            QuartEaseOut, QuartEaseIn, QuartEaseInOut, QuartEaseOutIn,
            QuintEaseOut, QuintEaseIn, QuintEaseInOut, QuintEaseOutIn,
            CircEaseOut, CircEaseIn, CircEaseInOut, CircEaseOutIn,
            SineEaseOut, SineEaseIn, SineEaseInOut, SineEaseOutIn,
            ElasticEaseOut, ElasticEaseIn, ElasticEaseInOut, ElasticEaseOutIn,
            BounceEaseOut, BounceEaseIn, BounceEaseInOut, BounceEaseOutIn,
            BackEaseOut, BackEaseIn, BackEaseInOut, BackEaseOutIn
        }

        public enum Style
        {
            Once,
            Loop,
            PingPong,
        }

        private Method m_method;
        private Style m_style;
        public Method method { get { return m_method; } }
        public Style style { get { return m_style; } }

        public float duration { get; set; }
        public float from { get; set; }
        public float to { get; set; }
        public float time { get; set; }
        public bool isGoBack { get { return m_isGoBack; } }

        public bool m_isStopped = false;
        private bool m_isGoBack = false;

        public List<Action> onFinished = new List<Action>();

        public Tweener(Method method, Style style = Style.Once)
        {
            this.m_method = method;
            this.m_style = style;
        }

        /// <summary>
        /// use for static method
        /// </summary>
        /// <param name="method">Method</param>
        /// <param name="time">当前时间</param>
        /// <param name="from">初始值</param>
        /// <param name="to">最终值</param>
        /// <param name="duration">持续时间</param>
        /// <returns></returns>
        public static float GetCurrentValue(Method method, float time, float from = 0, float to = 1, float duration = 1)
        {
            return s_tweenDictionary[method](time, from, to - from, duration);
        }

        /// <summary>
        /// You can use this method only when the tweener style is not None.
        /// </summary>
        /// <returns> the result </returns>
        public float GetCurrentValue()
        {
            return GetCurrentValue(method, time, from, to, duration);
        }

        /// <summary>
        /// use this for Tweener Begin Running when style is not None .
        /// </summary>
        /// <param name="from">初始值</param>
        /// <param name="to">最终值</param>
        /// <param name="duration">持续时间</param>
        public void Begin(float from = 0, float to = 1, float duration = 1)
        {
            this.from = from;
            this.to = to;
            this.duration = duration;
            m_isStopped = false;
            Reset();
        }

        public void Reset()
        {
            time = 0;
            if (m_isGoBack)
            {
                m_isGoBack = false;
                float tmp = from;
                from = to;
                to = tmp;
            }
        }

        /// <summary>
        /// You can use this method only when the tweener style is not None.(PS: You must update per frame.)
        /// </summary>
        /// <param name="dt">DeltaTime</param>
        public void ActiveUpdate(float dt)
        {
            if (m_isStopped)
                return;
            time += dt;

            switch (style)
            {
                case Style.Once:
                    RunOnceLogic(dt);
                    break;
                case Style.Loop:
                    RunLoopLogic(dt);
                    break;
                case Style.PingPong:
                    RunPingpangLogic(dt);
                    break;
            }
        }

        private void RunOnceLogic(float dt)
        {
            if (time >= duration)
            {
                m_isStopped = true;
                ExcuteOnFinishedEvent();
            }
        }

        private void RunLoopLogic(float dt)
        {
            if (time >= duration)
            {
                time -= duration;
                ExcuteOnFinishedEvent();
            }
        }

        private void RunPingpangLogic(float dt)
        {
            if (time >= duration)
            {
                time -= duration;
                float tmp = from;
                from = to;
                to = tmp;
                m_isGoBack = !m_isGoBack;
                ExcuteOnFinishedEvent();
            }
        }

        private void ExcuteOnFinishedEvent()
        {
            for (int i = 0; i < onFinished.Count; i++)
            {
                onFinished[i]();
            }
        }
    }
}
