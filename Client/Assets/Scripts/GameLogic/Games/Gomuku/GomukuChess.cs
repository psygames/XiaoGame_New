using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace RedStone
{
	public class GomukuChess : MonoBehaviour
	{
		public Image chess;

		public Sprite white;
		public Sprite black;

		public void SetNum(int num)
		{
			transform.localPosition = LogicHelper.Gomuku.GetChessPos(num);
		}

		public void SetData(ChessData data)
		{
			SetNum(data.num);
			SetType(data.type);
		}

		public void SetType(message.Enums.ChessType type)
		{
			if (type == message.Enums.ChessType.Black)
			{
				chess.sprite = black;
				gameObject.SetActive(true);
			}
			else if (type == message.Enums.ChessType.White)
			{
				chess.sprite = white;
				gameObject.SetActive(true);
			}
			else
			{
				gameObject.SetActive(false);
			}
		}
	}
}