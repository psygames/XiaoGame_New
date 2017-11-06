using UnityEngine;
using System;
using System.Collections;

namespace Hotfire
{
	public class TableLegionBadge
	{
		public TableLegionBadge() { }
		public TableLegionBadge(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.badge = (int)dict["badge"];
			this.bigBadgeID = (int)dict["bigBadgeID"];
		}

		/// <summary>
		/// ID
		/// </summary>
		public int id;
		/// <summary>
		/// 徽章
		/// </summary>
		public int badge;
		/// <summary>
		/// 大徽章
		/// </summary>
		public int bigBadgeID;
	}
}