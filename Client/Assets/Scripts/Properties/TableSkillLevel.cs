using UnityEngine;
using System;
using System.Collections;

namespace RedStone
{
	public class TableSkillLevel
	{
		public TableSkillLevel() { }
		public TableSkillLevel(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.groupID = (int)dict["groupID"];
			this.level = (int)dict["level"];
			this.levelLimit = (int)dict["levelLimit"];
			this.upgradeCostType = (int[])dict["upgradeCostType"];
			this.upgradeCostSubType = (int[])dict["upgradeCostSubType"];
			this.upgradeCostAmount = (int[])dict["upgradeCostAmount"];
		}

		/// <summary>
		/// 此列暂时程序无用处
		/// </summary>
		public int id;
		/// <summary>
		/// 技能组ID
		/// </summary>
		public int groupID;
		/// <summary>
		/// 技能等级
		/// </summary>
		public int level;
		/// <summary>
		/// 需要职业/枪械等级
		/// </summary>
		public int levelLimit;
		/// <summary>
		/// 升级消耗的大类id
		/// </summary>
		public int[] upgradeCostType;
		/// <summary>
		/// 升级消耗的具体id
		/// </summary>
		public int[] upgradeCostSubType;
		/// <summary>
		/// 升级消耗数量
		/// </summary>
		public int[] upgradeCostAmount;
	}
}