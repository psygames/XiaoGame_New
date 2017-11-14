using UnityEngine;
using System;
using System.Collections;

 namespace RedStone
{
	public class TableSkillBuff
	{
		public TableSkillBuff() { }
		public TableSkillBuff(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.name = (string)dict["name"];
			this.effectID = (int[])dict["effectID"];
			this.buffType = (int)dict["buffType"];
			this.touchTerm = (string)dict["touchTerm"];
			this.touchValue = (int)dict["touchValue"];
			this.touchPer = (int)dict["touchPer"];
			this.touchTerm2 = (string)dict["touchTerm2"];
			this.touchValue2 = (int)dict["touchValue2"];
			this.touchPer2 = (int)dict["touchPer2"];
			this.touchCd = (int)dict["touchCd"];
			this.lastTime = (int)dict["lastTime"];
			this.permanent = (int)dict["permanent"];
			this.touch_Odds = (float)dict["touch_Odds"];
			this.buffGroup = (int)dict["buffGroup"];
			this.buffCover = (int)dict["buffCover"];
			this.isDispel = (int)dict["isDispel"];
			this.isClean = (int)dict["isClean"];
			this.particles = (int)dict["particles"];
			this.endParticles = (int)dict["endParticles"];
			this.buffHintText = (string[])dict["buffHintText"];
			this.buffButtonSign = (string[])dict["buffButtonSign"];
		}

		/// <summary>
		/// buffID
		/// </summary>
		public int id;
		/// <summary>
		/// 名称
		/// </summary>
		public string name;
		/// <summary>
		/// effectID
		/// </summary>
		public int[] effectID;
		/// <summary>
		/// buff类型
		/// </summary>
		public int buffType;
		/// <summary>
		/// 触发条件
		/// </summary>
		public string touchTerm;
		/// <summary>
		/// 触发条件参数
		/// </summary>
		public int touchValue;
		/// <summary>
		/// 参数格式
		/// </summary>
		public int touchPer;
		/// <summary>
		/// 触发条件2
		/// </summary>
		public string touchTerm2;
		/// <summary>
		/// 触发条件2参数
		/// </summary>
		public int touchValue2;
		/// <summary>
		/// 触发条件2参数格式
		/// </summary>
		public int touchPer2;
		/// <summary>
		/// 触发CD
		/// </summary>
		public int touchCd;
		/// <summary>
		/// 持续时间
		/// </summary>
		public int lastTime;
		/// <summary>
		/// 是否常驻
		/// </summary>
		public int permanent;
		/// <summary>
		/// 触发概率
		/// </summary>
		public float touch_Odds;
		/// <summary>
		/// buff分组
		/// </summary>
		public int buffGroup;
		/// <summary>
		/// BUFF叠加
		/// </summary>
		public int buffCover;
		/// <summary>
		/// 可否被驱散
		/// </summary>
		public int isDispel;
		/// <summary>
		/// 死亡是否消失
		/// </summary>
		public int isClean;
		/// <summary>
		/// buff特效
		/// </summary>
		public int particles;
		/// <summary>
		/// buff消失时播放的特效
		/// </summary>
		public int endParticles;
		/// <summary>
		/// 多语言ID 数组
		/// </summary>
		public string[] buffHintText;
		/// <summary>
		/// 按钮BUFF标记，不区分大小写
		/// </summary>
		public string[] buffButtonSign;
	}
}