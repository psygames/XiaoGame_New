using UnityEngine;
using System;
using System.Collections;

namespace RedStone
{
	public class TableMap
	{
		public TableMap() { }
		public TableMap(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.name = (string)dict["name"];
			this.imagePath = (string)dict["imagePath"];
			this.nameId = (string)dict["nameId"];
			this.levelHeight = (float)dict["levelHeight"];
			this.destructibleItemIdArray = (int[])dict["destructibleItemIdArray"];
			this.image = (string)dict["image"];
		}

		/// <summary>
		/// id
		/// </summary>
		public int id;
		/// <summary>
		/// name
		/// </summary>
		public string name;
		/// <summary>
		/// 加载背景图片
		/// </summary>
		public string imagePath;
		/// <summary>
		/// 多语言ID
		/// </summary>
		public string nameId;
		/// <summary>
		/// 海拔高度
		/// </summary>
		public float levelHeight;
		/// <summary>
		/// 破坏物id
		/// </summary>
		public int[] destructibleItemIdArray;
		/// <summary>
		/// image name
		/// </summary>
		public string image;
	}
}