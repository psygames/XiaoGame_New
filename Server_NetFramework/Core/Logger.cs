using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class Logger
{
    public static void Log(object obj)
    {
        Log(obj.ToString());
    }

    public static void LogError(object obj)
    {
        LogError(obj.ToString());
    }

    public static void LogInfo(string str, params object[] parms)
    {
        LogWithColor(str, LogLevel.Info, parms);
    }

    public static void Log(string str, params object[] parms)
    {
        LogWithColor(str, LogLevel.Debug, parms);
    }

    public static void LogError(string str, params object[] parms)
    {
        LogWithColor(str, LogLevel.Error, parms);
    }

    private static object sync_tag = new object();
    private static void LogWithColor(string str, LogLevel level, params object[] parms)
    {
        lock (sync_tag)
        {
            var lastColor = Console.ForegroundColor;
            Console.ForegroundColor = GetLogLevelColor(level);
            Console.WriteLine(str, parms);
            Console.ForegroundColor = lastColor;

            if (newLogAction != null)
            {
                newLogAction.Invoke(level, str.FormatStr(parms));
            }
        }
    }

    private static ConsoleColor GetLogLevelColor(LogLevel level)
    {
        if (level == LogLevel.Info)
            return ConsoleColor.Green;
        else if (level == LogLevel.Error)
            return ConsoleColor.Red;
        else if (level == LogLevel.Warning)
            return ConsoleColor.DarkYellow;
        else
            return ConsoleColor.White;
    }


    public static Action<LogLevel, string> newLogAction = null;
}

public enum LogLevel
{
    Debug,
    Info,
    Error,
    Warning,
}
