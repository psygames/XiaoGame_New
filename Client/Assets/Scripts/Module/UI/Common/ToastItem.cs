using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RedStone
{
    public class ToastItem : MonoBehaviour
    {
        public const float showDuration = 2f;


        public Transform transformCached;
        public UI.Text text;
        public CanvasGroup canvasGroup;
        private Vector2 m_targetPos = Vector2.zero;

        public float m_cooldown { get; private set; }

        public float cooldown { get { return m_cooldown; } }

        public static readonly Color failColor = Color.red;
        public static readonly Color successColor = Color.white;
        public bool isShow { get { return m_cooldown > 0; } }

        void Start()
        {

        }

        void Update()
        {
            m_cooldown = Mathf.Max(0, m_cooldown - Time.deltaTime);
            if (m_cooldown <= 0 && gameObject.activeSelf)
                gameObject.SetActive(false);
            float lerp = Tweener.GetCurrentValue(Tweener.Method.QuartEaseOut, m_cooldown, 0, 1, showDuration);
            canvasGroup.alpha = lerp;

            transformCached.localPosition = Vector2.Lerp(transformCached.localPosition, m_targetPos, Time.deltaTime * 15);
            transformCached.localScale = Vector3.Lerp(transformCached.localScale, Vector3.one, Time.deltaTime * 15);
        }

        public void Show(string message, bool isFailMessage, float _showDuration = -1)
        {
            gameObject.SetActive(true);
            transformCached.localPosition = Vector2.zero;
            transformCached.localScale = Vector2.zero;
            m_targetPos = Vector2.zero;

            text.color = isFailMessage ? failColor : successColor;
            text.text = message;

            if (_showDuration > 0)
                m_cooldown = _showDuration;
            else
                m_cooldown = showDuration;
        }

        public void Move(Vector2 vec)
        {
            m_targetPos += vec;
        }
    }
}