using UnityEngine;
using Coolfish.System;

namespace Hotfire
{
    public struct TLine
    {
        // 直线方程  ax + by + c = 0 （一般式）
        public float a, b, c;
        public float k
        {
            get
            {
                if (b != 0)
                    return -a / b;
                return float.NaN;
            }
        }

        /// <summary>
        /// 一般式 ax + by + c = 0 (其中a,b不同时为0) 
        /// 若 b ≠ 0 ,则对 b 进行归一化处理，使 b = 1
        /// 若 b = 0 ,则对 a 进行归一化处理，是 a = 1
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        public TLine(float a, float b, float c)
        {
            if (b != 0)
            {
                this.a = a / b;
                this.c = c / b;
                this.b = 1;
            }
            else
            {
                this.c = c / a;
                this.b = 0;
                this.a = 1;
            }
        }

        /// <summary>
        /// 两点式: (y-y0)/(y0-y1)=(x-x0)/(x0-x1) 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        public TLine(Vector2 p1, Vector2 p2)
        {
            if (p1.x == p2.x) //平行于Y轴
            {
                a = 1;
                b = 0;
                c = -p1.x;
            }
            else
            {
                float k = (p1.y - p2.y) / (p1.x - p2.x);
                a = -k;
                b = 1;
                c = k * p1.x - p1.y;
            }
        }


        /// <summary>
        /// 是否与...平行
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool IsParallel(TLine line)
        {
            if (b == 0 && line.b == 0
                || b != 0 && line.b != 0 && k == line.k)
                return true;
            return false;
        }

        public Vector2 CrossWith(TLine line)
        {
            if (IsParallel(line))
                return new Vector2(float.NaN, float.NaN);
            float d = line.a;
            float e = line.b;
            float f = line.c;
            float x = (b * f - c * e) / (a * e - b * d);
            float y = (c * d - a * f) / (a * e - b * d);
            Vector2 crossPoint = new Vector2(x, y);
            return crossPoint;
        }

        public override string ToString()
        {
            return "EQ: {0}x + {1}y + {2} = 0".FormatStr(a, b, c);
        }
    }
}