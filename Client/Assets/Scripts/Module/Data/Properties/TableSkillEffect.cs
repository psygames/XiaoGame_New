using UnityEngine;
using System;
using System.Collections;

 namespace RedStone
{
	public class TableSkillEffect
	{
		public TableSkillEffect() { }
		public TableSkillEffect(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.upAction = (string)dict["upAction"];
			this.effectValue = (int)dict["effectValue"];
			this.effectPer = (int)dict["effectPer"];
			this.value1 = (string)dict["value1"];
			this.value2 = (int)dict["value2"];
			this.value3 = (int)dict["value3"];
			this.intervalTime = (int)dict["intervalTime"];
			this.intervalEffect = (string)dict["intervalEffect"];
			this.intervalValue = (int)dict["intervalValue"];
			this.intervalPer = (int)dict["intervalPer"];
		}

		/// <summary>
		/// effectID
		/// </summary>
		public int id;
		/// <summary>
		/// 上延
		/// </summary>
		public string upAction;
		/// <summary>
		/// 效果数值
		/// </summary>
		public int effectValue;
		/// <summary>
		/// 数值格式
		/// </summary>
		public int effectPer;
		/// <summary>
		/// value1
		/// </summary>
		public string value1;
		/// <summary>
		/// value2
		/// </summary>
		public int value2;
		/// <summary>
		/// value3
		/// </summary>
		public int value3;
		/// <summary>
		/// 间隔时间
		/// </summary>
		public int intervalTime;
		/// <summary>
		/// 间隔效果
		/// </summary>
		public string intervalEffect;
		/// <summary>
		/// 间隔效果数值
		/// </summary>
		public int intervalValue;
		/// <summary>
		/// 数值格式
		/// </summary>
		public int intervalPer;
	}
}