using UnityEngine;
using System;
using System.Collections;

namespace RedStone
{
	public class TableBattleDrop
	{
		public TableBattleDrop() { }
		public TableBattleDrop(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.name = (string)dict["name"];
			this.nameId = (string)dict["nameId"];
			this.type = (int)dict["type"];
			this.subType = (int)dict["subType"];
			this.lastTime = (int)dict["lastTime"];
			this.effectId = (int)dict["effectId"];
			this.airDropValue = (int)dict["airDropValue"];
			this.airDropRate = (int)dict["airDropRate"];
			this.deadDropValue = (int)dict["deadDropValue"];
			this.deadDropRate = (int)dict["deadDropRate"];
			this.modelPath = (string)dict["modelPath"];
			this.audioPath = (int)dict["audioPath"];
			this.battleHintImageId = (int)dict["battleHintImageId"];
			this.skillId = (int)dict["skillId"];
		}

		/// <summary>
		/// id
		/// </summary>
		public int id;
		/// <summary>
		/// key
		/// </summary>
		public string name;
		/// <summary>
		/// 多语言ID
		/// </summary>
		public string nameId;
		/// <summary>
		/// 1普通掉落，2超级掉落,3资源掉落, 4固定掉落
		/// </summary>
		public int type;
		/// <summary>
		/// 二级类型，如： 资源--小（1），资源--大（2）
		/// </summary>
		public int subType;
		/// <summary>
		/// 持续时间
		/// </summary>
		public int lastTime;
		/// <summary>
		/// 调用
		/// </summary>
		public int effectId;
		/// <summary>
		/// 值
		/// </summary>
		public int airDropValue;
		/// <summary>
		/// 概率
		/// </summary>
		public int airDropRate;
		/// <summary>
		/// 值
		/// </summary>
		public int deadDropValue;
		/// <summary>
		/// 概率
		/// </summary>
		public int deadDropRate;
		/// <summary>
		/// 模型路径
		/// </summary>
		public string modelPath;
		/// <summary>
		/// 音效路径
		/// </summary>
		public int audioPath;
		/// <summary>
		/// 战场提示图标ID
		/// </summary>
		public int battleHintImageId;
		/// <summary>
		/// 调用的技能id，如果为0则不调用技能。
		/// </summary>
		public int skillId;
	}
}