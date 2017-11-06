using UnityEngine;

public static class ObjectUtility
{
    //public static void DestroyImmediate(this GameObject go)
    //{
    //    GameObject.DestroyImmediate(go);
    //}

    public static void DestroyImmediate(this GameObject go, bool allowDestroyingAssets)
    {
        GameObject.DestroyImmediate(go, allowDestroyingAssets);
    }

    public static void Destory(this GameObject go)
    {
        GameObject.Destroy(go);
    }

    public static void Destory(this Object go, float delayTime)
    {
        Object.Destroy(go, delayTime);
    }

    public static void Destory(this Object go)
    {
        Object.Destroy(go);
    }

    public static void DestroyImmediate(this Object go)//, bool allowDestroyingAssets = false)
    {
        Object.DestroyImmediate(go);//, allowDestroyingAssets);
    }
}
