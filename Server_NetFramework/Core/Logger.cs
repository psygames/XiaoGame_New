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

            WriteToFile(str.FormatStr(parms), level);
        }
    }

    private static void WriteToFile(string str, LogLevel level)
    {
        string ext = System.IO.Path.GetExtension(saveFilePath);
        string dir = System.IO.Path.GetDirectoryName(saveFilePath);
        string path = System.IO.Path.GetFileNameWithoutExtension(saveFilePath);
        string dateStr = DateTime.Now.ToString("_yyyy_MM_dd");
        path = System.IO.Path.Combine(dir, path) + dateStr + ext;
        string spline = DateTime.Now.ToString("-> hh:mm:ss.fff \r\n");
        Core.FileHelper.WriteText(spline + str + "\r\n\r\n", path, true);
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

    private static string saveFilePath = "";
    private static bool saveFileSplitWithDate = false;
    public static void SetFilePath(string _filePath, bool dateSplit = true)
    {
        saveFilePath = _filePath;
        saveFileSplitWithDate = dateSplit;
    }
}

public enum LogLevel
{
    Debug,
    Info,
    Error,
    Warning,
}
