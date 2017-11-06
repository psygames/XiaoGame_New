using UnityEngine;
using System;
using System.Collections;

namespace Hotfire
{
	public class TableGun
	{
		public TableGun() { }
		public TableGun(IDictionary dict)
		{
			this.id = (int)dict["id"];
			this.name = (string)dict["name"];
			this.nameId = (string)dict["nameId"];
			this.modelPath = (string)dict["modelPath"];
			this.gunAudioID = (int)dict["gunAudioID"];
			this.animationID = (int)dict["animationID"];
			this.aimPoseUpModify = (float[])dict["aimPoseUpModify"];
			this.skillID = (int[])dict["skillID"];
			this.image = (string)dict["image"];
			this.weaponType = (int)dict["weaponType"];
			this.iconId = (int)dict["iconId"];
			this.needChambering = (int)dict["needChambering"];
			this.tier = (int)dict["tier"];
			this.fireParticle = (int)dict["fireParticle"];
			this.shellCaseParticle = (int)dict["shellCaseParticle"];
			this.shellDecalParticle = (int)dict["shellDecalParticle"];
			this.fireDelayTime = (float)dict["fireDelayTime"];
			this.burstFireDealyTime = (float)dict["burstFireDealyTime"];
			this.magzineCount = (int)dict["magzineCount"];
			this.hitDamage = (int)dict["hitDamage"];
			this.pierceDamage = (float)dict["pierceDamage"];
			this.accuracyMinNormal = (float)dict["accuracyMinNormal"];
			this.accuracyMaxNormal = (float)dict["accuracyMaxNormal"];
			this.recoilForceNormal = (float)dict["recoilForceNormal"];
			this.recoilDampingNormal = (float)dict["recoilDampingNormal"];
			this.aimCircleWeightNormal = (float[])dict["aimCircleWeightNormal"];
			this.accuracyMinSight = (float)dict["accuracyMinSight"];
			this.accuracyMaxSight = (float)dict["accuracyMaxSight"];
			this.recoilForceSight = (float)dict["recoilForceSight"];
			this.recoilDampingSight = (float)dict["recoilDampingSight"];
			this.aimCircleWeightSight = (float[])dict["aimCircleWeightSight"];
			this.aimFovFactor = (float)dict["aimFovFactor"];
			this.aimFovFactor2 = (float)dict["aimFovFactor2"];
			this.sightInTime = (float)dict["sightInTime"];
			this.sightInTime2 = (float)dict["sightInTime2"];
			this.sightOutTime = (float)dict["sightOutTime"];
			this.sightOutTime2 = (float)dict["sightOutTime2"];
			this.reduceSpeed = (float)dict["reduceSpeed"];
			this.reduceAccuracy = (float)dict["reduceAccuracy"];
			this.poundSpeed = (float)dict["poundSpeed"];
			this.maxBulletsCount = (int)dict["maxBulletsCount"];
			this.maxAimLength = (int)dict["maxAimLength"];
			this.damageHeadFactor = (float)dict["damageHeadFactor"];
			this.damageBodyFactor = (float)dict["damageBodyFactor"];
			this.damageArmFactor = (float)dict["damageArmFactor"];
			this.damageLegFactor = (float)dict["damageLegFactor"];
			this.damageDistanceReduceMin = (int)dict["damageDistanceReduceMin"];
			this.damageDistanceReduceMax = (int)dict["damageDistanceReduceMax"];
			this.damageDistanceReducePercent = (float)dict["damageDistanceReducePercent"];
			this.damagePierceWallPercentage = (float)dict["damagePierceWallPercentage"];
			this.rushMaxMoveSpeed = (float)dict["rushMaxMoveSpeed"];
			this.rushFov = (float)dict["rushFov"];
			this.maxMoveSpeed = (float)dict["maxMoveSpeed"];
			this.fireMaxMoveSpeed = (float)dict["fireMaxMoveSpeed"];
			this.aimMaxMoveSpeed2 = (float)dict["aimMaxMoveSpeed2"];
			this.fireMaxMoveSpeed2 = (float)dict["fireMaxMoveSpeed2"];
			this.fireReduceSpeedKeepTime = (float)dict["fireReduceSpeedKeepTime"];
			this.fireReduceSpeedKeepTime2 = (float)dict["fireReduceSpeedKeepTime2"];
			this.sightMaxMoveSpeed = (float)dict["sightMaxMoveSpeed"];
			this.normalLockRotSpeedMax = (float)dict["normalLockRotSpeedMax"];
			this.normalLockRotSpeed = (float)dict["normalLockRotSpeed"];
			this.aimLockRotSpeedMax = (float)dict["aimLockRotSpeedMax"];
			this.aimLockRotSpeed = (float)dict["aimLockRotSpeed"];
			this.patternPath = (string)dict["patternPath"];
			this.projectorPath = (string)dict["projectorPath"];
			this.glossMapPath = (string)dict["glossMapPath"];
			this.hurtCameraShake = (int)dict["hurtCameraShake"];
			this.shootAnimationType = (int)dict["shootAnimationType"];
			this.normalLockRectDistSize = (float[])dict["normalLockRectDistSize"];
			this.normalLockDist = (float[])dict["normalLockDist"];
			this.aimLockRectDistSize = (float[])dict["aimLockRectDistSize"];
			this.aimLockDist = (float[])dict["aimLockDist"];
			this.normalLockRectMinDistSize = (float)dict["normalLockRectMinDistSize"];
			this.normalLockRectBestSize = (float)dict["normalLockRectBestSize"];
			this.normalLockRectMaxDistSize = (float)dict["normalLockRectMaxDistSize"];
			this.normalLockAssistMinDist = (float)dict["normalLockAssistMinDist"];
			this.normalLockBestPlayerDist = (float)dict["normalLockBestPlayerDist"];
			this.normalLockMaxDist = (float)dict["normalLockMaxDist"];
			this.normalLockRectMultiRatioFireOnce = (float)dict["normalLockRectMultiRatioFireOnce"];
			this.aimLockRectMinDistSize = (float)dict["aimLockRectMinDistSize"];
			this.aimLockRectBestSize = (float)dict["aimLockRectBestSize"];
			this.aimLockRectMaxDistSize = (float)dict["aimLockRectMaxDistSize"];
			this.aimLockAssistMinDist = (float)dict["aimLockAssistMinDist"];
			this.aimLockBestPlayerDist = (float)dict["aimLockBestPlayerDist"];
			this.aimLockMaxDist = (float)dict["aimLockMaxDist"];
			this.aimLockRectMultiRatioFireOnce = (float)dict["aimLockRectMultiRatioFireOnce"];
			this.hitRatioByDistNormal = (float[])dict["hitRatioByDistNormal"];
			this.hitRatioByDistSight = (float[])dict["hitRatioByDistSight"];
			this.consumableID = (int)dict["consumableID"];
			this.sightKeepTime = (float)dict["sightKeepTime"];
			this.recoilUp = (float)dict["recoilUp"];
			this.recoilLeft = (float)dict["recoilLeft"];
			this.recoilRight = (float)dict["recoilRight"];
			this.recoilDec = (float)dict["recoilDec"];
			this.recoilChangeBullet = (int)dict["recoilChangeBullet"];
			this.recoilUpSecond = (float)dict["recoilUpSecond"];
			this.recoilLeftSecond = (float)dict["recoilLeftSecond"];
			this.recoilRightSecond = (float)dict["recoilRightSecond"];
			this.recoilDecSecond = (float)dict["recoilDecSecond"];
			this.shotDistanceBase = (float)dict["shotDistanceBase"];
			this.aimDistanceBase = (float)dict["aimDistanceBase"];
			this.hitRateReduceByMove = (float)dict["hitRateReduceByMove"];
			this.sniperAimWagglePeriod = (float)dict["sniperAimWagglePeriod"];
			this.sniperAimWaggleMaxDamageReduce = (float)dict["sniperAimWaggleMaxDamageReduce"];
			this.playerAimInsideCircleAngle = (float)dict["playerAimInsideCircleAngle"];
			this.playerAimInsideCircleAngleMin = (float)dict["playerAimInsideCircleAngleMin"];
			this.recoilAnimationType = (int)dict["recoilAnimationType"];
			this.aimMode = (bool)dict["aimMode"];
			this.fireBulletInsideCircleRaiusFactor = (float)dict["fireBulletInsideCircleRaiusFactor"];
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
		/// 多语言ID
		/// </summary>
		public string nameId;
		/// <summary>
		/// 模型相对Resources路径
		/// </summary>
		public string modelPath;
		/// <summary>
		/// 枪械音效id
		/// </summary>
		public int gunAudioID;
		/// <summary>
		/// 对应TableAnimation表动画
		/// </summary>
		public int animationID;
		/// <summary>
		/// 瞄准姿势上扬调整
		/// </summary>
		public float[] aimPoseUpModify;
		/// <summary>
		/// 枪械携带技能的ID
		/// </summary>
		public int[] skillID;
		/// <summary>
		/// 大厅中，选中枪械后的图标。
		/// </summary>
		public string image;
		/// <summary>
		/// 枪械类型，1-步枪，2-冲锋枪,3-机枪，4-单射散弹枪，5-单射狙击枪,6连发散弹枪，7手枪，8，连狙，100-火箭筒
		/// </summary>
		public int weaponType;
		/// <summary>
		/// 枪械类型，401-步枪，402-冲锋枪,403-机枪，404-散弹枪，405-狙击枪，406-火箭筒。图标ID
		/// </summary>
		public int iconId;
		/// <summary>
		/// 打完一发子弹是否需要上膛，0不需要，1需要
		/// </summary>
		public int needChambering;
		/// <summary>
		/// 枪械等级
		/// </summary>
		public int tier;
		/// <summary>
		/// 开火特效(没有填-1)
		/// </summary>
		public int fireParticle;
		/// <summary>
		/// 开火弹壳特效
		/// </summary>
		public int shellCaseParticle;
		/// <summary>
		/// 弹痕特效
		/// </summary>
		public int shellDecalParticle;
		/// <summary>
		/// 点击后到第一发子弹出现的时间，单位：秒。可以展示枪械重量/准备（如转轮机枪）。
		/// </summary>
		public float fireDelayTime;
		/// <summary>
		/// 每次burst fire之间的间隔，单位：秒。如果是自动武器(连射武器)则这个值为0。;2016.3.15,xiyue:这个是之前做半自动武器用的（三连发）。现在没用了。
		/// </summary>
		public float burstFireDealyTime;
		/// <summary>
		/// 弹夹数量 -1为无限
		/// </summary>
		public int magzineCount;
		/// <summary>
		/// 单发子弹伤害
		/// </summary>
		public int hitDamage;
		/// <summary>
		/// 穿透伤害,命中时减少的HP上限值;2016.3.15,xiyue:不掉血上线的话这个属性也没用。
		/// </summary>
		public float pierceDamage;
		/// <summary>
		/// 最小准确度，单位角度，可能是人物属性，或者常量;2016.3.15,xiyue:实际射击时用的是sight里面那套，可以把这两套合并成一套。
		/// </summary>
		public float accuracyMinNormal;
		/// <summary>
		/// 最大准确度，单位，角度，可能是人物属性，或者常量;2016.3.15,xiyue:实际射击时用的是sight里面那套，可以把这两套合并成一套。
		/// </summary>
		public float accuracyMaxNormal;
		/// <summary>
		/// 后坐力，单位角度每发;2016.3.15,xiyue:实际射击时用的是sight里面那套，可以把这两套合并成一套。
		/// </summary>
		public float recoilForceNormal;
		/// <summary>
		/// 稳定性，单位衰减倍率每秒，公式accuracy *= pow(recoilDamping, deltaTime)，可能是人物属性，或者常量;2016.3.15,xiyue:实际射击时用的是sight里面那套，可以把这两套合并成一套。
		/// </summary>
		public float recoilDampingNormal;
		/// <summary>
		/// 瞄准圈按20%分圈，每个圈的权重。;2016.3.15,xiyue:实际射击时用的是sight里面那套，可以把这两套合并成一套。
		/// </summary>
		public float[] aimCircleWeightNormal;
		/// <summary>
		/// 最小准确度，单位角度;2016.3.15,xiyue:仅决定弹线特效播放;2017.1.17,xiyue:指半角，从准星中心到准星边缘的角度
		/// </summary>
		public float accuracyMinSight;
		/// <summary>
		/// 最大准确度，单位，角度，可能是人物属性，或者常量;2016.3.15,xiyue:仅决定弹线特效播放
		/// </summary>
		public float accuracyMaxSight;
		/// <summary>
		/// 后坐力，单位角度每发;2016.3.15,xiyue:仅决定弹线特效播放
		/// </summary>
		public float recoilForceSight;
		/// <summary>
		/// 稳定性，单位衰减倍率每秒，公式accuracy *= pow(recoilDamping, deltaTime)，可能是人物属性，或者常量;2016.3.15,xiyue:仅决定弹线特效播放
		/// </summary>
		public float recoilDampingSight;
		/// <summary>
		/// 瞄准圈按20%分圈，每个圈的权重。;2016.3.15,xiyue:可以去掉，因为只是显示效果，然而现在命中是纯随机，所以弹线也就应该是纯随机，均匀的概率。
		/// </summary>
		public float[] aimCircleWeightSight;
		/// <summary>
		/// 平射时FOV
		/// </summary>
		public float aimFovFactor;
		/// <summary>
		/// 瞄准时FOV
		/// </summary>
		public float aimFovFactor2;
		/// <summary>
		/// 平射进入时间 & FOV缩放时间
		/// </summary>
		public float sightInTime;
		/// <summary>
		/// 瞄准进入时间 & FOV缩放时间
		/// </summary>
		public float sightInTime2;
		/// <summary>
		/// 平射退出时间 & FOV缩放时间
		/// </summary>
		public float sightOutTime;
		/// <summary>
		/// 瞄准退出时间 & FOV缩放时间
		/// </summary>
		public float sightOutTime2;
		/// <summary>
		/// 攻击造成减速
		/// </summary>
		public float reduceSpeed;
		/// <summary>
		/// 攻击造成散射
		/// </summary>
		public float reduceAccuracy;
		/// <summary>
		/// 冲击力速度
		/// </summary>
		public float poundSpeed;
		/// <summary>
		/// 最大子弹数上限;2016.3.15,xiyue:可删，没有子弹携带数量限制了
		/// </summary>
		public int maxBulletsCount;
		/// <summary>
		/// 射程;2016.3.15,xiyue:可删，实际上这个是决定了子弹显示用的射线检测距离。Yin:作用不大，直接程序写死一个值就行
		/// </summary>
		public int maxAimLength;
		/// <summary>
		/// 头部的伤害值增加倍率。做法是伤害值直接乘这个数值。
		/// </summary>
		public float damageHeadFactor;
		/// <summary>
		/// 身体的伤害值增加倍率。做法是伤害值直接乘这个数值。
		/// </summary>
		public float damageBodyFactor;
		/// <summary>
		/// 胳膊的伤害值增加倍率。做法是伤害值直接乘这个数值。;2016.3.15,xiyue:似乎用不着
		/// </summary>
		public float damageArmFactor;
		/// <summary>
		/// 腿部的伤害值增加倍率。做法是伤害值直接乘这个数值。
		/// </summary>
		public float damageLegFactor;
		/// <summary>
		/// 命中距离对伤害的衰减。开始衰减距离，单位：米;2016.3.15,xiyue:去掉了这个功能
		/// </summary>
		public int damageDistanceReduceMin;
		/// <summary>
		/// 命中距离对伤害的衰减。结束衰减距离，单位：米;2016.3.15,xiyue:去掉了这个功能
		/// </summary>
		public int damageDistanceReduceMax;
		/// <summary>
		/// 命中距离对伤害的衰减。最大衰减量。超过结束距离后一直只衰减这个量。单位：百分比（0.2即20%）;2016.3.15,xiyue:去掉了这个功能
		/// </summary>
		public float damageDistanceReducePercent;
		/// <summary>
		/// 穿墙伤害衰减量。单位：百分比/米（0.2即20%每米）;穿墙厚度*这个值，是穿墙后导致伤害缩减的百分比。;2016.3.15,xiyue:暂时无法穿墙，也许应该考虑被墙遮挡降低命中率
		/// </summary>
		public float damagePierceWallPercentage;
		/// <summary>
		/// 冲刺移动模式最大速度
		/// </summary>
		public float rushMaxMoveSpeed;
		/// <summary>
		/// 冲刺Fov
		/// </summary>
		public float rushFov;
		/// <summary>
		/// 移动模式最大速度
		/// </summary>
		public float maxMoveSpeed;
		/// <summary>
		/// 平射开火时的移动速度;2016.4.21,xiyue:作为开火保持状态下的速度
		/// </summary>
		public float fireMaxMoveSpeed;
		/// <summary>
		/// 瞄准状态下的移动速度;
		/// </summary>
		public float aimMaxMoveSpeed2;
		/// <summary>
		/// 瞄准开火下的速度
		/// </summary>
		public float fireMaxMoveSpeed2;
		/// <summary>
		/// 平射开火时降低移动速度持续时间;2016.4.21,xiyue:作为开火保持状态的持续时间
		/// </summary>
		public float fireReduceSpeedKeepTime;
		/// <summary>
		/// 瞄准时降低移动速度持续时间
		/// </summary>
		public float fireReduceSpeedKeepTime2;
		/// <summary>
		/// 瞄准模式下的最大移动速度;2016.3.15,xiyue:点击开火之后的移动速度
		/// </summary>
		public float sightMaxMoveSpeed;
		/// <summary>
		/// 镜头辅助最大角速度
		/// </summary>
		public float normalLockRotSpeedMax;
		/// <summary>
		/// 镜头辅助线速度
		/// </summary>
		public float normalLockRotSpeed;
		/// <summary>
		/// 镜头辅助最大角速度（瞄准模式）
		/// </summary>
		public float aimLockRotSpeedMax;
		/// <summary>
		/// 镜头辅助线速度（瞄准模式）
		/// </summary>
		public float aimLockRotSpeed;
		/// <summary>
		/// 涂装路径
		/// </summary>
		public string patternPath;
		/// <summary>
		/// 贴花路径
		/// </summary>
		public string projectorPath;
		/// <summary>
		/// 控制高光的贴图
		/// </summary>
		public string glossMapPath;
		/// <summary>
		/// 击中人的摄像机震动;2016.3.15,xiyue:击中敌人的震动根据本次伤害值占对方总血量的百分比设置，可删。
		/// </summary>
		public int hurtCameraShake;
		/// <summary>
		/// 开火动画类型 0 中断，1不中断
		/// </summary>
		public int shootAnimationType;
		/// <summary>
		/// 腰射锁定圈大小
		/// </summary>
		public float[] normalLockRectDistSize;
		/// <summary>
		/// 腰射锁定圈对应距离
		/// </summary>
		public float[] normalLockDist;
		/// <summary>
		/// 瞄准锁定圈大小
		/// </summary>
		public float[] aimLockRectDistSize;
		/// <summary>
		/// 瞄准锁定圈对应距离
		/// </summary>
		public float[] aimLockDist;
		/// <summary>
		/// 锁定框半径（最近距离）（腰射）
		/// </summary>
		public float normalLockRectMinDistSize;
		/// <summary>
		/// 锁定框半径（中间距离）（腰射）
		/// </summary>
		public float normalLockRectBestSize;
		/// <summary>
		/// 锁定框半径（最远距离）（腰射）
		/// </summary>
		public float normalLockRectMaxDistSize;
		/// <summary>
		/// 最小距离（腰射）
		/// </summary>
		public float normalLockAssistMinDist;
		/// <summary>
		///  中间距离（腰射）
		/// </summary>
		public float normalLockBestPlayerDist;
		/// <summary>
		/// 最大距离（腰射）
		/// </summary>
		public float normalLockMaxDist;
		/// <summary>
		/// 开火一次，锁定变化倍率（腰射）
		/// </summary>
		public float normalLockRectMultiRatioFireOnce;
		/// <summary>
		/// 锁定框半径（最近距离）（瞄准）
		/// </summary>
		public float aimLockRectMinDistSize;
		/// <summary>
		/// 锁定框半径（中间距离）（瞄准）
		/// </summary>
		public float aimLockRectBestSize;
		/// <summary>
		/// 锁定框半径（最远距离）（瞄准）
		/// </summary>
		public float aimLockRectMaxDistSize;
		/// <summary>
		/// 最小距离（瞄准）
		/// </summary>
		public float aimLockAssistMinDist;
		/// <summary>
		/// 中间距离（瞄准） 
		/// </summary>
		public float aimLockBestPlayerDist;
		/// <summary>
		/// 最大距离（瞄准）
		/// </summary>
		public float aimLockMaxDist;
		/// <summary>
		/// 开火一次，锁定变化倍率（瞄准）
		/// </summary>
		public float aimLockRectMultiRatioFireOnce;
		/// <summary>
		/// 规定枪械每个距离下的命中率，从0米开始，每5米一个数据点，两个点之间线性插值。;比如[1.5,1.5,1.25,1,0.9,0.8,0.7,0.6,0.5,0.4,0.3,0.2,0.1,0,0,0,0,0,0,0,0];规定了枪械从0到100米的命中率;其中命中率超过100%的部分，用于：当交战距离小于100%命中距离时，命中率基数可以高于100%，用于计算其他衰减因素，比如瞄准头部时的命中率降低。;对于近距离交战时，是否将敌人放到屏幕中心，瞄准头部时命中率下降多少相关。;
		/// </summary>
		public float[] hitRatioByDistNormal;
		/// <summary>
		/// 瞄准模式下命中率曲线;
		/// </summary>
		public float[] hitRatioByDistSight;
		/// <summary>
		/// 超级武器用到的数值在道具表
		/// </summary>
		public int consumableID;
		/// <summary>
		/// 视野退出之前保持的时间
		/// </summary>
		public float sightKeepTime;
		/// <summary>
		/// 后坐力，向上分量
		/// </summary>
		public float recoilUp;
		/// <summary>
		/// 后坐力，向左分量
		/// </summary>
		public float recoilLeft;
		/// <summary>
		/// 后坐力，向右分量
		/// </summary>
		public float recoilRight;
		/// <summary>
		/// 后坐力恢复速度
		/// </summary>
		public float recoilDec;
		/// <summary>
		/// 连射第几发后，后座力改变
		/// </summary>
		public int recoilChangeBullet;
		/// <summary>
		/// 后坐力，向上分量
		/// </summary>
		public float recoilUpSecond;
		/// <summary>
		/// 后坐力，向左分量
		/// </summary>
		public float recoilLeftSecond;
		/// <summary>
		/// 后坐力，向右分量
		/// </summary>
		public float recoilRightSecond;
		/// <summary>
		/// 后坐力恢复速度
		/// </summary>
		public float recoilDecSecond;
		/// <summary>
		/// 平射，摄像机和人的相对距离（标准距离2）
		/// </summary>
		public float shotDistanceBase;
		/// <summary>
		/// 瞄准时，摄像机和人的相对距离（标准距离2）
		/// </summary>
		public float aimDistanceBase;
		/// <summary>
		/// 移动中降低命中率比例
		/// </summary>
		public float hitRateReduceByMove;
		/// <summary>
		/// 狙击枪瞄准波动周期（时间秒）;ps:只针对狙击枪有效
		/// </summary>
		public float sniperAimWagglePeriod;
		/// <summary>
		/// 狙击枪瞄准晃动最大伤害减损（百分比）;ps:只针对狙击枪有效
		/// </summary>
		public float sniperAimWaggleMaxDamageReduce;
		/// <summary>
		/// 准星散射圈（角度），当准星未瞄中敌人时，使用此值。
		/// </summary>
		public float playerAimInsideCircleAngle;
		/// <summary>
		/// 准星最小散射圈（角度），当准星瞄中敌人时，使用此值。
		/// </summary>
		public float playerAimInsideCircleAngleMin;
		/// <summary>
		/// 后坐力动画类型;（每枪都一样-1，第一枪大-0）
		/// </summary>
		public int recoilAnimationType;
		/// <summary>
		/// 有没有瞄准键（有-1，没有-0）
		/// </summary>
		public bool aimMode;
		/// <summary>
		/// 子弹散射圈，相对准星比例大小，
		/// </summary>
		public float fireBulletInsideCircleRaiusFactor;
	}
}