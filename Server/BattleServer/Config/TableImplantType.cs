using UnityEngine;
using System;
using System.Collections;

namespace RedStone
{
	public class TableImplantType
	{
		public TableImplantType() { }
		public TableImplantType(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.implantTypeName = (string)dict["implantTypeName"];
			this.implantTypeDesc = (string)dict["implantTypeDesc"];
			this.skillGroupID = (int)dict["skillGroupID"];
		}

		/// <summary>
		/// 植入体的类型
		/// </summary>
		public int id;
		/// <summary>
		/// 植入体类型名字
		/// </summary>
		public string implantTypeName;
		/// <summary>
		/// 植入体类型描述
		/// </summary>
		public string implantTypeDesc;
		/// <summary>
		/// 技能组ID
		/// </summary>
		public int skillGroupID;
	}
}