using UnityEngine;
using System;
using System.Collections;

namespace RedStone
{
	public class TableGacha
	{
		public TableGacha() { }
		public TableGacha(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.chestType = (int)dict["chestType"];
			this.chestID = (int[])dict["chestID"];
			this.tier = (int)dict["tier"];
			this.profession = (int)dict["profession"];
		}

		/// <summary>
		/// ID
		/// </summary>
		public int id;
		/// <summary>
		/// 箱子类型
		/// </summary>
		public int chestType;
		/// <summary>
		/// 箱子ID
		/// </summary>
		public int[] chestID;
		/// <summary>
		/// 所属tier
		/// </summary>
		public int tier;
		/// <summary>
		/// 所属职业
		/// </summary>
		public int profession;
	}
}