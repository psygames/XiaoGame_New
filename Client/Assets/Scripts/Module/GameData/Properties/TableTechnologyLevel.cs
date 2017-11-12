using UnityEngine;
using System.Collections;

 namespace RedStone
{
	public class TableTechnologyLevel
	{
		public TableTechnologyLevel(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.level = (int)dict["level"];
			this.levelUpExp = (int)dict["levelUpExp"];
			this.boxId = (int)dict["boxId"];
		}

		/// <summary>
		/// id
		/// </summary>
		public int id;
		/// <summary>
		/// 等级
		/// </summary>
		public int level;
		/// <summary>
		/// 升到下一级所需经验。
		/// </summary>
		public int levelUpExp;
		/// <summary>
		/// 免费箱子ID
		/// </summary>
		public int boxId;
	}
}