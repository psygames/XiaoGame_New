using UnityEngine;
using System;
using System.Collections;

namespace RedStone
{
	public class TableMission
	{
		public TableMission() { }
		public TableMission(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.name = (string)dict["name"];
			this.mission = (int[])dict["mission"];
			this.reset = (bool)dict["reset"];
			this.difficulty = (int)dict["difficulty"];
			this.playerMinLevel = (int)dict["playerMinLevel"];
			this.playerMaxLevel = (int)dict["playerMaxLevel"];
			this.containerId = (int)dict["containerId"];
			this.weight = (int)dict["weight"];
			this.rewardType = (int[])dict["rewardType"];
			this.rewardSupType = (int[])dict["rewardSupType"];
			this.rewardAmount = (int[])dict["rewardAmount"];
			this.uiDestination = (string)dict["uiDestination"];
		}

		/// <summary>
		/// 任务id
		/// </summary>
		public int id;
		/// <summary>
		/// 任务名称文本
		/// </summary>
		public string name;
		/// <summary>
		/// 任务编号
		/// </summary>
		public int[] mission;
		/// <summary>
		/// 是否可重置
		/// </summary>
		public bool reset;
		/// <summary>
		/// 难度
		/// </summary>
		public int difficulty;
		/// <summary>
		/// 玩家最低等级(玩家等级只是与任务难度相对应，并不影响任务)
		/// </summary>
		public int playerMinLevel;
		/// <summary>
		/// 玩家最高等级
		/// </summary>
		public int playerMaxLevel;
		/// <summary>
		/// 任务槽
		/// </summary>
		public int containerId;
		/// <summary>
		/// 任务权重
		/// </summary>
		public int weight;
		/// <summary>
		/// 奖励类型
		/// </summary>
		public int[] rewardType;
		/// <summary>
		/// 奖励子类型
		/// </summary>
		public int[] rewardSupType;
		/// <summary>
		/// 奖励数量
		/// </summary>
		public int[] rewardAmount;
		/// <summary>
		/// 对应的前往界面
		/// </summary>
		public string uiDestination;
	}
}