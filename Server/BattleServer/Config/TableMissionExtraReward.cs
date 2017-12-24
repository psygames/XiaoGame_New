using UnityEngine;
using System;
using System.Collections;

namespace RedStone
{
	public class TableMissionExtraReward
	{
		public TableMissionExtraReward() { }
		public TableMissionExtraReward(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.name = (string)dict["name"];
			this.difficulty = (int)dict["difficulty"];
			this.activeness = (int)dict["activeness"];
			this.chestId = (int)dict["chestId"];
		}

		/// <summary>
		/// 任务id
		/// </summary>
		public int id;
		/// <summary>
		/// 任务名称文本
		/// </summary>
		public string name;
		/// <summary>
		/// 难度
		/// </summary>
		public int difficulty;
		/// <summary>
		/// 需要活跃度
		/// </summary>
		public int activeness;
		/// <summary>
		/// 宝箱id
		/// </summary>
		public int chestId;
	}
}