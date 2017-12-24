using UnityEngine;
using System;
using System.Collections;

namespace RedStone
{
	public class TableSeasonTimes
	{
		public TableSeasonTimes() { }
		public TableSeasonTimes(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.warInterval = (int)dict["warInterval"];
			this.truceInterval = (int)dict["truceInterval"];
			this.beginTimes = (DateTime)dict["beginTimes"];
			this.endTimes = (DateTime)dict["endTimes"];
		}

		/// <summary>
		/// 周期
		/// </summary>
		public int id;
		/// <summary>
		/// 赛季战斗期间持续时间（单位 m）
		/// </summary>
		public int warInterval;
		/// <summary>
		/// 赛季休战期持续时间（单位：m）
		/// </summary>
		public int truceInterval;
		/// <summary>
		/// 开始时间
		/// </summary>
		public DateTime beginTimes;
		/// <summary>
		/// 结束时间
		/// </summary>
		public DateTime endTimes;
	}
}