using UnityEngine;
using System;
using System.Collections;

 namespace RedStone
{
	public class TableProfessionData
	{
		public TableProfessionData() { }
		public TableProfessionData(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.name = (string)dict["name"];
			this.nameID = (string)dict["nameID"];
			this.imageID = (int)dict["imageID"];
			this.iconID = (int)dict["iconID"];
			this.tierMax = (int)dict["tierMax"];
			this.unlockPlayerLevel = (int)dict["unlockPlayerLevel"];
			this.unlockProfessionMedalId = (int)dict["unlockProfessionMedalId"];
			this.unlockProfessionMedal = (int)dict["unlockProfessionMedal"];
			this.showOrder = (int)dict["showOrder"];
			this.energyIncreasePerKill = (float)dict["energyIncreasePerKill"];
			this.energyIncreasePerSecond = (float)dict["energyIncreasePerSecond"];
			this.energyMax = (float)dict["energyMax"];
			this.rushPowerIncrease = (float)dict["rushPowerIncrease"];
			this.rushPowerDecrease = (float)dict["rushPowerDecrease"];
			this.rushPowerMax = (float)dict["rushPowerMax"];
			this.rushPowerRecoverDelay = (int)dict["rushPowerRecoverDelay"];
			this.primaryGunID = (int[])dict["primaryGunID"];
			this.secondaryGunID = (int[])dict["secondaryGunID"];
			this.defaultPrimaryGun = (int)dict["defaultPrimaryGun"];
			this.defaultSecondaryGun = (int)dict["defaultSecondaryGun"];
			this.cdSkillGroupID = (int)dict["cdSkillGroupID"];
			this.chargeableSkillGroupID = (int)dict["chargeableSkillGroupID"];
			this.passiveSkillGroupID = (int[])dict["passiveSkillGroupID"];
			this.perceptionRadius = (float)dict["perceptionRadius"];
			this.playerRotateAngleSpeed = (float)dict["playerRotateAngleSpeed"];
			this.playerSightRotateAngleSpeed = (float)dict["playerSightRotateAngleSpeed"];
			this.moveFrictionAccleration = (float)dict["moveFrictionAccleration"];
			this.moveAccleration = (float)dict["moveAccleration"];
		}

		/// <summary>
		/// ID
		/// </summary>
		public int id;
		/// <summary>
		/// Name
		/// </summary>
		public string name;
		/// <summary>
		/// nameID
		/// </summary>
		public string nameID;
		/// <summary>
		/// 职业展示图片，暂时没用到
		/// </summary>
		public int imageID;
		/// <summary>
		/// 用于职业选择按钮ICON
		/// </summary>
		public int iconID;
		/// <summary>
		/// 职业最大Tier值
		/// </summary>
		public int tierMax;
		/// <summary>
		/// 解锁所需玩家等级
		/// </summary>
		public int unlockPlayerLevel;
		/// <summary>
		/// 解锁需要的职业徽章id
		/// </summary>
		public int unlockProfessionMedalId;
		/// <summary>
		/// 解锁所需职业徽章（每个职业徽章都不一样）
		/// </summary>
		public int unlockProfessionMedal;
		/// <summary>
		/// 显示顺序
		/// </summary>
		public int showOrder;
		/// <summary>
		/// 每次击杀增加能量值
		/// </summary>
		public float energyIncreasePerKill;
		/// <summary>
		/// 每秒增加能量值
		/// </summary>
		public float energyIncreasePerSecond;
		/// <summary>
		/// 最大能量值
		/// </summary>
		public float energyMax;
		/// <summary>
		/// 体力回复值
		/// </summary>
		public float rushPowerIncrease;
		/// <summary>
		/// 体力减少值
		/// </summary>
		public float rushPowerDecrease;
		/// <summary>
		/// 最大体力值
		/// </summary>
		public float rushPowerMax;
		/// <summary>
		/// 体力恢复延迟
		/// </summary>
		public int rushPowerRecoverDelay;
		/// <summary>
		/// 主武器列表(主副武器ID不能重！)
		/// </summary>
		public int[] primaryGunID;
		/// <summary>
		/// 副武器列表(主副武器ID不能重！)
		/// </summary>
		public int[] secondaryGunID;
		/// <summary>
		/// 默认穿戴的主武器
		/// </summary>
		public int defaultPrimaryGun;
		/// <summary>
		/// 默认穿戴的副武器
		/// </summary>
		public int defaultSecondaryGun;
		/// <summary>
		/// CD技能
		/// </summary>
		public int cdSkillGroupID;
		/// <summary>
		/// 充能技能
		/// </summary>
		public int chargeableSkillGroupID;
		/// <summary>
		/// 被动技能
		/// </summary>
		public int[] passiveSkillGroupID;
		/// <summary>
		/// 感知半径
		/// </summary>
		public float perceptionRadius;
		/// <summary>
		/// 普通行走转身速度
		/// </summary>
		public float playerRotateAngleSpeed;
		/// <summary>
		/// 瞄准状态转身速度
		/// </summary>
		public float playerSightRotateAngleSpeed;
		/// <summary>
		/// 摩擦力
		/// </summary>
		public float moveFrictionAccleration;
		/// <summary>
		/// 移动加速度
		/// </summary>
		public float moveAccleration;
	}
}