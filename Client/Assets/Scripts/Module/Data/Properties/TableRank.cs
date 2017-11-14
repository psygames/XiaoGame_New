using UnityEngine;
using System.Collections;

 namespace RedStone
{
	public class TableRank
	{
		public TableRank(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.rankLv = (int)dict["rankLv"];
			this.starMax = (int)dict["starMax"];
			this.starLose = (int)dict["starLose"];
			this.rankLose = (int)dict["rankLose"];
			this.rankCurrencyType = (int)dict["rankCurrencyType"];
			this.rankCurrencyNumber = (int)dict["rankCurrencyNumber"];
			this.rankChest = (int)dict["rankChest"];
			this.excitationCurrencyType = (int)dict["excitationCurrencyType"];
			this.excitationCurrencyNumber = (int)dict["excitationCurrencyNumber"];
			this.excitationChest = (int)dict["excitationChest"];
		}

		/// <summary>
		/// ID
		/// </summary>
		public int id;
		/// <summary>
		/// rank等级
		/// </summary>
		public int rankLv;
		/// <summary>
		/// 星星上限
		/// </summary>
		public int starMax;
		/// <summary>
		/// 输了掉星星
		/// </summary>
		public int starLose;
		/// <summary>
		/// 是否掉rank
		/// </summary>
		public int rankLose;
		/// <summary>
		/// 货币奖励类型
		/// </summary>
		public int rankCurrencyType;
		/// <summary>
		/// 货币奖励数量
		/// </summary>
		public int rankCurrencyNumber;
		/// <summary>
		/// 宝箱ID
		/// </summary>
		public int rankChest;
		/// <summary>
		/// 货币奖励类型
		/// </summary>
		public int excitationCurrencyType;
		/// <summary>
		/// 货币奖励数量
		/// </summary>
		public int excitationCurrencyNumber;
		/// <summary>
		/// 宝箱ID
		/// </summary>
		public int excitationChest;
	}
}