using UnityEngine;
using System;
using System.Collections;

 namespace RedStone
{
	public class TableShop
	{
		public TableShop() { }
		public TableShop(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.nameID = (string)dict["nameID"];
			this.describeID = (string)dict["describeID"];
			this.refreshInterval = (float)dict["refreshInterval"];
			this.openTime = (float)dict["openTime"];
			this.endTime = (float)dict["endTime"];
			this.refreshCurrency = (int)dict["refreshCurrency"];
			this.refreshPrice = (int)dict["refreshPrice"];
			this.refreshDiscount = (float)dict["refreshDiscount"];
			this.increaseFactor = (float)dict["increaseFactor"];
			this.refreshTimeMax = (int)dict["refreshTimeMax"];
			this.obtainResourcesType = (int)dict["obtainResourcesType"];
		}

		/// <summary>
		/// 商店ID
		/// </summary>
		public int id;
		/// <summary>
		/// 商店名字多语言
		/// </summary>
		public string nameID;
		/// <summary>
		/// 商店描述多语言
		/// </summary>
		public string describeID;
		/// <summary>
		/// 商店刷新间隔
		/// </summary>
		public float refreshInterval;
		/// <summary>
		/// 商店开放时间
		/// </summary>
		public float openTime;
		/// <summary>
		/// 关闭时间
		/// </summary>
		public float endTime;
		/// <summary>
		/// 刷新用的货币类型
		/// </summary>
		public int refreshCurrency;
		/// <summary>
		/// 起始刷新价格
		/// </summary>
		public int refreshPrice;
		/// <summary>
		/// 起始刷新价格折扣
		/// </summary>
		public float refreshDiscount;
		/// <summary>
		/// 等差参数
		/// </summary>
		public float increaseFactor;
		/// <summary>
		/// 刷新次数上限
		/// </summary>
		public int refreshTimeMax;
		/// <summary>
		/// 得到的资源类型
		/// </summary>
		public int obtainResourcesType;
	}
}