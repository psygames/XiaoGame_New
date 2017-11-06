using UnityEngine;
using System.Collections;

namespace Hotfire
{
	public class TableSkillChange
	{
		public TableSkillChange(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.quondamSkillGroupID = (int)dict["quondamSkillGroupID"];
			this.implantID = (int)dict["implantID"];
			this.newSkillGroupID = (int)dict["newSkillGroupID"];
		}

		/// <summary>
		/// ID
		/// </summary>
		public int id;
		/// <summary>
		/// 原技能ID
		/// </summary>
		public int quondamSkillGroupID;
		/// <summary>
		/// 植入体ID
		/// </summary>
		public int implantID;
		/// <summary>
		/// 新技能ID
		/// </summary>
		public int newSkillGroupID;
	}
}