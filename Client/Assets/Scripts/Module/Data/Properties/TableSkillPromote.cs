using UnityEngine;
using System;
using System.Collections;

 namespace RedStone
{
	public class TableSkillPromote
	{
		public TableSkillPromote() { }
		public TableSkillPromote(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.groupID = (int)dict["groupID"];
			this.promote = (int)dict["promote"];
			this.upgradeCostType = (int[])dict["upgradeCostType"];
			this.upgradeCostSubType = (int[])dict["upgradeCostSubType"];
			this.upgradeCostAmount = (int[])dict["upgradeCostAmount"];
			this.description = (string)dict["description"];
		}

		/// <summary>
		/// ID程序暂时没用到此列
		/// </summary>
		public int id;
		/// <summary>
		/// 技能组ID
		/// </summary>
		public int groupID;
		/// <summary>
		/// 稀有度
		/// </summary>
		public int promote;
		/// <summary>
		/// 技能晋升消耗
		/// </summary>
		public int[] upgradeCostType;
		/// <summary>
		/// 技能晋升消耗
		/// </summary>
		public int[] upgradeCostSubType;
		/// <summary>
		/// 技能晋升消耗数量
		/// </summary>
		public int[] upgradeCostAmount;
		/// <summary>
		/// 多语言ID
		/// </summary>
		public string description;
	}
}