using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

namespace RedStone
{
	public class CellGenerator : MonoBehaviour
	{
		public GameObject template;
		[HideInInspector]
		public Action<int> onClickCallback;

		public void Init()
		{
			int row = LogicHelper.Gomuku.row;
			int column = LogicHelper.Gomuku.column;
			for (int i = 0; i < row; i++)
			{
				for (int j = 0; j < column; j++)
				{
					GameObject go = Instantiate(template);
					go.transform.SetParent(transform, false);
					go.SetActive(true);

					GomukuCell cell = go.GetComponent<GomukuCell>();
					cell.SetData(i, j);
					cell.onClickCallback = (a, b) => { onClickCallback.Invoke(a * column + b); };
				}
			}
		}
	}
}
