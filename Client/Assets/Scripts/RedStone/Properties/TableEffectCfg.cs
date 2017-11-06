using UnityEngine;
using System;
using System.Collections;

namespace Hotfire
{
	public class TableEffectCfg
	{
		public TableEffectCfg() { }
		public TableEffectCfg(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.name = (string)dict["name"];
			this.prefabPath = (string)dict["prefabPath"];
			this.offsetVector3 = (Vector3)dict["offsetVector3"];
			this.keepTime = (float)dict["keepTime"];
			this.AttachPoint = (string)dict["AttachPoint"];
			this.forward = (string)dict["forward"];
			this.loop = (int)dict["loop"];
			this.radius = (float)dict["radius"];
			this.needScale = (int)dict["needScale"];
			this.scaleFactor = (float)dict["scaleFactor"];
		}

		/// <summary>
		/// 没有描述信息
		/// </summary>
		public int id;
		/// <summary>
		/// 特效名
		/// </summary>
		public string name;
		/// <summary>
		/// 特效路径
		/// </summary>
		public string prefabPath;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public Vector3 offsetVector3;
		/// <summary>
		/// 存在时间，0为无限，单位s
		/// </summary>
		public float keepTime;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string AttachPoint;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string forward;
		/// <summary>
		/// 是否循环播放，1循环，0不循环
		/// </summary>
		public int loop;
		/// <summary>
		/// 特效大小
		/// </summary>
		public float radius;
		/// <summary>
		/// 1需要缩放，0不需要缩放
		/// </summary>
		public int needScale;
		/// <summary>
		/// 特效缩放倍数，计算方式为：原始大小*倍数
		/// </summary>
		public float scaleFactor;
	}
}