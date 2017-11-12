using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

 namespace RedStone.UI
{
    [AddComponentMenu("Project UI/Scroll Rect", 37)]
    [SelectionBase]
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RectTransform))]
    public class ScrollRect : UnityEngine.UI.ScrollRect, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        [SerializeField]
        private string m_Event;
        public string Event
        {
            get
            {
                return m_Event;
            }
            set
            {
                m_Event = value;
            }
        }
        public UnityEvent onDown = new UnityEvent();
        public UnityEvent onUp = new UnityEvent();
        public UnityEvent onClick = new UnityEvent();
        public ScrollRectEvent onDrag = new ScrollRectEvent();

        protected override void Awake()
        {
            base.Awake();
            onClick.AddListener(() => { OnEvent("OnClick"); });
            onDown.AddListener(() => { OnEvent("OnDown"); });
            onUp.AddListener(() => { OnEvent("OnUp"); });
            onDrag.AddListener((a) => { OnEvent("OnDrag", a); });

        }

        protected override void OnDestroy()
        {
            onClick.RemoveAllListeners();
            onDown.RemoveAllListeners();
            onUp.RemoveAllListeners();
            base.OnDestroy();
        }

        private void OnEvent(string prefix, Vector2 data)
        {
            if (string.IsNullOrEmpty(m_Event))
                return;
            this.DispatchEvent(prefix + m_Event, data);
        }

        private void OnEvent(string prefix)
        {
            if (string.IsNullOrEmpty(m_Event))
                return;
            this.DispatchEvent(prefix + m_Event);
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            onClick.Invoke();
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            onDown.Invoke();
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            onUp.Invoke();
        }

        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
            onDrag.Invoke(eventData.delta);
        }
    }

}
