using UnityEngine;
using System.Collections;
 namespace RedStone.UI
{
    [TweenEffect]
    [ExecuteInEditMode]
    public class UITopFillEffect : MonoBehaviour
    {
        public enum FillMethod
        {
            Horizontal,
            Vertical,
        }
        public Image fillImage;
        public SimpleSlider fillSlider;
        public Image topIcon;
        public float showTopDuration = 0.5f;
        public float hideDuration = 0.1f;
        public Vector2 posOffset;
        public float showStep = 0.01f;
        public FillMethod fillMethod = FillMethod.Horizontal;
        public bool keepShow = false;
        public bool useEdgeOffset = false;
        public float edgeOffset = 0;

        void Awake()
        {
            if (topIcon != null)
                UIHelper.HideTransform(topIcon.transform);
        }

        private float m_lastFill = 0;
        private float m_showTopCd = 0;
        private float m_hideCd = 0;

        private float fillAmount
        {
            get
            {
                if (fillSlider != null)
                    return fillSlider.value;
                else if (fillImage != null)
                    return fillImage.fillAmount;
                return 0;
            }
        }

        private Vector2 size
        {
            get
            {
                if (fillSlider != null)
                    return fillSlider.rectTransform.sizeDelta;
                else if (fillImage != null)
                    return fillImage.rectTransform.sizeDelta;
                return Vector2.zero;

            }
        }

        private bool canUse { get { return (fillSlider != null || fillImage != null) && topIcon != null; } }

        void Update()
        {
            if (!canUse)
                return;

            if (keepShow)
            {
                m_showTopCd = 1;
            }

            // Check Fill Changed
            if (m_lastFill < fillAmount - showStep)
            {
                m_showTopCd = showTopDuration;
                m_hideCd = hideDuration;
                m_lastFill = fillAmount;
            }
            else if (m_lastFill > fillAmount)
            {
                m_lastFill = fillAmount;
            }

            m_showTopCd = Mathf.Max(0, m_showTopCd - Time.deltaTime);

            if (m_showTopCd > 0)
            {
                UIHelper.ShowTransform(topIcon.transform);
                topIcon.SetAlpha(1);

                Vector2 pos = Vector2.zero;
                float fixedFill = fillAmount;
                if (fillSlider != null && fixedFill < fillSlider.minValue)
                    fixedFill = fillSlider.minValue;

                if (fillMethod == FillMethod.Horizontal)
                {
                    float posFill = (fixedFill - 0.5f) * size.x;
                    pos = Vector2.right * posFill;
                    if (useEdgeOffset)
                    {
                        pos += Vector2.right * fixedFill * edgeOffset;
                    }
                }
                else if (fillMethod == FillMethod.Vertical)
                {
                    float posFill = (fixedFill - 0.5f) * size.y;
                    pos = Vector2.up * posFill;
                    if (useEdgeOffset)
                    {
                        pos += Vector2.up * fixedFill * edgeOffset;
                    }
                }

                topIcon.transform.localPosition = pos + posOffset;
            }
            else if (m_hideCd > 0)
            {
                m_hideCd = Mathf.Max(0, m_hideCd - Time.deltaTime);
                float lerp = m_hideCd / hideDuration;
                topIcon.SetAlpha(lerp);
                UIHelper.ShowTransform(topIcon.transform);
            }
            else
            {
                UIHelper.HideTransform(topIcon.transform);
            }
        }
    }
}