using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using System;


 namespace RedStone.UI
{
    public class FlexibleListLayoutGroup : LayoutGroup
    {
        [SerializeField] protected float m_Spacing = 0;
        public float spacing { get { return m_Spacing; } set { SetProperty(ref m_Spacing, value); } }


		[SerializeField] protected bool m_isVertical = true;
		public bool isVertical { get { return m_isVertical; } set {
				if (m_isVertical != isVertical)
					ReCalcItemSize ();
				SetProperty(ref m_isVertical, value); } }

		[SerializeField] protected ScrollRect m_ScrollRect = null;
		public ScrollRect scrollRect{get{ return m_ScrollRect; } set{ SetProperty(ref m_ScrollRect, value);} }

		protected RectTransform m_scrollRectTransform = null;

		protected List<object> m_dataList = new List<object>();

		protected List<Vector2> m_sizeList = new List<Vector2>();
		protected List<float> m_positionList = new List<float>();

		protected float m_minSize = float.MaxValue;
		protected float m_otherMaxSize = 0f;
		private List<int> m_indexList = new List<int> ();

		private Action<int, GameObject, object> m_setContentHandler;

		private Func<int, object, Vector2> m_getSizeHandler;

		private MonoBehaviour m_itemTemplate;

		private Type m_objectType = typeof(MonoBehaviour);

		private int m_displayListCount = 0;
		private bool m_needHideItem = false;
		public void Init<P, D>(P itemPrefab, ICollection<D> dataList, System.Action<int, P, D> setContentHandler, Func<int, D, Vector2> getSizeHandler)
			where P : MonoBehaviour
		{
			if (m_ScrollRect == null)
				return;
			if (itemPrefab != m_itemTemplate) 
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
			m_itemTemplate = itemPrefab;
			if (m_itemTemplate == null)
			{
				Debug.LogError ("template is null");
				return;
			}
			Init<P, D> (dataList, setContentHandler, getSizeHandler);
		}
		public void Init<P, D>(ICollection<D> dataList, System.Action<int, P, D> setContentHandler, Func<int, D, Vector2> getSizeHandler)
			where P : MonoBehaviour
		{
			m_objectType = m_itemTemplate.GetType ();
			m_ScrollRect.vertical = isVertical;
			m_ScrollRect.horizontal = !isVertical;

			m_ScrollRect.normalizedPosition = Vector2.up;
			m_scrollRectTransform = m_ScrollRect.GetComponent<RectTransform> ();
			m_dataList.Clear ();
			m_indexList.Clear ();
			if (dataList == null)
				return;
			foreach (var data in dataList)
			{
				m_dataList.Add (data);
			}
			m_setContentHandler = (index, go, data) => {

				var dt = (D)data;
				var item = go.GetComponent(m_objectType);

				setContentHandler.Invoke (index, item as P, dt);
			};
			m_getSizeHandler = (index, data) =>{
				var dt = (D) data;

				return getSizeHandler(index,  dt);
			};
			m_ScrollRect.onValueChanged.RemoveListener (OnDrag);
			m_ScrollRect.onValueChanged.AddListener (OnDrag);
			ReCalcItemSize (true);
		}

		private float GetScrollRectSize()
		{
			return isVertical ? m_scrollRectTransform.rect.size.y :  m_scrollRectTransform.rect.size.x;
		}
		public void ReCalcItemSize(bool rebuildSizeList = false)
		{
			if (m_getSizeHandler == null)
				return;
			if(rebuildSizeList)
				m_sizeList.Clear ();
			m_positionList.Clear ();
			m_minSize = float.MaxValue;
			m_otherMaxSize = 0f;
			float totalSize = 0f;
			m_positionList.Add (0);
			for (int i = 0; i < m_dataList.Count; ++i)
			{
				var size = Vector2.zero;

				if (rebuildSizeList)
				{
					size = m_getSizeHandler (i, m_dataList [i]);
				} else
				{
					size = m_sizeList [i];
				}
				var cur = (isVertical ? size.y : size.x);
				m_minSize = Mathf.Min (cur, m_minSize);
				m_otherMaxSize = Mathf.Max ((isVertical ? size.x : size.y), m_otherMaxSize);
				if(rebuildSizeList)
					m_sizeList.Add (size);
				totalSize += cur + spacing;
				m_positionList.Add (totalSize);
			}

			m_displayListCount = Mathf.CeilToInt(GetScrollRectSize() / (m_minSize + spacing));
			if (m_dataList.Count > 1)
				m_displayListCount = Mathf.Max (2, m_displayListCount);
			for (int i = rectTransform.childCount ; i < m_displayListCount; ++i) 
			{
				var obj = GameObjectHelper.AddChild (rectTransform, m_itemTemplate);
				obj.gameObject.SetActive (i < m_dataList.Count);
			}
			ForceUpdateContent ();
			SetDirty();
		}

		void ForceUpdateContent ()
		{
			m_indexList.Clear ();
			LayoutRebuilder.ForceRebuildLayoutImmediate (m_scrollRectTransform);
		}
		protected virtual void OnDrag(Vector2 vec)
		{
			SetLayoutHorizontal ();
			SetLayoutVertical ();
		}

		public void RemoveFirst()
		{
			RemoveData (0);
		}

		public void RemoveLast()
		{
			RemoveData (m_dataList.Count - 1);
		}
		public void RemoveData(int index)
		{
			if (0 <= index && index < m_dataList.Count)
			{
				m_dataList.RemoveAt (index);
				m_sizeList.RemoveAt (index);
				ReCalcItemSize ();
			}
		}		
		public void AddLast<D>(D data)
		{
			Insert (data, m_dataList.Count);
		}
		public void AddFirst<D>(D data)
		{
			Insert (data, 0);
		}
		public void Insert<D>(D data, int index)
		{
			if (0 <= index && index <= m_dataList.Count)
			{
				var size = m_getSizeHandler (index, data);
				m_dataList.Insert (index, data);
				m_sizeList.Insert (index, size);
				ReCalcItemSize ();
			}

		}
		public override void CalculateLayoutInputHorizontal()
		{
			if (m_ScrollRect == null)
				return;
			base.CalculateLayoutInputHorizontal();
			CalcAlongAxis(0, isVertical);
		}

		public override void CalculateLayoutInputVertical()
		{
			if (m_ScrollRect == null)
				return;
			CalcAlongAxis(1, isVertical);
		}

		public override void SetLayoutHorizontal()
		{
			if (m_ScrollRect == null)
				return;
			SetChildrenAlongAxis(0, isVertical);
		}

		public override void SetLayoutVertical()
		{
			if (m_ScrollRect == null)
				return;
			SetChildrenAlongAxis(1, isVertical);
		}
        protected void CalcAlongAxis(int axis, bool calcVertical)
		{
			if (m_ScrollRect == null)
				return;
            float combinedPadding = (axis == 0 ? padding.horizontal : padding.vertical);

            float totalMin = combinedPadding;

            bool alongOtherAxis = (calcVertical ^ (axis == 1));
			if (alongOtherAxis)
				totalMin += m_otherMaxSize;
			
			if (!alongOtherAxis && m_positionList.Count > 0)
            {
				totalMin += m_positionList[m_positionList.Count - 1] - spacing;
            }
			SetLayoutInputForAxis(totalMin, totalMin, totalMin, axis);
		}	
		private  int GetActualIndex(int startIndex, int index)
		{
			var count = rectTransform.childCount;
			return ((startIndex + (startIndex >= 0 ? (count - index - 1) : -index)) / count * count + index);
		}
		void Update()
		{
			if (m_needHideItem)
			{
				m_needHideItem = false;
				for (int i = 0; i < m_indexList.Count; ++i)
				{
					if (i < rectTransform.childCount)
						rectTransform.GetChild(i).gameObject.SetActive (0 <= m_indexList[i] && m_indexList[i] < m_dataList.Count);
				}
			}
		}

		protected int GetStartIndex()
		{
			if (m_dataList == null || m_dataList.Count == 0)
				return 0;
			var curPosition = (isVertical ? rectTransform.anchoredPosition.y : rectTransform.anchoredPosition.x);
			if (isVertical)
				curPosition -= padding.top;
			else
				curPosition = -curPosition - padding.left;

			int index = 0;
			for (int i = 0; i < m_positionList.Count; ++i)
			{
				if (m_positionList [i] > curPosition)
					break;
				index = i;
			}
			return index;
		}

        protected void SetChildrenAlongAxis(int axis, bool calcVertical)
		{
			if (m_ScrollRect == null)
				return;
            float size = rectTransform.rect.size[axis];

            bool alongOtherAxis = (calcVertical ^ (axis == 1));
			int startIndex = GetStartIndex ();
            if (alongOtherAxis)
            {
				for (int i = 0; i <  rectTransform.childCount; i++)
                {
					var actualIndex = GetActualIndex (startIndex, i);
					RectTransform child = (RectTransform)rectTransform.GetChild(i);
					if (actualIndex >= m_sizeList.Count)
					{
						SetChildAlongAxis(child, axis, 0, 0);
						continue;
					}
					var curSize = m_sizeList [actualIndex];
					float min = (isVertical ? curSize.x : curSize.y);
                    float startOffset = GetStartOffset(axis, min);
					SetChildAlongAxis(child, axis, startOffset, min);
                }
            }
            else
            {
				float startPos = (axis == 0 ? padding.left : padding.top);
				for (int i = 0; i < rectTransform.childCount; i++)
				{

					RectTransform child = (RectTransform)rectTransform.GetChild(i);

					var actualIndex = GetActualIndex (startIndex, i);
					while (m_indexList.Count <= i)
					{
						m_indexList.Add (-1);
					}
					bool changed = (m_indexList [i] != actualIndex);
					m_indexList[i] = actualIndex;		
					if (actualIndex >= m_sizeList.Count)
					{
						SetChildAlongAxis(child, axis, 0, 0);
						m_needHideItem = true;
						continue;
					}
					 var curSize = m_sizeList [actualIndex];
					float min = (isVertical ? curSize.y : curSize.x);
					SetChildAlongAxis(child, axis, startPos + m_positionList[actualIndex], min);
					if (changed)
					{
						m_needHideItem = true;
						m_setContentHandler.Invoke (actualIndex, child.gameObject, m_dataList [actualIndex]);
					}
				}
            }
        }
    }
}
