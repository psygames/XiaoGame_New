using UnityEngine;
using System;
using System.Collections;

namespace Hotfire
{
	public class TableResourceApproach
	{
		public TableResourceApproach() { }
		public TableResourceApproach(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.name = (string)dict["name"];
			this.nameId = (string)dict["nameId"];
			this.approach = (string)dict["approach"];
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
		/// 多语言ID
		/// </summary>
		public string nameId;
		/// <summary>
		/// 获取途径(对应UI Emoudle名字，随程序会有所变动，只显示途径，无UI可填None,其他会做特殊处理)
		/// </summary>
		public string approach;
	}
}