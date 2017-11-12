using UnityEngine;
using System.Collections;
 namespace RedStone.UI
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(RectTransform))]
    public class RectTransformPosBind : MonoBehaviour
    {
        public RectTransform target;
        public bool worldPos = false;

        private RectTransform m_self;
        void Awake()
        {
            m_self = GetComponent<RectTransform>();
        }

        void Update()
        {
            if (target == null)
                return;

            if (worldPos)
                m_self.position = target.position;
            else
                m_self.anchoredPosition = target.anchoredPosition;
        }
    }
}