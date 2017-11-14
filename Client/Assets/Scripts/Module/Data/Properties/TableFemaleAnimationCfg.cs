using UnityEngine;
using System;
using System.Collections;

 namespace RedStone
{
	public class TableFemaleAnimationCfg
	{
		public TableFemaleAnimationCfg() { }
		public TableFemaleAnimationCfg(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.name = (string)dict["name"];
			this.weaponAnimatonType = (int)dict["weaponAnimatonType"];
			this.attachPoint = (string)dict["attachPoint"];
			this.ikDirection = (Vector3)dict["ikDirection"];
			this.aimCenterDownPath = (string)dict["aimCenterDownPath"];
			this.aimCenterCenterPath = (string)dict["aimCenterCenterPath"];
			this.aimCenterUpPath = (string)dict["aimCenterUpPath"];
			this.reloadBeginPath = (string)dict["reloadBeginPath"];
			this.reloadNormalPath = (string)dict["reloadNormalPath"];
			this.reloadEndPath = (string)dict["reloadEndPath"];
			this.aimReloadPath = (string)dict["aimReloadPath"];
			this.normalShootPath = (string)dict["normalShootPath"];
			this.normalIdleUpperPath = (string)dict["normalIdleUpperPath"];
			this.normalRunUpperPath = (string)dict["normalRunUpperPath"];
			this.hallIdle = (string)dict["hallIdle"];
			this.walkUpper = (string)dict["walkUpper"];
			this.chambering = (string)dict["chambering"];
		}

		/// <summary>
		/// 描述
		/// </summary>
		public int id;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string name;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public int weaponAnimatonType;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string attachPoint;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public Vector3 ikDirection;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string aimCenterDownPath;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string aimCenterCenterPath;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string aimCenterUpPath;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string reloadBeginPath;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string reloadNormalPath;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string reloadEndPath;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string aimReloadPath;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string normalShootPath;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string normalIdleUpperPath;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string normalRunUpperPath;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string hallIdle;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string walkUpper;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string chambering;
	}
}