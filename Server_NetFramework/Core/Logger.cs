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
        LogWithColor(str, ConsoleColor.Green, parms);
    }

    public static void Log(string str, params object[] parms)
    {
        LogWithColor(str, ConsoleColor.White, parms);
    }

    public static void LogError(string str, params object[] parms)
    {
        LogWithColor(str, ConsoleColor.DarkRed, parms);
    }

    private static object sync_tag = new object();
    private static void LogWithColor(string str, ConsoleColor color, params object[] parms)
    {
        lock (sync_tag)
        {
            var lastColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(str, parms);
            Console.ForegroundColor = lastColor;
        }
    }
}
