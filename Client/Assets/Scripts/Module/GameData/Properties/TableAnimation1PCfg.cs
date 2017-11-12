using UnityEngine;
using System.Collections;

 namespace RedStone
{
	public class TableAnimation1PCfg
	{
		public TableAnimation1PCfg(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.name = (string)dict["name"];
			this.aimCenterCenter = (string)dict["aimCenterCenter"];
			this.aimFire = (string)dict["aimFire"];
			this.normalReload = (string)dict["normalReload"];
			this.normalIdle = (string)dict["normalIdle"];
			this.normalRun = (string)dict["normalRun"];
			this.changeWeapon = (string)dict["changeWeapon"];
			this.normalCenterCenter = (string)dict["normalCenterCenter"];
			this.normalDownCenter = (string)dict["normalDownCenter"];
			this.normalUpCenter = (string)dict["normalUpCenter"];
			this.normalUpLeft = (string)dict["normalUpLeft"];
			this.normalCenterLeft = (string)dict["normalCenterLeft"];
			this.normalDownLeft = (string)dict["normalDownLeft"];
			this.normalUpRight = (string)dict["normalUpRight"];
			this.normalCenterRight = (string)dict["normalCenterRight"];
			this.normalDownRight = (string)dict["normalDownRight"];
			this.jump = (string)dict["jump"];
			this.normalIdleWeapon = (string)dict["normalIdleWeapon"];
			this.sprint = (string)dict["sprint"];
		}

		/// <summary>
		/// 描述
		/// </summary>
		public int id;
		/// <summary>
		/// 瞄准姿势
		/// </summary>
		public string name;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string aimCenterCenter;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string aimFire;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string normalReload;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string normalIdle;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string normalRun;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string changeWeapon;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string normalCenterCenter;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string normalDownCenter;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string normalUpCenter;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string normalUpLeft;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string normalCenterLeft;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string normalDownLeft;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string normalUpRight;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string normalCenterRight;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string normalDownRight;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string jump;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string normalIdleWeapon;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string sprint;
	}
}