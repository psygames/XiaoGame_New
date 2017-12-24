using UnityEngine;
using System;
using System.Collections;

namespace RedStone
{
	public class TableTextColor
	{
		public TableTextColor() { }
		public TableTextColor(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.desc = (string)dict["desc"];
			this.name = (string)dict["name"];
			this.value = (Color)dict["value"];
			this.order = (float)dict["order"];
		}

		/// <summary>
		/// 文本颜色ID
		/// </summary>
		public int id;
		/// <summary>
		/// 文本颜色名
		/// </summary>
		public string desc;
		/// <summary>
		/// 文本颜色名
		/// </summary>
		public string name;
		/// <summary>
		/// 色值(可填6位或8位，可以以#开头)
		/// </summary>
		public Color value;
		/// <summary>
		/// sort order
		/// </summary>
		public float order;
	}
}