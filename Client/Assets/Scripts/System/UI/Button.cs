using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

 namespace RedStone.UI
{
    // Button that's meant to work with mouse or touch-based devices.
    [AddComponentMenu("Project UI/Button", 30)]
    [RequireComponent(typeof(RectTransform))]
    public class Button : UnityEngine.UI.Button
    {

        private UnityEvent m_onDown = new UnityEvent();
        private UnityEvent m_OnUp = new UnityEvent();
        private UnityEvent m_OnExit = new UnityEvent();
        private UnityEvent m_onEnter = new UnityEvent();
        bool isStarted = false;
        [SerializeField]
        private EClickType m_clickType = EClickType.HandleByParent;

        [SerializeField]
        private string m_showModuleName;

        public string showModuleName
        {
            get
            {
                return m_showModuleName;
            }
        }
        [SerializeField]
        private string m_Event = "";
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

        public EClickType clickType
        {
            get
            {
                return m_clickType;
            }
            set
            {
                m_clickType = value;
            }
        }
        [SerializeField]
        private string m_spriteName = null;
        [SerializeField]
        private string m_spriteNameNormal = null;
        [SerializeField]
        private string m_spriteNamePressed = null;
        [SerializeField]
        private string m_spriteNameDisabled = null;
        [SerializeField]
        private string m_spriteGUIDNormal = null;
        [SerializeField]
        private string m_spriteGUIDPressed = null;
        [SerializeField]
        private string m_spriteGUIDDisabled = null;
        [SerializeField]
        public bool autoSetNativeSize = false;

        private float m_holdTime = 0f;
        private float m_holdInterval = 2f;
        private bool m_enableHold = false;
        private bool m_isButtonDown = false;
        private bool m_isHoldTriggered = false;

        private Action onHold;

        private ClickEventHandler eventHandler = new ClickEventHandler();
        public UnityEvent onDown
        {
            get { return m_onDown; }
            set { m_onDown = value; }
        }

        public int btnSelectionState
        {
            get
            {
                return (int)currentSelectionState;
            }
        }

        public UnityEvent onUp
        {
            get { return m_OnUp; }
            set { m_OnUp = value; }
        }

        public UnityEvent onExit
        {
            get { return m_OnExit; }
            set { m_OnExit = value; }
        }

        public UnityEvent onEnter
        {
            get { return m_onEnter; }
            set { m_onEnter = value; }
        }

        public object sendEvent
        {
            get;
            set;
        }
        protected override void Start()
        {
            base.Start();
            var state = spriteState;

            if (state.highlightedSprite != null)
            {
                m_spriteName = state.highlightedSprite.name;
                m_spriteNameNormal = state.highlightedSprite.name;
                m_spriteNameDisabled = (state.disabledSprite == null ? m_spriteNameNormal : state.disabledSprite.name);
                m_spriteNamePressed = (state.pressedSprite == null ? m_spriteNameNormal : state.pressedSprite.name);
            }
            onClick.AddListener(delegate () { OnEvent("OnClick"); });
            onDown.AddListener(delegate () { OnEvent("OnDown"); });
            onUp.AddListener(delegate () { OnEvent("OnUp"); });
            isStarted = true;
            OnEnable();
        }

        protected override void OnDestroy()
        {
            onClick.RemoveAllListeners();
            onDown.RemoveAllListeners();
            onUp.RemoveAllListeners();
            base.OnDestroy();
        }
        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            onExit.Invoke();
        }
        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            onEnter.Invoke();
        }
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            if (eventData.button != PointerEventData.InputButton.Left)
                return;
            onDown.Invoke();
            if (m_enableHold)
            {
                m_isButtonDown = true;
            }
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            if (eventData.button != PointerEventData.InputButton.Left)
                return;
            onUp.Invoke();
            m_isButtonDown = false;
        }

        private void OnEvent(string prefix)
        {
            if (m_enableHold && m_isHoldTriggered)
            {
                return;
            }
            eventHandler.OnEvent(this, prefix, m_clickType, m_Event, m_showModuleName, sendEvent);
        }

        public void SetSprite(string normal, string pressed, string disabled)
        {

        }
    }
}
