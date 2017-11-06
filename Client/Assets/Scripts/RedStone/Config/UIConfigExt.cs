using System;
using UnityEngine;

namespace Hotfire
{
    public class UIConfigExt
    {
        private static float s_maxDragRadius;
        private static bool b_maxDragRadius;
        /// <summary>
        /// 最大拖动半径 (80)
        /// </summary>
        public static float maxDragRadius
        {
            get
            {
                if (!b_maxDragRadius)
                {
                    b_maxDragRadius = true;
                    s_maxDragRadius = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6005).value));
                }
                return s_maxDragRadius;
            }
        }

        private static float s_centerDeadZoneRadius;
        private static bool b_centerDeadZoneRadius;
        /// <summary>
        /// 中心死区半径 (15)
        /// </summary>
        public static float centerDeadZoneRadius
        {
            get
            {
                if (!b_centerDeadZoneRadius)
                {
                    b_centerDeadZoneRadius = true;
                    s_centerDeadZoneRadius = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6006).value));
                }
                return s_centerDeadZoneRadius;
            }
        }

        private static float s_joystickMaxPowerRadius;
        private static bool b_joystickMaxPowerRadius;
        /// <summary>
        /// 摇杆达到最大值，半径（用于平滑差值） (15)
        /// </summary>
        public static float joystickMaxPowerRadius
        {
            get
            {
                if (!b_joystickMaxPowerRadius)
                {
                    b_joystickMaxPowerRadius = true;
                    s_joystickMaxPowerRadius = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6806).value));
                }
                return s_joystickMaxPowerRadius;
            }
        }

        private static float s_joystickFullPowerDragPercent;
        private static bool b_joystickFullPowerDragPercent;
        /// <summary>
        /// 摇杆拖拽最大力量 距离百分比（用于显示） (0.1875)
        /// </summary>
        public static float joystickFullPowerDragPercent
        {
            get
            {
                if (!b_joystickFullPowerDragPercent)
                {
                    b_joystickFullPowerDragPercent = true;
                    s_joystickFullPowerDragPercent = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6007).value));
                }
                return s_joystickFullPowerDragPercent;
            }
        }

        private static float s_rebornTime;
        private static bool b_rebornTime;
        /// <summary>
        /// 复活时间 (8)
        /// </summary>
        public static float rebornTime
        {
            get
            {
                if (!b_rebornTime)
                {
                    b_rebornTime = true;
                    s_rebornTime = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6008).value));
                }
                return s_rebornTime;
            }
        }

        private static float s_showLowHpWarningRatio;
        private static bool b_showLowHpWarningRatio;
        /// <summary>
        /// 低血量比例 (0.5)
        /// </summary>
        public static float showLowHpWarningRatio
        {
            get
            {
                if (!b_showLowHpWarningRatio)
                {
                    b_showLowHpWarningRatio = true;
                    s_showLowHpWarningRatio = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6009).value));
                }
                return s_showLowHpWarningRatio;
            }
        }

        private static float s_showWarningFadeinTime;
        private static bool b_showWarningFadeinTime;
        /// <summary>
        /// 溅血效果淡入时间 (0.1)
        /// </summary>
        public static float showWarningFadeinTime
        {
            get
            {
                if (!b_showWarningFadeinTime)
                {
                    b_showWarningFadeinTime = true;
                    s_showWarningFadeinTime = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6010).value));
                }
                return s_showWarningFadeinTime;
            }
        }

        private static float s_showWarningFadeoutTime;
        private static bool b_showWarningFadeoutTime;
        /// <summary>
        /// 减血效果淡出时间 (0.25)
        /// </summary>
        public static float showWarningFadeoutTime
        {
            get
            {
                if (!b_showWarningFadeoutTime)
                {
                    b_showWarningFadeoutTime = true;
                    s_showWarningFadeoutTime = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6011).value));
                }
                return s_showWarningFadeoutTime;
            }
        }

        private static float s_lowHpWarnDuration;
        private static bool b_lowHpWarnDuration;
        /// <summary>
        /// 低血量闪烁周期 (1)
        /// </summary>
        public static float lowHpWarnDuration
        {
            get
            {
                if (!b_lowHpWarnDuration)
                {
                    b_lowHpWarnDuration = true;
                    s_lowHpWarnDuration = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6014).value));
                }
                return s_lowHpWarnDuration;
            }
        }

        private static float s_lowHpWarnMinAlpha;
        private static bool b_lowHpWarnMinAlpha;
        /// <summary>
        /// 低血量最小Alpha (0.5)
        /// </summary>
        public static float lowHpWarnMinAlpha
        {
            get
            {
                if (!b_lowHpWarnMinAlpha)
                {
                    b_lowHpWarnMinAlpha = true;
                    s_lowHpWarnMinAlpha = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6015).value));
                }
                return s_lowHpWarnMinAlpha;
            }
        }

        private static Color s_buffIncrColor;
        private static bool b_buffIncrColor;
        /// <summary>
        /// 增益BUFF提示颜色 (a6ff0c)
        /// </summary>
        public static Color buffIncrColor
        {
            get
            {
                if (!b_buffIncrColor)
                {
                    b_buffIncrColor = true;
                    s_buffIncrColor = (Color)TableManager.ParseValue("Color", (TableManager.instance.GetData<TableConst>(6016).value));
                }
                return s_buffIncrColor;
            }
        }

        private static Color s_buffDecrColor;
        private static bool b_buffDecrColor;
        /// <summary>
        /// 减益BUFF提示颜色 (ffffff)
        /// </summary>
        public static Color buffDecrColor
        {
            get
            {
                if (!b_buffDecrColor)
                {
                    b_buffDecrColor = true;
                    s_buffDecrColor = (Color)TableManager.ParseValue("Color", (TableManager.instance.GetData<TableConst>(6017).value));
                }
                return s_buffDecrColor;
            }
        }

        private static Vector2 s_lockPlayerHpBarPosOffset;
        private static bool b_lockPlayerHpBarPosOffset;
        /// <summary>
        /// 锁定敌人后大血条位置偏移（距2D头顶） ((0, 100))
        /// </summary>
        public static Vector2 lockPlayerHpBarPosOffset
        {
            get
            {
                if (!b_lockPlayerHpBarPosOffset)
                {
                    b_lockPlayerHpBarPosOffset = true;
                    s_lockPlayerHpBarPosOffset = (Vector2)TableManager.ParseValue("Vector2", (TableManager.instance.GetData<TableConst>(6018).value));
                }
                return s_lockPlayerHpBarPosOffset;
            }
        }

        private static float s_showHpLowHintRatio;
        private static bool b_showHpLowHintRatio;
        /// <summary>
        /// 血量少于该比例时，显示低血量提示 (0.2)
        /// </summary>
        public static float showHpLowHintRatio
        {
            get
            {
                if (!b_showHpLowHintRatio)
                {
                    b_showHpLowHintRatio = true;
                    s_showHpLowHintRatio = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6019).value));
                }
                return s_showHpLowHintRatio;
            }
        }

        private static float s_showHpLowMagazineRatio;
        private static bool b_showHpLowMagazineRatio;
        /// <summary>
        /// 弹夹剩余子弹数 小于 该比例时，显示换弹提示 (0.2)
        /// </summary>
        public static float showHpLowMagazineRatio
        {
            get
            {
                if (!b_showHpLowMagazineRatio)
                {
                    b_showHpLowMagazineRatio = true;
                    s_showHpLowMagazineRatio = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6020).value));
                }
                return s_showHpLowMagazineRatio;
            }
        }

        private static float s_standardDist;
        private static bool b_standardDist;
        /// <summary>
        /// 血条缩放的标准距离（3D距离，米） (15)
        /// </summary>
        public static float standardDist
        {
            get
            {
                if (!b_standardDist)
                {
                    b_standardDist = true;
                    s_standardDist = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6021).value));
                }
                return s_standardDist;
            }
        }

        private static Color s_personalColor;
        private static bool b_personalColor;
        /// <summary>
        /// 结算面板 主角颜色 (FFAE00FF)
        /// </summary>
        public static Color personalColor
        {
            get
            {
                if (!b_personalColor)
                {
                    b_personalColor = true;
                    s_personalColor = (Color)TableManager.ParseValue("Color", (TableManager.instance.GetData<TableConst>(6022).value));
                }
                return s_personalColor;
            }
        }

        private static float s_blockedPerceptionAlpha;
        private static bool b_blockedPerceptionAlpha;
        /// <summary>
        /// 被遮挡时透明度 (0.3)
        /// </summary>
        public static float blockedPerceptionAlpha
        {
            get
            {
                if (!b_blockedPerceptionAlpha)
                {
                    b_blockedPerceptionAlpha = true;
                    s_blockedPerceptionAlpha = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6023).value));
                }
                return s_blockedPerceptionAlpha;
            }
        }

        private static float s_teammateHideNamePanelDelay;
        private static bool b_teammateHideNamePanelDelay;
        /// <summary>
        /// 队友延迟隐藏名字板时间 (0.4)
        /// </summary>
        public static float teammateHideNamePanelDelay
        {
            get
            {
                if (!b_teammateHideNamePanelDelay)
                {
                    b_teammateHideNamePanelDelay = true;
                    s_teammateHideNamePanelDelay = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6024).value));
                }
                return s_teammateHideNamePanelDelay;
            }
        }

        private static float s_enemyHideNamePanelDelay;
        private static bool b_enemyHideNamePanelDelay;
        /// <summary>
        /// 敌人延迟隐藏名字板时间 (0.4)
        /// </summary>
        public static float enemyHideNamePanelDelay
        {
            get
            {
                if (!b_enemyHideNamePanelDelay)
                {
                    b_enemyHideNamePanelDelay = true;
                    s_enemyHideNamePanelDelay = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6025).value));
                }
                return s_enemyHideNamePanelDelay;
            }
        }

        private static float s_hideShowNamePanelDuration;
        private static bool b_hideShowNamePanelDuration;
        /// <summary>
        /// 隐藏显示名字板 变化持续时间 (0.2)
        /// </summary>
        public static float hideShowNamePanelDuration
        {
            get
            {
                if (!b_hideShowNamePanelDuration)
                {
                    b_hideShowNamePanelDuration = true;
                    s_hideShowNamePanelDuration = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6026).value));
                }
                return s_hideShowNamePanelDuration;
            }
        }

        private static float s_perceptionItemStateChangeCooldown;
        private static bool b_perceptionItemStateChangeCooldown;
        /// <summary>
        /// 感知状态切换冷却时间 (1)
        /// </summary>
        public static float perceptionItemStateChangeCooldown
        {
            get
            {
                if (!b_perceptionItemStateChangeCooldown)
                {
                    b_perceptionItemStateChangeCooldown = true;
                    s_perceptionItemStateChangeCooldown = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6027).value));
                }
                return s_perceptionItemStateChangeCooldown;
            }
        }

        private static float s_teammateHideTagArrowDist;
        private static bool b_teammateHideTagArrowDist;
        /// <summary>
        /// 队友 超出该距离隐藏提示箭头（3D距离，米） (0)
        /// </summary>
        public static float teammateHideTagArrowDist
        {
            get
            {
                if (!b_teammateHideTagArrowDist)
                {
                    b_teammateHideTagArrowDist = true;
                    s_teammateHideTagArrowDist = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6028).value));
                }
                return s_teammateHideTagArrowDist;
            }
        }

        private static float s_enemyHideTagArrowDist;
        private static bool b_enemyHideTagArrowDist;
        /// <summary>
        /// 敌人 超出该距离隐藏提示箭头（3D距离，米） (30)
        /// </summary>
        public static float enemyHideTagArrowDist
        {
            get
            {
                if (!b_enemyHideTagArrowDist)
                {
                    b_enemyHideTagArrowDist = true;
                    s_enemyHideTagArrowDist = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6029).value));
                }
                return s_enemyHideTagArrowDist;
            }
        }

        private static float s_signDistShowDelay;
        private static bool b_signDistShowDelay;
        /// <summary>
        /// 从血量状态转换到距离状态延迟时间 秒 (3)
        /// </summary>
        public static float signDistShowDelay
        {
            get
            {
                if (!b_signDistShowDelay)
                {
                    b_signDistShowDelay = true;
                    s_signDistShowDelay = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6030).value));
                }
                return s_signDistShowDelay;
            }
        }

        private static Color s_teammateSignDistColor;
        private static bool b_teammateSignDistColor;
        /// <summary>
        /// 队友距离颜色 (84d70e)
        /// </summary>
        public static Color teammateSignDistColor
        {
            get
            {
                if (!b_teammateSignDistColor)
                {
                    b_teammateSignDistColor = true;
                    s_teammateSignDistColor = (Color)TableManager.ParseValue("Color", (TableManager.instance.GetData<TableConst>(6031).value));
                }
                return s_teammateSignDistColor;
            }
        }

        private static Color s_enemySignDistColor;
        private static bool b_enemySignDistColor;
        /// <summary>
        /// 敌人距离颜色 (eb0000)
        /// </summary>
        public static Color enemySignDistColor
        {
            get
            {
                if (!b_enemySignDistColor)
                {
                    b_enemySignDistColor = true;
                    s_enemySignDistColor = (Color)TableManager.ParseValue("Color", (TableManager.instance.GetData<TableConst>(6032).value));
                }
                return s_enemySignDistColor;
            }
        }

        private static Color s_teammateNameColor;
        private static bool b_teammateNameColor;
        /// <summary>
        /// 队友名字颜色 (0096d4)
        /// </summary>
        public static Color teammateNameColor
        {
            get
            {
                if (!b_teammateNameColor)
                {
                    b_teammateNameColor = true;
                    s_teammateNameColor = (Color)TableManager.ParseValue("Color", (TableManager.instance.GetData<TableConst>(6033).value));
                }
                return s_teammateNameColor;
            }
        }

        private static Color s_enemyNameColor;
        private static bool b_enemyNameColor;
        /// <summary>
        /// 敌人名字颜色 (d5180a)
        /// </summary>
        public static Color enemyNameColor
        {
            get
            {
                if (!b_enemyNameColor)
                {
                    b_enemyNameColor = true;
                    s_enemyNameColor = (Color)TableManager.ParseValue("Color", (TableManager.instance.GetData<TableConst>(6034).value));
                }
                return s_enemyNameColor;
            }
        }

        private static float s_perceptionArrowUIRadius;
        private static bool b_perceptionArrowUIRadius;
        /// <summary>
        /// 受伤方向 提示 UI 旋转半径 (300)
        /// </summary>
        public static float perceptionArrowUIRadius
        {
            get
            {
                if (!b_perceptionArrowUIRadius)
                {
                    b_perceptionArrowUIRadius = true;
                    s_perceptionArrowUIRadius = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6035).value));
                }
                return s_perceptionArrowUIRadius;
            }
        }

        private static float s_hurtPerceptionMaxHitRatio4Fill;
        private static bool b_hurtPerceptionMaxHitRatio4Fill;
        /// <summary>
        /// 受伤百分比，达到最大伤害提示 (0.1)
        /// </summary>
        public static float hurtPerceptionMaxHitRatio4Fill
        {
            get
            {
                if (!b_hurtPerceptionMaxHitRatio4Fill)
                {
                    b_hurtPerceptionMaxHitRatio4Fill = true;
                    s_hurtPerceptionMaxHitRatio4Fill = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6036).value));
                }
                return s_hurtPerceptionMaxHitRatio4Fill;
            }
        }

        private static float s_hurtPerceptionMaxFillAmount;
        private static bool b_hurtPerceptionMaxFillAmount;
        /// <summary>
        /// 最大伤害提示 填充圆 (0.15)
        /// </summary>
        public static float hurtPerceptionMaxFillAmount
        {
            get
            {
                if (!b_hurtPerceptionMaxFillAmount)
                {
                    b_hurtPerceptionMaxFillAmount = true;
                    s_hurtPerceptionMaxFillAmount = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6037).value));
                }
                return s_hurtPerceptionMaxFillAmount;
            }
        }

        private static float s_hurtPerceptionMinFillAmount;
        private static bool b_hurtPerceptionMinFillAmount;
        /// <summary>
        /// 最小伤害提示 填充圆 (0)
        /// </summary>
        public static float hurtPerceptionMinFillAmount
        {
            get
            {
                if (!b_hurtPerceptionMinFillAmount)
                {
                    b_hurtPerceptionMinFillAmount = true;
                    s_hurtPerceptionMinFillAmount = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6038).value));
                }
                return s_hurtPerceptionMinFillAmount;
            }
        }

        private static float s_hurtPerceptionDamageDecreaseRatio;
        private static bool b_hurtPerceptionDamageDecreaseRatio;
        /// <summary>
        /// 受伤提示按 百分比恢复 /s (0.25)
        /// </summary>
        public static float hurtPerceptionDamageDecreaseRatio
        {
            get
            {
                if (!b_hurtPerceptionDamageDecreaseRatio)
                {
                    b_hurtPerceptionDamageDecreaseRatio = true;
                    s_hurtPerceptionDamageDecreaseRatio = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6039).value));
                }
                return s_hurtPerceptionDamageDecreaseRatio;
            }
        }

        private static float s_hurtPerceptionDuration;
        private static bool b_hurtPerceptionDuration;
        /// <summary>
        /// 受伤方向 提示 UI 延迟时间 (5)
        /// </summary>
        public static float hurtPerceptionDuration
        {
            get
            {
                if (!b_hurtPerceptionDuration)
                {
                    b_hurtPerceptionDuration = true;
                    s_hurtPerceptionDuration = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6040).value));
                }
                return s_hurtPerceptionDuration;
            }
        }

        private static Color s_resourceLargeColor;
        private static bool b_resourceLargeColor;
        /// <summary>
        /// 大资源颜色 (ffc303)
        /// </summary>
        public static Color resourceLargeColor
        {
            get
            {
                if (!b_resourceLargeColor)
                {
                    b_resourceLargeColor = true;
                    s_resourceLargeColor = (Color)TableManager.ParseValue("Color", (TableManager.instance.GetData<TableConst>(6041).value));
                }
                return s_resourceLargeColor;
            }
        }

        private static Color s_resourceWillAppearColor;
        private static bool b_resourceWillAppearColor;
        /// <summary>
        /// 资源即将出现提示颜色 (636363FF)
        /// </summary>
        public static Color resourceWillAppearColor
        {
            get
            {
                if (!b_resourceWillAppearColor)
                {
                    b_resourceWillAppearColor = true;
                    s_resourceWillAppearColor = (Color)TableManager.ParseValue("Color", (TableManager.instance.GetData<TableConst>(6042).value));
                }
                return s_resourceWillAppearColor;
            }
        }

        private static float s_perceptionHideDuration;
        private static bool b_perceptionHideDuration;
        /// <summary>
        /// 感知渐隐用时 (0.2)
        /// </summary>
        public static float perceptionHideDuration
        {
            get
            {
                if (!b_perceptionHideDuration)
                {
                    b_perceptionHideDuration = true;
                    s_perceptionHideDuration = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6043).value));
                }
                return s_perceptionHideDuration;
            }
        }

        private static float s_perceptionBlockCheckInterval;
        private static bool b_perceptionBlockCheckInterval;
        /// <summary>
        /// 感知射线遮挡检测间隔时间 (0.5)
        /// </summary>
        public static float perceptionBlockCheckInterval
        {
            get
            {
                if (!b_perceptionBlockCheckInterval)
                {
                    b_perceptionBlockCheckInterval = true;
                    s_perceptionBlockCheckInterval = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6044).value));
                }
                return s_perceptionBlockCheckInterval;
            }
        }

        private static float s_perceptionSetUIPosThreshold;
        private static bool b_perceptionSetUIPosThreshold;
        /// <summary>
        /// 设置UI 位置的像素阈值 (2)
        /// </summary>
        public static float perceptionSetUIPosThreshold
        {
            get
            {
                if (!b_perceptionSetUIPosThreshold)
                {
                    b_perceptionSetUIPosThreshold = true;
                    s_perceptionSetUIPosThreshold = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6045).value));
                }
                return s_perceptionSetUIPosThreshold;
            }
        }

        private static float s_perceptionSetUIScaleThreshold;
        private static bool b_perceptionSetUIScaleThreshold;
        /// <summary>
        /// 设置UI 缩放比例阈值 (0.01)
        /// </summary>
        public static float perceptionSetUIScaleThreshold
        {
            get
            {
                if (!b_perceptionSetUIScaleThreshold)
                {
                    b_perceptionSetUIScaleThreshold = true;
                    s_perceptionSetUIScaleThreshold = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6046).value));
                }
                return s_perceptionSetUIScaleThreshold;
            }
        }

        private static float s_perceptionSetUIQuaternionAngleThreshold;
        private static bool b_perceptionSetUIQuaternionAngleThreshold;
        /// <summary>
        /// 设置UI 旋转的角度阈值 (1)
        /// </summary>
        public static float perceptionSetUIQuaternionAngleThreshold
        {
            get
            {
                if (!b_perceptionSetUIQuaternionAngleThreshold)
                {
                    b_perceptionSetUIQuaternionAngleThreshold = true;
                    s_perceptionSetUIQuaternionAngleThreshold = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6047).value));
                }
                return s_perceptionSetUIQuaternionAngleThreshold;
            }
        }

        private static float s_perceptionUIBlockCheckInterval;
        private static bool b_perceptionUIBlockCheckInterval;
        /// <summary>
        /// 设置感知数据间隔 (0.5)
        /// </summary>
        public static float perceptionUIBlockCheckInterval
        {
            get
            {
                if (!b_perceptionUIBlockCheckInterval)
                {
                    b_perceptionUIBlockCheckInterval = true;
                    s_perceptionUIBlockCheckInterval = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6048).value));
                }
                return s_perceptionUIBlockCheckInterval;
            }
        }

        private static Vector2 s_perceptionUIPosOffset;
        private static bool b_perceptionUIPosOffset;
        /// <summary>
        /// 感知UI偏移(默认) ((0, 30))
        /// </summary>
        public static Vector2 perceptionUIPosOffset
        {
            get
            {
                if (!b_perceptionUIPosOffset)
                {
                    b_perceptionUIPosOffset = true;
                    s_perceptionUIPosOffset = (Vector2)TableManager.ParseValue("Vector2", (TableManager.instance.GetData<TableConst>(6049).value));
                }
                return s_perceptionUIPosOffset;
            }
        }

        private static Vector2 s_summonedPerceptionUIPosOffset;
        private static bool b_summonedPerceptionUIPosOffset;
        /// <summary>
        /// 感知UI偏移（召唤物） ((0, 75))
        /// </summary>
        public static Vector2 summonedPerceptionUIPosOffset
        {
            get
            {
                if (!b_summonedPerceptionUIPosOffset)
                {
                    b_summonedPerceptionUIPosOffset = true;
                    s_summonedPerceptionUIPosOffset = (Vector2)TableManager.ParseValue("Vector2", (TableManager.instance.GetData<TableConst>(6050).value));
                }
                return s_summonedPerceptionUIPosOffset;
            }
        }

        private static float s_perceptionScreenCenterOffset;
        private static bool b_perceptionScreenCenterOffset;
        /// <summary>
        /// 默认屏幕中心显示方式 圆半径 (150)
        /// </summary>
        public static float perceptionScreenCenterOffset
        {
            get
            {
                if (!b_perceptionScreenCenterOffset)
                {
                    b_perceptionScreenCenterOffset = true;
                    s_perceptionScreenCenterOffset = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6051).value));
                }
                return s_perceptionScreenCenterOffset;
            }
        }

        private static float s_dropPerceptionMinScale;
        private static bool b_dropPerceptionMinScale;
        /// <summary>
        /// 掉落物感知缩放最小尺寸 (0.4)
        /// </summary>
        public static float dropPerceptionMinScale
        {
            get
            {
                if (!b_dropPerceptionMinScale)
                {
                    b_dropPerceptionMinScale = true;
                    s_dropPerceptionMinScale = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6052).value));
                }
                return s_dropPerceptionMinScale;
            }
        }

        private static float s_hurtPerceptionMinScale;
        private static bool b_hurtPerceptionMinScale;
        /// <summary>
        /// 受伤感知缩放最小尺寸（建议不动） (0.6)
        /// </summary>
        public static float hurtPerceptionMinScale
        {
            get
            {
                if (!b_hurtPerceptionMinScale)
                {
                    b_hurtPerceptionMinScale = true;
                    s_hurtPerceptionMinScale = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6053).value));
                }
                return s_hurtPerceptionMinScale;
            }
        }

        private static float s_summonedPerceptionMinScale;
        private static bool b_summonedPerceptionMinScale;
        /// <summary>
        /// 召唤物感知缩放最小尺寸 (0.4)
        /// </summary>
        public static float summonedPerceptionMinScale
        {
            get
            {
                if (!b_summonedPerceptionMinScale)
                {
                    b_summonedPerceptionMinScale = true;
                    s_summonedPerceptionMinScale = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6054).value));
                }
                return s_summonedPerceptionMinScale;
            }
        }

        private static float s_playerPerceptionMinScale;
        private static bool b_playerPerceptionMinScale;
        /// <summary>
        /// 玩家感知缩放最小尺寸 (0.4)
        /// </summary>
        public static float playerPerceptionMinScale
        {
            get
            {
                if (!b_playerPerceptionMinScale)
                {
                    b_playerPerceptionMinScale = true;
                    s_playerPerceptionMinScale = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6055).value));
                }
                return s_playerPerceptionMinScale;
            }
        }

        private static float s_playerPerceptioArrownMinScale;
        private static bool b_playerPerceptioArrownMinScale;
        /// <summary>
        /// 玩家感知箭头缩放最小尺寸 (0.8)
        /// </summary>
        public static float playerPerceptioArrownMinScale
        {
            get
            {
                if (!b_playerPerceptioArrownMinScale)
                {
                    b_playerPerceptioArrownMinScale = true;
                    s_playerPerceptioArrownMinScale = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6056).value));
                }
                return s_playerPerceptioArrownMinScale;
            }
        }

        private static float s_occupationPerceptionMinScale;
        private static bool b_occupationPerceptionMinScale;
        /// <summary>
        /// 占点区域感知缩放最小尺寸 (1)
        /// </summary>
        public static float occupationPerceptionMinScale
        {
            get
            {
                if (!b_occupationPerceptionMinScale)
                {
                    b_occupationPerceptionMinScale = true;
                    s_occupationPerceptionMinScale = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6057).value));
                }
                return s_occupationPerceptionMinScale;
            }
        }

        private static bool s_isDropPerceptionUseScale;
        private static bool b_isDropPerceptionUseScale;
        /// <summary>
        /// 感知缩放开关 (TRUE)
        /// </summary>
        public static bool isDropPerceptionUseScale
        {
            get
            {
                if (!b_isDropPerceptionUseScale)
                {
                    b_isDropPerceptionUseScale = true;
                    s_isDropPerceptionUseScale = (bool)TableManager.ParseValue("bool", (TableManager.instance.GetData<TableConst>(6058).value));
                }
                return s_isDropPerceptionUseScale;
            }
        }

        private static bool s_isHurtPerceptionUseScale;
        private static bool b_isHurtPerceptionUseScale;
        /// <summary>
        /// 感知缩放开关 (FALSE)
        /// </summary>
        public static bool isHurtPerceptionUseScale
        {
            get
            {
                if (!b_isHurtPerceptionUseScale)
                {
                    b_isHurtPerceptionUseScale = true;
                    s_isHurtPerceptionUseScale = (bool)TableManager.ParseValue("bool", (TableManager.instance.GetData<TableConst>(6059).value));
                }
                return s_isHurtPerceptionUseScale;
            }
        }

        private static bool s_isSummonedPerceptionUseScale;
        private static bool b_isSummonedPerceptionUseScale;
        /// <summary>
        /// 感知缩放开关 (TRUE)
        /// </summary>
        public static bool isSummonedPerceptionUseScale
        {
            get
            {
                if (!b_isSummonedPerceptionUseScale)
                {
                    b_isSummonedPerceptionUseScale = true;
                    s_isSummonedPerceptionUseScale = (bool)TableManager.ParseValue("bool", (TableManager.instance.GetData<TableConst>(6060).value));
                }
                return s_isSummonedPerceptionUseScale;
            }
        }

        private static bool s_isPlayerPerceptionUseScale;
        private static bool b_isPlayerPerceptionUseScale;
        /// <summary>
        /// 感知缩放开关 (TRUE)
        /// </summary>
        public static bool isPlayerPerceptionUseScale
        {
            get
            {
                if (!b_isPlayerPerceptionUseScale)
                {
                    b_isPlayerPerceptionUseScale = true;
                    s_isPlayerPerceptionUseScale = (bool)TableManager.ParseValue("bool", (TableManager.instance.GetData<TableConst>(6061).value));
                }
                return s_isPlayerPerceptionUseScale;
            }
        }

        private static bool s_isOccupationPerceptionUseScale;
        private static bool b_isOccupationPerceptionUseScale;
        /// <summary>
        /// 感知缩放开关 (TRUE)
        /// </summary>
        public static bool isOccupationPerceptionUseScale
        {
            get
            {
                if (!b_isOccupationPerceptionUseScale)
                {
                    b_isOccupationPerceptionUseScale = true;
                    s_isOccupationPerceptionUseScale = (bool)TableManager.ParseValue("bool", (TableManager.instance.GetData<TableConst>(6062).value));
                }
                return s_isOccupationPerceptionUseScale;
            }
        }

        private static bool s_isDropPerceptionUseBlockCheck;
        private static bool b_isDropPerceptionUseBlockCheck;
        /// <summary>
        /// 感知缩放开关 (FALSE)
        /// </summary>
        public static bool isDropPerceptionUseBlockCheck
        {
            get
            {
                if (!b_isDropPerceptionUseBlockCheck)
                {
                    b_isDropPerceptionUseBlockCheck = true;
                    s_isDropPerceptionUseBlockCheck = (bool)TableManager.ParseValue("bool", (TableManager.instance.GetData<TableConst>(6063).value));
                }
                return s_isDropPerceptionUseBlockCheck;
            }
        }

        private static bool s_isHurtPerceptionUseBlockCheck;
        private static bool b_isHurtPerceptionUseBlockCheck;
        /// <summary>
        /// 感知缩放开关 (FALSE)
        /// </summary>
        public static bool isHurtPerceptionUseBlockCheck
        {
            get
            {
                if (!b_isHurtPerceptionUseBlockCheck)
                {
                    b_isHurtPerceptionUseBlockCheck = true;
                    s_isHurtPerceptionUseBlockCheck = (bool)TableManager.ParseValue("bool", (TableManager.instance.GetData<TableConst>(6064).value));
                }
                return s_isHurtPerceptionUseBlockCheck;
            }
        }

        private static bool s_isSummonedPerceptionUseBlockCheck;
        private static bool b_isSummonedPerceptionUseBlockCheck;
        /// <summary>
        /// 感知缩放开关 (TRUE)
        /// </summary>
        public static bool isSummonedPerceptionUseBlockCheck
        {
            get
            {
                if (!b_isSummonedPerceptionUseBlockCheck)
                {
                    b_isSummonedPerceptionUseBlockCheck = true;
                    s_isSummonedPerceptionUseBlockCheck = (bool)TableManager.ParseValue("bool", (TableManager.instance.GetData<TableConst>(6065).value));
                }
                return s_isSummonedPerceptionUseBlockCheck;
            }
        }

        private static bool s_isPlayerPerceptionUseBlockCheck;
        private static bool b_isPlayerPerceptionUseBlockCheck;
        /// <summary>
        /// 感知缩放开关 (TRUE)
        /// </summary>
        public static bool isPlayerPerceptionUseBlockCheck
        {
            get
            {
                if (!b_isPlayerPerceptionUseBlockCheck)
                {
                    b_isPlayerPerceptionUseBlockCheck = true;
                    s_isPlayerPerceptionUseBlockCheck = (bool)TableManager.ParseValue("bool", (TableManager.instance.GetData<TableConst>(6066).value));
                }
                return s_isPlayerPerceptionUseBlockCheck;
            }
        }

        private static bool s_isOccupationPerceptionUseBlockCheck;
        private static bool b_isOccupationPerceptionUseBlockCheck;
        /// <summary>
        /// 感知缩放开关 (FALSE)
        /// </summary>
        public static bool isOccupationPerceptionUseBlockCheck
        {
            get
            {
                if (!b_isOccupationPerceptionUseBlockCheck)
                {
                    b_isOccupationPerceptionUseBlockCheck = true;
                    s_isOccupationPerceptionUseBlockCheck = (bool)TableManager.ParseValue("bool", (TableManager.instance.GetData<TableConst>(6067).value));
                }
                return s_isOccupationPerceptionUseBlockCheck;
            }
        }

        private static float[] s_perceptionBaseMargin;
        private static bool b_perceptionBaseMargin;
        /// <summary>
        /// 上下左右 基准 ([ 150, 30, 75, 75 ])
        /// </summary>
        public static float[] perceptionBaseMargin
        {
            get
            {
                if (!b_perceptionBaseMargin)
                {
                    b_perceptionBaseMargin = true;
                    s_perceptionBaseMargin = (float[])TableManager.ParseValue("float[]", (TableManager.instance.GetData<TableConst>(6068).value));
                }
                return s_perceptionBaseMargin;
            }
        }

        private static float[] s_perceptionDropMargin;
        private static bool b_perceptionDropMargin;
        /// <summary>
        /// 上下左右 掉落物 ([ 150, 150, 150, 150 ])
        /// </summary>
        public static float[] perceptionDropMargin
        {
            get
            {
                if (!b_perceptionDropMargin)
                {
                    b_perceptionDropMargin = true;
                    s_perceptionDropMargin = (float[])TableManager.ParseValue("float[]", (TableManager.instance.GetData<TableConst>(6069).value));
                }
                return s_perceptionDropMargin;
            }
        }

        private static float[] s_perceptionHurtMargin;
        private static bool b_perceptionHurtMargin;
        /// <summary>
        /// 上下左右 扇形受伤_op_如果过大就会影响显示，填写一个小数值即可 ([ 30, 30, 30, 30 ])
        /// </summary>
        public static float[] perceptionHurtMargin
        {
            get
            {
                if (!b_perceptionHurtMargin)
                {
                    b_perceptionHurtMargin = true;
                    s_perceptionHurtMargin = (float[])TableManager.ParseValue("float[]", (TableManager.instance.GetData<TableConst>(6070).value));
                }
                return s_perceptionHurtMargin;
            }
        }

        private static float[] s_perceptionOccupationPointMargin;
        private static bool b_perceptionOccupationPointMargin;
        /// <summary>
        /// 上下左右 占点区域 ([ 195, 375, 195, 195 ])
        /// </summary>
        public static float[] perceptionOccupationPointMargin
        {
            get
            {
                if (!b_perceptionOccupationPointMargin)
                {
                    b_perceptionOccupationPointMargin = true;
                    s_perceptionOccupationPointMargin = (float[])TableManager.ParseValue("float[]", (TableManager.instance.GetData<TableConst>(6071).value));
                }
                return s_perceptionOccupationPointMargin;
            }
        }

        private static float[] s_perceptionPlayerMargin;
        private static bool b_perceptionPlayerMargin;
        /// <summary>
        /// 上下左右 所有玩家 ([ 45, 45, 45, 45 ])
        /// </summary>
        public static float[] perceptionPlayerMargin
        {
            get
            {
                if (!b_perceptionPlayerMargin)
                {
                    b_perceptionPlayerMargin = true;
                    s_perceptionPlayerMargin = (float[])TableManager.ParseValue("float[]", (TableManager.instance.GetData<TableConst>(6072).value));
                }
                return s_perceptionPlayerMargin;
            }
        }

        private static float[] s_perceptionSummonedMargin;
        private static bool b_perceptionSummonedMargin;
        /// <summary>
        /// 上下左右 召唤物 ([ 45, 15, 15, 15 ])
        /// </summary>
        public static float[] perceptionSummonedMargin
        {
            get
            {
                if (!b_perceptionSummonedMargin)
                {
                    b_perceptionSummonedMargin = true;
                    s_perceptionSummonedMargin = (float[])TableManager.ParseValue("float[]", (TableManager.instance.GetData<TableConst>(6073).value));
                }
                return s_perceptionSummonedMargin;
            }
        }

        private static float[] s_perceptionBatteryRobotMargin;
        private static bool b_perceptionBatteryRobotMargin;
        /// <summary>
        /// 上下左右 炮台 ([ 15, 15, 15, 15 ])
        /// </summary>
        public static float[] perceptionBatteryRobotMargin
        {
            get
            {
                if (!b_perceptionBatteryRobotMargin)
                {
                    b_perceptionBatteryRobotMargin = true;
                    s_perceptionBatteryRobotMargin = (float[])TableManager.ParseValue("float[]", (TableManager.instance.GetData<TableConst>(6074).value));
                }
                return s_perceptionBatteryRobotMargin;
            }
        }

        private static float[] s_perceptionGrenadeMargin;
        private static bool b_perceptionGrenadeMargin;
        /// <summary>
        /// 上下左右 手雷 ([ 45, 15, 15, 15 ])
        /// </summary>
        public static float[] perceptionGrenadeMargin
        {
            get
            {
                if (!b_perceptionGrenadeMargin)
                {
                    b_perceptionGrenadeMargin = true;
                    s_perceptionGrenadeMargin = (float[])TableManager.ParseValue("float[]", (TableManager.instance.GetData<TableConst>(6075).value));
                }
                return s_perceptionGrenadeMargin;
            }
        }

        private static float[] s_perceptionLandmineMargin;
        private static bool b_perceptionLandmineMargin;
        /// <summary>
        /// 上下左右 地雷 ([ 45, 15, 15, 15 ])
        /// </summary>
        public static float[] perceptionLandmineMargin
        {
            get
            {
                if (!b_perceptionLandmineMargin)
                {
                    b_perceptionLandmineMargin = true;
                    s_perceptionLandmineMargin = (float[])TableManager.ParseValue("float[]", (TableManager.instance.GetData<TableConst>(6076).value));
                }
                return s_perceptionLandmineMargin;
            }
        }

        private static float s_dropPerceptionHeight;
        private static bool b_dropPerceptionHeight;
        /// <summary>
        /// 掉落物感知高度（3D距离，米） (0.8)
        /// </summary>
        public static float dropPerceptionHeight
        {
            get
            {
                if (!b_dropPerceptionHeight)
                {
                    b_dropPerceptionHeight = true;
                    s_dropPerceptionHeight = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6077).value));
                }
                return s_dropPerceptionHeight;
            }
        }

        private static float s_playerPerceptionHeight;
        private static bool b_playerPerceptionHeight;
        /// <summary>
        /// 人物感知高度（距离头顶）（3D距离，米） (0.35)
        /// </summary>
        public static float playerPerceptionHeight
        {
            get
            {
                if (!b_playerPerceptionHeight)
                {
                    b_playerPerceptionHeight = true;
                    s_playerPerceptionHeight = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6078).value));
                }
                return s_playerPerceptionHeight;
            }
        }

        private static float s_summonedPerceptionHeight;
        private static bool b_summonedPerceptionHeight;
        /// <summary>
        /// 召唤物默认感知高度（3D距离，米） (0.2)
        /// </summary>
        public static float summonedPerceptionHeight
        {
            get
            {
                if (!b_summonedPerceptionHeight)
                {
                    b_summonedPerceptionHeight = true;
                    s_summonedPerceptionHeight = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6079).value));
                }
                return s_summonedPerceptionHeight;
            }
        }

        private static float s_batteryRobotPerceptionHeight;
        private static bool b_batteryRobotPerceptionHeight;
        /// <summary>
        /// 炮台感知高度（召唤物）（3D距离，米） (1.2)
        /// </summary>
        public static float batteryRobotPerceptionHeight
        {
            get
            {
                if (!b_batteryRobotPerceptionHeight)
                {
                    b_batteryRobotPerceptionHeight = true;
                    s_batteryRobotPerceptionHeight = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6080).value));
                }
                return s_batteryRobotPerceptionHeight;
            }
        }

        private static float s_grenadePerceptionHeight;
        private static bool b_grenadePerceptionHeight;
        /// <summary>
        /// 手雷感知高度（召唤物）（3D距离，米） (0.2)
        /// </summary>
        public static float grenadePerceptionHeight
        {
            get
            {
                if (!b_grenadePerceptionHeight)
                {
                    b_grenadePerceptionHeight = true;
                    s_grenadePerceptionHeight = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6081).value));
                }
                return s_grenadePerceptionHeight;
            }
        }

        private static float s_landminePerceptionHeight;
        private static bool b_landminePerceptionHeight;
        /// <summary>
        /// 地雷感知高度（召唤物）（3D距离，米） (0.2)
        /// </summary>
        public static float landminePerceptionHeight
        {
            get
            {
                if (!b_landminePerceptionHeight)
                {
                    b_landminePerceptionHeight = true;
                    s_landminePerceptionHeight = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6082).value));
                }
                return s_landminePerceptionHeight;
            }
        }

        private static float s_resourceRecyclePerceptionHeight;
        private static bool b_resourceRecyclePerceptionHeight;
        /// <summary>
        /// 资源回收点感知高度（3D距离，米） (0.1)
        /// </summary>
        public static float resourceRecyclePerceptionHeight
        {
            get
            {
                if (!b_resourceRecyclePerceptionHeight)
                {
                    b_resourceRecyclePerceptionHeight = true;
                    s_resourceRecyclePerceptionHeight = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6083).value));
                }
                return s_resourceRecyclePerceptionHeight;
            }
        }

        private static float s_occupationPointPerceptionHeight;
        private static bool b_occupationPointPerceptionHeight;
        /// <summary>
        /// 据点模式占点感知高度（3D距离，米） (0.1)
        /// </summary>
        public static float occupationPointPerceptionHeight
        {
            get
            {
                if (!b_occupationPointPerceptionHeight)
                {
                    b_occupationPointPerceptionHeight = true;
                    s_occupationPointPerceptionHeight = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6084).value));
                }
                return s_occupationPointPerceptionHeight;
            }
        }

        private static float s_attackDefensePerceptionHeight;
        private static bool b_attackDefensePerceptionHeight;
        /// <summary>
        /// 攻防模式占点感知高度（3D距离，米） (0.1)
        /// </summary>
        public static float attackDefensePerceptionHeight
        {
            get
            {
                if (!b_attackDefensePerceptionHeight)
                {
                    b_attackDefensePerceptionHeight = true;
                    s_attackDefensePerceptionHeight = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6085).value));
                }
                return s_attackDefensePerceptionHeight;
            }
        }

        private static float s_hitFlyUIMaxScale;
        private static bool b_hitFlyUIMaxScale;
        /// <summary>
        /// 伤害文字最大缩放 (1)
        /// </summary>
        public static float hitFlyUIMaxScale
        {
            get
            {
                if (!b_hitFlyUIMaxScale)
                {
                    b_hitFlyUIMaxScale = true;
                    s_hitFlyUIMaxScale = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6086).value));
                }
                return s_hitFlyUIMaxScale;
            }
        }

        private static float s_hitFlyUIMinScale;
        private static bool b_hitFlyUIMinScale;
        /// <summary>
        /// 伤害文字最小缩放 (0.4)
        /// </summary>
        public static float hitFlyUIMinScale
        {
            get
            {
                if (!b_hitFlyUIMinScale)
                {
                    b_hitFlyUIMinScale = true;
                    s_hitFlyUIMinScale = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6087).value));
                }
                return s_hitFlyUIMinScale;
            }
        }

        private static float s_hpStyleRedShowPercent;
        private static bool b_hpStyleRedShowPercent;
        /// <summary>
        /// 血条变红 条件 (0.5)
        /// </summary>
        public static float hpStyleRedShowPercent
        {
            get
            {
                if (!b_hpStyleRedShowPercent)
                {
                    b_hpStyleRedShowPercent = true;
                    s_hpStyleRedShowPercent = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6088).value));
                }
                return s_hpStyleRedShowPercent;
            }
        }

        private static float s_hpHitShowRedCD;
        private static bool b_hpHitShowRedCD;
        /// <summary>
        /// 变红持续时间 (0.5)
        /// </summary>
        public static float hpHitShowRedCD
        {
            get
            {
                if (!b_hpHitShowRedCD)
                {
                    b_hpHitShowRedCD = true;
                    s_hpHitShowRedCD = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6089).value));
                }
                return s_hpHitShowRedCD;
            }
        }

        private static float s_hpStyleNormalMinAlpha;
        private static bool b_hpStyleNormalMinAlpha;
        /// <summary>
        /// 不受伤害时，血条变透明的透明度 (0.4)
        /// </summary>
        public static float hpStyleNormalMinAlpha
        {
            get
            {
                if (!b_hpStyleNormalMinAlpha)
                {
                    b_hpStyleNormalMinAlpha = true;
                    s_hpStyleNormalMinAlpha = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6090).value));
                }
                return s_hpStyleNormalMinAlpha;
            }
        }

        private static float s_hpNormalShowDelay;
        private static bool b_hpNormalShowDelay;
        /// <summary>
        /// 血条血恢复后 延迟变透明时间 (3)
        /// </summary>
        public static float hpNormalShowDelay
        {
            get
            {
                if (!b_hpNormalShowDelay)
                {
                    b_hpNormalShowDelay = true;
                    s_hpNormalShowDelay = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6091).value));
                }
                return s_hpNormalShowDelay;
            }
        }

        private static float s_damageItemShowAlphaMin;
        private static bool b_damageItemShowAlphaMin;
        /// <summary>
        /// 血条受伤效果展示(Alpha)最小值 (0)
        /// </summary>
        public static float damageItemShowAlphaMin
        {
            get
            {
                if (!b_damageItemShowAlphaMin)
                {
                    b_damageItemShowAlphaMin = true;
                    s_damageItemShowAlphaMin = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6094).value));
                }
                return s_damageItemShowAlphaMin;
            }
        }

        private static float s_damageItemShowDuration;
        private static bool b_damageItemShowDuration;
        /// <summary>
        /// 血条受伤效果展示时间 (0.3)
        /// </summary>
        public static float damageItemShowDuration
        {
            get
            {
                if (!b_damageItemShowDuration)
                {
                    b_damageItemShowDuration = true;
                    s_damageItemShowDuration = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6095).value));
                }
                return s_damageItemShowDuration;
            }
        }

        private static float s_damageItemShowMaxHeightRatio;
        private static bool b_damageItemShowMaxHeightRatio;
        /// <summary>
        /// 血条受伤效果做大拉伸比例 (10)
        /// </summary>
        public static float damageItemShowMaxHeightRatio
        {
            get
            {
                if (!b_damageItemShowMaxHeightRatio)
                {
                    b_damageItemShowMaxHeightRatio = true;
                    s_damageItemShowMaxHeightRatio = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6096).value));
                }
                return s_damageItemShowMaxHeightRatio;
            }
        }

        private static Color s_hpBarHurtItemColor;
        private static bool b_hpBarHurtItemColor;
        /// <summary>
        /// 玩家血条受伤效果 颜色 (ff0000)
        /// </summary>
        public static Color hpBarHurtItemColor
        {
            get
            {
                if (!b_hpBarHurtItemColor)
                {
                    b_hpBarHurtItemColor = true;
                    s_hpBarHurtItemColor = (Color)TableManager.ParseValue("Color", (TableManager.instance.GetData<TableConst>(6097).value));
                }
                return s_hpBarHurtItemColor;
            }
        }

        private static Color s_shieldBarHurtItemColor;
        private static bool b_shieldBarHurtItemColor;
        /// <summary>
        /// 玩家血条(护盾条)受伤效果 颜色 (0099cc)
        /// </summary>
        public static Color shieldBarHurtItemColor
        {
            get
            {
                if (!b_shieldBarHurtItemColor)
                {
                    b_shieldBarHurtItemColor = true;
                    s_shieldBarHurtItemColor = (Color)TableManager.ParseValue("Color", (TableManager.instance.GetData<TableConst>(6098).value));
                }
                return s_shieldBarHurtItemColor;
            }
        }

        private static Color s_shieldBarNormalColor;
        private static bool b_shieldBarNormalColor;
        /// <summary>
        /// 玩家血条(护盾条)半透效果 颜色 (ffffffb3)
        /// </summary>
        public static Color shieldBarNormalColor
        {
            get
            {
                if (!b_shieldBarNormalColor)
                {
                    b_shieldBarNormalColor = true;
                    s_shieldBarNormalColor = (Color)TableManager.ParseValue("Color", (TableManager.instance.GetData<TableConst>(6099).value));
                }
                return s_shieldBarNormalColor;
            }
        }

        private static Color s_shieldBarHighColor;
        private static bool b_shieldBarHighColor;
        /// <summary>
        /// 玩家血条(护盾条)半透效果 颜色 (ffffffe5)
        /// </summary>
        public static Color shieldBarHighColor
        {
            get
            {
                if (!b_shieldBarHighColor)
                {
                    b_shieldBarHighColor = true;
                    s_shieldBarHighColor = (Color)TableManager.ParseValue("Color", (TableManager.instance.GetData<TableConst>(6100).value));
                }
                return s_shieldBarHighColor;
            }
        }

        private static Color s_gunSkillTriggerColor;
        private static bool b_gunSkillTriggerColor;
        /// <summary>
        /// 玩家枪械被动技能触发 颜色 (0099cc)
        /// </summary>
        public static Color gunSkillTriggerColor
        {
            get
            {
                if (!b_gunSkillTriggerColor)
                {
                    b_gunSkillTriggerColor = true;
                    s_gunSkillTriggerColor = (Color)TableManager.ParseValue("Color", (TableManager.instance.GetData<TableConst>(6101).value));
                }
                return s_gunSkillTriggerColor;
            }
        }

        private static float s_weaponMagazineLowPercent;
        private static bool b_weaponMagazineLowPercent;
        /// <summary>
        /// 当前弹夹子弹数过低，HUD效果 (0.2)
        /// </summary>
        public static float weaponMagazineLowPercent
        {
            get
            {
                if (!b_weaponMagazineLowPercent)
                {
                    b_weaponMagazineLowPercent = true;
                    s_weaponMagazineLowPercent = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6102).value));
                }
                return s_weaponMagazineLowPercent;
            }
        }

        private static float s_weaponTotalMagazineLowPercent;
        private static bool b_weaponTotalMagazineLowPercent;
        /// <summary>
        /// 当前总子弹数过低，HUD效果 (0.2)
        /// </summary>
        public static float weaponTotalMagazineLowPercent
        {
            get
            {
                if (!b_weaponTotalMagazineLowPercent)
                {
                    b_weaponTotalMagazineLowPercent = true;
                    s_weaponTotalMagazineLowPercent = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6103).value));
                }
                return s_weaponTotalMagazineLowPercent;
            }
        }

        private static Color s_weaponMagazineLowColor;
        private static bool b_weaponMagazineLowColor;
        /// <summary>
        /// 子弹数过低，HUD效果，颜色 (ff0000)
        /// </summary>
        public static Color weaponMagazineLowColor
        {
            get
            {
                if (!b_weaponMagazineLowColor)
                {
                    b_weaponMagazineLowColor = true;
                    s_weaponMagazineLowColor = (Color)TableManager.ParseValue("Color", (TableManager.instance.GetData<TableConst>(6104).value));
                }
                return s_weaponMagazineLowColor;
            }
        }

        private static float s_sniperFrontSightMaxDist;
        private static bool b_sniperFrontSightMaxDist;
        /// <summary>
        /// 狙击枪准星最大扩散范围 (50)
        /// </summary>
        public static float sniperFrontSightMaxDist
        {
            get
            {
                if (!b_sniperFrontSightMaxDist)
                {
                    b_sniperFrontSightMaxDist = true;
                    s_sniperFrontSightMaxDist = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6105).value));
                }
                return s_sniperFrontSightMaxDist;
            }
        }

        private static float s_uiCenterRadius;
        private static bool b_uiCenterRadius;
        /// <summary>
        /// 判断，子弹是否精准命中 UI 半径，影响显示伤害数字的颜色 (45)
        /// </summary>
        public static float uiCenterRadius
        {
            get
            {
                if (!b_uiCenterRadius)
                {
                    b_uiCenterRadius = true;
                    s_uiCenterRadius = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6106).value));
                }
                return s_uiCenterRadius;
            }
        }

        private static float s_uiCenterMaxRadius;
        private static bool b_uiCenterMaxRadius;
        /// <summary>
        /// 中心半径最大扩散（暂时没用） (300)
        /// </summary>
        public static float uiCenterMaxRadius
        {
            get
            {
                if (!b_uiCenterMaxRadius)
                {
                    b_uiCenterMaxRadius = true;
                    s_uiCenterMaxRadius = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6107).value));
                }
                return s_uiCenterMaxRadius;
            }
        }

        private static float s_sniperWarningDelyTime;
        private static bool b_sniperWarningDelyTime;
        /// <summary>
        /// 狙击枪提示线，延迟展示时间(暂时没用) (0.5)
        /// </summary>
        public static float sniperWarningDelyTime
        {
            get
            {
                if (!b_sniperWarningDelyTime)
                {
                    b_sniperWarningDelyTime = true;
                    s_sniperWarningDelyTime = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6108).value));
                }
                return s_sniperWarningDelyTime;
            }
        }

        private static float s_resourceZoneHintTextShowDurationRecycleSuccess;
        private static bool b_resourceZoneHintTextShowDurationRecycleSuccess;
        /// <summary>
        /// 资源模式中心提示文本显示时长【资源回收成功】 (1.6)
        /// </summary>
        public static float resourceZoneHintTextShowDurationRecycleSuccess
        {
            get
            {
                if (!b_resourceZoneHintTextShowDurationRecycleSuccess)
                {
                    b_resourceZoneHintTextShowDurationRecycleSuccess = true;
                    s_resourceZoneHintTextShowDurationRecycleSuccess = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6115).value));
                }
                return s_resourceZoneHintTextShowDurationRecycleSuccess;
            }
        }

        private static string s_colorGoldValue;
        private static bool b_colorGoldValue;
        /// <summary>
        /// 金色色值 (ffb21d)
        /// </summary>
        public static string colorGoldValue
        {
            get
            {
                if (!b_colorGoldValue)
                {
                    b_colorGoldValue = true;
                    s_colorGoldValue = (string)TableManager.ParseValue("string", (TableManager.instance.GetData<TableConst>(6117).value));
                }
                return s_colorGoldValue;
            }
        }

        private static Color s_colorGold;
        private static bool b_colorGold;
        /// <summary>
        /// 金色 (ffb21d)
        /// </summary>
        public static Color colorGold
        {
            get
            {
                if (!b_colorGold)
                {
                    b_colorGold = true;
                    s_colorGold = (Color)TableManager.ParseValue("Color", (TableManager.instance.GetData<TableConst>(6118).value));
                }
                return s_colorGold;
            }
        }

        private static Color s_colorHide;
        private static bool b_colorHide;
        /// <summary>
        /// 透明色 (00000000)
        /// </summary>
        public static Color colorHide
        {
            get
            {
                if (!b_colorHide)
                {
                    b_colorHide = true;
                    s_colorHide = (Color)TableManager.ParseValue("Color", (TableManager.instance.GetData<TableConst>(6119).value));
                }
                return s_colorHide;
            }
        }

        private static Color[] s_gunRarityColor;
        private static bool b_gunRarityColor;
        /// <summary>
        /// 武器库枪械稀有度 颜色 （直接对应rarity 从0开始） ([977dba,5a8cba,ffae00,709814,f0f0f0,000000])
        /// </summary>
        public static Color[] gunRarityColor
        {
            get
            {
                if (!b_gunRarityColor)
                {
                    b_gunRarityColor = true;
                    s_gunRarityColor = (Color[])TableManager.ParseValue("Color[]", (TableManager.instance.GetData<TableConst>(6120).value));
                }
                return s_gunRarityColor;
            }
        }

        private static float s_dragRotRatioDefault;
        private static bool b_dragRotRatioDefault;
        /// <summary>
        /// 默认拖屏旋转灵敏度（系数） (0.24)
        /// </summary>
        public static float dragRotRatioDefault
        {
            get
            {
                if (!b_dragRotRatioDefault)
                {
                    b_dragRotRatioDefault = true;
                    s_dragRotRatioDefault = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6121).value));
                }
                return s_dragRotRatioDefault;
            }
        }

        private static float s_autoRotRatioDefault;
        private static bool b_autoRotRatioDefault;
        /// <summary>
        /// 默认屏幕边缘自动旋转灵敏度（系数） (0.24)
        /// </summary>
        public static float autoRotRatioDefault
        {
            get
            {
                if (!b_autoRotRatioDefault)
                {
                    b_autoRotRatioDefault = true;
                    s_autoRotRatioDefault = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(6122).value));
                }
                return s_autoRotRatioDefault;
            }
        }

    }
}
