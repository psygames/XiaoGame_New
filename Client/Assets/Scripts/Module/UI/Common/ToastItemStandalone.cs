using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RedStone.UI;

namespace RedStone
{
    public class ToastItemStandalone : MonoBehaviour
    {
        public const float showDuration = 2f;
        public const float fadeIn = 0.3f;
        public const float fadeOut = 0.3f;
        public const int needShakeCount = 3;

        public Transform transformCached;
        public Text text;
        public CanvasGroup canvasGroup;
        public uTools.uTweenShake skake;

        private float m_cooldown;
        private float m_fadeIn;
        private float m_fadeOut;
        private bool m_isShow;
        private int m_needShakeCount = 0;
        public static readonly Color failColor = Color.red;
        public static readonly Color successColor = Color.white;

        public bool isShow { get { return m_isShow; } }

        void Update()
        {
            m_cooldown = Mathf.Max(0, m_cooldown - Time.deltaTime);
            m_fadeIn = Mathf.Max(0, m_fadeIn - Time.deltaTime);
            m_fadeOut = Mathf.Max(0, m_fadeOut - Time.deltaTime);

            float lerp = 0;
            if (m_isShow)
            {
                lerp = Tweener.GetCurrentValue(Tweener.Method.CubicEaseOut, 1 - m_fadeIn / fadeIn);
            }
            else
            {
                lerp = Tweener.GetCurrentValue(Tweener.Method.CubicEaseIn, m_fadeOut / fadeOut);
                canvasGroup.alpha = m_fadeOut / fadeOut;
            }
            transformCached.localScale = Vector3.Lerp(Vector2.up, Vector2.one, lerp);

            if (m_isShow && m_cooldown <= 0)
                Hide();
            if (!m_isShow && m_fadeOut <= 0)
                gameObject.SetActive(false);
        }

        public void Show(string message, bool isFailMessage, float _showDuration = showDuration)
        {
            gameObject.SetActive(true);
            text.color = isFailMessage ? failColor : successColor;
            text.text = message;
            m_cooldown = _showDuration;
            m_needShakeCount++;

            if (!m_isShow)
            {
                transformCached.localPosition = Vector2.zero;
                transformCached.localScale = Vector2.up;
                m_fadeIn = fadeIn;
                skake.enabled = false;
                canvasGroup.alpha = 1;
            }
            else if (m_needShakeCount >= needShakeCount && !skake.enabled)
            {
                skake.enabled = true;
                skake.ResetToBeginning();
            }

            m_isShow = true;
        }

        public void Hide(float _fadeOut = fadeOut)
        {
            m_fadeOut = _fadeOut;
            transformCached.localScale = Vector2.one;
            m_isShow = false;
            m_needShakeCount = 0;
        }
    }
}