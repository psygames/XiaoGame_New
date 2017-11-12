using UnityEngine;
using System.Collections;
using RedStone.UI;
using System.Collections.Generic;


[RequireComponent(typeof(FlexibleListLayoutGroup))]
public class FlexibleListViewTest : MonoBehaviour
{
	public FlexibleListLayoutGroup m_listView = null;
	public string[] testData;
	public string data;
	public int index;
	public MonoBehaviour template;
	public bool init;
	public bool addFirst;
	public bool addLast;
	public bool insertData;
	public bool removeFirst;
	public bool removeLast;
	public bool removeData;
	public int GetContentSizeCount = 0;
	public int SetContentCount = 0;
	void Awake()
	{
		m_listView = GetComponent<FlexibleListLayoutGroup> ();
	}


	void Update()
	{
		if (init)
		{
			GetContentSizeCount = 0;
			SetContentCount = 0;
			m_listView.Init (template, testData, (index, p, d) =>
			{
				++ SetContentCount;
				p.GetComponentInChildren<Text> ().text = d.ToString ();
			}, (index, d) =>
			{
				++ GetContentSizeCount;
				var text = template.GetComponentInChildren<Text>();
				var fitter = text.GetComponent<UnityEngine.UI.ContentSizeFitter>();
				if(fitter != null)
				{

					text.text = d;
					return new Vector2(Mathf.Min(text.preferredWidth + 40f, 400f), text.preferredHeight + 20f);
				}
				else
				{
					var size = int.Parse(d);
					return new Vector2(400f, size * 100f);
				}
			});
		}
		init = false;

		if (addFirst)
		{
			m_listView.AddFirst (data);
			addFirst = false;
		}
		if (addLast)
		{
			addLast = false;
			m_listView.AddLast (data);
			m_listView.scrollRect.normalizedPosition = Vector2.zero;
		}
		if (insertData)
		{
			m_listView.Insert (data, index);
			insertData = false;
		}
		if (removeData)
		{
			removeData = false;
			m_listView.RemoveData (index);
		}
		if (removeFirst)
		{
			removeFirst = false;
			m_listView.RemoveFirst ();
		}
		if (removeLast)
		{
			removeLast = false;
			m_listView.RemoveLast ();
		}
	}

}

