using UnityEngine;
using System.Collections;

using uTools;

 namespace RedStone.UI
{
    [TweenEffect]
    [RequireComponent(typeof(RectTransform))]
    public class EdgeRun : MonoBehaviour
    {
        public float duration = 2f;
        public EaseType method = EaseType.linear;
        public bool clockwise = false;
        public RectTransform aroundEdgeTransform;
        public Image autoFillImageHead;
        public Image autoFillImageTail;
        public EdgeRun tweenEdgeRun;
        public float tweenEdgeRunOffset = 0.5f;
        public bool cornerHeadFill = false;
        public float cornerHeadFillPercent = 1;
        public float cornerTailFillPercent = -1;

        private RectTransform m_rectTrans;
        private uTweenFloat m_tween;
        public float tweenValue { get { return m_tween.value; } }


        void Awake()
        {
            m_rectTrans = GetComponent<RectTransform>();
            m_tween = uTweenFloat.Begin(gameObject, 0, 1, duration, 0);
            m_tween.style = uTweener.Style.Loop;
            if (aroundEdgeTransform == null)
                aroundEdgeTransform = transform.parent.GetComponent<RectTransform>();
        }

        void OnEnable()
        {
            m_tween.ResetToBeginning();
        }

        void Update()
        {
            float value = 0;

            if (tweenEdgeRun != null && tweenEdgeRun != this)
            {
                value = (tweenEdgeRun.tweenValue + tweenEdgeRunOffset + 1) % 1f;
            }
            else
            {
                m_tween.duration = duration;
                m_tween.method = method;
                value = m_tween.value;
            }



            if (autoFillImageHead != null && autoFillImageTail != null)
            {
                float offset = autoFillImageHead.rectTransform.sizeDelta.y * 0.5f;
                Vector2 pos;
                float rot;
                float fill;
                GetPosAndRot(aroundEdgeTransform.sizeDelta, value, offset, false, clockwise, out pos, out rot, out fill);
                autoFillImageHead.rectTransform.anchoredPosition = pos;
                autoFillImageHead.rectTransform.localRotation = Quaternion.AngleAxis(rot, Vector3.forward);
                autoFillImageHead.type = UnityEngine.UI.Image.Type.Filled;
                autoFillImageHead.fillMethod = UnityEngine.UI.Image.FillMethod.Vertical;
                autoFillImageHead.fillOrigin = 0;
                autoFillImageHead.fillAmount = fill;


                if (fill < 1)
                {
                    float fillOffset = !cornerHeadFill ? 0 : autoFillImageHead.rectTransform.sizeDelta.x / autoFillImageHead.rectTransform.sizeDelta.y * 0.5f;
                    autoFillImageHead.fillAmount = fill + cornerHeadFillPercent * fillOffset;


                    autoFillImageTail.gameObject.SetActive(true);
                    offset = -autoFillImageTail.rectTransform.sizeDelta.y * 0.5f;
                    GetPosAndRot(aroundEdgeTransform.sizeDelta, value, offset, true, clockwise, out pos, out rot, out fill);
                    autoFillImageTail.rectTransform.anchoredPosition = pos;
                    autoFillImageTail.rectTransform.localRotation = Quaternion.AngleAxis(rot, Vector3.forward);
                    autoFillImageTail.type = UnityEngine.UI.Image.Type.Filled;
                    autoFillImageTail.fillMethod = UnityEngine.UI.Image.FillMethod.Vertical;
                    autoFillImageTail.fillOrigin = 1;
                    autoFillImageTail.fillAmount = fill + cornerTailFillPercent * fillOffset;
                }
                else
                {
                    autoFillImageTail.gameObject.SetActive(false);
                }
            }
            else
            {
                Vector2 pos;
                float rot;
                float fill;
                GetPosAndRot(aroundEdgeTransform.sizeDelta, value, 0, false, clockwise, out pos, out rot, out fill);
                m_rectTrans.anchoredPosition = pos;
                m_rectTrans.localRotation = Quaternion.AngleAxis(rot, Vector3.forward);
            }
        }


        private void GetPosAndRot(Vector2 size, float lerpValue, float offset, bool isTail, bool clockwise, out Vector2 outPos, out float outRot, out float fill)
        {
            float w = clockwise ? size.y : size.x;
            float h = clockwise ? size.x : size.y;
            float s = w + h + w + h;
            float v = lerpValue + offset / s;
            v = (v + 1) % 1f;

            fill = 0;
            float rot = 0;
            float x = 0;
            float y = 0;
            if (v < w / s)
            {
                x = v * s - offset;
                y = 0;
                rot = 90;
                if (isTail)
                    fill = (w - x - offset) / offset * 0.5f;
                else
                    fill = (x + offset) / offset * 0.5f;
            }
            else if (v < 0.5f)
            {
                x = w;
                y = v * s - w - offset;
                rot = 180;
                if (isTail)
                    fill = (h - y - offset) / offset * 0.5f;
                else
                    fill = (y + offset) / offset * 0.5f;
            }
            else if (v < 0.5f + w / s)
            {
                x = w + w + h - v * s + offset;
                y = h;
                rot = 270;
                if (isTail)
                    fill = (x - offset) / offset * 0.5f;
                else
                    fill = (w - x + offset) / offset * 0.5f;
            }
            else if (v < 1f)
            {
                x = 0;
                y = s - v * s + offset;
                rot = 0;
                if (isTail)
                    fill = (y - offset) / offset * 0.5f;
                else
                    fill = (h - y + offset) / offset * 0.5f;
            }

            x -= w * 0.5f;
            y -= h * 0.5f;
            fill = Mathf.Clamp01(Mathf.Abs(fill));

            if (clockwise)
            {
                rot = 270 - rot;
                float tmp = x;
                x = y;
                y = tmp;
            }

            outPos = new Vector2(x, y);
            outRot = rot;
        }
    }

}