using UnityEngine;
using System;
using System.Collections;

 namespace RedStone
{
	public class TableAnimationCfg
	{
		public TableAnimationCfg() { }
		public TableAnimationCfg(IDictionary dict)
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
			this.normalIdle = (string)dict["normalIdle"];
			this.normalRun = (string)dict["normalRun"];
			this.hallIdle = (string)dict["hallIdle"];
			this.walk = (string)dict["walk"];
			this.chambering = (string)dict["chambering"];
			this.aimPoseDown = (string)dict["aimPoseDown"];
			this.aimWalkFrontDown = (string)dict["aimWalkFrontDown"];
			this.aimWalkLeftDown = (string)dict["aimWalkLeftDown"];
			this.aimWalkRightDown = (string)dict["aimWalkRightDown"];
			this.aimWalkBackDown = (string)dict["aimWalkBackDown"];
			this.runBack = (string)dict["runBack"];
			this.runBack_Left = (string)dict["runBack_Left"];
			this.runLeft = (string)dict["runLeft"];
			this.runRight = (string)dict["runRight"];
			this.beginRun = (string)dict["beginRun"];
			this.stopRun = (string)dict["stopRun"];
			this.crossOver = (string)dict["crossOver"];
			this.weaponIdle = (string)dict["weaponIdle"];
			this.weaponReload = (string)dict["weaponReload"];
			this.weaponFire = (string)dict["weaponFire"];
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
		public string normalIdle;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string normalRun;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string hallIdle;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string walk;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string chambering;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string aimPoseDown;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string aimWalkFrontDown;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string aimWalkLeftDown;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string aimWalkRightDown;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string aimWalkBackDown;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string runBack;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string runBack_Left;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string runLeft;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string runRight;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string beginRun;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string stopRun;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string crossOver;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string weaponIdle;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string weaponReload;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string weaponFire;
	}
}