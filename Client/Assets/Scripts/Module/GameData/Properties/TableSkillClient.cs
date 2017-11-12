using UnityEngine;
using System.Collections;

 namespace RedStone
{
	public class TableSkillClient
	{
		public TableSkillClient(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.name = (string)dict["name"];
			this.description = (string)dict["description"];
			this.nameId = (string)dict["nameId"];
			this.descriptionId = (string)dict["descriptionId"];
			this.icon = (int)dict["icon"];
			this.castEffect = (string)dict["castEffect"];
			this.sound = (int)dict["sound"];
		}

		/// <summary>
		/// ID
		/// </summary>
		public int id;
		/// <summary>
		/// name
		/// </summary>
		public string name;
		/// <summary>
		/// 说明
		/// </summary>
		public string description;
		/// <summary>
		/// 多语言ID
		/// </summary>
		public string nameId;
		/// <summary>
		/// 多语言ID
		/// </summary>
		public string descriptionId;
		/// <summary>
		/// 图标(图集)
		/// </summary>
		public int icon;
		/// <summary>
		/// 吟唱特效(特效表id)
		/// </summary>
		public string castEffect;
		/// <summary>
		/// 音效(音效表id)
		/// </summary>
		public int sound;
	}
}