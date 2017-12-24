using UnityEngine;
using System;
using System.Collections;

namespace RedStone
{
	public class TableMode
	{
		public TableMode() { }
		public TableMode(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.name = (string)dict["name"];
			this.nameID = (string)dict["nameID"];
			this.descriptionID = (string)dict["descriptionID"];
			this.imageID = (int)dict["imageID"];
		}

		/// <summary>
		/// modeID
		/// </summary>
		public int id;
		/// <summary>
		/// name
		/// </summary>
		public string name;
		/// <summary>
		/// 多语言ID
		/// </summary>
		public string nameID;
		/// <summary>
		/// 描述ID
		/// </summary>
		public string descriptionID;
		/// <summary>
		/// 图片
		/// </summary>
		public int imageID;
	}
}