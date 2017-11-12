using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(ScrollRect))]
public class PageScroll : MonoBehaviour, IEndDragHandler, IBeginDragHandler
{
	public ListLayoutGroup listLayoutGroup;
	public GridLayoutGroup gridLayoutGroup;
	public PageIconGroup pageIconGroup;

	private ScrollRect scrollRect;
	public float scrollSpeed = 1f;
	public float pageSensitive = 0.2f;
	private bool m_scrolling = false;
	private float m_deltaDistance = 0;
	private float m_targetPosition = 0;
	private float m_dragDirection;
	private float m_startDragPosition;
	private int m_lastPage = 0;
	// Use this for initialization
	void OnEnable()
	{
		scrollRect = GetComponent<ScrollRect> ();
		if (listLayoutGroup == null && gridLayoutGroup == null) 
		{
			listLayoutGroup = GetComponentInChildren<ListLayoutGroup> ();
		}
		if (listLayoutGroup != null)
			listLayoutGroup.onDragHandler = OnDrag;
		else if (gridLayoutGroup == null)
		{
			gridLayoutGroup = GetComponentInChildren<GridLayoutGroup> ();
		}
		if (listLayoutGroup == null && gridLayoutGroup != null) 
		{
			scrollRect.onValueChanged.RemoveListener (OnDrag);
			scrollRect.onValueChanged.AddListener (OnDrag);
		}
		UpdatePageIcon ();
	}

	public void OnDrag(Vector2 vec)
	{
		if (!m_scrolling) 
		{
			UpdatePageIcon ();
		}
		m_dragDirection = normalizedPosition - m_startDragPosition;
	}

	public int GetPageCount()
	{
		return listLayoutGroup != null ? listLayoutGroup.GetMaxListCount () : (gridLayoutGroup.transform.childCount / gridLayoutGroup.constraintCount);
	}
	public void UpdatePageIcon()
	{
		if(pageIconGroup != null)
		{
			int pageCount = GetPageCount();
			int curPage = GetCurrentPage ();
			pageIconGroup.SetPageCount (Mathf.Clamp(curPage, 0, pageCount - 1), pageCount);
			pageIconGroup.onPageChanged = OnPageChanged;
		}
	}
	public void OnBeginDrag(PointerEventData listener)
	{
		m_scrolling = false;
		m_dragDirection = 0f;
		m_startDragPosition = normalizedPosition;
		m_lastPage = GetCurrentPage ();
	}
	public void OnEndDrag(PointerEventData listener)
	{
		var pos = GetTargetPosition (listener.delta);
		if (normalizedPosition != pos) 
		{
			m_deltaDistance = pos - normalizedPosition;
			m_deltaDistance *= scrollSpeed;
			m_scrolling = true;
			m_targetPosition = pos;
		}
	}
	private float scrollTotalSize
	{
		get{
			if (listLayoutGroup == null)
				return scrollRect.vertical ? gridLayoutGroup.GetComponent<RectTransform> ().sizeDelta.y : gridLayoutGroup.GetComponent<RectTransform> ().sizeDelta.x;
			return scrollRect.vertical ? listLayoutGroup.SizeDelta.y : listLayoutGroup.SizeDelta.x;
		}
	}

	private float normalizedPosition
	{
		get{
			return scrollRect.vertical ? scrollRect.verticalNormalizedPosition : scrollRect.horizontalNormalizedPosition;
		}
		set{
			if (scrollRect.vertical)
				scrollRect.verticalNormalizedPosition = value;
			else
				scrollRect.horizontalNormalizedPosition = value;
		}
	}

	private float itemSize
	{
		get {
			if (listLayoutGroup == null)
				return scrollRect.vertical ? (gridLayoutGroup.cellSize.y + gridLayoutGroup.spacing.y) : (gridLayoutGroup.cellSize.x + gridLayoutGroup.spacing.x);
			return scrollRect.vertical ? (listLayoutGroup.cellSize.y + listLayoutGroup.spacing.y) : (listLayoutGroup.cellSize.x + listLayoutGroup.spacing.x);
		}
	}

	private float cellSpacing
	{
		get {
			if (listLayoutGroup == null)
				return scrollRect.vertical ? (gridLayoutGroup.spacing.y) : (gridLayoutGroup.spacing.x);
			return scrollRect.vertical ? (listLayoutGroup.spacing.y) : (listLayoutGroup.spacing.x);
		}
	}
	private float padding
	{
		get {
			if (listLayoutGroup == null)
				return scrollRect.vertical ? (gridLayoutGroup.padding.top) : (gridLayoutGroup.padding.left);
			return listLayoutGroup.IsVertical ? (listLayoutGroup.padding.top) : (listLayoutGroup.padding.left);
		}
	}
	public float GetTargetPosition(Vector2 dragDelta)
	{
		float size = scrollTotalSize;
		float pageSize = itemSize / size;
		int pageIndex = m_lastPage;
		if (m_dragDirection > pageSize * pageSensitive)
			++pageIndex;
		else if (m_dragDirection < -pageSize * pageSensitive)
			--pageIndex;
		var pos = GetPagePosition (pageIndex);
		return Mathf.Clamp (pos / size, 0f, 1f);
	}
	int GetCurrentPage()
	{
		return GetPositionPage (normalizedPosition * scrollTotalSize);
	}
	float GetPagePosition(int pageIndex)
	{
		return pageIndex * itemSize - cellSpacing * 0.5f + padding ;
	}

	void OnPageChanged(int index)
	{
		var pagePosition = GetPagePosition (index);
		normalizedPosition =  Mathf.Clamp (pagePosition / scrollTotalSize, 0f, 1f);
	}
	int GetPositionPage(float pos)
	{
		return Mathf.RoundToInt((pos - cellSpacing * 0.5f + padding) / itemSize);
	}
	void Update()
	{
		if (m_scrolling) 
		{
			normalizedPosition += m_deltaDistance * UnityEngine.Time.deltaTime;
			if ((m_targetPosition - normalizedPosition) * m_deltaDistance < 0) 
			{
				normalizedPosition = m_targetPosition;
				m_scrolling = false;
				UpdatePageIcon ();
			}

		}
	}
}

