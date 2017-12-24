using UnityEngine;
using System;
using System.Collections;

namespace RedStone
{
	public class TableDispatchMissionRegion
	{
		public TableDispatchMissionRegion() { }
		public TableDispatchMissionRegion(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.maxDifficulty = (int)dict["maxDifficulty"];
			this.missionPerDay = (int)dict["missionPerDay"];
			this.missionType = (int[])dict["missionType"];
			this.missionWeight = (int[])dict["missionWeight"];
			this.goldenMissionRate = (float)dict["goldenMissionRate"];
			this.goldenMissionRewardType = (int)dict["goldenMissionRewardType"];
			this.goldenMissionRewardNumber = (int)dict["goldenMissionRewardNumber"];
			this.goldenMissionExtraRewardNum = (int)dict["goldenMissionExtraRewardNum"];
			this.icon = (int)dict["icon"];
			this.location = (string)dict["location"];
			this.reward = (string)dict["reward"];
		}

		/// <summary>
		/// 没有描述信息
		/// </summary>
		public int id;
		/// <summary>
		/// 最大难度
		/// </summary>
		public int maxDifficulty;
		/// <summary>
		/// 战区每日可以完成的任务数
		/// </summary>
		public int missionPerDay;
		/// <summary>
		/// 将会在战区出现的任务类型
		/// </summary>
		public int[] missionType;
		/// <summary>
		/// 战区各个任务类型出现的概率
		/// </summary>
		public int[] missionWeight;
		/// <summary>
		/// 赏金任务出现概率
		/// </summary>
		public float goldenMissionRate;
		/// <summary>
		/// 奖励类型
		/// </summary>
		public int goldenMissionRewardType;
		/// <summary>
		/// 奖励数量
		/// </summary>
		public int goldenMissionRewardNumber;
		/// <summary>
		/// 赏金区域完成任务额外条件时还有额外数额的奖励
		/// </summary>
		public int goldenMissionExtraRewardNum;
		/// <summary>
		/// 图片ID
		/// </summary>
		public int icon;
		/// <summary>
		/// 区域
		/// </summary>
		public string location;
		/// <summary>
		/// 奖励
		/// </summary>
		public string reward;
	}
}