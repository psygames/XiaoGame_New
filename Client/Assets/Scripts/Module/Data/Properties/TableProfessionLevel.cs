using UnityEngine;
using System;
using System.Collections;

 namespace RedStone
{
	public class TableProfessionLevel
	{
		public TableProfessionLevel() { }
		public TableProfessionLevel(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.profession = (int)dict["profession"];
			this.level = (int)dict["level"];
			this.stage = (int)dict["stage"];
			this.nextExp = (int)dict["nextExp"];
			this.winExp = (int)dict["winExp"];
			this.loseExp = (int)dict["loseExp"];
			this.drawExp = (int)dict["drawExp"];
			this.costFund = (int)dict["costFund"];
			this.suppliesID = (int)dict["suppliesID"];
			this.suppliesNum = (int)dict["suppliesNum"];
			this.hp = (float)dict["hp"];
			this.defense = (float)dict["defense"];
			this.shield = (float)dict["shield"];
			this.shieldRecover = (float)dict["shieldRecover"];
			this.shieldRecoverDelay = (int)dict["shieldRecoverDelay"];
		}

		/// <summary>
		/// ID
		/// </summary>
		public int id;
		/// <summary>
		/// 所属职业
		/// </summary>
		public int profession;
		/// <summary>
		/// 等级
		/// </summary>
		public int level;
		/// <summary>
		/// 阶段
		/// </summary>
		public int stage;
		/// <summary>
		/// 升下一级所需经验
		/// </summary>
		public int nextExp;
		/// <summary>
		/// 胜利经验
		/// </summary>
		public int winExp;
		/// <summary>
		/// 失败经验
		/// </summary>
		public int loseExp;
		/// <summary>
		/// 平局经验
		/// </summary>
		public int drawExp;
		/// <summary>
		/// 下一级军费
		/// </summary>
		public int costFund;
		/// <summary>
		/// 下一级补给品ID
		/// </summary>
		public int suppliesID;
		/// <summary>
		/// 补给品数量
		/// </summary>
		public int suppliesNum;
		/// <summary>
		/// 基础生命
		/// </summary>
		public float hp;
		/// <summary>
		/// 基础防御
		/// </summary>
		public float defense;
		/// <summary>
		/// 护盾
		/// </summary>
		public float shield;
		/// <summary>
		/// 护盾回血
		/// </summary>
		public float shieldRecover;
		/// <summary>
		/// 护盾恢复延迟
		/// </summary>
		public int shieldRecoverDelay;
	}
}