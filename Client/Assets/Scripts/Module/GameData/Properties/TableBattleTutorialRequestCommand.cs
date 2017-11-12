using UnityEngine;
using System;
using System.Collections;

 namespace RedStone
{
	public class TableBattleTutorialRequestCommand
	{
		public TableBattleTutorialRequestCommand() { }
		public TableBattleTutorialRequestCommand(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.requestType = (int)dict["requestType"];
			this.commandType = (int)dict["commandType"];
			this.commandDataType = (string)dict["commandDataType"];
			this.commandData = (string)dict["commandData"];
			this.description = (string)dict["description"];
		}

		/// <summary>
		/// ID
		/// </summary>
		public int id;
		/// <summary>
		/// 请求类型枚举
		/// </summary>
		public int requestType;
		/// <summary>
		/// 命令类型
		/// </summary>
		public int commandType;
		/// <summary>
		/// 命令数据类型
		/// </summary>
		public string commandDataType;
		/// <summary>
		/// 命令数据
		/// </summary>
		public string commandData;
		/// <summary>
		/// 数据意义
		/// </summary>
		public string description;
	}
}