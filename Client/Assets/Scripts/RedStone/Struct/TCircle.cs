using UnityEngine;

namespace Hotfire
{
    public struct TCircle
    {
        /// <summary>
        /// Center X
        /// </summary>
        public float x;
        /// <summary>
        /// Center Y
        /// </summary>
        public float y;

        public float radius;


        /// <summary>
        /// 外接矩形
        /// </summary>
        public TRect boundingRect
        {
            get { return new TRect(center, Vector2.one * radius * 2); }
        }

        /// <summary>
        /// 表面积
        /// </summary>
        public float surfaceArea
        {
            get { return Mathf.PI * radius * radius; }
        }

        /// <summary>
        /// 周长
        /// </summary>
        public float perimeter
        {
            get { return Mathf.PI * 2 * radius; }
        }

        public Vector2 center
        {
            get { return new Vector2(x, y); }
            set { x = value.x; y = value.y; }
        }

        public TCircle(Vector2 centerPos, float radius) : this(centerPos.x, centerPos.y, radius)
        {

        }

        public TCircle(float x, float y, float radius)
        {
            this.x = x;
            this.y = y;
            this.radius = radius;
        }

        public bool IsOverLapWith(TRect rect)
        {
            float halfWidth = rect.width * 0.5f;
            float halfHeight = rect.height * 0.5f;
            float distanceX = Mathf.Abs(x - rect.x);
            float distanceY = Mathf.Abs(y - rect.y);
            if (distanceX > (halfWidth + radius) || distanceY > (halfHeight + radius))
                return false;
            else if (distanceX <= halfWidth || distanceY <= halfHeight)
                return true;
            float sq = (distanceX - halfWidth) * (distanceX - halfWidth)
                + (distanceY - halfHeight) * (distanceY - halfHeight);
            return (sq <= radius * radius);
        }

        public bool IsOverLapWith(TCircle circle)
        {
            return (radius + circle.radius) * (radius + circle.radius) > (center - circle.center).sqrMagnitude;
        }

        public bool IsOverLapWith(Vector2 point)
        {
            return radius * radius > (center - point).sqrMagnitude;
        }

        public override string ToString()
        {
            return "Pos:" + center.ToString() + " Radius:" + radius.ToString();
        }

        public static TCircle Lerp(TCircle a, TCircle b, float t)
        {
            return new TCircle(Vector2.Lerp(a.center, b.center, t), Mathf.Lerp(a.radius, b.radius, t));
        }

        public static TCircle operator *(TCircle rect, float a)
        {
            rect.radius *= a;
            return rect;
        }
    }
}