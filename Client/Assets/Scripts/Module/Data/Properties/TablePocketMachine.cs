using UnityEngine;
using System;
using System.Collections;

 namespace RedStone
{
	public class TablePocketMachine
	{
		public TablePocketMachine() { }
		public TablePocketMachine(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.nameID = (string)dict["nameID"];
			this.name = (string)dict["name"];
			this.itemType = (int)dict["itemType"];
			this.iconID = (int)dict["iconID"];
			this.motionId = (int)dict["motionId"];
			this.energy = (int)dict["energy"];
			this.maxHp = (int)dict["maxHp"];
			this.stayTime = (int)dict["stayTime"];
			this.workTime = (int)dict["workTime"];
			this.workSkillId = (int)dict["workSkillId"];
			this.triggerRadius = (float)dict["triggerRadius"];
			this.workRadius = (float)dict["workRadius"];
			this.perceptionRadius = (float)dict["perceptionRadius"];
			this.damageMin = (float)dict["damageMin"];
			this.damageMax = (float)dict["damageMax"];
		}

		/// <summary>
		/// 没有描述信息
		/// </summary>
		public int id;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string nameID;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string name;
		/// <summary>
		/// 1手雷，2地雷，3油桶，4火箭筒，5 仪器
		/// </summary>
		public int itemType;
		/// <summary>
		/// iconID
		/// </summary>
		public int iconID;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public int motionId;
		/// <summary>
		/// 能量
		/// </summary>
		public int energy;
		/// <summary>
		/// 生命
		/// </summary>
		public int maxHp;
		/// <summary>
		/// 持续时间，毫秒
		/// </summary>
		public int stayTime;
		/// <summary>
		/// 运作时间 毫秒
		/// </summary>
		public int workTime;
		/// <summary>
		/// 运作skill Id
		/// </summary>
		public int workSkillId;
		/// <summary>
		/// 触发半径(单位 米）
		/// </summary>
		public float triggerRadius;
		/// <summary>
		/// 运作半径
		/// </summary>
		public float workRadius;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public float perceptionRadius;
		/// <summary>
		/// 最小伤害，百分比扣血，距离爆炸点最远时产生最小伤害。
		/// </summary>
		public float damageMin;
		/// <summary>
		/// 最大伤害，百分比扣血，距离爆炸点距离刚好大于致死距离时产生最大伤害。
		/// </summary>
		public float damageMax;
	}
}