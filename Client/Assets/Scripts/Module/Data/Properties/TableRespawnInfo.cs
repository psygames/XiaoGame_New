using UnityEngine;
using System;
using System.Collections;

 namespace RedStone
{
	public class TableRespawnInfo
	{
		public TableRespawnInfo() { }
		public TableRespawnInfo(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.SceneName = (string)dict["SceneName"];
			this.Pos = (Vector3)dict["Pos"];
			this.Direction = (Vector3)dict["Direction"];
		}

		/// <summary>
		/// id
		/// </summary>
		public int id;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string SceneName;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public Vector3 Pos;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public Vector3 Direction;
	}
}