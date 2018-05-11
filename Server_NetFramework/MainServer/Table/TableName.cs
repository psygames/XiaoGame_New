using UnityEngine;
using System;
using System.Collections;

namespace RedStone
{
	public class TableName
	{
		public TableName() { }
		public TableName(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.name = (string)dict["name"];
		}

		/// <summary>
		/// 箱子ID
		/// </summary>
		public int id;
		/// <summary>
		/// 名字
		/// </summary>
		public string name;
	}
}