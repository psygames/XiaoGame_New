using UnityEngine;
using System;
using System.Collections;

namespace RedStone
{
	public class TableSkillGroup
	{
		public TableSkillGroup() { }
		public TableSkillGroup(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.initPromote = (int)dict["initPromote"];
			this.initLevel = (int)dict["initLevel"];
			this.unlockCostType = (int[])dict["unlockCostType"];
			this.unlockCostSubType = (int[])dict["unlockCostSubType"];
			this.unlockCostAmount = (int[])dict["unlockCostAmount"];
			this.description = (string)dict["description"];
			this.unlockProfesionLevel = (int)dict["unlockProfesionLevel"];
			this.modelPath = (string)dict["modelPath"];
		}

		/// <summary>
		/// 技能组ID
		/// </summary>
		public int id;
		/// <summary>
		/// 解锁时的品质
		/// </summary>
		public int initPromote;
		/// <summary>
		/// 初始等级
		/// </summary>
		public int initLevel;
		/// <summary>
		/// 解锁消耗
		/// </summary>
		public int[] unlockCostType;
		/// <summary>
		/// 解锁消耗
		/// </summary>
		public int[] unlockCostSubType;
		/// <summary>
		/// 解锁/升级消耗数量
		/// </summary>
		public int[] unlockCostAmount;
		/// <summary>
		/// 多语言ID
		/// </summary>
		public string description;
		/// <summary>
		/// 解锁需要的玩家等级（这个版本临时用）_
		/// </summary>
		public int unlockProfesionLevel;
		/// <summary>
		/// 大厅展示模型路径
		/// </summary>
		public string modelPath;
	}
}