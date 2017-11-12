using UnityEngine;
using System;
using System.Collections;

 namespace RedStone
{
	public class TableShopGoods
	{
		public TableShopGoods() { }
		public TableShopGoods(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.shopID = (int)dict["shopID"];
			this.goodsType = (int)dict["goodsType"];
			this.goodsID = (int)dict["goodsID"];
			this.goodsNumber = (int)dict["goodsNumber"];
			this.goodsIcon = (int)dict["goodsIcon"];
			this.goodsDesc = (string)dict["goodsDesc"];
			this.nameID = (string)dict["nameID"];
			this.typeNameID = (string)dict["typeNameID"];
			this.positionID = (int)dict["positionID"];
			this.playerLevelMin = (int)dict["playerLevelMin"];
			this.playerLevelMax = (int)dict["playerLevelMax"];
			this.conditionType = (int)dict["conditionType"];
			this.buyCurrency = (int)dict["buyCurrency"];
			this.price = (int)dict["price"];
			this.purchaseTimes = (int)dict["purchaseTimes"];
			this.purchaseTimesMax = (int)dict["purchaseTimesMax"];
			this.freeTimes = (int)dict["freeTimes"];
			this.freeInterval = (float)dict["freeInterval"];
			this.initialiDscount = (float)dict["initialiDscount"];
			this.increaseFactor = (float)dict["increaseFactor"];
			this.discountTimes = (int)dict["discountTimes"];
		}

		/// <summary>
		/// 商品ID
		/// </summary>
		public int id;
		/// <summary>
		/// 所属商店ID
		/// </summary>
		public int shopID;
		/// <summary>
		/// 物品类型
		/// </summary>
		public int goodsType;
		/// <summary>
		/// 物品小类型
		/// </summary>
		public int goodsID;
		/// <summary>
		/// 物品数量
		/// </summary>
		public int goodsNumber;
		/// <summary>
		/// 物品图标
		/// </summary>
		public int goodsIcon;
		/// <summary>
		/// 物品描述多语言
		/// </summary>
		public string goodsDesc;
		/// <summary>
		/// 名字多语言
		/// </summary>
		public string nameID;
		/// <summary>
		/// 多语言
		/// </summary>
		public string typeNameID;
		/// <summary>
		/// 位置ID
		/// </summary>
		public int positionID;
		/// <summary>
		/// 所需玩家等级下限
		/// </summary>
		public int playerLevelMin;
		/// <summary>
		/// 所需玩家等级上限
		/// </summary>
		public int playerLevelMax;
		/// <summary>
		/// 所需的条件类型
		/// </summary>
		public int conditionType;
		/// <summary>
		/// 购买所需货币类型
		/// </summary>
		public int buyCurrency;
		/// <summary>
		/// 货币价格
		/// </summary>
		public int price;
		/// <summary>
		/// 可购买次数
		/// </summary>
		public int purchaseTimes;
		/// <summary>
		/// 最多购买次数
		/// </summary>
		public int purchaseTimesMax;
		/// <summary>
		/// 免费次数
		/// </summary>
		public int freeTimes;
		/// <summary>
		/// 免费间隔
		/// </summary>
		public float freeInterval;
		/// <summary>
		/// 初始打折
		/// </summary>
		public float initialiDscount;
		/// <summary>
		/// 等差参数
		/// </summary>
		public float increaseFactor;
		/// <summary>
		/// 打折次数限制
		/// </summary>
		public int discountTimes;
	}
}