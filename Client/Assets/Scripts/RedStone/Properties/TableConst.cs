using UnityEngine;
using System;
using System.Collections;

namespace Hotfire
{
	public class TableConst
	{
		public TableConst() { }
		public TableConst(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.key = (string)dict["key"];
			this.type = (string)dict["type"];
			this.value = (string)dict["value"];
			this.description = (string)dict["description"];
		}

		/// <summary>
		/// id
		/// </summary>
		public int id;
		/// <summary>
		/// 键
		/// </summary>
		public string key;
		/// <summary>
		/// 类型
		/// </summary>
		public string type;
		/// <summary>
		/// 值
		/// </summary>
		public string value;
		/// <summary>
		/// 描述
		/// </summary>
		public string description;
	}
}