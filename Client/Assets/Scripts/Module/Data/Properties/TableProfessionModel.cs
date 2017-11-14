using UnityEngine;
using System;
using System.Collections;

 namespace RedStone
{
	public class TableProfessionModel
	{
		public TableProfessionModel() { }
		public TableProfessionModel(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.professionID = (int)dict["professionID"];
			this.skinID = (int)dict["skinID"];
			this.name = (string)dict["name"];
			this.nameId = (string)dict["nameId"];
			this.descriptionId = (string)dict["descriptionId"];
			this.uiModelPath = (string)dict["uiModelPath"];
			this.battleModelPath1P = (string)dict["battleModelPath1P"];
			this.battleModelPath3P = (string)dict["battleModelPath3P"];
			this.imageID = (int)dict["imageID"];
			this.audioPath = (int)dict["audioPath"];
			this.homeModelShowPos = (Vector3)dict["homeModelShowPos"];
			this.homeModelShowRot = (Vector3)dict["homeModelShowRot"];
			this.matchModelShowPos = (Vector3)dict["matchModelShowPos"];
			this.matchModelShowRot = (Vector3)dict["matchModelShowRot"];
			this.heroSelectModelShowPos = (Vector3)dict["heroSelectModelShowPos"];
			this.heroSelectModelShowRot = (Vector3)dict["heroSelectModelShowRot"];
		}

		/// <summary>
		/// id
		/// </summary>
		public int id;
		/// <summary>
		/// 职业ID
		/// </summary>
		public int professionID;
		/// <summary>
		/// 阵营ID
		/// </summary>
		public int skinID;
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
		/// <summary>
		/// 大厅模型展示位置
		/// </summary>
		public Vector3 homeModelShowPos;
		/// <summary>
		/// 大厅模型展示旋转
		/// </summary>
		public Vector3 homeModelShowRot;
		/// <summary>
		/// 匹配界面模型展示位置
		/// </summary>
		public Vector3 matchModelShowPos;
		/// <summary>
		/// 匹配界面模型展示旋转
		/// </summary>
		public Vector3 matchModelShowRot;
		/// <summary>
		/// 英雄选择模型展示位置
		/// </summary>
		public Vector3 heroSelectModelShowPos;
		/// <summary>
		/// 英雄选择模型展示旋转
		/// </summary>
		public Vector3 heroSelectModelShowRot;
	}
}