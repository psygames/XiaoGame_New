using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class GUID
{
    private static Random rand = new Random();

    public static long Long
    {
        get
        {
            long id = 0;
            byte[] bytes = new byte[7];
            rand.NextBytes(bytes);
            for (int i = 0; i < bytes.Length; i++)
            {
                byte b = bytes[i];
                id |= ((long)b) << (i * 8);
            }
            return id;
        }
    }

    public static long UserID
    {
        get
        {
            return DateTime.UtcNow.Ticks;
        }
    }
}
