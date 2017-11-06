using UnityEngine;
namespace Hotfire.UI
{
#if UNITY_EDITOR
    [ExecuteInEditMode]
#endif
    [RequireComponent(typeof(RectTransform))]
    public class AutoResize : MonoBehaviour
    {
        public enum StretchType
        {
            Horizontal,
            Vertical,
            Both,
        }
        public bool isUseMaxWidthText;
        public bool isSetPos = true;
        public Text referText;
        public GetMaxWidthText maxWidthText;
        public StretchType stretchType;
        public Vector2 sizeOffset = Vector2.zero;
        public Vector2 posOffset = Vector2.zero;
        public float minWidth = 0;

        public Text targetText
        {
            get
            {
                if (isUseMaxWidthText)
                {
                    if (maxWidthText == null) { return null; }
                    else { maxWidthText.FindTexts(); return maxWidthText.maxWidthText; }
                }
                else
                    return referText;
            }
        }

        private RectTransform m_rectTrans;
        private Vector2 m_rawPos;
        private Vector2 m_rawSize;
        void Awake()
        {
            m_rectTrans = GetComponent<RectTransform>();

            m_rawPos = Vector2.zero;
            m_rawSize = m_rectTrans.sizeDelta;
            m_rectTrans.pivot = new Vector2(0.5f, 0.5f);
        }

        private float targetWidth { get { return Mathf.Max(minWidth, targetText.preferredWidth); } }
        private float targetHeight { get { return targetText.preferredHeight; } }

        void Update()
        {
            if (targetText == null)
                return;

            Vector2 size = m_rectTrans.sizeDelta;

            switch (stretchType)
            {
                case StretchType.Both:
                    size.x = targetWidth;
                    size.y = targetHeight;
                    size += sizeOffset;
                    break;
                case StretchType.Horizontal:
                    size.x = targetWidth + sizeOffset.x;
                    //size.y = m_rawSize.y;
                    break;
                case StretchType.Vertical:
                    //size.x = m_rawSize.x;
                    size.y = targetHeight + sizeOffset.y;
                    break;
            }
            m_rectTrans.sizeDelta = size;

            if (!isSetPos)
                return;

            Vector2 pos = Vector2.zero;
            pos.x = targetText.rectTransform.localPosition.x;
            pos.y = m_rawPos.y;
            pos += posOffset;

            if (targetText.alignment == TextAnchor.LowerLeft || targetText.alignment == TextAnchor.MiddleLeft || targetText.alignment == TextAnchor.UpperLeft)
                pos.x += targetWidth * 0.5f + (1 - targetText.rectTransform.pivot.x) * targetText.rectTransform.sizeDelta.x;
            if (targetText.alignment == TextAnchor.LowerRight || targetText.alignment == TextAnchor.MiddleRight || targetText.alignment == TextAnchor.UpperRight)
                pos.x -= targetWidth * 0.5f - +(1 - targetText.rectTransform.pivot.x) * targetText.rectTransform.sizeDelta.x;

            if (targetText.alignment == TextAnchor.LowerCenter || targetText.alignment == TextAnchor.LowerLeft || targetText.alignment == TextAnchor.LowerRight)
                pos.y -=  targetText.rectTransform.pivot.y * targetText.rectTransform.sizeDelta.y - targetHeight * 0.5f;
            if (targetText.alignment == TextAnchor.UpperCenter || targetText.alignment == TextAnchor.UpperLeft || targetText.alignment == TextAnchor.UpperRight)
                pos.y += (1-targetText.rectTransform.pivot.y)* targetText.rectTransform.sizeDelta.y - targetHeight * 0.5f;
            if (targetText.alignment == TextAnchor.MiddleCenter || targetText.alignment == TextAnchor.MiddleLeft || targetText.alignment == TextAnchor.MiddleRight)
                pos.y += (0.5f - targetText.rectTransform.pivot.y) * targetText.rectTransform.sizeDelta.y;
            m_rectTrans.localPosition = pos;
        }
    }
}