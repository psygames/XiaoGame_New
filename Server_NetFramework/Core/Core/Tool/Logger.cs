using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Logger
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
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(str, parms);
    }

    public static void Log(string str, params object[] parms)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(str, parms);
    }

    public static void LogError(string str, params object[] parms)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(str, parms);
    }
}
