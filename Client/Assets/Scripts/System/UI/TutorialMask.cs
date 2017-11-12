using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TutorialMask : MonoBehaviour, IInitializePotentialDragHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IScrollHandler
{


    private Graphic graphic;

    public Camera cam;

    public bool changeMaterial = true;

    public bool enableClick;
    public bool enableDrag;
    public bool followOnUpdate;
    private Vector3[] corners = new Vector3[4];
    private Vector2 lMin = Vector2.zero;
    private Vector2 lMax = Vector2.zero;

    private RectTransform m_target = null;
    void Awake()
    {
        graphic = GetComponent<Graphic>();
        if (graphic != null)
        {
            graphic.raycastTarget = true;
            graphic.rectTransform.GetLocalCorners(corners);
        }
    }
    void OnDisable()
    {
        m_target = null;
    }
    private void SetEnabled(bool enabled)
    {
        if (graphic != null)
            graphic.enabled = enabled;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (enableClick)
        {
            PassEvent(eventData, ExecuteEvents.submitHandler);
            PassEvent(eventData, ExecuteEvents.pointerClickHandler);
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (enableClick)
            PassEvent(eventData, ExecuteEvents.pointerDownHandler);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (enableClick)
            PassEvent(eventData, ExecuteEvents.pointerUpHandler);
    }
    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        if (enableDrag)
            PassEvent(eventData, ExecuteEvents.initializePotentialDrag);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (enableDrag)
            PassEvent(eventData, ExecuteEvents.beginDragHandler);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (enableDrag)
            PassEvent(eventData, ExecuteEvents.endDragHandler);
    }
    public void OnScroll(PointerEventData eventData)
    {
        if (enableDrag)
            PassEvent(eventData, ExecuteEvents.scrollHandler);
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (enableDrag)
            PassEvent(eventData, ExecuteEvents.dragHandler);
    }

    void Update()
    {
        if (followOnUpdate && m_target != null)
            SetTarget(m_target);

    }
    public void SetTarget(RectTransform target)
    {
        if (graphic == null || target == graphic.rectTransform)
            return;
        if (target == null)
            target = graphic.rectTransform;
        if (followOnUpdate)
            m_target = target;
        else
            m_target = null;
        target.GetWorldCorners(corners);

        Vector3 min = corners[0];
        Vector3 max = corners[0];
        for (int i = 1; i < corners.Length; ++i)
        {
            min = Vector3.Min(min, corners[i]);
            max = Vector3.Max(max, corners[i]);
        }
        min = cam.WorldToScreenPoint(min);
        max = cam.WorldToScreenPoint(max);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(graphic.rectTransform, min, cam, out lMin);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(graphic.rectTransform, max, cam, out lMax);
        if (changeMaterial && graphic.material != null)
        {
            graphic.material.SetVector("_Min", lMin);
            graphic.material.SetVector("_Max", lMax);
        }
    }

    public void PassEvent<T>(PointerEventData data, ExecuteEvents.EventFunction<T> function)
        where T : IEventSystemHandler
    {
        var mouse = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(graphic.rectTransform, data.position, cam, out mouse);
        if (!Rect.MinMaxRect(lMin.x, lMin.y, lMax.x, lMax.y).Contains(mouse))
            return;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(data, results);
        GameObject current = data.pointerCurrentRaycast.gameObject;
        for (int i = 0; i < results.Count; i++)
        {
            if (current != results[i].gameObject)
            {
                ExecuteEvents.Execute(results[i].gameObject, data, function);
            }
        }
    }
}