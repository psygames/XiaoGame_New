using System;

[Serializable]
public class Vector2
{
    public float x;
    public float y;
    public Vector2() { }
    public Vector2(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public void Normalize()
    {
        float mol = this.mol;
        x = x / mol;
        y = y / mol;
    }

    public float mol
    {
        get { return Sqrt(x * x + y * y); }
    }

    public Vector2 normalized
    {
        get
        {
            float mol = this.mol;
            if (mol == 0)
                return Vector2.zero;
            else
                return new Vector2(x / mol, y / mol);
        }
    }

    public float Dot(Vector2 vector)
    {
        Vector2 v1 = this;
        Vector2 v2 = vector;
        return v1.x * v2.x + v1.y * v2.y;
    }

    public override string ToString()
    {
        return " [ " + this.x + " , " + this.y + " ] ";
    }

    private float Sqrt(float value)
    {
        return (float)Math.Sqrt(value);
    }

    public static Vector2 operator -(Vector2 v1, Vector2 v2)
    {
        return new Vector2(v1.x - v2.x, v1.y - v2.y);
    }

    public static Vector2 operator +(Vector2 v1, Vector2 v2)
    {
        return new Vector2(v1.x + v2.x, v1.y + v2.y);
    }

    public static Vector2 operator /(Vector2 v, float value)
    {
        return new Vector2(v.x / value, v.y / value);
    }

    public static Vector2 operator *(Vector2 v, float value)
    {
        return new Vector2(v.x * value, v.y * value);
    }

    public static Vector2 operator *(float value, Vector2 v)
    {
        return new Vector2(v.x * value, v.y * value);
    }

    public static float Distance(Vector2 v1, Vector2 v2)
    {
        return (v1 - v2).mol;
    }

    public static Vector2 zero
    { get { return new Vector2(0, 0); } }
}
