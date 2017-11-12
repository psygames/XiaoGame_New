using UnityEngine;
using System;
using System.Collections;

 namespace RedStone
{
	public class TableItem
	{
		public TableItem() { }
		public TableItem(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.name = (string)dict["name"];
			this.nameID = (string)dict["nameID"];
			this.imageID = (int)dict["imageID"];
			this.description = (string)dict["description"];
			this.descriptionID = (string)dict["descriptionID"];
			this.type = (int)dict["type"];
			this.functionType = (int)dict["functionType"];
			this.functionParam1 = (int)dict["functionParam1"];
			this.typeLanguage = (string)dict["typeLanguage"];
			this.quality = (int)dict["quality"];
			this.stage = (int)dict["stage"];
			this.subType = (int)dict["subType"];
			this.fundPrice = (int)dict["fundPrice"];
			this.diamondPrice = (int)dict["diamondPrice"];
			this.fundSellPrice = (int)dict["fundSellPrice"];
			this.diamondSellPrice = (int)dict["diamondSellPrice"];
			this.exchangeGoods = (int)dict["exchangeGoods"];
			this.exchangeNumber = (int)dict["exchangeNumber"];
			this.sellGoods = (int)dict["sellGoods"];
			this.qualityNameID = (string)dict["qualityNameID"];
			this.shopIcon = (string)dict["shopIcon"];
			this.shopName = (string)dict["shopName"];
		}

		/// <summary>
		/// 物品ID
		/// </summary>
		public int id;
		/// <summary>
		/// 物品名称
		/// </summary>
		public string name;
		/// <summary>
		/// 物品名
		/// </summary>
		public string nameID;
		/// <summary>
		/// 物品图标
		/// </summary>
		public int imageID;
		/// <summary>
		/// 物品描述
		/// </summary>
		public string description;
		/// <summary>
		/// 物品描述多语言Id
		/// </summary>
		public string descriptionID;
		/// <summary>
		/// 物品类型
		/// </summary>
		public int type;
		/// <summary>
		/// 如果物品可以使用，使用后的收益类型
		/// </summary>
		public int functionType;
		/// <summary>
		/// 物品使用收益数量
		/// </summary>
		public int functionParam1;
		/// <summary>
		/// 物品类型多语言
		/// </summary>
		public string typeLanguage;
		/// <summary>
		/// 物品品质
		/// </summary>
		public int quality;
		/// <summary>
		/// 物品stage
		/// </summary>
		public int stage;
		/// <summary>
		/// 子类型
		/// </summary>
		public int subType;
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
		/// <summary>
		/// 多语言
		/// </summary>
		public string qualityNameID;
		/// <summary>
		/// 商店出售ICON ID
		/// </summary>
		public string shopIcon;
		/// <summary>
		/// 商店展示名
		/// </summary>
		public string shopName;
	}
}