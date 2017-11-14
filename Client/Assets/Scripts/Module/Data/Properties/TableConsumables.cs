using UnityEngine;
using System.Collections;

 namespace RedStone
{
	public class TableConsumables
	{
		public TableConsumables(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.type = (int)dict["type"];
			this.name = (string)dict["name"];
			this.typeLanguage = (string)dict["typeLanguage"];
			this.actionPart = (int)dict["actionPart"];
			this.nameID = (string)dict["nameID"];
			this.icon = (int)dict["icon"];
			this.effectiveTime = (int)dict["effectiveTime"];
			this.sellRewardFund = (int)dict["sellRewardFund"];
			this.quality = (int)dict["quality"];
			this.qualityNameID = (string)dict["qualityNameID"];
			this.attack = (float)dict["attack"];
			this.hit = (float)dict["hit"];
			this.crit = (float)dict["crit"];
			this.critDamage = (float)dict["critDamage"];
			this.fireRate = (float)dict["fireRate"];
			this.magazine = (float)dict["magazine"];
			this.reloadTime = (float)dict["reloadTime"];
			this.hp = (float)dict["hp"];
			this.defense = (float)dict["defense"];
			this.hpRecover = (float)dict["hpRecover"];
			this.attackPercent = (float)dict["attackPercent"];
			this.hitPercent = (float)dict["hitPercent"];
			this.critPercent = (float)dict["critPercent"];
			this.critDamagePercent = (float)dict["critDamagePercent"];
			this.fireRatePercent = (float)dict["fireRatePercent"];
			this.magazinePercent = (float)dict["magazinePercent"];
			this.reloadTimePercent = (float)dict["reloadTimePercent"];
			this.hpPercent = (float)dict["hpPercent"];
			this.defensePercent = (float)dict["defensePercent"];
			this.hpRecoverPercent = (float)dict["hpRecoverPercent"];
		}

		/// <summary>
		/// ID
		/// </summary>
		public int id;
		/// <summary>
		/// 消耗品类型
		/// </summary>
		public int type;
		/// <summary>
		/// 名字
		/// </summary>
		public string name;
		/// <summary>
		/// 物品类型多语言
		/// </summary>
		public string typeLanguage;
		/// <summary>
		/// 消耗品作用部位
		/// </summary>
		public int actionPart;
		/// <summary>
		/// 名字多语言
		/// </summary>
		public string nameID;
		/// <summary>
		/// 图标
		/// </summary>
		public int icon;
		/// <summary>
		/// 时效
		/// </summary>
		public int effectiveTime;
		/// <summary>
		/// 卖出获得
		/// </summary>
		public int sellRewardFund;
		/// <summary>
		/// 品质
		/// </summary>
		public int quality;
		/// <summary>
		/// 品质多语言
		/// </summary>
		public string qualityNameID;
		/// <summary>
		/// 攻击力
		/// </summary>
		public float attack;
		/// <summary>
		/// 命中
		/// </summary>
		public float hit;
		/// <summary>
		/// 暴击
		/// </summary>
		public float crit;
		/// <summary>
		/// 暴击伤害
		/// </summary>
		public float critDamage;
		/// <summary>
		/// 射击速度
		/// </summary>
		public float fireRate;
		/// <summary>
		/// 弹夹容量
		/// </summary>
		public float magazine;
		/// <summary>
		/// 换弹时间
		/// </summary>
		public float reloadTime;
		/// <summary>
		/// 生命值
		/// </summary>
		public float hp;
		/// <summary>
		/// 防御
		/// </summary>
		public float defense;
		/// <summary>
		/// 回血速率
		/// </summary>
		public float hpRecover;
		/// <summary>
		/// 攻击力
		/// </summary>
		public float attackPercent;
		/// <summary>
		/// 命中
		/// </summary>
		public float hitPercent;
		/// <summary>
		/// 暴击
		/// </summary>
		public float critPercent;
		/// <summary>
		/// 暴击伤害
		/// </summary>
		public float critDamagePercent;
		/// <summary>
		/// 射击速度
		/// </summary>
		public float fireRatePercent;
		/// <summary>
		/// 弹夹容量
		/// </summary>
		public float magazinePercent;
		/// <summary>
		/// 换弹时间
		/// </summary>
		public float reloadTimePercent;
		/// <summary>
		/// 生命值
		/// </summary>
		public float hpPercent;
		/// <summary>
		/// 防御
		/// </summary>
		public float defensePercent;
		/// <summary>
		/// 回血速率
		/// </summary>
		public float hpRecoverPercent;
	}
}