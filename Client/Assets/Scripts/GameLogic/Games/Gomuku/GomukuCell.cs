using UnityEngine;
using System;
using System.Collections;

namespace RedStone
{
	public class GomukuCell : MonoBehaviour
	{
		public UIEventListener listener;
		public GameObject point;
		public Action<int, int> onClickCallback;

		public int x;
		public int y;


		private void Awake()
		{
			listener.onClick = (obj) =>
			{
				if (onClickCallback == null)
					return;
				onClickCallback.Invoke(x, y);
			};
		}

		public void SetData(int x, int y)
		{
			this.x = x;
			this.y = y;

			transform.localPosition = LogicHelper.Gomuku.GetChessPos(x, y);

			if (x % 5 == 3 && y % 5 == 3)
				point.SetActive(true);
			else
				point.SetActive(false);

		}

	}
}