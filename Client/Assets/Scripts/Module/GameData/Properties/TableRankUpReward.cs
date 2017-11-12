using UnityEngine;
using System.Collections;

 namespace RedStone
{
	public class TableRankUpReward
	{
		public TableRankUpReward(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.placingMin = (int)dict["placingMin"];
			this.placingMax = (int)dict["placingMax"];
			this.currencyRewardTyoe = (int)dict["currencyRewardTyoe"];
			this.currencyRewardNumber = (int)dict["currencyRewardNumber"];
			this.rewardChest = (int)dict["rewardChest"];
		}

		/// <summary>
		/// ID
		/// </summary>
		public int id;
		/// <summary>
		/// 名次下限
		/// </summary>
		public int placingMin;
		/// <summary>
		/// 名次上限
		/// </summary>
		public int placingMax;
		/// <summary>
		/// 货币奖励类型
		/// </summary>
		public int currencyRewardTyoe;
		/// <summary>
		/// 货币奖励数量
		/// </summary>
		public int currencyRewardNumber;
		/// <summary>
		/// 宝箱ID
		/// </summary>
		public int rewardChest;
	}
}