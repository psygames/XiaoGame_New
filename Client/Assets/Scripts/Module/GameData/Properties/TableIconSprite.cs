using UnityEngine;
using System;
using System.Collections;

 namespace RedStone
{
	public class TableIconSprite
	{
		public TableIconSprite() { }
		public TableIconSprite(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.type = (int)dict["type"];
			this.path = (string)dict["path"];
			this.description = (string)dict["description"];
		}

		/// <summary>
		/// ID
		/// </summary>
		public int id;
		/// <summary>
		/// 图片资源类型(图集填1，单个的图填2)
		/// </summary>
		public int type;
		/// <summary>
		/// 路径
		/// </summary>
		public string path;
		/// <summary>
		/// 描述信息
		/// </summary>
		public string description;
	}
}