using UnityEngine;
using System.Collections;
using Hotfire;

/*----------------- 以下像素值没有特殊说明的，均按照 baseWidth 作为表针分辨率 ------------------*/
public class UIConfig : UIConfigExt
{
    public const float goldenRatio = 0.618f;

    /*---------- 基础配置 -------------------------*/
    public static readonly Vector3 vector3Hide = Vector3.one * 10000f;
    public static readonly Vector3 vector3Show = Vector3.zero;
    public static Vector2 virtualScreenCenter { get { return GetVirtualScreenCenter(); } }
    public static Vector2 virtualScreenSize { get { return GetVirtualScreenSize(); } }
    public static Vector2 realScreenCenter { get { return GetRealScreenCenter(); } }
    public static Vector2 realScreenSize { get { return GetRealScreenSize(); } }
    public const int textColorNoneID = 1;
    public const int textStyleNoneID = 1;
    public const int TEXT_CREATE_STYLE_ID_DEFAULT = 30;


    /*---------- 标准分辨率 -------------*/
    public static float baseWidth { get { return 1920; } }                      // 因为Mac上有null 问题，暂时这样写死。
    public static float baseHeight { get { return 1 / realAspect * baseWidth; } }
    public static float realAspect { get { return (float)Screen.width / (float)Screen.height; } }
    public const float defaultUIAspect = 16 / 9f;
    public static float virtualToRealRate { get { return (float)Screen.width / baseWidth; } } // 虚拟到屏幕size转换率
    public static float realToVirtualRate { get { return baseWidth / (float)Screen.width; } } // 屏幕到虚拟size转换率


    /*---------- UI 提示信息（UISpecialEffect）-----*/
    public const float beHitHpWarningTimeIncr = 0.4f;                                       // 被击中增加Warning显示时间
    public const Tweener.Method lowHpWarnMethod = Tweener.Method.QuadEaseIn;                // 低血量闪烁方法

    /*----------- 主角血条 -----------------*/
    public const Tweener.Method damgeItemShowMethod = Tweener.Method.QuadEaseOut;           // 血条受伤效果展示曲线
    public const Tweener.Method damgeItemShowAlphaMethod = Tweener.Method.QuadEaseInOut;    // 血条受伤效果展示曲线(Alpha)

    /*----------- 资源掉落模式 --------------*/
    public const float resourceZoneHintTextShowDuration = 0.8f;                             // 资源模式中心提示文本显示时长

    /*----------- 3D Panel ----------------------*/
    public static readonly TRect topPanel3DRectNormal = new TRect(0, -300, 150, 75);           //相对屏幕中心，普通状态血条
    public static readonly TRect topPanel3DRectSniperAim = new TRect(0, -300, 150, 75);        //相对屏幕中心，狙击枪状态血条
    public static readonly TRect normalModeScorePanel3DRect = new TRect(870, 15, 0, 0);        //相对屏幕上方，普通模式比分
    public static readonly TRect occupyModeScorePanel3DRect = new TRect(0, -72, 0, 0);         //相对屏幕上方，占点模式比分
    public static readonly TRect sideHintPanel3DRect = new TRect(-600, -13.5f, 150, 75);       //相对屏幕中心，buff内容提示
    public static readonly TRect teamHintPanel3DRect = new TRect(-675, -234, 75, 30);          //相对屏幕上方，左侧战场情况提示

    public static readonly string diamondImageName = "icon_diamond";//小钻石通用图标
    /*----------- 临时增加，待整理 ---------------*/
    public const float resourceWillAppearTime = 10;                 // 资源掉落模式，资源即将出现时间
    public const float recyclePointWillAppearTime = 10;             // 资源掉落模式，回收点即将出现时间
    public const float recyclePointWillDisappearTime = 10;          // 资源掉落模式，回收点即将消失时间
    public const float timeModeEndCountDown = 15;                   // 时间模式，结束倒计时
    public const float killModeWillEndKills = 5;                    // 击杀模式，即将完成剩余击杀数。

    public const float killHintDuration = 0.3f;                     // 击杀提示持续时间
    public const float killHintHideDuration = 0.1f;                 // 击杀提示隐藏时间
    public const float killHintShowDuration = 0.1f;                 // 击杀提示显示时间
    public const float hideFrontSightLineCD = 0.04f;                // 隐藏十字准星延迟

    public const float frontSightHitDuration = 0.3f;                // 命中提示准星持续时间
    public const float frontSightHitItemDist = 30f;                 // 命中提示准星距离
    public static Color frontSightHitCritColor { get { return UIHelper.FormatColor("ffc000"); } } // 命中提示准星，暴击颜色
    public static Color frontSightHitColor { get { return UIHelper.FormatColor("ffffff"); } } // 命中提示准星，正常颜色
    public static float hitEnemyFrontSightAlpha = 0.7f;             // 命中时，准星透明度变化
    public static float hitEnemyFrontSightAlphaNormalDelay = 0.3f;  // 命中结束后，准星透明度恢复时间

    public const float rushButtonDragSpeedFactor = 1.5f;            // 冲刺按钮拖屏速度倍率

    public const float frontSightItemMaxLength = 30;                // 准星条最大长度
    public const float frontSightItemMinLength = 20;                // 准星条最小长度
    public const float frontSightItemWidth = 6;                     // 准星条宽度
    public const float frontSightItemLengthMultiFireOnce = 1.1f;    // 准星条放大比例，每次开火
    public const float keepFireInterval = 0.3f;                     // 持续开火，判定时间
    public const float frontSightItemLengthReduceDuration = 0.3f;   // 缩小到最小，用时

    public const float rushJoystickDeadZoneRadius = 10;             // 冲刺摇杆中心死区

    public const float sniperUIShowDelay = 0.1f;             // 狙击开镜延迟时间

    /*------------ FOR DEBUG UI ---------------*/
    public static readonly Color hitCheckPlayerColor = UIHelper.FormatColor("880022");
    public static readonly Color hitCheckCircleColor = UIHelper.FormatColor("00ff22");
    public static readonly Color hitCheckInsideCircleColor = UIHelper.FormatColor("ffff00");
    public static readonly Color frontSightCircleColor = UIHelper.FormatColor("00ff22");
    public static readonly Color lockCheckCircleColor = UIHelper.FormatColor("229900");
    public static readonly Color lockCheckPlayerColor = UIHelper.FormatColor("008822");

    /*--------------- GET -----------------*/
    private static Vector2 GetVirtualScreenSize()
    {
        Vector2 ret = Vector2.zero;
        ret.x = UIConfig.baseWidth;
        ret.y = ret.x / (float)Screen.width * Screen.height;
        return ret;
    }

    private static Vector2 GetVirtualScreenCenter()
    {
        return GetVirtualScreenSize() * 0.5f;
    }

    private static Vector2 GetRealScreenSize()
    {
        return new Vector2(Screen.width, Screen.height);
    }

    private static Vector2 GetRealScreenCenter()
    {
        return GetRealScreenSize() * 0.5f;
    }
}