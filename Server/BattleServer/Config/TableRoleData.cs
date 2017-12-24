using UnityEngine;
using System;
using System.Collections;

namespace RedStone
{
	public class TableRoleData
	{
		public TableRoleData() { }
		public TableRoleData(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.name = (string)dict["name"];
			this.nameId = (string)dict["nameId"];
			this.descriptionId = (string)dict["descriptionId"];
			this.uiModelPath = (string)dict["uiModelPath"];
			this.battleModelPath1P = (string)dict["battleModelPath1P"];
			this.battleModelPath3P = (string)dict["battleModelPath3P"];
			this.imageID = (int)dict["imageID"];
			this.audioPath = (int)dict["audioPath"];
		}

		/// <summary>
		/// id
		/// </summary>
		public int id;
		/// <summary>
		/// 角色名
		/// </summary>
		public string name;
		/// <summary>
		/// 多语言Id
		/// </summary>
		public string nameId;
		/// <summary>
		/// 角色描述说明
		/// </summary>
		public string descriptionId;
		/// <summary>
		/// 模型路径
		/// </summary>
		public string uiModelPath;
		/// <summary>
		/// 模型路径
		/// </summary>
		public string battleModelPath1P;
		/// <summary>
		/// 模型路径
		/// </summary>
		public string battleModelPath3P;
		/// <summary>
		/// 图片
		/// </summary>
		public int imageID;
		/// <summary>
		/// 音效
		/// </summary>
		public int audioPath;
	}
}