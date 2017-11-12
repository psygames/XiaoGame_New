using UnityEngine;
using System;
using System.Collections;

 namespace RedStone
{
	public class TableChest
	{
		public TableChest() { }
		public TableChest(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.name = (string)dict["name"];
			this.nameID = (string)dict["nameID"];
			this.icon = (int)dict["icon"];
			this.openedIcon = (int)dict["openedIcon"];
			this.description = (string)dict["description"];
			this.type = (int)dict["type"];
			this.quality = (int)dict["quality"];
			this.randomType = (int)dict["randomType"];
			this.randomTimes = (int)dict["randomTimes"];
			this.itemId = (int[])dict["itemId"];
			this.itemType = (int[])dict["itemType"];
			this.itemProbability = (int[])dict["itemProbability"];
			this.itemNumber = (string[])dict["itemNumber"];
			this.fundPrice = (int)dict["fundPrice"];
			this.diamondPrice = (int)dict["diamondPrice"];
			this.fundSellPrice = (int)dict["fundSellPrice"];
			this.diamondSellPrice = (int)dict["diamondSellPrice"];
			this.exchangeGoods = (int)dict["exchangeGoods"];
			this.exchangeNumber = (int)dict["exchangeNumber"];
			this.sellGoods = (int)dict["sellGoods"];
		}

		/// <summary>
		/// 箱子ID
		/// </summary>
		public int id;
		/// <summary>
		/// 箱子名称
		/// </summary>
		public string name;
		/// <summary>
		/// 箱子名称多语言
		/// </summary>
		public string nameID;
		/// <summary>
		/// 箱子图标
		/// </summary>
		public int icon;
		/// <summary>
		/// 箱子图标
		/// </summary>
		public int openedIcon;
		/// <summary>
		/// 箱子描述
		/// </summary>
		public string description;
		/// <summary>
		/// 箱子类型（固定宝箱0，随机宝箱1）
		/// </summary>
		public int type;
		/// <summary>
		/// 箱子品质
		/// </summary>
		public int quality;
		/// <summary>
		/// 概率类型
		/// </summary>
		public int randomType;
		/// <summary>
		/// 随机次数
		/// </summary>
		public int randomTimes;
		/// <summary>
		/// 宝箱内物品
		/// </summary>
		public int[] itemId;
		/// <summary>
		/// 物品类型
		/// </summary>
		public int[] itemType;
		/// <summary>
		/// 物品概率
		/// </summary>
		public int[] itemProbability;
		/// <summary>
		/// 物品数量
		/// </summary>
		public string[] itemNumber;
		/// <summary>
		/// 军费价格
		/// </summary>
		public int fundPrice;
		/// <summary>
		/// 钻石价格
		/// </summary>
		public int diamondPrice;
		/// <summary>
		/// 军费出售价格
		/// </summary>
		public int fundSellPrice;
		/// <summary>
		/// 钻石出售价格
		/// </summary>
		public int diamondSellPrice;
		/// <summary>
		/// 兑换所需物品
		/// </summary>
		public int exchangeGoods;
		/// <summary>
		/// 兑换所需数量
		/// </summary>
		public int exchangeNumber;
		/// <summary>
		/// 出售获取物品
		/// </summary>
		public int sellGoods;
	}
}