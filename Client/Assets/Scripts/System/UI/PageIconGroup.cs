using System;
using System.Collections;
using System.Collections.Generic;
using RedStone.UI;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class PageIconGroup : MonoBehaviour
{
	public ToggleGroup parent;
	public Toggle itemTemplate;

	private int m_pageCount = -1;
	private List<Toggle> toggleList = new List<Toggle>();

	public Action<int> onPageChanged;
	private bool m_notifyChanged = true;
	void Awake()
	{
		itemTemplate.gameObject.SetActive (false);
	}
	public void SetPageCount(int currentPage, int count)
	{
        if (count == 0)
            return;

		if (count != m_pageCount) 
		{
			m_pageCount = count;
			GameObjectHelper.CreateListItems (itemTemplate, parent.transform, toggleList, count, (index, item) => {
				item.group = parent;
				parent.RegisterToggle (item);
				item.onValueChanged.RemoveAllListeners ();
				item.onValueChanged.AddListener (OnPageChanged);
				item.ForceSetIsOn (index == currentPage);
			});
		}
		m_notifyChanged = false;
		toggleList [currentPage].isOn = true;
		m_notifyChanged = true;
	}

	public void OnPageChanged(bool isOn)
	{
		if (m_notifyChanged && isOn && onPageChanged != null) 
		{
			for (int i = 0; i < toggleList.Count; ++i) 
			{
				if (toggleList [i].isOn) 
				{
					onPageChanged.Invoke (i);
				}
			}
		}
	}
}

