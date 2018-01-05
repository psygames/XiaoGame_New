using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class MathHelper
{
    static Random rand = new Random();
    public static long LongGUID
    {
        get
        {
            byte[] b = new byte[8];
            rand.NextBytes(b);
            b[7] &= 0x7f;
            long l = 0;
            l = b[0];
            l |= ((long)b[1] << 8);
            l |= ((long)b[2] << 16);
            l |= ((long)b[3] << 24);
            l |= ((long)b[4] << 32);
            l |= ((long)b[5] << 40);
            l |= ((long)b[6] << 48);
            l |= ((long)b[7] << 56);
            return l;
        }
    }

    public static Vector3 RandomPos(float insideRadius, float outsideRadius)
    {
        float xDir = (float)rand.NextDouble() - 0.5f;
        float zDir = (float)rand.NextDouble() - 0.5f;
        float dis = (float)rand.NextDouble();
        dis = (outsideRadius - insideRadius) * dis + insideRadius;
        Vector3 vec = new Vector3(xDir, 0, zDir);
        vec = vec.normalized * dis;
        return vec;
    }

    /// <summary>
    /// random integer num
    /// </summary>
    /// <param name="min">include</param>
    /// <param name="max">exclude</param>
    public static int RandomInt(int min, int max)
    {
        return rand.Next(min, max);
    }
}
