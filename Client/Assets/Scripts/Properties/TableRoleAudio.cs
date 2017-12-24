using UnityEngine;
using System;
using System.Collections;

namespace RedStone
{
	public class TableRoleAudio
	{
		public TableRoleAudio() { }
		public TableRoleAudio(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.zhushi = (string)dict["zhushi"];
			this.dead = (int)dict["dead"];
			this.jumpBegin = (int)dict["jumpBegin"];
			this.jumpLand = (int)dict["jumpLand"];
			this.hurtFire = (int)dict["hurtFire"];
			this.stepLeftStone = (int)dict["stepLeftStone"];
			this.stepRightStone = (int)dict["stepRightStone"];
			this.stepLeftWood = (int)dict["stepLeftWood"];
			this.stepRightWood = (int)dict["stepRightWood"];
			this.stepLeftMetal = (int)dict["stepLeftMetal"];
			this.stepRightMetal = (int)dict["stepRightMetal"];
			this.stepLeftGlass = (int)dict["stepLeftGlass"];
			this.stepRightGlass = (int)dict["stepRightGlass"];
			this.changeGun = (int)dict["changeGun"];
			this.melee = (int)dict["melee"];
			this.meleeHit = (int)dict["meleeHit"];
		}

		/// <summary>
		/// 没有描述信息
		/// </summary>
		public int id;
		/// <summary>
		/// 注释
		/// </summary>
		public string zhushi;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public int dead;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public int jumpBegin;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public int jumpLand;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public int hurtFire;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public int stepLeftStone;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public int stepRightStone;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public int stepLeftWood;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public int stepRightWood;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public int stepLeftMetal;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public int stepRightMetal;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public int stepLeftGlass;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public int stepRightGlass;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public int changeGun;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public int melee;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public int meleeHit;
	}
}