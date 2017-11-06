using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
namespace RedStone
{
    public class UIEventListener : MonoBehaviour,
    IPointerClickHandler,
    IPointerDownHandler,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerUpHandler,
    ISelectHandler,
    IUpdateSelectedHandler,
    IDeselectHandler,
    IDragHandler,
    IEndDragHandler,
    IDropHandler,
    IScrollHandler,
    IMoveHandler
    {
        public delegate void VoidDelegate(UIEventListener listener);
        public VoidDelegate onClick;
        public VoidDelegate onDown;
        public VoidDelegate onEnter;
        public VoidDelegate onExit;
        public VoidDelegate onUp;
        public VoidDelegate onSelect;
        public VoidDelegate onUpdateSelect;
        public VoidDelegate onDeSelect;
        public VoidDelegate onDrag;
        public VoidDelegate onDragEnd;
        public VoidDelegate onDrop;
        public VoidDelegate onScroll;
        public VoidDelegate onMove;

        //自定义的数据
        public object parameter;
        //底层系统返回的事件数据，根据不同事件，具体类型不同，参见下面事件回调处即可
        private BaseEventData m_eventData;
        public BaseEventData eventData
        {
            get { return m_eventData; }
        }

        public PointerEventData pointerEventData
        {
            get { return m_eventData is PointerEventData ? m_eventData as PointerEventData : null; }
        }

        public void OnPointerClick(PointerEventData eventData) { if (onClick != null) { m_eventData = eventData; onClick(this); } }
        public void OnPointerDown(PointerEventData eventData) { if (onDown != null) { m_eventData = eventData; onDown(this); } }
        public void OnPointerEnter(PointerEventData eventData) { if (onEnter != null) { m_eventData = eventData; onEnter(this); } }
        public void OnPointerExit(PointerEventData eventData) { if (onExit != null) { m_eventData = eventData; onExit(this); } }
        public void OnPointerUp(PointerEventData eventData) { if (onUp != null) { m_eventData = eventData; onUp(this); } }
        public void OnSelect(BaseEventData eventData) { if (onSelect != null) { m_eventData = eventData; onSelect(this); } }
        public void OnUpdateSelected(BaseEventData eventData) { if (onUpdateSelect != null) { m_eventData = eventData; onUpdateSelect(this); } }
        public void OnDeselect(BaseEventData eventData) { if (onDeSelect != null) { m_eventData = eventData; onDeSelect(this); } }
        public void OnDrag(PointerEventData eventData) { if (onDrag != null) { m_eventData = eventData; onDrag(this); } }
        public void OnEndDrag(PointerEventData eventData) { if (onDragEnd != null) { m_eventData = eventData; onDragEnd(this); } }
        public void OnDrop(PointerEventData eventData) { if (onDrop != null) { m_eventData = eventData; onDrop(this); } }
        public void OnScroll(PointerEventData eventData) { if (onScroll != null) { m_eventData = eventData; onScroll(this); } }
        public void OnMove(AxisEventData eventData) { if (onMove != null) { m_eventData = eventData; onMove(this); } }

        public void Awake()
        {
            
        }

        static public UIEventListener Get(GameObject go)
        {
            UIEventListener listener = go.GetComponent<UIEventListener>();
            if (listener == null) listener = go.AddComponent<UIEventListener>();
            return listener;
        }
    }
}