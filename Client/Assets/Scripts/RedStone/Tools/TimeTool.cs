using UnityEngine;
using Coolfish.System;
using System.Collections;
using System.IO;
using System;
using Hotfire;
using System.Collections.Generic;

public class TimeTool
{
    public const long millisecondToTicksRatio = 10000L;//1ticks=100纳秒=0.1微秒
    public const long secondToTicksRatio = 10000000L;//1ticks=100纳秒=0.1微秒

    private static bool s_isInitTimeZoneOffset = false;
    private static TimeSpan s_timeZoneOffset;
    private static TimeSpan timeZoneOffset
    {
        get
        {
            if (!s_isInitTimeZoneOffset)
            {
                s_isInitTimeZoneOffset = true;
                s_timeZoneOffset = DateTime.Now - DateTime.UtcNow;
            }
            return s_timeZoneOffset;
        }
    }

    public static DateTime timeStampZero = new DateTime(1970, 1, 1);

    /// <summary>
    /// 时间戳转DateTime
    /// ticks这个属性值是指从0001年1月1日12：00:00开始到此时的
    /// 以ticks为单位的时间，就是以ticks表示的时间的间隔数。
    /// TimeStamp 是以1970年1月1日 至今的毫秒数
    /// </summary>
    /// <param name="timeStamp"></param>
    /// <returns></returns>
    public static DateTime ToDateTime(long timeStamp)
    {
        return timeStampZero.AddMilliseconds(timeStamp);
    }

    /// <summary>
    /// 将UTC时间转换成本地时间。（+ 时区）
    /// </summary>
    /// <param name="utc"></param>
    /// <returns></returns>
    public static DateTime UtcToLocal(DateTime utc)
    {
        return utc + timeZoneOffset;
    }

    public static TimeCountDown GetCountDown(float countDown)
    {
        TimeSpan span = new TimeSpan((long)(countDown * secondToTicksRatio));
        if (span.Days > 0)
            return new TimeCountDown(TimeUnit.Day, span.Days);
        else if (span.Hours > 0)
            return new TimeCountDown(TimeUnit.Hour, span.Hours);
        else if (span.Minutes > 0)
            return new TimeCountDown(TimeUnit.Minute, span.Minutes);
        else
            return new TimeCountDown(TimeUnit.Second, span.Seconds);
    }

    public static string Format(float timespan, FormatType formatType)
    {
        string str = "";
        if (formatType == FormatType.HHMMSS)
        {
            DateTime time = new System.DateTime((long)(timespan * 10000000L));
            str = "{0:HH:mm:ss}".FormatStr(time);
        }
        return str;
    }
}

public enum FormatType
{
    HHMMSS,
}

public class TimeCountDown
{
    public TimeUnit unit;
    public int value;
    public string unitLT { get { return LT.GetText(s_unitLTDict[unit]); } }
    private static Dictionary<TimeUnit, string> s_unitLTDict = new Dictionary<TimeUnit, string>
    {
        { TimeUnit.Year,LTKey.GENRAL_TIME_YEAR_SHORT},
        { TimeUnit.Month,LTKey.GENRAL_TIME_MONTH_SHORT},
        { TimeUnit.Day,LTKey.GENRAL_TIME_DAY_SHORT},
        { TimeUnit.Hour,LTKey.GENRAL_TIME_HOUR_SHORT},
        { TimeUnit.Minute,LTKey.GENRAL_TIME_MINUTE_SHORT},
        { TimeUnit.Second,LTKey.GENRAL_TIME_SECOND_SHORT},
    };

    public TimeCountDown(TimeUnit unit, int value)
    {
        this.unit = unit;
        this.value = value;
    }

    public override string ToString()
    {
        return value.ToString() + unitLT;
    }
}

public enum TimeUnit
{
    Year,
    Month,
    Day,
    Hour,
    Minute,
    Second,
}
