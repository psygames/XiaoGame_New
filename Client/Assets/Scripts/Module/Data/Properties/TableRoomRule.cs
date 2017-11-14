using UnityEngine;
using System;
using System.Collections;

 namespace RedStone
{
	public class TableRoomRule
	{
		public TableRoomRule() { }
		public TableRoomRule(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.name = (string)dict["name"];
			this.mapID = (int)dict["mapID"];
			this.modeID = (int)dict["modeID"];
			this.nameID = (string)dict["nameID"];
			this.teamNum = (int)dict["teamNum"];
			this.teamSize = (int)dict["teamSize"];
			this.cost = (int)dict["cost"];
			this.maxKills = (int)dict["maxKills"];
			this.time = (int)dict["time"];
			this.unlockConflict = (int)dict["unlockConflict"];
			this.overtimeConflict = (int)dict["overtimeConflict"];
			this.captureSpeed = (float[])dict["captureSpeed"];
			this.defenseReduceSpeedPer = (int[])dict["defenseReduceSpeedPer"];
			this.reduceSpeed = (int)dict["reduceSpeed"];
			this.scoreSpeed = (int)dict["scoreSpeed"];
			this.scoreSpeedAfterRemainTime = (int)dict["scoreSpeedAfterRemainTime"];
			this.remainTime = (int)dict["remainTime"];
			this.scoreTimeForLastPoint = (int)dict["scoreTimeForLastPoint"];
			this.supplyPos = (Vector3[])dict["supplyPos"];
			this.supplyNum = (int)dict["supplyNum"];
			this.teamABornPosList = (int[])dict["teamABornPosList"];
			this.teamBBornPosList = (int[])dict["teamBBornPosList"];
			this.teamABornPosSecList = (int[])dict["teamABornPosSecList"];
			this.teamBBornPosSecList = (int[])dict["teamBBornPosSecList"];
			this.superDropId = (int[])dict["superDropId"];
			this.superDropStartTime = (int[])dict["superDropStartTime"];
			this.superDropCDTime = (int)dict["superDropCDTime"];
			this.resourceDropCDTime = (int)dict["resourceDropCDTime"];
			this.superDropPos = (Vector3[])dict["superDropPos"];
			this.AreaZoneInfo = (Vector3[])dict["AreaZoneInfo"];
			this.resourceModeTime = (int[])dict["resourceModeTime"];
			this.largeRecoveryInterval = (int)dict["largeRecoveryInterval"];
			this.largeRecoveryUpgradeProbability = (int)dict["largeRecoveryUpgradeProbability"];
			this.recoveryEndNotify = (int)dict["recoveryEndNotify"];
			this.recoveryTime = (int)dict["recoveryTime"];
			this.recoveryHitReduceTime = (int)dict["recoveryHitReduceTime"];
			this.resourceItemId = (int[])dict["resourceItemId"];
			this.resourceItemDropPos = (Vector3[])dict["resourceItemDropPos"];
			this.resourceItemMaxNum = (int)dict["resourceItemMaxNum"];
			this.atkDefConflictProgressBar = (int[])dict["atkDefConflictProgressBar"];
			this.fixedDropId = (int[])dict["fixedDropId"];
			this.fixedDropCDTime = (int)dict["fixedDropCDTime"];
			this.fixedDropPos = (Vector3[])dict["fixedDropPos"];
		}

		/// <summary>
		/// id
		/// </summary>
		public int id;
		/// <summary>
		/// 模式名字
		/// </summary>
		public string name;
		/// <summary>
		/// 所属地图ID
		/// </summary>
		public int mapID;
		/// <summary>
		/// 模式ID,1-Time，2-Kill，3-Conflict，4-Resource，5-AttackDefense
		/// </summary>
		public int modeID;
		/// <summary>
		/// 多语言
		/// </summary>
		public string nameID;
		/// <summary>
		/// 参赛队伍数量
		/// </summary>
		public int teamNum;
		/// <summary>
		/// 每支队伍人数
		/// </summary>
		public int teamSize;
		/// <summary>
		/// 消耗体力值
		/// </summary>
		public int cost;
		/// <summary>
		/// 最大杀人数，0为无限制
		/// </summary>
		public int maxKills;
		/// <summary>
		/// 战场总时间，单位s
		/// </summary>
		public int time;
		/// <summary>
		/// 据点解锁时间，单位s
		/// </summary>
		public int unlockConflict;
		/// <summary>
		/// 加时时间，单位s
		/// </summary>
		public int overtimeConflict;
		/// <summary>
		/// 占领速度，分/秒
		/// </summary>
		public float[] captureSpeed;
		/// <summary>
		/// 防御减缓占领速度的百分比数组
		/// </summary>
		public int[] defenseReduceSpeedPer;
		/// <summary>
		/// 消逝速度，分/秒
		/// </summary>
		public int reduceSpeed;
		/// <summary>
		/// 得分速度，分/秒
		/// </summary>
		public int scoreSpeed;
		/// <summary>
		/// 在剩余时间<RemainTime后的得分速度，分/秒
		/// </summary>
		public int scoreSpeedAfterRemainTime;
		/// <summary>
		/// 剩余时间小于这个时间后，得分加速，秒
		/// </summary>
		public int remainTime;
		/// <summary>
		/// 最后一分，在不触发加时的时候，得分所需时间。秒
		/// </summary>
		public int scoreTimeForLastPoint;
		/// <summary>
		/// 补给位置
		/// </summary>
		public Vector3[] supplyPos;
		/// <summary>
		/// 补给出现数量
		/// </summary>
		public int supplyNum;
		/// <summary>
		/// A队出生位置
		/// </summary>
		public int[] teamABornPosList;
		/// <summary>
		/// B队出生位置
		/// </summary>
		public int[] teamBBornPosList;
		/// <summary>
		/// A队第二套出生位置
		/// </summary>
		public int[] teamABornPosSecList;
		/// <summary>
		/// B队第二套出生位置
		/// </summary>
		public int[] teamBBornPosSecList;
		/// <summary>
		/// 超级掉落物Id
		/// </summary>
		public int[] superDropId;
		/// <summary>
		/// 超级掉落物开始时间
		/// </summary>
		public int[] superDropStartTime;
		/// <summary>
		/// 超级掉落物CD
		/// </summary>
		public int superDropCDTime;
		/// <summary>
		/// 资源掉落物CD，创建到激活所用时间
		/// </summary>
		public int resourceDropCDTime;
		/// <summary>
		/// 超级掉落物位置
		/// </summary>
		public Vector3[] superDropPos;
		/// <summary>
		/// 特殊区域（立方体)信息。每五个为一组，坐标，size，方向x3。
		/// </summary>
		public Vector3[] AreaZoneInfo;
		/// <summary>
		/// 资源模式时间，三个为一组，依次为刷新，拾取，回收的开始时间。单位 ms 最后一组触发大量回收状态
		/// </summary>
		public int[] resourceModeTime;
		/// <summary>
		/// 大量回收的时间间隔，单位ms
		/// </summary>
		public int largeRecoveryInterval;
		/// <summary>
		/// 大量回收时升级成大资源的几率
		/// </summary>
		public int largeRecoveryUpgradeProbability;
		/// <summary>
		/// 回收结束前的预告时间
		/// </summary>
		public int recoveryEndNotify;
		/// <summary>
		/// 回收时间,单位ms
		/// </summary>
		public int recoveryTime;
		/// <summary>
		/// 被打掉1%生命退多少生命条
		/// </summary>
		public int recoveryHitReduceTime;
		/// <summary>
		/// 资源掉落物Id数组，最后一位为大资源
		/// </summary>
		public int[] resourceItemId;
		/// <summary>
		/// 资源掉落物位置
		/// </summary>
		public Vector3[] resourceItemDropPos;
		/// <summary>
		/// 资源掉落物最大数量
		/// </summary>
		public int resourceItemMaxNum;
		/// <summary>
		/// 攻防模式据点进度条信息
		/// </summary>
		public int[] atkDefConflictProgressBar;
		/// <summary>
		/// 固定掉落物Id
		/// </summary>
		public int[] fixedDropId;
		/// <summary>
		/// 固定掉落物CD
		/// </summary>
		public int fixedDropCDTime;
		/// <summary>
		/// 固定掉落物位置
		/// </summary>
		public Vector3[] fixedDropPos;
	}
}