using UnityEngine;
using System;
using System.Collections;

namespace RedStone
{
	public class TableDispatchMissionReward
	{
		public TableDispatchMissionReward() { }
		public TableDispatchMissionReward(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.time = (int)dict["time"];
			this.region = (int)dict["region"];
			this.difficulty = (int)dict["difficulty"];
			this.rewardType = (int[])dict["rewardType"];
			this.rewardSupType = (int[])dict["rewardSupType"];
			this.rewardAmount = (int[])dict["rewardAmount"];
			this.bonusRewardType = (int[])dict["bonusRewardType"];
			this.bonusRewardSupType = (int[])dict["bonusRewardSupType"];
			this.bonusRewardAmount = (int[])dict["bonusRewardAmount"];
		}

		/// <summary>
		/// ID
		/// </summary>
		public int id;
		/// <summary>
		/// 时间（3天一个循环）
		/// </summary>
		public int time;
		/// <summary>
		/// 任务战区
		/// </summary>
		public int region;
		/// <summary>
		/// 任务难度
		/// </summary>
		public int difficulty;
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
		/// 奖励类型
		/// </summary>
		public int[] bonusRewardType;
		/// <summary>
		/// 奖励子类型
		/// </summary>
		public int[] bonusRewardSupType;
		/// <summary>
		/// 奖励数量
		/// </summary>
		public int[] bonusRewardAmount;
	}
}