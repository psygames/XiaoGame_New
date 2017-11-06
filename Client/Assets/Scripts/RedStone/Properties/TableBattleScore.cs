using UnityEngine;
using System;
using System.Collections;

namespace Hotfire
{
	public class TableBattleScore
	{
		public TableBattleScore() { }
		public TableBattleScore(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.name = (string)dict["name"];
			this.score = (int[])dict["score"];
			this.target = (int)dict["target"];
			this.isPlural = (int)dict["isPlural"];
			this.professionID = (int)dict["professionID"];
			this.buffGroupId = (int)dict["buffGroupId"];
			this.motionType = (int)dict["motionType"];
			this.itemID = (int)dict["itemID"];
		}

		/// <summary>
		/// id
		/// </summary>
		public int id;
		/// <summary>
		/// 得分途径名字
		/// </summary>
		public string name;
		/// <summary>
		/// 各模式下得分，不计算填0;模式ID,1-Time，2-Kill，3-Conflict，4-Resource，5-AttackDefense
		/// </summary>
		public int[] score;
		/// <summary>
		/// 目标，0为自己，1为全队
		/// </summary>
		public int target;
		/// <summary>
		/// 是否单次计算,0为单次，1为复数
		/// </summary>
		public int isPlural;
		/// <summary>
		/// 职业ID，无限制填0
		/// </summary>
		public int professionID;
		/// <summary>
		/// buff分组ID，无限制填0
		/// </summary>
		public int buffGroupId;
		/// <summary>
		/// motion类型ID，无限制填0
		/// </summary>
		public int motionType;
		/// <summary>
		/// 拾取道具ID，无限制填0
		/// </summary>
		public int itemID;
	}
}