using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Tools
{
    public class CountDownTimer
    {
        private float m_duration;
        private float m_startTime = 0;

        public CountDownTimer(float duration = 0f)
        {
            m_duration = duration;
        }

        public bool IsStoped
        {
            get
            {
                return m_startTime == 0;
            }
        }

        public bool IsExpired
        {
            get
            {
                return !IsStoped && (Time.time - m_startTime) > m_duration;
            }
        }

        public static CountDownTimer StartNew(float duration = 0f)
        {
            CountDownTimer result = new CountDownTimer(duration);
            result.Start();
            return result;
        }
        public void Start()
        {
            m_startTime = Time.time;
        }

        public void Stop()
        {
            m_startTime = 0;
        }

        public float value
        {
            get
            {
                return Mathf.Max(m_duration - (Time.time - m_startTime), 0);
            }
        }
        public float elapsedRatio
        {
            get
            {
                return 1 - ratio;
            }
        }

        public float ratio
        {
            get
            {
                if (IsStoped)
                {
                    return 1;
                }
                else
                {
                    float delta = Time.time - m_startTime;
                    return 1 - Mathf.Min(delta / m_duration, 1);
                }
            }

        }
    }
}
