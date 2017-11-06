using System;
using UnityEngine;
using System.Collections.Generic;

public class GameObjectHelper
{
    public static void ChangeLayer(GameObject gameObject, int layer, string ingoreByName = "", bool includeInActive = false, int ignoreByLayer = -1)
    {
        if (gameObject == null)
            return;
        Transform[] all = gameObject.GetComponentsInChildren<Transform>(includeInActive);
        GameObject tmp;
        for (int i = 0; i < all.Length; i++)
        {
            tmp = all[i].gameObject;
            if (ignoreByLayer != -1 && tmp.layer == ignoreByLayer)
                continue;

            if (string.IsNullOrEmpty(ingoreByName))
                tmp.layer = layer;
            else if (!tmp.name.StartsWith(ingoreByName))
                tmp.layer = layer;
        }
    }

    public static void ChangeLayerIgnorePaticles(GameObject gameObject, int layer, string ingoreByName = "", bool includeInActive = false, int ignoreByLayer = -1)
    {
        if (gameObject == null)
            return;
        Transform[] all = gameObject.GetComponentsInChildren<Transform>(includeInActive);
        GameObject tmp;
        for (int i = 0; i < all.Length; i++)
        {
            tmp = all[i].gameObject;
            if (ignoreByLayer != -1 && tmp.layer == ignoreByLayer)
                continue;
            if (tmp.GetComponent<ParticleSystem>() != null)
                continue;

            if (string.IsNullOrEmpty(ingoreByName))
                tmp.layer = layer;
            else if (!tmp.name.StartsWith(ingoreByName))
                tmp.layer = layer;
        }
    }
    public static void ChangeSkinnedMeshRendererAttribute(GameObject obj, bool includeInactive, Action<SkinnedMeshRenderer> rendererAction)
    {
        if (obj == null || rendererAction == null)
            return;
        SkinnedMeshRenderer[] renderers = obj.GetComponentsInChildren<SkinnedMeshRenderer>(includeInactive);
        for (int i = 0; i < renderers.Length; ++i)
        {
            rendererAction.Invoke(renderers[i]);
        }
    }
    public static void WriteStencilBuffer(GameObject obj)
    {
        ChangeSkinnedMeshRendererAttribute(obj, false, (r) =>
            {
                r.material.SetInt("_StencilValue", 22);
            });
    }

    public static void ChangeSkinnedMeshRendererLayer(GameObject gameObject, int layer, bool includeInActive = false)
    {
        ChangeSkinnedMeshRendererAttribute(gameObject, includeInActive, (r) =>
        {
            r.gameObject.layer = layer;
        });
    }
    public static Transform GetNodeTransform(GameObject gameObject, string nodeName)
    {
        Transform[] allChildren = gameObject.GetComponentsInChildren<Transform>(true);
        for (int i = 0; i < allChildren.Length; i++)
        {
            Transform t = allChildren[i];
            if (t.name.CompareTo(nodeName) == 0)
            {
                return t;
            }
        }
        return null;
    }
    /// <summary>
    /// 标准化gameobject transform各项参数
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    public static void NormalizedTransform(GameObject gameObject)
    {
        if (gameObject != null)
        {
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.identity;
            gameObject.transform.localScale = Vector3.one;
        }
    }

    public static void PlayParticles(GameObject gameObject, bool loop = false)
    {
        StopParticles(gameObject);
        ParticleSystem[] allPar = gameObject.GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < allPar.Length; ++i)
        {
            ParticleSystem tmp = allPar[i];
            tmp.loop = loop;
            tmp.Play();
        }
    }

    public static void StopParticles(GameObject gameObject)
    {
        ParticleSystem[] allPar = gameObject.GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < allPar.Length; ++i)
        {
            allPar[i].loop = false;
            //allPar[i].Clear();
            allPar[i].Stop();
        }
    }
    //扇体和gameObject需垂直轴向应一样Vector3.up
    public static bool PointInSector(Vector3 gameObjectCenter, float gameObjectHeightBottom, float gameObjectHeightTop, float gameObjectRadidus,
        Vector3 sectorCenter, Vector3 sectorForward, float halfAngleCosin, float sectorRadius, float sectorHeightBottom, float sectorHeightTop,
        out Vector3 hitPosOnXZPlane)
    {
        if (gameObjectHeightBottom >= gameObjectHeightTop || sectorHeightBottom >= sectorHeightTop)
        {
            Debug.LogError("height params is error");
        }
        Vector3 nomal = Vector3.up;
        gameObjectHeightBottom += gameObjectCenter.y;
        gameObjectHeightTop += gameObjectCenter.y;
        sectorHeightBottom += sectorCenter.y;
        sectorHeightTop += sectorCenter.y;
        float bottomTmp = gameObjectHeightBottom >= sectorHeightBottom ? gameObjectHeightBottom : sectorHeightBottom;
        float topTmp = gameObjectHeightTop <= sectorHeightTop ? gameObjectHeightTop : sectorHeightTop;
        if (topTmp >= bottomTmp && bottomTmp >= sectorHeightBottom && topTmp <= sectorHeightTop)
        {
            Vector3 centerTmp1 = Vector3.ProjectOnPlane(gameObjectCenter, nomal);
            Vector3 centerTmp2 = Vector3.ProjectOnPlane(sectorCenter, nomal);
            Vector3 dirTmp = centerTmp1 - centerTmp2;
            float dotTmp = Vector3.Dot(dirTmp, sectorForward);
            if (dirTmp.magnitude < sectorRadius + gameObjectRadidus && dotTmp >= halfAngleCosin)
            {
                hitPosOnXZPlane = gameObjectCenter - gameObjectRadidus * dirTmp.normalized;
                return true;
            }
        }
        hitPosOnXZPlane = Vector3.zero;
        return false;
    }
    public static AnimationEvent CreateAnimationEvent(float triggerTime, string functionName)
    {
        AnimationEvent ret = new AnimationEvent();
        ret.time = triggerTime;
        ret.functionName = functionName;
        return ret;
    }

    public static void CopyCameraState(Camera srcCam, Camera desCam)
    {
        desCam.transform.position = srcCam.transform.position;
        desCam.transform.rotation = srcCam.transform.rotation;
        desCam.fieldOfView = srcCam.fieldOfView;
        desCam.nearClipPlane = srcCam.nearClipPlane;
        desCam.farClipPlane = srcCam.farClipPlane;
    }
    public static Vector3 GetNormalByPos(Vector3 pos, Vector3 fireDir, int layer = -1)
    {
        Vector3 normal = Vector3.zero;
        fireDir = fireDir.normalized;
        RaycastHit hit;
        if (Physics.Linecast(pos - fireDir * 0.05f, pos, out hit, layer))
            normal = hit.normal;
        return normal;
    }
    public static bool SameSideAndSamePlane(Vector3 p1, Vector3 p2, Vector3 lineStart, Vector3 lineEnd)
    {
        Vector3 tmp1 = p1 - lineStart;
        Vector3 tmp2 = p2 - lineStart;
        Vector3 tmp3 = lineEnd - lineStart;

        Vector3 tmp4 = Vector3.Cross(tmp1, tmp3);
        Vector3 tmp5 = Vector3.Cross(tmp2, tmp3);
        return Vector3.Dot(tmp4, tmp5) > 0 && Vector3.Cross(tmp4, tmp5) == Vector3.zero;
    }
    public static bool PointInTriangle(Vector3 p, Vector3 a, Vector3 b, Vector3 c)
    {
        return SameSideAndSamePlane(p, a, b, c) && SameSideAndSamePlane(p, b, a, c) && SameSideAndSamePlane(p, c, a, b);
    }
    public static float MillisecondToSecond(float millsceond)
    {
        return millsceond * 0.001f;
    }
    public static float SecondToMillsecond(float second)
    {
        return second * 1000;
    }
    public static float ServerHpToClientHP(int hp)
    {
        return hp * 0.001f;
    }

    public static T AddChild<T>(Transform parent, T prefab) where T : MonoBehaviour
    {
        T t = GameObject.Instantiate(prefab) as T;
        t.transform.SetParent(parent);
        NormalizedTransform(t.gameObject);

        return t;
    }

    public static T AddChildAsync<T>(Transform parent, string prefabPath, Action<T> callback) where T : MonoBehaviour
    {
        var prefab = Resources.Load(prefabPath);
        T t = GameObject.Instantiate(prefab) as T;
        t.transform.SetParent(parent);
        NormalizedTransform(t.gameObject);
        callback.Invoke(t);
        return t;
    }

    public delegate void SetDataContentFunc<P, D>(int index, P item, D content);

    public delegate bool IsItemEnabledFunc<P, D>(int index, D content);
    public delegate void SetItemContentFunc<P>(int index, P item);

    public static void CreateListItems<P>(P prefab, Transform parent, List<P> itemList, int count, SetItemContentFunc<P> function = null)
        where P : MonoBehaviour
    {
        if (itemList == null)
            return;
        for (int i = itemList.Count; i < count; ++i)
        {
            itemList.Add(AddChild(parent, prefab));
        }
        for (int i = 0; i < itemList.Count; ++i)
        {
            itemList[i].gameObject.SetActive(i < count);
            if (i < count && function != null)
            {
                function(i, itemList[i]);
            }
        }
    }
    public static void SetListContent<P, D>(P prefab, Transform parent, List<P> itemList, System.Collections.Generic.ICollection<D> dataList, SetDataContentFunc<P, D> setContentFunc,
        int start = 0,
        int count = 0)
        where P : MonoBehaviour
    {
        SetListContent<P, D>(prefab, parent, itemList, dataList, null, setContentFunc, start, count);
    }
    public static void SetListContent<P, D>(P prefab, Transform parent, List<P> itemList, System.Collections.Generic.ICollection<D> dataList, IsItemEnabledFunc<P, D> isItemEnabledFunc, SetDataContentFunc<P, D> setContentFunc,
        int start = 0,
        int count = 0)
        where P : MonoBehaviour
    {
        if (itemList == null)
            return;
        if (dataList == null)
        {
            foreach (var item in itemList)
            {
                item.gameObject.SetActive(false);
            }
        }
        else
        {
            if (count == 0 || (start + count) > dataList.Count)
            {
                count = dataList.Count - start;
            }
            while (itemList.Count < count)
            {
                itemList.Add(AddChild(parent, prefab));
            }

            var emu = dataList.GetEnumerator();

            for (int i = 0; i < start && emu.MoveNext(); ++i)
            {
            }

            for (int i = 0; i < itemList.Count; ++i)
            {
                if (emu.MoveNext())
                {
                    int index = start + i;
                    var item = itemList[i];
                    var data = emu.Current;
                    if (isItemEnabledFunc != null)
                        if (!isItemEnabledFunc(index, data))
                        {
                            item.gameObject.SetActive(false);
                            continue;
                        }
                    itemList[i].gameObject.SetActive(true);
                    if (setContentFunc != null)
                        setContentFunc(index, item, data);
                }
                else
                {
                    itemList[i].gameObject.SetActive(false);
                }
            }
        }
    }
    /// <summary>
    /// sphereRadius传入是单位为米的半径长度，返回后是投影到屏幕上的近似像素长度
    /// </summary>
    /// <param name="cones"></param>
    /// <param name="spherePosition"></param>
    /// <param name="sphereRadius"></param>
    /// <returns></returns>
    public static bool CheckSphereInCones(Plane[] cones, Vector3 spherePosition, ref float sphereRadius)
    {
        bool isInCones = true;
        for (int i = 0; i < cones.Length; i++)
        {
            if (cones[i].GetDistanceToPoint(spherePosition) < -sphereRadius)
            {
                isInCones = false;
                break;
            }
        }
        if (isInCones)
        {
            Camera mainCam = Camera.main;
            float depth = (mainCam.transform.position - spherePosition).magnitude;
            sphereRadius = mainCam.pixelHeight * sphereRadius / (2 * depth * Mathf.Tan(mainCam.fieldOfView * 0.5f * Mathf.Deg2Rad));
        }
        return isInCones;
    }
}
