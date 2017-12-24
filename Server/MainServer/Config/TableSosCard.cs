using UnityEngine;
using System;
using System.Collections;

namespace RedStone
{
	public class TableSosCard
	{
		public TableSosCard() { }
		public TableSosCard(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.name = (string)dict["name"];
			this.description = (string)dict["description"];
			this.star = (int)dict["star"];
			this.point = (int)dict["point"];
			this.effect = (string)dict["effect"];
			this.image = (string)dict["image"];
			this.type = (int)dict["type"];
		}

		/// <summary>
		/// ID
		/// </summary>
		public int id;
		/// <summary>
		/// 名字
		/// </summary>
		public string name;
		/// <summary>
		/// 描述
		/// </summary>
		public string description;
		/// <summary>
		/// 星星数（同时表示该卡片数量）
		/// </summary>
		public int star;
		/// <summary>
		/// 卡牌点数
		/// </summary>
		public int point;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string effect;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string image;
		/// <summary>
		/// 1;-自己;2;-目标;3;-群体;
		/// </summary>
		public int type;
	}
}