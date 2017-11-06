using System;
using UnityEngine;

namespace Hotfire
{
    class HFMath
    {
        public static float Round(float f,int digits)
        {
            return (float)Math.Round(f, digits);
        }
    }
}
