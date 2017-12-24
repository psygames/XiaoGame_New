using UnityEngine;
using System;
using System.Collections;

namespace RedStone
{
	public class TableRigSuit
	{
		public TableRigSuit() { }
		public TableRigSuit(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.professionId = (int)dict["professionId"];
			this.suitPosition = (int)dict["suitPosition"];
			this.name = (string)dict["name"];
			this.description = (string)dict["description"];
			this.icon = (int)dict["icon"];
			this.level = (int)dict["level"];
			this.upgradeTechnologies = (int)dict["upgradeTechnologies"];
			this.incrHp = (float)dict["incrHp"];
			this.incrDefense = (float)dict["incrDefense"];
			this.incrShield = (float)dict["incrShield"];
			this.incrShielRecover = (float)dict["incrShielRecover"];
			this.mulPrimaryAttack = (float)dict["mulPrimaryAttack"];
			this.mulSecondaryAttack = (float)dict["mulSecondaryAttack"];
			this.mulPrimaryHit = (float)dict["mulPrimaryHit"];
			this.mulSecondaryHit = (float)dict["mulSecondaryHit"];
			this.mulPrimaryCrit = (float)dict["mulPrimaryCrit"];
			this.mulSecondaryCrit = (float)dict["mulSecondaryCrit"];
			this.mulPrimaryCritDamage = (float)dict["mulPrimaryCritDamage"];
			this.mulSecondaryCritDamage = (float)dict["mulSecondaryCritDamage"];
		}

		/// <summary>
		/// id
		/// </summary>
		public int id;
		/// <summary>
		/// 职业id
		/// </summary>
		public int professionId;
		/// <summary>
		/// 职业穿戴的防具的位置
		/// </summary>
		public int suitPosition;
		/// <summary>
		/// 名字
		/// </summary>
		public string name;
		/// <summary>
		/// 描述
		/// </summary>
		public string description;
		/// <summary>
		/// 防具图标
		/// </summary>
		public int icon;
		/// <summary>
		/// 防具等级
		/// </summary>
		public int level;
		/// <summary>
		/// 升级消耗的科技点
		/// </summary>
		public int upgradeTechnologies;
		/// <summary>
		/// 生命值
		/// </summary>
		public float incrHp;
		/// <summary>
		/// 防御力
		/// </summary>
		public float incrDefense;
		/// <summary>
		/// 护盾
		/// </summary>
		public float incrShield;
		/// <summary>
		/// 护盾回复速度
		/// </summary>
		public float incrShielRecover;
		/// <summary>
		/// 攻击力
		/// </summary>
		public float mulPrimaryAttack;
		/// <summary>
		/// 攻击力
		/// </summary>
		public float mulSecondaryAttack;
		/// <summary>
		/// 命中等级
		/// </summary>
		public float mulPrimaryHit;
		/// <summary>
		/// 命中等级
		/// </summary>
		public float mulSecondaryHit;
		/// <summary>
		/// 暴击等级
		/// </summary>
		public float mulPrimaryCrit;
		/// <summary>
		/// 暴击等级
		/// </summary>
		public float mulSecondaryCrit;
		/// <summary>
		/// 暴伤等级
		/// </summary>
		public float mulPrimaryCritDamage;
		/// <summary>
		/// 暴伤等级
		/// </summary>
		public float mulSecondaryCritDamage;
	}
}