using UnityEngine;
using System;
using System.Collections;

 namespace RedStone
{
	public class TableSeasonLevel
	{
		public TableSeasonLevel() { }
		public TableSeasonLevel(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.level = (int)dict["level"];
			this.inter = (int)dict["inter"];
			this.taotalStars = (int)dict["taotalStars"];
			this.title = (string)dict["title"];
			this.titleID = (string)dict["titleID"];
			this.eloActive = (bool)dict["eloActive"];
			this.loseStar = (bool)dict["loseStar"];
			this.loseLevel = (bool)dict["loseLevel"];
			this.returnToLevel = (int)dict["returnToLevel"];
			this.excitationCurrencyType = (int)dict["excitationCurrencyType"];
			this.excitationCurrencyNumber = (int)dict["excitationCurrencyNumber"];
			this.excitationChest = (int)dict["excitationChest"];
			this.imageID = (int)dict["imageID"];
			this.battleRewardNeedCount = (int)dict["battleRewardNeedCount"];
			this.battleReward = (int)dict["battleReward"];
		}

		/// <summary>
		/// id
		/// </summary>
		public int id;
		/// <summary>
		/// 天梯等级，0级代表传说
		/// </summary>
		public int level;
		/// <summary>
		/// 满星状态再得inter个星星才会晋升
		/// </summary>
		public int inter;
		/// <summary>
		/// 升级需要的星星
		/// </summary>
		public int taotalStars;
		/// <summary>
		/// 称号
		/// </summary>
		public string title;
		/// <summary>
		/// 称号（多语言）
		/// </summary>
		public string titleID;
		/// <summary>
		/// 是否启用elo算法
		/// </summary>
		public bool eloActive;
		/// <summary>
		/// 输掉比赛是否会掉星
		/// </summary>
		public bool loseStar;
		/// <summary>
		/// 输光星星是否会掉级
		/// </summary>
		public bool loseLevel;
		/// <summary>
		/// 新赛季回落到的等级
		/// </summary>
		public int returnToLevel;
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
		/// <summary>
		/// 图标
		/// </summary>
		public int imageID;
		/// <summary>
		/// 场次奖励需要的场次
		/// </summary>
		public int battleRewardNeedCount;
		/// <summary>
		/// 场次奖励的奖牌数
		/// </summary>
		public int battleReward;
	}
}