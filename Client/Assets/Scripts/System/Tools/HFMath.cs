using System;
using UnityEngine;

 namespace RedStone
{
    class HFMath
    {
        public static float Round(float f,int digits)
        {
            return (float)Math.Round(f, digits);
        }
    }
}
