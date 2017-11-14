using UnityEngine;
using System;
using System.Collections;

 namespace RedStone
{
	public class TableGrenade
	{
		public TableGrenade() { }
		public TableGrenade(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.nameID = (string)dict["nameID"];
			this.name = (string)dict["name"];
			this.itemType = (int)dict["itemType"];
			this.motionId = (int)dict["motionId"];
			this.cdTime = (int)dict["cdTime"];
			this.triggerTime = (int)dict["triggerTime"];
			this.triggerRadius = (float)dict["triggerRadius"];
			this.deadthRadius = (float)dict["deadthRadius"];
			this.explosionRadius = (float)dict["explosionRadius"];
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
		/// 1手雷，2地雷，3油桶，4火箭筒
		/// </summary>
		public int itemType;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public int motionId;
		/// <summary>
		/// 单位（ms）
		/// </summary>
		public int cdTime;
		/// <summary>
		/// 触发时间（地雷用,单位ms)
		/// </summary>
		public int triggerTime;
		/// <summary>
		/// 触发半径(单位 米）
		/// </summary>
		public float triggerRadius;
		/// <summary>
		/// 必杀半径(单位 米）
		/// </summary>
		public float deadthRadius;
		/// <summary>
		/// 爆炸范围（单位 米）;作用公式：必杀半径内，扣血100%。;必杀半径~爆炸半径内，扣血damageMax~damageMin，根据距离线性插值
		/// </summary>
		public float explosionRadius;
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