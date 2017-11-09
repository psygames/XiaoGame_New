using System;
public static class ExtMethods
{   
    public static string FormatStr(this string str, params object[] p)
    {
        return string.Format(str,p);
    }
}