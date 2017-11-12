using UnityEngine;
using System;
using System.Collections;

 namespace RedStone
{
	public class TableDestructibleItem
	{
		public TableDestructibleItem() { }
		public TableDestructibleItem(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.name = (string)dict["name"];
			this.nameID = (string)dict["nameID"];
			this.maxHp = (int)dict["maxHp"];
			this.brokenStatusHpPercent = (int)dict["brokenStatusHpPercent"];
			this.heavyBrokenHpDropPerSecond = (int)dict["heavyBrokenHpDropPerSecond"];
			this.modelRadius = (float)dict["modelRadius"];
			this.workSkillId = (int)dict["workSkillId"];
			this.modelPos = (Vector3)dict["modelPos"];
		}

		/// <summary>
		/// id
		/// </summary>
		public int id;
		/// <summary>
		/// 名字
		/// </summary>
		public string name;
		/// <summary>
		/// 击杀消息使用的文本，多语言id
		/// </summary>
		public string nameID;
		/// <summary>
		/// 最大血量
		/// </summary>
		public int maxHp;
		/// <summary>
		/// 轻度到重度损伤时，hp线，数值代表百分比。 如果数值为0，则意味着没有重度损伤状态 填100，则意味着没有轻度损伤状态
		/// </summary>
		public int brokenStatusHpPercent;
		/// <summary>
		/// 重度破损状态下， 每秒损失的血量，百分比
		/// </summary>
		public int heavyBrokenHpDropPerSecond;
		/// <summary>
		/// 模型包围球的半径，用作服务器命中验证
		/// </summary>
		public float modelRadius;
		/// <summary>
		/// 运作skill Id
		/// </summary>
		public int workSkillId;
		/// <summary>
		/// 可破坏物模型中心点坐标
		/// </summary>
		public Vector3 modelPos;
	}
}