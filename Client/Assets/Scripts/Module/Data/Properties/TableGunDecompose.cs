using UnityEngine;
using System;
using System.Collections;

 namespace RedStone
{
	public class TableGunDecompose
	{
		public TableGunDecompose() { }
		public TableGunDecompose(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.gunType = (int)dict["gunType"];
			this.promote = (int)dict["promote"];
			this.stage = (int)dict["stage"];
			this.gainsType = (int[])dict["gainsType"];
			this.gainsSubType = (int[])dict["gainsSubType"];
			this.gainsAmount = (int[])dict["gainsAmount"];
		}

		/// <summary>
		/// ID
		/// </summary>
		public int id;
		/// <summary>
		/// 枪械类型
		/// </summary>
		public int gunType;
		/// <summary>
		/// 枪械品质
		/// </summary>
		public int promote;
		/// <summary>
		/// 枪械Stage
		/// </summary>
		public int stage;
		/// <summary>
		/// 分解获得的物品类型
		/// </summary>
		public int[] gainsType;
		/// <summary>
		/// 分解获得的物品二级类型
		/// </summary>
		public int[] gainsSubType;
		/// <summary>
		/// 分解获得的物品数量
		/// </summary>
		public int[] gainsAmount;
	}
}