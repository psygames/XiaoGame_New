using UnityEngine;
using System;
using System.Collections;

 namespace RedStone
{
	public class TableMotion
	{
		public TableMotion() { }
		public TableMotion(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.name = (string)dict["name"];
			this.type = (int)dict["type"];
			this.motionType = (int)dict["motionType"];
			this.triggerPosOffset = (Vector3)dict["triggerPosOffset"];
			this.throwAngle = (float)dict["throwAngle"];
			this.throwSpeed = (float)dict["throwSpeed"];
			this.workSpeed = (float)dict["workSpeed"];
			this.speedReduceFactorHitPlayer = (float)dict["speedReduceFactorHitPlayer"];
			this.speedReduceFactorHitOther = (float)dict["speedReduceFactorHitOther"];
			this.colliderSphereRadius = (float)dict["colliderSphereRadius"];
			this.colliderCubeSize = (Vector3)dict["colliderCubeSize"];
			this.colliderCylinderSize = (Vector2)dict["colliderCylinderSize"];
			this.animationTime = (float)dict["animationTime"];
			this.animationTimeEvent = (float)dict["animationTimeEvent"];
			this.prefabPath = (string)dict["prefabPath"];
			this.prefabPath2 = (string)dict["prefabPath2"];
			this.animationTime2 = (float)dict["animationTime2"];
			this.animationTimeEvent2 = (float)dict["animationTimeEvent2"];
			this.takeOutAnimationPath = (string)dict["takeOutAnimationPath"];
			this.holdAnimationPath = (string)dict["holdAnimationPath"];
			this.fireAnimation3PPath = (string)dict["fireAnimation3PPath"];
			this.uiButtonImageID = (int)dict["uiButtonImageID"];
			this.uiPerceptionImageID = (int)dict["uiPerceptionImageID"];
			this.installEffect = (int)dict["installEffect"];
			this.deadEffect = (int)dict["deadEffect"];
			this.explosionEffect = (int)dict["explosionEffect"];
			this.holeEffect = (int)dict["holeEffect"];
			this.explosionSound = (int)dict["explosionSound"];
			this.fireEffect = (int)dict["fireEffect"];
			this.moveSpeed = (float)dict["moveSpeed"];
			this.fov = (float)dict["fov"];
			this.fovTime = (float)dict["fovTime"];
			this.maxFixAngle = (float)dict["maxFixAngle"];
			this.fixAngleTriggerDistance = (float)dict["fixAngleTriggerDistance"];
			this.maxShakeRatio = (float)dict["maxShakeRatio"];
			this.minShakeRatio = (float)dict["minShakeRatio"];
			this.maxShakeRange = (float)dict["maxShakeRange"];
			this.attachPoint = (string)dict["attachPoint"];
			this.maxAngleUp = (float)dict["maxAngleUp"];
			this.maxAngleDown = (float)dict["maxAngleDown"];
			this.maxThrowSpeedUp = (float)dict["maxThrowSpeedUp"];
			this.maxThrowSpeedDown = (float)dict["maxThrowSpeedDown"];
			this.gravityFactor = (float)dict["gravityFactor"];
			this.deadDropSpeed = (float)dict["deadDropSpeed"];
		}

		/// <summary>
		/// 没有描述信息
		/// </summary>
		public int id;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public string name;
		/// <summary>
		/// 1一段时间后爆炸，2碰到物体爆炸，3看时间先到还是先碰到物体（例如：火箭筒)。4,无限时间，地雷用。
		/// </summary>
		public int type;
		/// <summary>
		/// 运动类型， 0静止，1直线，2抛物线
		/// </summary>
		public int motionType;
		/// <summary>
		/// 被触发时，位置修正;也用于地雷的触发检测
		/// </summary>
		public Vector3 triggerPosOffset;
		/// <summary>
		/// 扔出时的角度（单位 度）
		/// </summary>
		public float throwAngle;
		/// <summary>
		/// 抛掷速度（单位m/s）
		/// </summary>
		public float throwSpeed;
		/// <summary>
		/// 工作时的移动速度
		/// </summary>
		public float workSpeed;
		/// <summary>
		/// 撞到人，速度衰减系数，0-1撞到人以外的物体，速度衰减系数，0-1
		/// </summary>
		public float speedReduceFactorHitPlayer;
		/// <summary>
		/// 没有描述信息
		/// </summary>
		public float speedReduceFactorHitOther;
		/// <summary>
		/// 碰撞半径(球体用，单位 米）
		/// </summary>
		public float colliderSphereRadius;
		/// <summary>
		/// 碰撞尺寸（长方体用,宽，高，厚）
		/// </summary>
		public Vector3 colliderCubeSize;
		/// <summary>
		/// 碰撞尺寸（圆柱体半径，高）
		/// </summary>
		public Vector2 colliderCylinderSize;
		/// <summary>
		/// 动画持续时间，也是扔手雷的过程时间，ms
		/// </summary>
		public float animationTime;
		/// <summary>
		/// 动画事件处理时间点
		/// </summary>
		public float animationTimeEvent;
		/// <summary>
		/// 模型路径
		/// </summary>
		public string prefabPath;
		/// <summary>
		/// 动画模型路径，有两个动画模型的道具，c4遥控器
		/// </summary>
		public string prefabPath2;
		/// <summary>
		/// 第二个动画播放时间
		/// </summary>
		public float animationTime2;
		/// <summary>
		/// 第二个动画事件处理时间点
		/// </summary>
		public float animationTimeEvent2;
		/// <summary>
		/// 取出动画路径
		/// </summary>
		public string takeOutAnimationPath;
		/// <summary>
		/// 持有动画路径
		/// </summary>
		public string holdAnimationPath;
		/// <summary>
		/// 开火动画路径3P
		/// </summary>
		public string fireAnimation3PPath;
		/// <summary>
		/// 按钮图标
		/// </summary>
		public int uiButtonImageID;
		/// <summary>
		/// 感知图标
		/// </summary>
		public int uiPerceptionImageID;
		/// <summary>
		/// 被安装时的特效
		/// </summary>
		public int installEffect;
		/// <summary>
		/// 普通死亡特效
		/// </summary>
		public int deadEffect;
		/// <summary>
		/// 爆炸特效
		/// </summary>
		public int explosionEffect;
		/// <summary>
		/// 爆炸造成的地面黑效果
		/// </summary>
		public int holeEffect;
		/// <summary>
		/// 爆炸音效
		/// </summary>
		public int explosionSound;
		/// <summary>
		/// 开火特效
		/// </summary>
		public int fireEffect;
		/// <summary>
		/// 特殊道具需要控制移动速度（-1不控制，>=0的数字表示需要控制的移动速度）
		/// </summary>
		public float moveSpeed;
		/// <summary>
		/// 同moveSpeed
		/// </summary>
		public float fov;
		/// <summary>
		/// fov过渡时间
		/// </summary>
		public float fovTime;
		/// <summary>
		/// 最大修正角度(度/s)
		/// </summary>
		public float maxFixAngle;
		/// <summary>
		/// 触发跟踪的距离
		/// </summary>
		public float fixAngleTriggerDistance;
		/// <summary>
		/// 未受伤时最大的摄像机震动幅度
		/// </summary>
		public float maxShakeRatio;
		/// <summary>
		/// 最小的摄像机震动幅度
		/// </summary>
		public float minShakeRatio;
		/// <summary>
		/// 影响摄像机的最大半径
		/// </summary>
		public float maxShakeRange;
		/// <summary>
		/// 不用时挂人身上的挂点
		/// </summary>
		public string attachPoint;
		/// <summary>
		/// 影响出手速度的最大仰视角
		/// </summary>
		public float maxAngleUp;
		/// <summary>
		/// 影响出手速度的最大俯视角
		/// </summary>
		public float maxAngleDown;
		/// <summary>
		/// 仰视最大出手速度
		/// </summary>
		public float maxThrowSpeedUp;
		/// <summary>
		/// 俯视最大出手速度
		/// </summary>
		public float maxThrowSpeedDown;
		/// <summary>
		/// 影响重力系数，0-1
		/// </summary>
		public float gravityFactor;
		/// <summary>
		/// 死亡掉落速度
		/// </summary>
		public float deadDropSpeed;
	}
}