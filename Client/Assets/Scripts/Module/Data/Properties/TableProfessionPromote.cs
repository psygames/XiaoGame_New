using UnityEngine;
using System;
using System.Collections;

 namespace RedStone
{
	public class TableProfessionPromote
	{
		public TableProfessionPromote() { }
		public TableProfessionPromote(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.professionId = (int)dict["professionId"];
			this.promote = (int)dict["promote"];
			this.name = (string)dict["name"];
			this.nameID = (string)dict["nameID"];
			this.icon = (int)dict["icon"];
			this.promoteCostType = (int[])dict["promoteCostType"];
			this.promoteCostSubType = (int[])dict["promoteCostSubType"];
			this.promoteCostAmount = (int[])dict["promoteCostAmount"];
			this.mulHp = (float)dict["mulHp"];
			this.mulDefense = (float)dict["mulDefense"];
			this.mulShield = (float)dict["mulShield"];
			this.mulShieldRecover = (float)dict["mulShieldRecover"];
			this.detailInfo = (string)dict["detailInfo"];
		}

		/// <summary>
		/// id
		/// </summary>
		public int id;
		/// <summary>
		/// int
		/// </summary>
		public int professionId;
		/// <summary>
		/// 晋升等级
		/// </summary>
		public int promote;
		/// <summary>
		/// 名字
		/// </summary>
		public string name;
		/// <summary>
		/// 名字
		/// </summary>
		public string nameID;
		/// <summary>
		/// 英雄职业图标
		/// </summary>
		public int icon;
		/// <summary>
		/// 职业晋升到下一军衔消耗类型
		/// </summary>
		public int[] promoteCostType;
		/// <summary>
		/// 职业晋升到下一军衔消耗小类型
		/// </summary>
		public int[] promoteCostSubType;
		/// <summary>
		/// 职业晋升到下一军衔消耗数量
		/// </summary>
		public int[] promoteCostAmount;
		/// <summary>
		/// 生命值
		/// </summary>
		public float mulHp;
		/// <summary>
		/// 防御力
		/// </summary>
		public float mulDefense;
		/// <summary>
		/// 护盾
		/// </summary>
		public float mulShield;
		/// <summary>
		/// 护盾回复速度
		/// </summary>
		public float mulShieldRecover;
		/// <summary>
		/// 多语言
		/// </summary>
		public string detailInfo;
	}
}