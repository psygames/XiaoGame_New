using System;

[Serializable]
public class Vector3
{
    public float x;
    public float y;
    public float z;
    public Vector3() { }
    public Vector3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public void Normalize()
    {
        float mol = this.mol;
        x = x / mol;
        y = y / mol;
        z = z / mol;
    }

    public float mol
    {
        get { return Sqrt(x * x + y * y + z * z); }
    }

    public Vector3 normalized
    {
        get
        {
            float mol = this.mol;
            if (mol == 0)
                return Vector3.zero;
            else
                return new Vector3(x / mol, y / mol, z / mol);
        }
    }

    public float Dot(Vector3 vector)
    {
        Vector3 v1 = this;
        Vector3 v2 = vector;
        return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
    }

    public Vector3 Cross(Vector3 vector)
    {
        float x2 = vector.x;
        float y2 = vector.y;
        float z2 = vector.z;
        return new Vector3(y * z2 - z * y2, z * x2 - x * z2, x * y2 - y * x2);
    }

    public override string ToString()
    {
        return " [ " + this.x + " , " + this.y + " , " + this.z + " ] ";
    }

    private float Sqrt(float value)
    {
        return (float)Math.Sqrt(value);
    }

    public static Vector3 operator -(Vector3 v1, Vector3 v2)
    {
        return new Vector3(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
    }

    public static Vector3 operator +(Vector3 v1, Vector3 v2)
    {
        return new Vector3(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
    }

    public static Vector3 operator /(Vector3 v, float value)
    {
        return new Vector3(v.x / value, v.y / value, v.z / value);
    }

    public static Vector3 operator *(Vector3 v, float value)
    {
        return new Vector3(v.x * value, v.y * value, v.z * value);
    }

    public static Vector3 operator *(float value, Vector3 v)
    {
        return new Vector3(v.x * value, v.y * value, v.z * value);
    }

    public static float Distance(Vector3 v1, Vector3 v2)
    {
        return (v1 - v2).mol;
    }

    public static Vector3 zero
    { get { return new Vector3(0, 0, 0); } }
}
