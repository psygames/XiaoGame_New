using UnityEngine;
using System;
using System.Collections;

namespace RedStone
{
	public class TableGunLevel
	{
		public TableGunLevel() { }
		public TableGunLevel(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.abilityId = (int)dict["abilityId"];
			this.gunLevel = (int)dict["gunLevel"];
			this.stage = (int)dict["stage"];
			this.partFireCostType = (int[])dict["partFireCostType"];
			this.partFireCostID = (int[])dict["partFireCostID"];
			this.partFireCost = (int[])dict["partFireCost"];
			this.partAimCostType = (int[])dict["partAimCostType"];
			this.partAimCostID = (int[])dict["partAimCostID"];
			this.partAimCost = (int[])dict["partAimCost"];
			this.partReloadCostType = (int[])dict["partReloadCostType"];
			this.partReloadCostID = (int[])dict["partReloadCostID"];
			this.partReloadCost = (int[])dict["partReloadCost"];
			this.UpgradeCostType = (int[])dict["UpgradeCostType"];
			this.UpgradeCostId = (int[])dict["UpgradeCostId"];
			this.UpgradeCost = (int[])dict["UpgradeCost"];
			this.attack = (float)dict["attack"];
			this.hit = (float)dict["hit"];
			this.crit = (float)dict["crit"];
			this.critDamage = (float)dict["critDamage"];
		}

		/// <summary>
		/// ID
		/// </summary>
		public int id;
		/// <summary>
		/// 枪械abilityID
		/// </summary>
		public int abilityId;
		/// <summary>
		/// 枪械等级
		/// </summary>
		public int gunLevel;
		/// <summary>
		/// 星级
		/// </summary>
		public int stage;
		/// <summary>
		/// 射击部件填充消耗类型
		/// </summary>
		public int[] partFireCostType;
		/// <summary>
		/// 射击部件填充消耗小类型
		/// </summary>
		public int[] partFireCostID;
		/// <summary>
		/// 射击部件填充消耗数量
		/// </summary>
		public int[] partFireCost;
		/// <summary>
		/// 瞄准部件填充消耗类型
		/// </summary>
		public int[] partAimCostType;
		/// <summary>
		/// 瞄准部件填充消耗小类型
		/// </summary>
		public int[] partAimCostID;
		/// <summary>
		/// 瞄准部件填充消耗数量
		/// </summary>
		public int[] partAimCost;
		/// <summary>
		/// 装弹部件填充消耗大类型
		/// </summary>
		public int[] partReloadCostType;
		/// <summary>
		/// 装弹部件填充消耗小类型
		/// </summary>
		public int[] partReloadCostID;
		/// <summary>
		/// 装弹部件填充消耗数量
		/// </summary>
		public int[] partReloadCost;
		/// <summary>
		/// 升级消耗大类型
		/// </summary>
		public int[] UpgradeCostType;
		/// <summary>
		/// 升级消耗小类型
		/// </summary>
		public int[] UpgradeCostId;
		/// <summary>
		/// 升级消耗数量
		/// </summary>
		public int[] UpgradeCost;
		/// <summary>
		/// 攻击力
		/// </summary>
		public float attack;
		/// <summary>
		/// 命中等级
		/// </summary>
		public float hit;
		/// <summary>
		/// 暴击等级
		/// </summary>
		public float crit;
		/// <summary>
		/// 暴伤等级
		/// </summary>
		public float critDamage;
	}
}