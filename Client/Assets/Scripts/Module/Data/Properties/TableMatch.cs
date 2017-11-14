using UnityEngine;
using System;
using System.Collections;

 namespace RedStone
{
	public class TableMatch
	{
		public TableMatch() { }
		public TableMatch(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.matchType = (int)dict["matchType"];
			this.matchName = (string)dict["matchName"];
			this.energyCost = (int)dict["energyCost"];
		}

		/// <summary>
		/// ID
		/// </summary>
		public int id;
		/// <summary>
		/// 赛事类型
		/// </summary>
		public int matchType;
		/// <summary>
		/// 赛事名字
		/// </summary>
		public string matchName;
		/// <summary>
		/// 消耗体力
		/// </summary>
		public int energyCost;
	}
}