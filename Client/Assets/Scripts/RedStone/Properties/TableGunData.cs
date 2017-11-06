using UnityEngine;
using System;
using System.Collections;

namespace Hotfire
{
	public class TableGunData
	{
		public TableGunData() { }
		public TableGunData(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.name = (string)dict["name"];
			this.gunType = (int)dict["gunType"];
			this.initialPromote = (int)dict["initialPromote"];
			this.promoteMax = (int)dict["promoteMax"];
			this.unlockItemID = (int)dict["unlockItemID"];
			this.unlockItemCount = (int)dict["unlockItemCount"];
			this.showOrder = (int)dict["showOrder"];
			this.unlockProfessionLevel = (int)dict["unlockProfessionLevel"];
			this.traitGroupId = (int)dict["traitGroupId"];
			this.traitIcon = (int)dict["traitIcon"];
			this.traitName = (string)dict["traitName"];
			this.traitDesc = (string)dict["traitDesc"];
			this.energyIncreasePerHit = (float)dict["energyIncreasePerHit"];
			this.fireRate = (float)dict["fireRate"];
			this.oneShotBulletNumber = (int)dict["oneShotBulletNumber"];
			this.magazineCapacity = (int)dict["magazineCapacity"];
			this.rechargeCd = (float)dict["rechargeCd"];
			this.loadBulletNumOnce = (int)dict["loadBulletNumOnce"];
			this.gunPowerRatio = (float)dict["gunPowerRatio"];
			this.gunFirstLevel = (int)dict["gunFirstLevel"];
			this.abilityID = (int)dict["abilityID"];
			this.systemToAcquire = (int)dict["systemToAcquire"];
			this.shopImage = (string)dict["shopImage"];
		}

		/// <summary>
		/// ID
		/// </summary>
		public int id;
		/// <summary>
		/// Name
		/// </summary>
		public string name;
		/// <summary>
		/// 枪械类型
		/// </summary>
		public int gunType;
		/// <summary>
		/// 枪械初始Promote
		/// </summary>
		public int initialPromote;
		/// <summary>
		/// 枪械最大Promote
		/// </summary>
		public int promoteMax;
		/// <summary>
		/// 解锁所需图纸ID
		/// </summary>
		public int unlockItemID;
		/// <summary>
		/// 解锁所需图纸数量
		/// </summary>
		public int unlockItemCount;
		/// <summary>
		/// 显示顺序
		/// </summary>
		public int showOrder;
		/// <summary>
		/// 解锁条件 - 玩家等级
		/// </summary>
		public int unlockProfessionLevel;
		/// <summary>
		/// 特性
		/// </summary>
		public int traitGroupId;
		/// <summary>
		/// 特性图标
		/// </summary>
		public int traitIcon;
		/// <summary>
		/// 特性名字多语言
		/// </summary>
		public string traitName;
		/// <summary>
		/// 特性描述多语言
		/// </summary>
		public string traitDesc;
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
		/// <summary>
		/// 初始等级
		/// </summary>
		public int gunFirstLevel;
		/// <summary>
		/// 能力ID
		/// </summary>
		public int abilityID;
		/// <summary>
		/// 获取途径;1 ;- 解锁;2 ;- 商店购买
		/// </summary>
		public int systemToAcquire;
		/// <summary>
		/// 商店展示图标
		/// </summary>
		public string shopImage;
	}
}