using UnityEngine;
using System;
using System.Collections;

 namespace RedStone
{
	public class TableGunAudio
	{
		public TableGunAudio() { }
		public TableGunAudio(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.zhushi = (string)dict["zhushi"];
			this.fireID = (int)dict["fireID"];
			this.headShot = (int)dict["headShot"];
			this.bodyShot = (int)dict["bodyShot"];
			this.clipOutID = (int)dict["clipOutID"];
			this.clipInID = (int)dict["clipInID"];
			this.slideID = (int)dict["slideID"];
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
		/// 开火音效id
		/// </summary>
		public int fireID;
		/// <summary>
		/// 爆头音效
		/// </summary>
		public int headShot;
		/// <summary>
		/// 打中身体音效
		/// </summary>
		public int bodyShot;
		/// <summary>
		/// 弹夹拔出
		/// </summary>
		public int clipOutID;
		/// <summary>
		/// 弹夹插入
		/// </summary>
		public int clipInID;
		/// <summary>
		/// 上膛
		/// </summary>
		public int slideID;
	}
}