using UnityEngine;
using System;
using System.Collections;

namespace RedStone
{
	public class TableDispatchMissionType
	{
		public TableDispatchMissionType() { }
		public TableDispatchMissionType(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.type = (int)dict["type"];
			this.name = (string)dict["name"];
			this.difficulty = (int)dict["difficulty"];
			this.needTime = (int)dict["needTime"];
			this.leaderLevel = (int)dict["leaderLevel"];
			this.leaderClass = (int[])dict["leaderClass"];
			this.teammateLevel = (int)dict["teammateLevel"];
			this.heroNumber = (int)dict["heroNumber"];
			this.heroEfficiency = (int)dict["heroEfficiency"];
			this.priceType = (int)dict["priceType"];
			this.price = (int)dict["price"];
			this.gunRate = (float)dict["gunRate"];
			this.gunIds = (int[])dict["gunIds"];
			this.gunIdRate = (int[])dict["gunIdRate"];
			this.gunLevel = (int)dict["gunLevel"];
		}

		/// <summary>
		/// ID
		/// </summary>
		public int id;
		/// <summary>
		/// 任务类型
		/// </summary>
		public int type;
		/// <summary>
		/// 任务名称
		/// </summary>
		public string name;
		/// <summary>
		/// 任务难度
		/// </summary>
		public int difficulty;
		/// <summary>
		/// 任务耗时(h)
		/// </summary>
		public int needTime;
		/// <summary>
		/// 队长等级要求
		/// </summary>
		public int leaderLevel;
		/// <summary>
		/// 队长职业要求
		/// </summary>
		public int[] leaderClass;
		/// <summary>
		/// 队员等级要求
		/// </summary>
		public int teammateLevel;
		/// <summary>
		/// 最多英雄数量
		/// </summary>
		public int heroNumber;
		/// <summary>
		/// 每个队员可节省时间单位：小时
		/// </summary>
		public int heroEfficiency;
		/// <summary>
		/// 价格类型
		/// </summary>
		public int priceType;
		/// <summary>
		/// 价格
		/// </summary>
		public int price;
		/// <summary>
		/// 奖励条件枪械出现概率
		/// </summary>
		public float gunRate;
		/// <summary>
		/// 枪械id池
		/// </summary>
		public int[] gunIds;
		/// <summary>
		/// 枪械id被随机到的概率
		/// </summary>
		public int[] gunIdRate;
		/// <summary>
		/// 枪械等级要求
		/// </summary>
		public int gunLevel;
	}
}