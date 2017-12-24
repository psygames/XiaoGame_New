using UnityEngine;
using System;
using System.Collections;

namespace RedStone
{
	public class TablePlayerLevel
	{
		public TablePlayerLevel() { }
		public TablePlayerLevel(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.level = (int)dict["level"];
			this.nextExp = (int)dict["nextExp"];
			this.winExp = (int)dict["winExp"];
			this.loseExp = (int)dict["loseExp"];
			this.drawExp = (int)dict["drawExp"];
			this.energyMax = (int)dict["energyMax"];
			this.firstScore = (int)dict["firstScore"];
			this.firstChest = (int)dict["firstChest"];
			this.secondScore = (int)dict["secondScore"];
			this.secondChest = (int)dict["secondChest"];
			this.thirdScore = (int)dict["thirdScore"];
			this.thirdChest = (int)dict["thirdChest"];
			this.winFund = (int)dict["winFund"];
			this.failFund = (int)dict["failFund"];
			this.drawFund = (int)dict["drawFund"];
			this.donateCommon = (int)dict["donateCommon"];
			this.donateRare = (int)dict["donateRare"];
		}

		/// <summary>
		/// ID
		/// </summary>
		public int id;
		/// <summary>
		/// 等级
		/// </summary>
		public int level;
		/// <summary>
		/// 升级所需经验
		/// </summary>
		public int nextExp;
		/// <summary>
		/// 胜利经验
		/// </summary>
		public int winExp;
		/// <summary>
		/// 失败经验
		/// </summary>
		public int loseExp;
		/// <summary>
		/// 平局经验
		/// </summary>
		public int drawExp;
		/// <summary>
		/// 体力上限
		/// </summary>
		public int energyMax;
		/// <summary>
		/// 第一档分数
		/// </summary>
		public int firstScore;
		/// <summary>
		/// 第一挡宝箱
		/// </summary>
		public int firstChest;
		/// <summary>
		/// 第二档分数
		/// </summary>
		public int secondScore;
		/// <summary>
		/// 第二挡宝箱
		/// </summary>
		public int secondChest;
		/// <summary>
		/// 第三档分数
		/// </summary>
		public int thirdScore;
		/// <summary>
		/// 第三挡宝箱
		/// </summary>
		public int thirdChest;
		/// <summary>
		/// 胜利获取军费
		/// </summary>
		public int winFund;
		/// <summary>
		/// 失败获取军费
		/// </summary>
		public int failFund;
		/// <summary>
		/// 平局军费
		/// </summary>
		public int drawFund;
		/// <summary>
		/// 捐献普通蓝图上限
		/// </summary>
		public int donateCommon;
		/// <summary>
		/// 捐献稀有蓝图上限
		/// </summary>
		public int donateRare;
	}
}