using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Project UI/List View")]
[RequireComponent(typeof(RectTransform))]
public class ListLayoutGroup : LayoutGroup
{
	public enum Corner
	{
		UpperLeft = 0,
		UpperRight = 1,
		LowerLeft = 2,
		LowerRight = 3

	}

	public enum Axis
	{
		Horizontal = 0,
		Vertical = 1

	}

	public enum Constraint
	{
		FixedColumnCount = 0,
		FixedRowCount = 1

	}

	[SerializeField] protected Corner m_StartCorner = Corner.UpperLeft;

	public Corner startCorner { get { return m_StartCorner; } set { SetProperty (ref m_StartCorner, value); } }


	[SerializeField] protected Vector2 m_CellSize = new Vector2 (100, 100);

	public Vector2 cellSize { get { return m_CellSize; } set { SetProperty (ref m_CellSize, value); } }

	[SerializeField] protected Vector2 m_Spacing = Vector2.zero;

	public Vector2 spacing { get { return m_Spacing; } set { SetProperty (ref m_Spacing, value); } }


	[SerializeField] protected int m_ConstraintCount = 2;

	[SerializeField] protected RectTransform m_positionTransform;


	[SerializeField] protected bool adjustCellIndex = false;

	[SerializeField] protected bool m_autoSetConstraint = true;

	public RectTransform positionRectTransform
	{
		get {
			if (m_positionTransform == null)
				return rectTransform;
			return m_positionTransform;
		}
        set
        {
            m_positionTransform = value;
        }
	}

	public int constraintCount { get { return m_ConstraintCount; } set { SetProperty (ref m_ConstraintCount, Mathf.Max (1, value)); } }

	private Vector2 m_centerPriorPadding = Vector2.zero;
	private Axis m_scrollDirection = Axis.Horizontal;

	protected Constraint m_Constraint = Constraint.FixedColumnCount;

	[SerializeField] Constraint m_dataConstraint = Constraint.FixedColumnCount;
	public Constraint DataConstraint {
		get {
			return m_dataConstraint;
		}
		set {
			SetProperty (ref m_dataConstraint, value);
		}
	}
	protected ScrollRect m_scrollRect;

	public bool centerPrior = false;
	public int centerPriorThreshold = 1;

	[SerializeField]
	protected bool createParentGameobject = false;

	private List<object> m_dataList = new List<object>();

	private List<object> m_cacheList = new List<object>();

	private int displayListCount = 0;

	private System.Action<int, GameObject, object> m_setContentHandler = null;

	[SerializeField]
	protected MonoBehaviour m_objectTemplate;

	private List<int> m_indexList = new List<int>();

	private Dictionary<GameObject, int> m_setContentList = new Dictionary<GameObject, int>();
	private Vector2 m_dragDirection = Vector2.zero;

	private bool m_endlessScroll = false;

	public bool HasParent
	{
		get {
			return m_positionTransform != null;
		}
	}

	public Action<Vector2> onDragHandler;
	private Type objectType = typeof(MonoBehaviour);
	public MonoBehaviour objectTemplate
	{
		set 
		{
			if (m_objectTemplate != null || value == null)
				return;
			if (value != null)
				objectType = value.GetType ();
			m_objectTemplate = value;
			var rect = m_objectTemplate.GetComponent<RectTransform> ();
			rect.anchorMin = Vector2.up;
			rect.anchorMax = Vector2.up;
			rect.sizeDelta = cellSize;
		}
		get {
			return m_objectTemplate;
		}
	}


	public ScrollRect GetScrollRect()
	{
		return m_scrollRect;
	}
		
	public Vector2 SizeDelta
	{
		get {
			return rectTransform.sizeDelta;
		}
	}
	public bool IsVertical
	{
		get {
			return m_scrollDirection == Axis.Vertical;
		}
	}
	#if UNITY_EDITOR
	protected override void OnValidate ()
	{
		base.OnValidate ();
		constraintCount = constraintCount;
	}

	#endif

	private Vector2 GetScrollRectSize()
	{
		return m_scrollRect.GetComponent<RectTransform> ().rect.size;
	}

	protected override void Awake ()
	{
		base.Awake ();
		m_scrollRect = GetComponentInParent<ScrollRect> ();
        if (m_scrollRect != null)
        {
            m_scrollDirection = m_scrollRect.vertical ? Axis.Vertical : Axis.Horizontal;
            m_scrollRect.onValueChanged.AddListener(OnDrag);
        }
        
        if(m_objectTemplate != null)
        {
            var rect = m_objectTemplate.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.up;
            rect.anchorMax = Vector2.up;
            rect.sizeDelta = cellSize;
        }

    }

	private bool IsEndlessScroll()
	{
		return m_scrollRect.movementType == ScrollRect.MovementType.Unrestricted;
	}
	//private void SetContentFunc<P, D>(int index, GameObject go, object data, System.Action<int, P, D> setContentHandler)
	//	where P : MonoBehaviour
	//{

	//}

	public void SetDataForListItem<P, D>(P itemPrefab, ICollection<D> dataList)
		where P : MonoBehaviour
	{
		SetData (itemPrefab, dataList, null);
	}
    /// <summary>
    /// 如果objectTemplate是拖到资源上的，可以调用这个方法
    /// </summary>
    /// <typeparam name="D"></typeparam>
    /// <param name="dataList"></param>
    /// <param name="setContentHandler"></param>
    public void InitData<D>(ICollection<D> dataList,Action<int,MonoBehaviour,D> setContentHandler)
    {
        SetData(m_objectTemplate, dataList, setContentHandler);
    }
	public void SetData<P, D>(P itemPrefab, ICollection<D> dataList, System.Action<int, P, D> setContentHandler)
		where P : MonoBehaviour
	{
		if (itemPrefab != m_objectTemplate) 
		{
			List<GameObject> goList = new List<GameObject> ();
			for (int i = 0; i < rectTransform.childCount; ++i) 
			{
				goList.Add (rectTransform.GetChild (i).gameObject);
			}
			rectTransform.DetachChildren ();
			while (goList.Count > 0) 
			{
				var go = goList [goList.Count - 1];
				GameObject.DestroyImmediate (go);
				goList.RemoveAt(goList.Count - 1);
			}
		}
		m_objectTemplate = null;
		objectTemplate = itemPrefab;
		m_setContentHandler = null;
		if (setContentHandler == null && itemPrefab is RedStone.UI.IListItem)
			setContentHandler = (index, p, d)=>(p as RedStone.UI.IListItem).SetContent(index, d);
		m_scrollRect.normalizedPosition = Vector2.up;
		SetData<P, D> (dataList, setContentHandler);
	}
	public void SetData<P, D>(ICollection<D> dataList, System.Action<int, P, D> setContentHandler)
		where P : MonoBehaviour
	{
		objectType = typeof(P);
		m_dataList.Clear ();
		if (dataList != null && dataList.Count > 0) 
		{
			var enu = dataList.GetEnumerator ();
			while (enu.MoveNext ()) 
			{
				m_dataList.Add (enu.Current);
			}
		}

		if (setContentHandler == null)
			m_setContentHandler = null;
		else
			m_setContentHandler = (index, go, data) => {

			var dt = (D)data;
			var item = go.GetComponentInChildren(objectType);

			if (setContentHandler != null) 
			{
				setContentHandler.Invoke (index, item as P, dt);
			}
		};
		m_scrollRect.vertical = (m_scrollDirection == Axis.Vertical);
		m_scrollRect.horizontal = (m_scrollDirection == Axis.Horizontal);
		m_Constraint = m_scrollRect.vertical ? Constraint.FixedColumnCount : Constraint.FixedRowCount;
		if (!m_autoSetConstraint && m_Constraint != m_dataConstraint) 
		{
			m_cacheList.Clear ();
			m_cacheList.AddRange (m_dataList);
			int lineCount = m_cacheList.Count / constraintCount;
			for (int i = 0; i < m_cacheList.Count; ++i) 
			{
				int x = i / constraintCount;
				int y = i % constraintCount;
				m_dataList[i] = m_cacheList[y * lineCount + x];
			}	
		}
		var scrollRectSize = GetScrollRectSizeAxis ();
		var cellSize = GetCellSizeAxis (true);
		displayListCount = Mathf.CeilToInt(scrollRectSize / cellSize) + 1;

		if (centerPrior == true && displayListCount > (m_dataList.Count / constraintCount) + centerPriorThreshold)
		{
			var centerPadding = (scrollRectSize - (m_dataList.Count / constraintCount) * cellSize + GetSpacingAxis ()) * 0.5f;
			if (m_scrollRect.horizontal)
				m_centerPriorPadding = Vector2.right * centerPadding;
			else
				m_centerPriorPadding = Vector2.up * centerPadding;
		} else
		{
			m_centerPriorPadding = Vector2.zero;
		}
		for (int i = rectTransform.childCount; i < (displayListCount * constraintCount); ++i)
		{
			RectTransform parent = rectTransform;
			if (createParentGameobject)
			{
				parent = new GameObject (i.ToString ()).AddComponent<RectTransform> ();
				parent.SetParent (rectTransform, false);
			}
			var obj = GameObjectHelper.AddChild (parent, m_objectTemplate);
			if (createParentGameobject)
				obj.GetComponent<RectTransform> ().pivot = Vector2.up;
			obj.gameObject.SetActive (true);
		}
		ForceUpdateContent ();
        SetDirty();
	}
	public void ReplaceData(int index, object data)
	{
		if (0 <= index && index <= m_dataList.Count)
		{
			m_dataList [index] = data;
			ForceUpdateContent ();
		}
	}
	float GetScrollRectSizeAxis()
	{
		if (m_scrollRect.horizontal)
			return GetScrollRectSize ().x;
		else
			return GetScrollRectSize ().y;
	}
	float GetSpacingAxis()
	{
		if (m_scrollRect.horizontal)
			return spacing.x;
		else
			return spacing.y;
	}
	float GetCellSizeAxis(bool includingSpacing)
	{
		if (m_scrollRect.horizontal)
			return cellSize.x + (includingSpacing ? spacing.x : 0f);
		else
			return cellSize.y + (includingSpacing ? spacing.y : 0f);
	}
	void ForceUpdateContent ()
	{
		m_indexList.Clear ();
		OnDrag (Vector2.zero);
		UpdateItemContents ();
	}
	void UpdateItemContents()
	{
		if (m_setContentList.Count > 0)
		{
			if (m_setContentHandler != null)
			{
				foreach (var item in m_setContentList)
				{
					m_setContentHandler.Invoke (item.Value, item.Key, m_dataList[item.Value]);
				}
			}
			m_setContentList.Clear ();
		}
	}
	void Update()
	{
		UpdateItemContents ();
	}
	protected virtual void OnDrag(Vector2 vec)
	{
		m_dragDirection = vec;
		m_endlessScroll = IsEndlessScroll ();
		SetLayoutHorizontal ();
		SetLayoutVertical ();
		if (onDragHandler != null)
			onDragHandler.Invoke (vec);
	}

	public int GetMaxListCount()
	{
		if (m_dataList == null || m_dataList.Count == 0)
			return 0;
		return Mathf.CeilToInt (m_dataList.Count / (float)m_ConstraintCount - 0.001f);
	}
	protected int GetStartIndex()
	{
		if (m_dataList == null || m_dataList.Count == 0)
			return 0;
		Vector2 anchorPosition = positionRectTransform.anchoredPosition;
		if (HasParent)
		{
			var position = rectTransform.anchoredPosition;
            var parentTransform = rectTransform.transform;
            while (parentTransform != null && parentTransform != positionRectTransform.parent)
            {
                var rc = parentTransform.GetComponent<RectTransform>();
                if (rc == null)
                    break;
                position.x += rc.rect.width * (rc.anchorMin.x - 0.5f);
                position.y += rc.rect.height * (rc.anchorMin.y - 0.5f);
                parentTransform = parentTransform.parent;
            }
			anchorPosition += position;
		}
		anchorPosition.x *= -1;
		if (adjustCellIndex)
		{
			anchorPosition.x -= (m_dragDirection.x > 0 ? 1 : 2) * (cellSize.x + spacing.x) / 2;
			anchorPosition.y -= (m_dragDirection.y > 0 ? 1 : 2) * (cellSize.y + spacing.y) / 2;
		}
		int index = 0;

		anchorPosition.x -= padding.left + m_centerPriorPadding.x;
		anchorPosition.y -= padding.top + m_centerPriorPadding.y;

		switch (m_scrollRect.vertical)
		{
		case true:
			index = (int)(anchorPosition.y / (cellSize.y + spacing.y)) * constraintCount;
			break;
		case false:
			index = (int)(anchorPosition.x / (cellSize.x + spacing.x)) * constraintCount;
			break;
		}

		if (!m_endlessScroll) {
			if (index < 0)
				index = 0;
			if (index >= m_dataList.Count)
				index = 0;
		}
		return index;
	}

	public override void CalculateLayoutInputHorizontal ()
	{
		base.CalculateLayoutInputHorizontal ();

		int minColumns = 0;
		int preferredColumns = 0;
		if (m_Constraint == Constraint.FixedColumnCount) {
			minColumns = preferredColumns = m_ConstraintCount;
		} else{
			minColumns = preferredColumns = GetMaxListCount ();
		} 
		SetLayoutInputForAxis (
			padding.horizontal + (cellSize.x + spacing.x) * minColumns - spacing.x,
			padding.horizontal + (cellSize.x + spacing.x) * preferredColumns - spacing.x,
			-1, 0);
	}

	public override void CalculateLayoutInputVertical ()
	{
		int minRows = 0;
		if (m_Constraint == Constraint.FixedColumnCount) {
			minRows = GetMaxListCount ();
		} else{
			minRows = m_ConstraintCount;
		}

		float minSpace = padding.vertical + (cellSize.y + spacing.y) * minRows - spacing.y;
		SetLayoutInputForAxis (minSpace, minSpace, -1, 1);
	}

	public override void SetLayoutHorizontal ()
	{
		SetCellsAlongAxis (0);
	}

	public override void SetLayoutVertical ()
	{
		SetCellsAlongAxis (1);
	}

	//return true if index of current item changed
	private  int GetActualIndex(int startIndex, int index)
	{
		var count = rectTransform.childCount;
		return ((startIndex + (startIndex >= 0 ? (count - index - 1) : -index)) / count * count + index);
	}
	private void SetCellsAlongAxis (int axis)
	{
		// Normally a Layout Controller should only set horizontal values when invoked for the horizontal axis
		// and only vertical values when invoked for the vertical axis.
		// However, in this case we set both the horizontal and vertical position when invoked for the vertical axis.
		// Since we only set the horizontal position and not the size, it shouldn't affect children's layout,
		// and thus shouldn't break the rule that all horizontal layout must be calculated before all vertical layout.
		if (axis == 0) {
			// Only set the sizes when invoked for horizontal axis, not the positions.
			for (int i = 0; i < rectTransform.childCount; i++) {
				RectTransform rect = (RectTransform)rectTransform.GetChild(i);

				m_Tracker.Add (this, rect,
					DrivenTransformProperties.Anchors |
					DrivenTransformProperties.AnchoredPosition |
					DrivenTransformProperties.SizeDelta);

				rect.anchorMin = Vector2.up;
				rect.anchorMax = Vector2.up;
				rect.sizeDelta = cellSize;
			}
			return;
		}

		float width = rectTransform.rect.size.x;
		float height = rectTransform.rect.size.y;

		int cellCountX = 1;
		int cellCountY = 1;
		if (m_Constraint == Constraint.FixedColumnCount) {
			cellCountX = m_ConstraintCount;
			cellCountY = Mathf.CeilToInt (rectTransform.childCount / (float)cellCountX - 0.001f);
		} else if (m_Constraint == Constraint.FixedRowCount) {
			cellCountY = m_ConstraintCount;
			cellCountX = Mathf.CeilToInt (rectTransform.childCount / (float)cellCountY - 0.001f);
		} else {
			if (cellSize.x + spacing.x <= 0)
				cellCountX = int.MaxValue;
			else
				cellCountX = Mathf.Max (1, Mathf.FloorToInt ((width - padding.horizontal + spacing.x + 0.001f) / (cellSize.x + spacing.x)));

			if (cellSize.y + spacing.y <= 0)
				cellCountY = int.MaxValue;
			else
				cellCountY = Mathf.Max (1, Mathf.FloorToInt ((height - padding.vertical + spacing.y + 0.001f) / (cellSize.y + spacing.y)));
		}

		int cornerX = (int)startCorner % 2;
		int cornerY = (int)startCorner / 2;

		int cellsPerMainAxis, actualCellCountX, actualCellCountY;
		if (m_scrollDirection == Axis.Vertical) {
			cellsPerMainAxis = cellCountX;
			actualCellCountX = Mathf.Clamp (cellCountX, 1, rectTransform.childCount);
			actualCellCountY = Mathf.Clamp (cellCountY, 1, Mathf.CeilToInt (rectTransform.childCount / (float)cellsPerMainAxis));
		} else {
			cellsPerMainAxis = cellCountY;
			actualCellCountY = Mathf.Clamp (cellCountY, 1, rectTransform.childCount);
			actualCellCountX = Mathf.Clamp (cellCountX, 1, Mathf.CeilToInt (rectTransform.childCount / (float)cellsPerMainAxis));
		}

		Vector2 requiredSpace = new Vector2 (
			                         actualCellCountX * cellSize.x + (actualCellCountX - 1) * spacing.x,
			                         actualCellCountY * cellSize.y + (actualCellCountY - 1) * spacing.y
		                         );
		Vector2 startOffset = new Vector2 (
			                       GetStartOffset (0, requiredSpace.x),
			                       GetStartOffset (1, requiredSpace.y)
		) + m_centerPriorPadding;

		int startIndex = GetStartIndex ();
		bool changed = false;
		for (int i = 0 ; i < rectTransform.childCount; i++) {
			int positionX;
			int positionY;
			int actualIndex = GetActualIndex(startIndex, i - constraintCount);
			int recycleIndex = m_endlessScroll ? actualIndex % m_dataList.Count : actualIndex;
			if (actualIndex < 0)
				actualIndex -= constraintCount - 1;
			if (i >= m_indexList.Count) {
				m_indexList.Add (actualIndex);
				changed = true;
			} else
				changed = (m_indexList [i] != actualIndex);
			m_indexList [i] = actualIndex;
			while (recycleIndex < 0)
				recycleIndex += m_dataList.Count;
			var child = (RectTransform)(rectTransform.GetChild (i));
			if (changed)
			{
				if (recycleIndex >= m_dataList.Count)
				{
					child.gameObject.SetActive (false);
					continue;
				}
				child.gameObject.SetActive (true);
			}
			if (m_scrollDirection == Axis.Vertical) {
				positionX = Mathf.Abs(actualIndex) % cellsPerMainAxis;
				positionY = actualIndex / cellsPerMainAxis;
			} else {
				positionX = actualIndex / cellsPerMainAxis;
				positionY = Mathf.Abs(actualIndex) % cellsPerMainAxis;
			}

			if (cornerX == 1)
				positionX = actualCellCountX - 1 - positionX;
			if (cornerY == 1)
				positionY = actualCellCountY - 1 - positionY;
			if (m_setContentHandler != null && changed) 
			{
				SetItemContent (recycleIndex, child.gameObject);	
			}
			SetChildAlongAxis (child, 0, startOffset.x + (cellSize [0] + spacing [0]) * positionX, cellSize [0]);
			SetChildAlongAxis (child, 1, startOffset.y + (cellSize [1] + spacing [1]) * positionY, cellSize [1]);
		}
	}
	private void SetItemContent(int recycleIndex, GameObject go)
	{
		m_setContentList [go] = recycleIndex;
	}
	private int GetIndexDistance(int index, int actualIndex)
	{
		while (actualIndex < 0)
			actualIndex += m_indexList.Count;
		return index - actualIndex;
	}

	private void GetXYIndex(int index, out int xIndex, out int yIndex)
	{
		xIndex = m_scrollRect.vertical ? index % constraintCount : index / constraintCount;
		yIndex = m_scrollRect.vertical ? index / constraintCount : index % constraintCount;
	}

	private Vector3 GetItemPosition(int dataIndex)
	{
		if (m_indexList.Count == 0)
			return Vector3.zero;
		int firstIndex = -1;
		for (int i = 0; i < rectTransform.childCount; ++i) 
		{
			if (rectTransform.GetChild (i).gameObject.activeSelf) {
				firstIndex = i;
				break;
			}
		}
		if (firstIndex < 0)
			return Vector3.zero;
		int indexDistance = GetIndexDistance (dataIndex, m_indexList [firstIndex]);
		int xIndexDistance;
		int yIndexDistance;
		GetXYIndex (indexDistance, out xIndexDistance, out yIndexDistance);

		var pos = rectTransform.GetChild (firstIndex).GetComponent<RectTransform>().anchoredPosition3D;

		pos.x += xIndexDistance * (cellSize.x + spacing.x);
		pos.y -= yIndexDistance * (cellSize.y + spacing.y);

		return pos;

	}

	public Vector3 GetItemPositionInScroll(int dataIndex)
	{
		return GetItemPosition (dataIndex) + rectTransform.anchoredPosition3D;
	}

}
