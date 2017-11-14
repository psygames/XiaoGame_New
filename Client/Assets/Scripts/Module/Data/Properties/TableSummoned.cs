using UnityEngine;
using System;
using System.Collections;

 namespace RedStone
{
	public class TableSummoned
	{
		public TableSummoned() { }
		public TableSummoned(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.nameID = (string)dict["nameID"];
			this.name = (string)dict["name"];
			this.itemType = (int)dict["itemType"];
			this.iconID = (int)dict["iconID"];
			this.motionId = (int)dict["motionId"];
			this.injuredType = (int)dict["injuredType"];
			this.maxHpBaseAttribute = (string)dict["maxHpBaseAttribute"];
			this.maxHpMultiplier = (float)dict["maxHpMultiplier"];
			this.maxHpFixed = (float)dict["maxHpFixed"];
			this.installType = (int)dict["installType"];
			this.installMinFlyTime = (int)dict["installMinFlyTime"];
			this.installMaxFlyTime = (int)dict["installMaxFlyTime"];
			this.installAnimTime = (int)dict["installAnimTime"];
			this.stayTime = (int)dict["stayTime"];
			this.stayIntervalTime = (int)dict["stayIntervalTime"];
			this.stayHpReducePer = (float)dict["stayHpReducePer"];
			this.workTarget = (int)dict["workTarget"];
			this.workSkillId = (int)dict["workSkillId"];
			this.workIntervalSkillId = (int)dict["workIntervalSkillId"];
			this.workIntervalTime = (int)dict["workIntervalTime"];
			this.triggerRadius = (float)dict["triggerRadius"];
			this.triggerTime = (int)dict["triggerTime"];
			this.workRadius = (float)dict["workRadius"];
			this.perceptionRadius = (float)dict["perceptionRadius"];
			this.traceLineType = (int)dict["traceLineType"];
			this.attackBaseAttribute = (string)dict["attackBaseAttribute"];
			this.attackMultiplier = (float)dict["attackMultiplier"];
			this.attackFixed = (float)dict["attackFixed"];
			this.fireRate = (float)dict["fireRate"];
			this.scanRate = (int)dict["scanRate"];
			this.fireParticle = (int)dict["fireParticle"];
			this.maxAmount = (int)dict["maxAmount"];
			this.targetAreaCenterLength = (float)dict["targetAreaCenterLength"];
			this.targetAreaRadius = (float)dict["targetAreaRadius"];
			this.createPosOffset = (Vector3)dict["createPosOffset"];
		}

		/// <summary>
		/// ID
		/// </summary>
		public int id;
		/// <summary>
		/// 名字多语言
		/// </summary>
		public string nameID;
		/// <summary>
		/// 名字
		/// </summary>
		public string name;
		/// <summary>
		/// 1手雷，2地雷，3油桶，4火箭筒，5 仪器，6炮台，7滚雷，8盾牌
		/// </summary>
		public int itemType;
		/// <summary>
		/// iconID
		/// </summary>
		public int iconID;
		/// <summary>
		/// 运动轨迹ID，对于motion表
		/// </summary>
		public int motionId;
		/// <summary>
		/// 受伤Type，0为不受伤害，1为受伤害
		/// </summary>
		public int injuredType;
		/// <summary>
		/// 召唤物生命百分比基数属性
		/// </summary>
		public string maxHpBaseAttribute;
		/// <summary>
		/// 召唤物生命百分比比例
		/// </summary>
		public float maxHpMultiplier;
		/// <summary>
		/// 召唤物生命值固定数值
		/// </summary>
		public float maxHpFixed;
		/// <summary>
		/// 安装类型，0不安装，1安装
		/// </summary>
		public int installType;
		/// <summary>
		/// 部署时飞行0米所用飞行时间，毫秒
		/// </summary>
		public int installMinFlyTime;
		/// <summary>
		/// 部署时最大飞行时间，毫秒
		/// </summary>
		public int installMaxFlyTime;
		/// <summary>
		/// 部署时的播动画时间，毫秒
		/// </summary>
		public int installAnimTime;
		/// <summary>
		/// 持续时间，毫秒
		/// </summary>
		public int stayTime;
		/// <summary>
		/// 持续时间间隔 ms
		/// </summary>
		public int stayIntervalTime;
		/// <summary>
		/// 持续时间减少生命值百分比
		/// </summary>
		public float stayHpReducePer;
		/// <summary>
		/// 触发/作用目标，对处于作用范围内的全部单位进行筛选，符合筛选条件的，会对这个召唤物进行触发
		/// </summary>
		public int workTarget;
		/// <summary>
		/// 运作skill Id
		/// </summary>
		public int workSkillId;
		/// <summary>
		/// 运作间隔skill Id
		/// </summary>
		public int workIntervalSkillId;
		/// <summary>
		/// 运作间隔时间 ms
		/// </summary>
		public int workIntervalTime;
		/// <summary>
		/// 触发半径(单位 米）
		/// </summary>
		public float triggerRadius;
		/// <summary>
		/// 触发时间（地雷用,单位ms)
		/// </summary>
		public int triggerTime;
		/// <summary>
		/// 运作半径
		/// </summary>
		public float workRadius;
		/// <summary>
		/// 感知半径
		/// </summary>
		public float perceptionRadius;
		/// <summary>
		/// 辅助线类型，0不需要。1辅助线和碰撞体大小、2辅助线和爆炸范围
		/// </summary>
		public int traceLineType;
		/// <summary>
		/// 召唤物攻击力基数属性
		/// </summary>
		public string attackBaseAttribute;
		/// <summary>
		/// 召唤物攻击力系数
		/// </summary>
		public float attackMultiplier;
		/// <summary>
		/// 召唤物攻击力固定数值
		/// </summary>
		public float attackFixed;
		/// <summary>
		/// 开火频率，给炮台用
		/// </summary>
		public float fireRate;
		/// <summary>
		/// 扫描间隔，一秒扫描几次敌人
		/// </summary>
		public int scanRate;
		/// <summary>
		/// 火焰离子
		/// </summary>
		public int fireParticle;
		/// <summary>
		/// 最大数量限制，如果安装超过这个数量，则最先安装的被顶掉。
		/// </summary>
		public int maxAmount;
		/// <summary>
		/// 目标区域中心点最远距离
		/// </summary>
		public float targetAreaCenterLength;
		/// <summary>
		/// 目标区域半径
		/// </summary>
		public float targetAreaRadius;
		/// <summary>
		/// 召唤物生成时相对于人物脚下的位置偏移
		/// </summary>
		public Vector3 createPosOffset;
	}
}