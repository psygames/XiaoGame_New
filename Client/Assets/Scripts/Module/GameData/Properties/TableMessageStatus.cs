using UnityEngine;
using System;
using System.Collections;

 namespace RedStone
{
	public class TableMessageStatus
	{
		public TableMessageStatus() { }
		public TableMessageStatus(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.name = (string)dict["name"];
			this.nameID = (string)dict["nameID"];
			this.displayType = (int)dict["displayType"];
			this.triggerEvent = (string)dict["triggerEvent"];
		}

		/// <summary>
		/// id
		/// </summary>
		public int id;
		/// <summary>
		/// 描述
		/// </summary>
		public string name;
		/// <summary>
		/// 显示字符串
		/// </summary>
		public string nameID;
		/// <summary>
		/// 显示类型（0：对话框,1：toast）
		/// </summary>
		public int displayType;
		/// <summary>
		/// 触发的事件(前缀Event.Error.，定义在RedStone.Event.Error.cs)
		/// </summary>
		public string triggerEvent;
	}
}