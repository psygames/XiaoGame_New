using UnityEngine;
using System.Collections;

namespace Hotfire
{
	public class TableGunRarity
	{
		public TableGunRarity(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.gunID = (int)dict["gunID"];
			this.rarity = (int)dict["rarity"];
			this.upgradeItemID = (int)dict["upgradeItemID"];
			this.upgradeItemCount = (int)dict["upgradeItemCount"];
			this.attackFactor = (float)dict["attackFactor"];
			this.hitFactor = (float)dict["hitFactor"];
			this.critFactor = (float)dict["critFactor"];
			this.critDamageFactor = (float)dict["critDamageFactor"];
			this.hpRecoverFactor = (float)dict["hpRecoverFactor"];
			this.fireRateFactor = (float)dict["fireRateFactor"];
			this.reloadTimeFactor = (float)dict["reloadTimeFactor"];
		}

		/// <summary>
		/// id
		/// </summary>
		public int id;
		/// <summary>
		/// 枪械ID
		/// </summary>
		public int gunID;
		/// <summary>
		/// 稀有度 
		/// </summary>
		public int rarity;
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
		public float attackFactor;
		/// <summary>
		/// 基础命中值
		/// </summary>
		public float hitFactor;
		/// <summary>
		/// 基础暴击值
		/// </summary>
		public float critFactor;
		/// <summary>
		/// 基础暴击伤害(倍率)
		/// </summary>
		public float critDamageFactor;
		/// <summary>
		/// 基础回血速率(hp/s)
		/// </summary>
		public float hpRecoverFactor;
		/// <summary>
		/// 基础射击速度（发/s）
		/// </summary>
		public float fireRateFactor;
		/// <summary>
		/// 基础换弹时间
		/// </summary>
		public float reloadTimeFactor;
	}
}