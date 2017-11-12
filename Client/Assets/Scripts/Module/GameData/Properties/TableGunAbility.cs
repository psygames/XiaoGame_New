using UnityEngine;
using System;
using System.Collections;

 namespace RedStone
{
	public class TableGunAbility
	{
		public TableGunAbility() { }
		public TableGunAbility(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.name = (string)dict["name"];
			this.energyIncreasePerHit = (float)dict["energyIncreasePerHit"];
			this.fireRate = (float)dict["fireRate"];
			this.oneShotBulletNumber = (int)dict["oneShotBulletNumber"];
			this.magazineCapacity = (int)dict["magazineCapacity"];
			this.rechargeCd = (float)dict["rechargeCd"];
			this.loadBulletNumOnce = (int)dict["loadBulletNumOnce"];
			this.gunPowerRatio = (float)dict["gunPowerRatio"];
		}

		/// <summary>
		/// ID
		/// </summary>
		public int id;
		/// <summary>
		/// 描述
		/// </summary>
		public string name;
		/// <summary>
		/// 每次命中敌人，增加能量数值
		/// </summary>
		public float energyIncreasePerHit;
		/// <summary>
		/// 射速
		/// </summary>
		public float fireRate;
		/// <summary>
		/// 单发子弹数
		/// </summary>
		public int oneShotBulletNumber;
		/// <summary>
		/// 弹夹容量，单位：发。弹夹最大容量，决定一次最多打多少发子弹后上弹。
		/// </summary>
		public int magazineCapacity;
		/// <summary>
		/// 换弹时间，单位：秒。Cs当中步枪大概是3秒多一点。
		/// </summary>
		public float rechargeCd;
		/// <summary>
		/// 上弹一次，上弹的子弹数量，-1表示上弹一个弹夹，其他数字表示一次上弹的真实子弹数
		/// </summary>
		public int loadBulletNumOnce;
		/// <summary>
		/// 枪械战力系数
		/// </summary>
		public float gunPowerRatio;
	}
}