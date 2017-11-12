using UnityEngine;
using System.Collections;
using RedStone.UI;
using System.Collections.Generic;


[RequireComponent(typeof(ListLayoutGroup))]
public class ListLayoutGroupTest : MonoBehaviour
{
	public Image template;
	[Range(1,20)]
	public int dataLength = 5;

	ListLayoutGroup m_listLayoutGroup;
	// Use this for initialization

	public bool set = false; 
	void Start()
	{
		m_listLayoutGroup = GetComponent<ListLayoutGroup> ();
		List<Color> list = new List<Color> ();
		for (int i = 0; i < dataLength; ++i)
		{
			list.Add (new Color ((float)i / dataLength, (float)((i * 2) % dataLength) / dataLength, (float)((i * i) % dataLength) / dataLength));
		}
		m_listLayoutGroup.SetData (template, list, (i, p, d) => p.color = d);
	}


	void Update()
	{
		if (set)
		{
			Start ();
			set = false;
		}
	}
}

