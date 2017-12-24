using UnityEngine;
using System;
using System.Collections;

namespace RedStone
{
	public class TableSkill
	{
		public TableSkill() { }
		public TableSkill(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.name = (string)dict["name"];
			this.icon = (int)dict["icon"];
			this.description = (string)dict["description"];
			this.nameId = (string)dict["nameId"];
			this.descriptionId = (string)dict["descriptionId"];
			this.dynamicProperties = (string)dict["dynamicProperties"];
			this.staticProperties = (string)dict["staticProperties"];
			this.bulleteffect = (int)dict["bulleteffect"];
			this.castEffect = (int)dict["castEffect"];
			this.sound = (int)dict["sound"];
			this.lvLimit = (int)dict["lvLimit"];
			this.powerType = (int)dict["powerType"];
			this.powerCos = (float)dict["powerCos"];
			this.powerPer = (int)dict["powerPer"];
			this.coolDown = (int)dict["coolDown"];
			this.target = (int)dict["target"];
			this.targetRange = (int)dict["targetRange"];
			this.targetNum = (int)dict["targetNum"];
			this.buffID = (int[])dict["buffID"];
			this.skillType = (int)dict["skillType"];
			this.summonedId = (int)dict["summonedId"];
			this.buttonType = (int)dict["buttonType"];
			this.animationModelPath = (string)dict["animationModelPath"];
			this.preCastAnimation = (string)dict["preCastAnimation"];
			this.preCastAnimationTime = (float)dict["preCastAnimationTime"];
			this.preCastAnimationTimeEvent = (float)dict["preCastAnimationTimeEvent"];
			this.holdAnimation = (string)dict["holdAnimation"];
			this.holdAnimationTime = (float)dict["holdAnimationTime"];
			this.castAnimation = (string)dict["castAnimation"];
			this.castAnimationTime = (float)dict["castAnimationTime"];
			this.castAnimationTimeEvent = (float)dict["castAnimationTimeEvent"];
			this.castCanMove = (int)dict["castCanMove"];
			this.uiButtonImageID = (int)dict["uiButtonImageID"];
		}

		/// <summary>
		/// 技能ID
		/// </summary>
		public int id;
		/// <summary>
		/// 技能名称
		/// </summary>
		public string name;
		/// <summary>
		/// 技能图标
		/// </summary>
		public int icon;
		/// <summary>
		/// 技能描述
		/// </summary>
		public string description;
		/// <summary>
		/// 多语言ID
		/// </summary>
		public string nameId;
		/// <summary>
		/// 多语言ID
		/// </summary>
		public string descriptionId;
		/// <summary>
		/// 变化的属性（多语言ID）
		/// </summary>
		public string dynamicProperties;
		/// <summary>
		/// 不变的属性（多语言ID）
		/// </summary>
		public string staticProperties;
		/// <summary>
		/// 子弹特效
		/// </summary>
		public int bulleteffect;
		/// <summary>
		/// 技能特效
		/// </summary>
		public int castEffect;
		/// <summary>
		/// 技能声音
		/// </summary>
		public int sound;
		/// <summary>
		/// 等级限制
		/// </summary>
		public int lvLimit;
		/// <summary>
		/// 技能消耗
		/// </summary>
		public int powerType;
		/// <summary>
		/// 技能消耗数值
		/// </summary>
		public float powerCos;
		/// <summary>
		/// 消耗数值格式
		/// </summary>
		public int powerPer;
		/// <summary>
		/// 冷却时间
		/// </summary>
		public int coolDown;
		/// <summary>
		/// 目标类型
		/// </summary>
		public int target;
		/// <summary>
		/// 目标范围
		/// </summary>
		public int targetRange;
		/// <summary>
		/// 目标个数
		/// </summary>
		public int targetNum;
		/// <summary>
		/// buffID
		/// </summary>
		public int[] buffID;
		/// <summary>
		/// 技能类型
		/// </summary>
		public int skillType;
		/// <summary>
		/// 召唤物ID
		/// </summary>
		public int summonedId;
		/// <summary>
		/// 按键类型
		/// </summary>
		public int buttonType;
		/// <summary>
		/// 动画模型路径
		/// </summary>
		public string animationModelPath;
		/// <summary>
		/// 起手动画
		/// </summary>
		public string preCastAnimation;
		/// <summary>
		/// 动画持续时间，也是扔手雷的过程时间，ms
		/// </summary>
		public float preCastAnimationTime;
		/// <summary>
		/// 动画事件处理时间点
		/// </summary>
		public float preCastAnimationTimeEvent;
		/// <summary>
		/// 施法保持动画
		/// </summary>
		public string holdAnimation;
		/// <summary>
		/// 施法保持动画的时间
		/// </summary>
		public float holdAnimationTime;
		/// <summary>
		/// 施法动画
		/// </summary>
		public string castAnimation;
		/// <summary>
		/// 施法动画时间
		/// </summary>
		public float castAnimationTime;
		/// <summary>
		/// 出手时间
		/// </summary>
		public float castAnimationTimeEvent;
		/// <summary>
		/// 出手动画时能否移动,0不能移动，否则能移动
		/// </summary>
		public int castCanMove;
		/// <summary>
		/// UI按钮图标
		/// </summary>
		public int uiButtonImageID;
	}
}