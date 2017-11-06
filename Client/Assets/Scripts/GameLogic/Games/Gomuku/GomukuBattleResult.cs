using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

namespace RedStone
{
	public class GomukuBattleResult : MonoBehaviour
	{
		public UIEventListener listener;
		public Text resultText;
		public Action onClickCallback;

		private void Awake()
		{
			listener.onClick = (obj) =>
			{
				if (onClickCallback == null)
					return;
				onClickCallback.Invoke();
			};
		}

		public void SetData(ECamp camp)
		{
			PlayerData player = ProxyManager.instance.GetProxy<HallProxy>().mainPlayerData;
			if (camp == player.camp)
			{
				resultText.text = "+ 获胜 +";
				resultText.color = Color.red;
			}
			else if (camp == player.enemyCamp)
			{
				resultText.text = "- 失败 -";
				resultText.color = Color.gray;
			}
			else
			{
				resultText.text = "= 平局 =";
				resultText.color = Color.gray;
			}
		}
	}
}