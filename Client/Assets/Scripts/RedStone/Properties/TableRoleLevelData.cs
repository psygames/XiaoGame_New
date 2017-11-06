using UnityEngine;
using System;
using System.Collections;

namespace Hotfire
{
	public class TableRoleLevelData
	{
		public TableRoleLevelData() { }
		public TableRoleLevelData(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.roleType = (int)dict["roleType"];
			this.level = (int)dict["level"];
			this.maxHp = (float)dict["maxHp"];
			this.hpRecover = (float)dict["hpRecover"];
			this.hpRecoverTime = (int)dict["hpRecoverTime"];
			this.hpRecoverDelayTime = (int)dict["hpRecoverDelayTime"];
			this.defence = (float)dict["defence"];
			this.critRate = (float)dict["critRate"];
			this.critDamage = (float)dict["critDamage"];
			this.maxHpUpgradeMultiplier = (float)dict["maxHpUpgradeMultiplier"];
			this.hpRecoverUpgradeMultiplier = (float)dict["hpRecoverUpgradeMultiplier"];
			this.hpRecoverDelayTimeUpgradeMultiplier = (float)dict["hpRecoverDelayTimeUpgradeMultiplier"];
			this.defenceUpgradeMultiplier = (float)dict["defenceUpgradeMultiplier"];
			this.skillCooldownReductionUpgradeMultiplier = (float)dict["skillCooldownReductionUpgradeMultiplier"];
			this.critRateUpgradeMultiplier = (float)dict["critRateUpgradeMultiplier"];
			this.critDamageUpgradeMultiplier = (float)dict["critDamageUpgradeMultiplier"];
			this.moveSpeedMultiplier = (float)dict["moveSpeedMultiplier"];
			this.levelUpExp = (int)dict["levelUpExp"];
			this.winExp = (int)dict["winExp"];
			this.loseExp = (int)dict["loseExp"];
			this.winFund = (int)dict["winFund"];
			this.loseFund = (int)dict["loseFund"];
			this.levelFund = (int)dict["levelFund"];
			this.levelDiamond = (int)dict["levelDiamond"];
		}

		/// <summary>
		/// id
		/// </summary>
		public int id;
		/// <summary>
		/// 不同角色type区分
		/// </summary>
		public int roleType;
		/// <summary>
		/// 等级，直接读表中数据
		/// </summary>
		public int level;
		/// <summary>
		/// 血量上限
		/// </summary>
		public float maxHp;
		/// <summary>
		/// 血量恢复，每次蹦的数值
		/// </summary>
		public float hpRecover;
		/// <summary>
		/// 血量恢复，每次间隔多少毫秒蹦一次数
		/// </summary>
		public int hpRecoverTime;
		/// <summary>
		/// 血量恢复延迟，不受攻击多少毫秒后开始回血。
		/// </summary>
		public int hpRecoverDelayTime;
		/// <summary>
		/// 防御力,最终伤害=原始伤害*100/(100+防御力）
		/// </summary>
		public float defence;
		/// <summary>
		/// 暴击率，命中时以这个概率暴击
		/// </summary>
		public float critRate;
		/// <summary>
		/// 暴击伤害倍率，暴击时造成普通攻击*暴击倍率的伤害。
		/// </summary>
		public float critDamage;
		/// <summary>
		/// 血上限，装备升级系数。
		/// </summary>
		public float maxHpUpgradeMultiplier;
		/// <summary>
		/// 血量恢复速度，装备升级系数
		/// </summary>
		public float hpRecoverUpgradeMultiplier;
		/// <summary>
		/// 血量恢复延迟，装备升级系数
		/// </summary>
		public float hpRecoverDelayTimeUpgradeMultiplier;
		/// <summary>
		/// 防御力，装备升级系数
		/// </summary>
		public float defenceUpgradeMultiplier;
		/// <summary>
		/// 冷却缩减，装备升级系数
		/// </summary>
		public float skillCooldownReductionUpgradeMultiplier;
		/// <summary>
		/// 暴击率，装备升级系数
		/// </summary>
		public float critRateUpgradeMultiplier;
		/// <summary>
		/// 暴击伤害，装备升级系数
		/// </summary>
		public float critDamageUpgradeMultiplier;
		/// <summary>
		/// 人物使用不同枪械时，移动速度系数。影响maxMoveSpeed,fireMaxMoveSpeed,sightMaxMoveSpeed三个参数。
		/// </summary>
		public float moveSpeedMultiplier;
		/// <summary>
		/// 升到下一级所需经验。
		/// </summary>
		public int levelUpExp;
		/// <summary>
		/// 战斗结算经验(胜）
		/// </summary>
		public int winExp;
		/// <summary>
		/// 战斗结算经验（败）
		/// </summary>
		public int loseExp;
		/// <summary>
		/// 战斗结算军费（胜）
		/// </summary>
		public int winFund;
		/// <summary>
		/// 战斗结算军费（败）
		/// </summary>
		public int loseFund;
		/// <summary>
		/// 升级奖励军费
		/// </summary>
		public int levelFund;
		/// <summary>
		/// 升级奖励宝石
		/// </summary>
		public int levelDiamond;
	}
}