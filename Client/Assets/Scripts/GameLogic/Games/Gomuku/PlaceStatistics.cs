using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

namespace RedStone
{
	public class PlaceStatistics : MonoBehaviour
	{
		public Image chess;
		public Text text;

		public void SetNum(int num)
		{
			transform.localPosition = LogicHelper.Gomuku.GetChessPos(num);
		}

		public void SetText(string str)
		{
			text.text = str;
		}

		public void SetData(PlaceStatisticsData data)
		{
			SetNum(data.num);
			SetText(data.ratio.ToString("P1"));
		}
	}
}