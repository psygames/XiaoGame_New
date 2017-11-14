using UnityEngine;
using System.Collections;

 namespace RedStone
{
	public class TableIntegralReward
	{
		public TableIntegralReward(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.integralMin = (int)dict["integralMin"];
			this.integralMax = (int)dict["integralMax"];
			this.integralCurrencyTyoe = (int)dict["integralCurrencyTyoe"];
			this.integralCurrencyNumber = (int)dict["integralCurrencyNumber"];
			this.integralChest = (int)dict["integralChest"];
		}

		/// <summary>
		/// ID
		/// </summary>
		public int id;
		/// <summary>
		/// 积分下限
		/// </summary>
		public int integralMin;
		/// <summary>
		/// 积分上限
		/// </summary>
		public int integralMax;
		/// <summary>
		/// 货币奖励类型
		/// </summary>
		public int integralCurrencyTyoe;
		/// <summary>
		/// 货币奖励数量
		/// </summary>
		public int integralCurrencyNumber;
		/// <summary>
		/// 宝箱ID
		/// </summary>
		public int integralChest;
	}
}