using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
 namespace RedStone
{
    public class UITouchListener : UUIEventListener
    {
        public delegate void VoidDelegate(UUIEventListener listener);
        public delegate void MultiFingerDelegate(float distance, int fingerCount);
        public delegate void DistanceDelegate(float distance);
        public delegate void SingleFingleDelegate(Vector2 distance);


        public SingleFingleDelegate onFingerDrag;
        public DistanceDelegate onFingerScroll;
        public DistanceDelegate onAllFingersUp;



        private Dictionary<int, Vector2> m_panelTouchPosDict = new Dictionary<int, Vector2>();
        private TRect m_rawRect = new TRect();
        private int[] m_rawScaleFinger = new int[2];

        public void Awake()
        {
            onDrag += OnDragHandlerScale;
            onUp += OnUpHandler;
            onDown += OnDownHandler;
            onScroll += OnScrollHandler;
        }

        void OnDestroy()
        {
            onDrag -= OnDragHandlerScale;
            onUp -= OnUpHandler;
            onDown -= OnDownHandler;
            onScroll -= OnScrollHandler;
        }

        private void OnScrollHandler(UUIEventListener listener)
        {
            if (onFingerScroll != null)
            {
                float scale = 1 + listener.pointerEventData.scrollDelta.y * 0.1f;
                onFingerScroll.Invoke(scale);
            }
        }

        private void OnDragHandlerScale(UUIEventListener listener)
        {
            int id = listener.pointerEventData.pointerId;
            Vector2 delta = listener.pointerEventData.delta;
            Vector2 pos = listener.pointerEventData.position;
            Vector2 lastPos = pos - delta;
            m_panelTouchPosDict[id] = pos;

            if (m_panelTouchPosDict.Count < 2 || (m_rawScaleFinger[0] != id && m_rawScaleFinger[1] != id))
            {
                if (onFingerDrag != null)
                    onFingerDrag.Invoke(delta);
                return;
            }

            Vector2 sndPos = Vector2.zero;
            if (m_rawScaleFinger[0] == id)
                sndPos = m_panelTouchPosDict[m_rawScaleFinger[1]];
            else
                sndPos = m_panelTouchPosDict[m_rawScaleFinger[0]];

            TRect lastRect = UIHelper.GetRect(lastPos, sndPos);
            TRect curRect = UIHelper.GetRect(pos, sndPos);

            float lastScale = GetRectScale(ref lastRect, ref m_rawRect);
            float curScale = GetRectScale(ref curRect, ref m_rawRect);

            if (onFingerScroll != null)
                onFingerScroll.Invoke(curScale / lastScale);
        }

        internal float GetRectScale(ref TRect rect, ref TRect rawRect)
        {
            if (rect.width / rect.height > rawRect.width / rawRect.height)
                return rect.width / rawRect.width;
            else
                return rect.height / rawRect.height;
        }


        /// <summary>
        /// 暂时废弃，不使用这种距离的方式
        /// </summary>
        /// <param name="listener"></param>
        private void OnDragHandler(UUIEventListener listener)
        {
            Vector2 delta = listener.pointerEventData.delta;
            Vector2 pos = listener.pointerEventData.position;
            Vector2 lastPos = pos - delta;
            int id = listener.pointerEventData.pointerId;
            m_panelTouchPosDict[id] = pos;
            if (m_panelTouchPosDict.Count >= 2) // scroll
            {
                float dragToOtherDist = 0;
                Vector2 otherPos = Vector2.zero;
                int otherCount = 0;
                var itr = m_panelTouchPosDict.GetEnumerator();
                while (itr.MoveNext())
                {
                    if (itr.Current.Key != id)
                    {
                        otherPos += itr.Current.Value;
                        otherCount++;
                    }
                }
                otherPos /= otherCount;
                dragToOtherDist = Vector2.Distance(pos, otherPos) - Vector2.Distance(lastPos, otherPos);
                if (onFingerScroll != null)
                    onFingerScroll.Invoke(dragToOtherDist);
            }
            else
            {
                onFingerDrag.Invoke(delta);
            }
        }

        private void OnDownHandler(UUIEventListener listener)
        {
            int touchID = listener.pointerEventData.pointerId;
            Vector3 pos = listener.pointerEventData.position;
            m_panelTouchPosDict[touchID] = pos;

            if (m_panelTouchPosDict.Count == 2)
            {
                Vector2 from = new Vector2();
                Vector2 to = new Vector2();
                int index = 0;
                foreach (var touch in m_panelTouchPosDict)
                {
                    m_rawScaleFinger[index] = touch.Key;
                    if (index++ == 0)
                        from = touch.Value;
                    else
                        to = touch.Value;
                }
                m_rawRect = UIHelper.GetRect(from, to);
            }
        }

        private void OnUpHandler(UUIEventListener listener)
        {
            int touchID = listener.pointerEventData.pointerId;
            m_panelTouchPosDict.Remove(touchID);
            if (m_panelTouchPosDict.Count > 0)
                return;

            float dragDist = Vector2.Distance(listener.pointerEventData.pressPosition, listener.pointerEventData.position);
            if (onAllFingersUp != null)
            {
                onAllFingersUp.Invoke(dragDist);
            }
        }
    }
}