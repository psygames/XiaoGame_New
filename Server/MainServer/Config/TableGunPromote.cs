using UnityEngine;
using System;
using System.Collections;

namespace RedStone
{
	public class TableGunPromote
	{
		public TableGunPromote() { }
		public TableGunPromote(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.abilityID = (int)dict["abilityID"];
			this.promote = (int)dict["promote"];
			this.promoteNameID = (string)dict["promoteNameID"];
			this.upgradeItemID = (int)dict["upgradeItemID"];
			this.upgradeItemCount = (int)dict["upgradeItemCount"];
			this.mulAttack = (float)dict["mulAttack"];
			this.mulHit = (float)dict["mulHit"];
			this.mulCrit = (float)dict["mulCrit"];
			this.mulCritDamage = (float)dict["mulCritDamage"];
		}

		/// <summary>
		/// id
		/// </summary>
		public int id;
		/// <summary>
		/// 枪械abilityID
		/// </summary>
		public int abilityID;
		/// <summary>
		/// 稀有度 
		/// </summary>
		public int promote;
		/// <summary>
		/// 枪械品质多语言串
		/// </summary>
		public string promoteNameID;
		/// <summary>
		/// 升下一阶所需道具ID
		/// </summary>
		public int upgradeItemID;
		/// <summary>
		/// 升下一阶所需道具数量
		/// </summary>
		public int upgradeItemCount;
		/// <summary>
		/// 基础攻击系数
		/// </summary>
		public float mulAttack;
		/// <summary>
		/// 基础命中值
		/// </summary>
		public float mulHit;
		/// <summary>
		/// 基础暴击值
		/// </summary>
		public float mulCrit;
		/// <summary>
		/// 基础暴击伤害(倍率)
		/// </summary>
		public float mulCritDamage;
	}
}