using UnityEngine;
using System;
using System.Collections;

 namespace RedStone
{
	public class TableSkillRelation
	{
		public TableSkillRelation() { }
		public TableSkillRelation(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.groupID = (int)dict["groupID"];
			this.promote = (int)dict["promote"];
			this.level = (int)dict["level"];
		}

		/// <summary>
		/// skillId
		/// </summary>
		public int id;
		/// <summary>
		/// 技能组ID
		/// </summary>
		public int groupID;
		/// <summary>
		/// 晋升
		/// </summary>
		public int promote;
		/// <summary>
		/// 技能等级
		/// </summary>
		public int level;
	}
}