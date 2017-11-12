using UnityEngine;
using System.Collections;
using uTools;

 namespace RedStone.UI
{
	[TweenEffect]
    [RequireComponent(typeof(RectTransform))]
    public class UISlideIn : MonoBehaviour
    {
        public float duration = 0.5f;
        public Vector2 fromPosition = Vector2.zero;
        public EaseType method = EaseType.easeOutQuad;

        private Vector2 m_rawPos;

        void Awake()
        {
            m_rawPos = GetComponent<RectTransform>().anchoredPosition;
        }

        void OnEnable()
        {
            var tw = uTweenPosition.Begin(gameObject, fromPosition, m_rawPos, duration);
            tw.method = method;
        }
    }

}