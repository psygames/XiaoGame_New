using UnityEngine;
using System;
using System.Collections;

 namespace RedStone
{
	public class TableGunPart
	{
		public TableGunPart() { }
		public TableGunPart(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.abilityId = (int)dict["abilityId"];
			this.partType = (int)dict["partType"];
			this.icon = (int)dict["icon"];
			this.name = (string)dict["name"];
			this.nameID = (string)dict["nameID"];
			this.stage = (int)dict["stage"];
			this.incrAttack = (float)dict["incrAttack"];
			this.incrHit = (float)dict["incrHit"];
			this.incrCrit = (float)dict["incrCrit"];
			this.incrCritDamage = (float)dict["incrCritDamage"];
		}

		/// <summary>
		/// ID
		/// </summary>
		public int id;
		/// <summary>
		/// 能力ID
		/// </summary>
		public int abilityId;
		/// <summary>
		/// 部件类型
		/// </summary>
		public int partType;
		/// <summary>
		/// 部件图标
		/// </summary>
		public int icon;
		/// <summary>
		/// 部件名称
		/// </summary>
		public string name;
		/// <summary>
		/// 部件名称ID
		/// </summary>
		public string nameID;
		/// <summary>
		/// 星级
		/// </summary>
		public int stage;
		/// <summary>
		/// 增加攻击力
		/// </summary>
		public float incrAttack;
		/// <summary>
		/// 增加命中等级
		/// </summary>
		public float incrHit;
		/// <summary>
		/// 增加暴击等级
		/// </summary>
		public float incrCrit;
		/// <summary>
		/// 增加暴伤等级
		/// </summary>
		public float incrCritDamage;
	}
}