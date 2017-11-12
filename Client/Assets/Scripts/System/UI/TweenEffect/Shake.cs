using UnityEngine;
using System.Collections;
using uTools;

 namespace RedStone.UI
{
    [TweenEffect]
    [RequireComponent(typeof(RectTransform))]
    public class Shake : MonoBehaviour
    {
        public float duration = 0.5f;
        public float interval = 5f;
        public Vector2 limit = new Vector2(15, 15);
        public EaseType method = EaseType.easeOutQuad;

        private float m_interval = 0;
        private uTweenShake m_tween;

        void OnEnable()
        {
            m_interval = interval;
        }

        void BeginShake()
        {
            var tw = uTweenShake.Begin(gameObject, Vector3.zero, duration, 0);
            tw.limit = limit;
            tw.method = method;
            tw.shakeType = eShake.Position;
            tw.style = uTweener.Style.Once;
            m_tween = tw;
            m_interval = interval;
        }

        public void Update()
        {
            m_interval = Mathf.Max(0, m_interval - Time.deltaTime);
            if (m_interval <= 0)
            {
                BeginShake();
            }

            if (m_tween != null)
            {
                m_tween.duration = duration;
                m_tween.method = method;
                m_tween.limit = limit;
            } 
            
        }
    }

}