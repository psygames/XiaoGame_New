using System;
using UnityEngine;
using System.Collections.Generic;
using RedStone;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Text.RegularExpressions;
 

public class UIHelper
{
    public static float FormatToVirtual(float value)
    {
        return value * UIConfig.realToVirtualRate;
    }

    public static Vector2 FormatToVirtual(Vector2 vec2)
    {
        return vec2 * UIConfig.realToVirtualRate;
    }

    public static Vector3 FormatToVirtual(Vector3 vec3)
    {
        return new Vector3(vec3.x * UIConfig.realToVirtualRate, vec3.y * UIConfig.realToVirtualRate, vec3.z);
    }

    public static TCircle FormatToVirtual(TCircle circle)
    {
        TCircle _new = new TCircle();
        _new.center = FormatToVirtual(circle.center);
        _new.radius = FormatToVirtual(circle.radius);
        return _new;
    }

    public static TRect FormatToVirtual(TRect ellipse)
    {
        TRect _new = new TRect();
        _new.center = FormatToVirtual(ellipse.center);
        _new.size = FormatToVirtual(ellipse.size);
        return _new;
    }

    public static TEllipse FormatToVirtual(TEllipse ellipse)
    {
        TEllipse _new = new TEllipse();
        _new.center = FormatToVirtual(ellipse.center);
        _new.size = FormatToVirtual(ellipse.size);
        return _new;
    }

    public static float FormatToReal(float value)
    {
        return value * UIConfig.virtualToRealRate;
    }

    public static Vector2 FormatToReal(Vector2 vec2)
    {
        return vec2 * UIConfig.virtualToRealRate;
    }

    public static TCircle FormatToReal(TCircle circle)
    {
        TCircle _new = new TCircle();
        _new.center = FormatToReal(circle.center);
        _new.radius = FormatToReal(circle.radius);
        return _new;
    }

    public static TRect FormatToReal(TRect ellipse)
    {
        TRect _new = new TRect();
        _new.center = FormatToReal(ellipse.center);
        _new.size = FormatToReal(ellipse.size);
        return _new;
    }

    public static TEllipse FormatToReal(TEllipse ellipse)
    {
        TEllipse _new = new TEllipse();
        _new.center = FormatToReal(ellipse.center);
        _new.size = FormatToReal(ellipse.size);
        return _new;
    }

    public static Vector2 GetScreenSize(Vector2 scale)
    {
        return Vector2.Scale(UIConfig.realScreenSize, scale);
    }

    public static bool IsPosInCameraSight(Vector3 pos)
    {
        Vector3 screenPos = mainCamera.WorldToScreenPoint(pos);
        if (IsWorldToScreenPosInScreen(screenPos))
            return true;
        return false;
    }

    public static bool IsPosInCameraForward(Vector3 pos)
    {
        Vector3 screenPos = mainCamera.WorldToScreenPoint(pos);
        return screenPos.z > 0;
    }

    public static TRect GetVirtualScreenRect()
    {
        return new TRect(UIConfig.virtualScreenCenter, UIConfig.virtualScreenSize);
    }

    public static TRect GetObjectScreenRect(Vector3 centerPos, float width, float height)
    {
        Vector3 cp = mainCamera.WorldToScreenPoint(centerPos);
        Vector3 up = mainCamera.WorldToScreenPoint(centerPos + Vector3.up);
        int project = cp.z > 0 ? 1 : -1; // 是否指在屏幕后？
        float point3dTo2dRatio = up.y - cp.y;
        float boxWidth = point3dTo2dRatio * width;
        float boxHeight = point3dTo2dRatio * height;
        TRect rect = new TRect(cp, new Vector2(boxWidth, boxHeight) * project);
        return rect;
    }

    public static TRect GetObjectScreenRect2(Vector3 bottomPos, float width, float height)
    {
        return GetObjectScreenRect(bottomPos + Vector3.up * height * 0.5f, width, height);
    }

    /// <summary>
    /// 圆内随机点（面积均匀分布随机）
    /// </summary>
    /// <param name="circle"></param>
    /// <returns></returns>
    public static Vector2 RandomCirclePoint(TCircle circle)
    {
        float theta = UnityEngine.Random.value * 2 * Mathf.PI;
        float radius = Mathf.Sqrt(UnityEngine.Random.value) * circle.radius;
        float x = radius * Mathf.Sin(theta);
        float y = radius * Mathf.Cos(theta);
        return new Vector2(x, y) + circle.center;
    }

    public static Vector3 GetPosToLineProjectPos(Vector3 pos, Vector3 line1, Vector3 line2)
    {
        Vector3 vec1 = pos - line1;
        Vector3 vec2 = line2 - line1;
        Vector3 vecProj = Vector3.Project(vec1, vec2);
        return vecProj + line1;
    }

    /// <summary>
    /// 点到直线距离以及投影点
    /// </summary>
    /// <param name="point"></param>
    /// <param name="lineStart"></param>
    /// <param name="lineEnd"></param>
    /// <param name="subPoint"></param>
    /// <param name="dist"></param>
    
    public static void GetPointToCircleNearestPointAndDist(Vector2 point, TCircle circle, out Vector2 outPoint, out float dist)
    {
        Vector2 delta = point - circle.center;
        dist = delta.magnitude - circle.radius;
        outPoint = circle.center + Mathf.Min(circle.radius, delta.magnitude) * delta.normalized;
    }
    /// <summary>
    /// 点到直线距离以及投影点
    /// </summary>
    /// <param name="point"></param>
    /// <param name="lineStart"></param>
    /// <param name="lineEnd"></param>
    /// <param name="subPoint"></param>
    /// <param name="dist"></param>
    public static void GetPointToLineDistAndSubpoint(Vector2 point, Vector2 lineStart, Vector2 lineEnd, out Vector2 subPoint, out float dist)
    {
        subPoint = Vector2.zero;
        dist = 0;

        float rtX = lineEnd.x - lineStart.x;
        float rtY = lineEnd.y - lineStart.y;

        if (rtX == 0 && rtY == 0)
        {
            subPoint = lineStart;
        }
        else if (Mathf.Abs(rtX) <= 0.001f)
        {
            // x = c;直线方程 （c == lineStart.x）
            subPoint = new Vector2(lineStart.x, point.y);
        }
        else if (rtY == 0)
        {
            // y = c;直线方程 （c == lineStart.y）
            subPoint = new Vector2(point.x, lineStart.y);
        }
        else
        {
            float rt = rtY / rtX;
            float c = -rt * lineStart.x + lineStart.y;

            float rtNormal = -1 / rt;
            float cNormal = point.y - rtNormal * point.x;

            float intersectionPointX = (cNormal - c) / (rt - rtNormal);
            float intersectionPointY = rt * intersectionPointX + c;
            subPoint = new Vector2(intersectionPointX, intersectionPointY);
        }
        dist = Vector2.Distance(subPoint, point);
    }

    /// <summary>
    /// 点到线段最近距离以及最近的点
    /// </summary>
    /// <param name="point"></param>
    /// <param name="lineStart"></param>
    /// <param name="lineEnd"></param>
    /// <param name="subPoint"></param>
    /// <param name="dist"></param>
    public static void GetPointToSegmentNearestPointAndDist(Vector2 point, Vector2 lineStart, Vector2 lineEnd, out Vector2 subPoint, out float dist)
    {
        Vector2 outSubPoint;
        float outDist;

        GetPointToLineDistAndSubpoint(point, lineStart, lineEnd, out outSubPoint, out outDist);

        Vector2 dir = lineEnd - lineStart;
        Vector2 dir2 = point - lineStart;
        Vector2 dir3 = point - lineEnd;
        if (Vector2.Dot(dir, dir2) < 0)
        {
            outSubPoint = lineStart;
            outDist = Vector2.Distance(point, outSubPoint);
        }
        else if (Vector2.Dot(-dir, dir3) < 0)
        {
            outSubPoint = lineEnd;
            outDist = Vector2.Distance(point, outSubPoint);
        }

        subPoint = outSubPoint;
        dist = outDist;
    }

    /// <summary>
    /// 点到线段横向最近距离以及最近的点
    /// </summary>
    /// <param name="point"></param>
    /// <param name="lineStart"></param>
    /// <param name="lineEnd"></param>
    /// <param name="subPoint"></param>
    /// <param name="dist"></param>
    public static void GetPointToSegmentNearestPointAndDistWithSameY(Vector2 point, Vector2 lineStart, Vector2 lineEnd, out Vector2 subPoint, out float dist)
    {
        Vector2 outSubPoint;
        float outDist;

        GetPointToLineDistAndSubpoint(point, lineStart, lineEnd, out outSubPoint, out outDist);

        Vector2 dir = lineEnd - lineStart;
        Vector2 dir2 = point - lineStart;
        Vector2 dir3 = point - lineEnd;
        if (Vector2.Dot(dir, dir2) < 0)
        {
            outSubPoint = lineStart;
            outDist = Vector2.Distance(point, outSubPoint);
        }
        else if (Vector2.Dot(-dir, dir3) < 0)
        {
            outSubPoint = lineEnd;
            outDist = Vector2.Distance(point, outSubPoint);
        }

        subPoint = outSubPoint;
        dist = outDist;
    }

    public static void GetPointToPointLinesAndDistWithSameY(Vector2 point, Vector2[] allPoints, float yMin, float yMax, out Vector2 subPoint, out float dist)
    {
        subPoint = Vector2.zero;
        dist = float.MaxValue;
        Vector2 tmpSubP;
        float tmpDist;
        for (int i = 0; i < allPoints.Length - 1; i++)
        {
            GetLinePointWithY(allPoints[i], allPoints[i + 1], point, out tmpSubP, out tmpDist);
            if (dist > tmpDist)
            {
                dist = tmpDist;
                subPoint = tmpSubP;
            }
        }
        if (dist == float.MaxValue)
        {
            float dtmp1 = Vector2.Distance(point, allPoints[0]);
            float dtmp2 = Vector2.Distance(point, allPoints[allPoints.Length - 1]);
            if (dtmp1 > dtmp2)
            {
                dist = dtmp2;
                subPoint = new Vector2(allPoints[allPoints.Length - 1].x, Mathf.Max(point.y, yMin));
            }
            else
            {
                dist = dtmp1;
                subPoint = new Vector2(allPoints[0].x, Mathf.Min(point.y, yMax));
            }
        }
    }

    public static void GetLinePointWithY(Vector2 lineStart, Vector2 lineEnd, Vector2 point, out Vector2 outSubPoint, out float outDist)
    {
        outSubPoint = Vector2.zero;
        outDist = float.MaxValue;
        float y = point.y;
        float x1 = lineStart.x;
        float y1 = lineStart.y;
        float x2 = lineEnd.x;
        float y2 = lineEnd.y;

        if ((y >= y1 && y <= y2) || (y >= y2 && y <= y1))
        {
            float x = (y - y2) * (x1 - x2) / (y1 - y2) + x2; //两点式
            outSubPoint = new Vector2(x, y);
            outDist = Vector2.Distance(point, outSubPoint);
        }
    }

    public static void GetPointToPointLinesNearestPointAndDist(Vector2 point, Vector2[] rectSubPoint, out Vector2 subPoint, out float dist)
    {
        Vector2 outSubPoint = Vector2.zero;
        float outDist = float.MaxValue;

        Vector2 tmpSubP;
        float tmpDist;
        for (int i = 0; i < rectSubPoint.Length - 1; i++)
        {
            GetPointToSegmentNearestPointAndDist(point, rectSubPoint[i], rectSubPoint[i + 1], out tmpSubP, out tmpDist);
            if (tmpDist < outDist)
            {
                outSubPoint = tmpSubP;
                outDist = tmpDist;
            }
        }

        subPoint = outSubPoint;
        dist = outDist;
    }

    /// <summary>
    /// 点到矩形四条边最近距离以及最近的点
    /// </summary>
    /// <param name="point"></param>
    /// <param name="rect"></param>
    /// <param name="subPoint"></param>
    /// <param name="dist"></param>
    public static void GetPointToRectSidesNearestPointAndDist(Vector2 point, TRect rect, out Vector2 subPoint, out float dist)
    {
        Vector2 outSubPoint = Vector2.zero;
        float outDist = float.MaxValue;
        Vector2[] rectSubPoint = { new Vector2(rect.x - rect.width*0.5f , rect.y + rect.height * 0.5f) // 左上
                                  ,new Vector2(rect.x + rect.width*0.5f , rect.y + rect.height * 0.5f) // 右上
                                  ,new Vector2(rect.x + rect.width*0.5f , rect.y - rect.height * 0.5f) // 右下
                                  ,new Vector2(rect.x - rect.width*0.5f , rect.y - rect.height * 0.5f) // 左下 
                                  ,new Vector2(rect.x - rect.width*0.5f , rect.y + rect.height * 0.5f) // 左上 
                                 };

        GetPointToPointLinesNearestPointAndDist(point, rectSubPoint, out outSubPoint, out outDist);
        subPoint = outSubPoint;
        dist = outDist;
    }

    public static void HideTransform(Transform transform)
    {
        transform.localScale = Vector3.zero;
        //transform.localPosition = UIConfig.vector3Hide;
    }

    public static void ShowTransform(Transform transform, bool show)
    {
        transform.localScale = show ? Vector3.one : Vector3.zero;
    }
    public static void ShowTransform(Transform transform, Vector3 scale)
    {
        transform.localScale = scale;
    }
    public static void ShowTransform(Transform transform)
    {
        transform.localScale = Vector3.one;
        //transform.localPosition = UIConfig.vector3Show;
    }

    public static Vector2 GetListenerPos(UUIEventListener listener)
    {
        return listener.pointerEventData.position;
    }

    public static Vector2 GetListenerDelta(UUIEventListener listener)
    {
        return listener.pointerEventData.delta;
    }

    public static Vector2 GetListenerVirtualPos(UUIEventListener listener)
    {
        return FormatToVirtual(GetListenerPos(listener));
    }

    public static Vector2 GetListenerVirtualDelta(UUIEventListener listener)
    {
        return FormatToVirtual(GetListenerDelta(listener));
    }

    public static void SetImageAlpha(UnityEngine.UI.Image image, float alpha)
    {
        if (image == null)
            return;
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
    }

    public static void SetImageAlpha(UnityEngine.UI.RawImage image, float alpha)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
    }

    public static void SetImageAlpha(UnityEngine.UI.Text text, float alpha)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
    }

    public static void SetAlpha(UnityEngine.UI.Graphic graphic, float alpha)
    {
        if (graphic.color.a != alpha)
            graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, alpha);
    }

    /// <summary>
    /// 适应屏
    /// </summary>
    /// <param name="background"></param>
    public static void AdapteBackground(RectTransform background)
    {
        float aspect1 = background.sizeDelta.x / background.sizeDelta.y;
        Vector2 size = new Vector2(UIConfig.baseWidth, UIConfig.baseWidth / aspect1);
        if (aspect1 > UIConfig.realAspect)
        {
            size *= aspect1 / UIConfig.realAspect;
        }
        background.sizeDelta = size;
    }

    /// <summary>
    /// 拉伸
    /// </summary>
    /// <param name="background"></param>
    public static void StretchBackground(RectTransform background)
    {
        float aspectReal = UIConfig.realAspect;
        float aspectVirtual = UIConfig.defaultUIAspect;
        if (aspectVirtual > aspectReal)
        {
            background.localScale = new Vector3(1, aspectVirtual / aspectReal, 1);
        }
    }

    public static Color ColorInverse(Color c)
    {
        float h, s, v;
        Color.RGBToHSV(c, out h, out s, out v);
        h = (h + 0.5f) % 1f;
        v = 1 - v;
        Color inverse = Color.HSVToRGB(h, s, v);
        return inverse;
    }

    public static Color SimpleColorInverse(Color c)
    {
        if (c.grayscale < 0.35f)
            return Color.white;
        return Color.black;
    }

    public static string FormatColorToHexStr(Color c, string prefix = "#")
    {
        float[] rgba = new float[] { c.r, c.g, c.b, c.a };
        string hexRgba = prefix;
        for (int i = 0; i < rgba.Length; i++)
        {
            hexRgba += "{0:X2}".FormatStr(Mathf.RoundToInt(rgba[i] * 255));
        }
        return hexRgba;
    }

    public static Color FormatColor(string color)
    {
        color = color.Trim('#');
        float max = 255;
        Color c = new Color();
        if (color.Length == 8)
        {
            c.a = Int32.Parse(color.Substring(6, 2), System.Globalization.NumberStyles.HexNumber) / max;
        }
        else if (color.Length == 6)
        {
            c.a = 1;
        }
        else
            Debug.LogError("Font Color Error :" + color);
        c.r = Int32.Parse(color.Substring(0, 2), System.Globalization.NumberStyles.HexNumber) / max;
        c.g = Int32.Parse(color.Substring(2, 2), System.Globalization.NumberStyles.HexNumber) / max;
        c.b = Int32.Parse(color.Substring(4, 2), System.Globalization.NumberStyles.HexNumber) / max;
        return c;
    }



    private static EllipseCollisionDetection s_ellipseCollisionDetectionTool = new EllipseCollisionDetection(10);
    /// <summary>
    /// 检测平面中两个椭圆是否碰撞
    /// 注意：两个椭圆必须平行于X轴或者Y轴
    /// </summary>
    /// <param name="p1">椭圆1 位置</param>
    /// <param name="s1">椭圆1 横轴半径/纵轴半径</param>
    /// <param name="p2">椭圆2 位置</param>
    /// <param name="s2">椭圆2 横轴半径/纵轴半径</param>
    public static bool IsTwoEllipseCollision(Vector2 p1, Vector2 s1, Vector2 p2, Vector2 s2)
    {
        return s_ellipseCollisionDetectionTool.collide(p1.x, p1.y, s1.x, s1.y, p2.x, p2.y, s2.x, s2.y);
    }

    public static bool IsPointInsideEllipse(Vector2 p1, TRect rect1)
    {
        float a = 0;
        float b = 0;
        float c = 0;
        Vector2 c1;
        Vector2 c2;
        if (rect1.width > rect1.height)
        {
            a = rect1.width * 0.5f;
            b = rect1.height * 0.5f;
            c = Mathf.Sqrt(a * a - b * b);
            c1 = new Vector2(rect1.x - c, rect1.y);
            c2 = new Vector2(rect1.x + c, rect1.y);
        }
        else
        {
            b = rect1.width * 0.5f;
            a = rect1.height * 0.5f;
            c = Mathf.Sqrt(a * a - b * b);
            c1 = new Vector2(rect1.x, rect1.y - c);
            c2 = new Vector2(rect1.x, rect1.y + c);
        }
        float dist = Vector2.Distance(p1, c1) + Vector2.Distance(p1, c2);
        return dist < 2 * a;
    }

    public static bool IsTwoEllipseCollisionSimple(TRect rect1, TRect rect2)
    {
        return IsTwoEllipseCollisionSimple(rect1.center, rect1.size, rect2.center, rect2.size);
    }

    public static bool IsTwoEllipseCollisionSimple(Vector2 p1, Vector2 s1, Vector2 p2, Vector2 s2)
    {
        s1 *= 0.5f;
        s2 *= 0.5f;

        float ratio = s2.x / s2.y;
        s2.y = s2.x;
        p2.y *= ratio;
        s1.y *= ratio;
        p1.y *= ratio;

        s1.x += s2.x;
        s1.y += s2.x;

        float a = 0;
        float b = 0;
        float c = 0;
        float standDist = 0;
        Vector2 c0 = Vector2.zero;
        Vector2 c1 = Vector2.zero;
        if (s1.x > s1.y)
        {
            a = s1.x;
            b = s1.y;
            c = Mathf.Sqrt(a * a - b * b);
            standDist = 2 * a;
            c0 = new Vector2(p1.x - c, p1.y);
            c1 = new Vector2(p1.x + c, p1.y);
        }
        else
        {
            a = s1.y;
            b = s1.x;
            c = Mathf.Sqrt(a * a - b * b);
            standDist = 2 * a;
            c0 = new Vector2(p1.x, p1.y - c);
            c1 = new Vector2(p1.x, p1.y + c);
        }

        float p2ToCCDist = Vector2.Distance(p2, c0) + Vector2.Distance(p2, c1);

        if (p2ToCCDist < standDist)
            return true;
        return false;
    }

    public static bool IsWorldToScreenPosInScreen(Vector3 pos)
    {
        float w = Screen.width;
        float h = Screen.height;
        if (pos.z < 0 || pos.x > w || pos.y > h || pos.x < 0 || pos.y < 0)
            return false;
        return true;
    }

    public static float GetTwoLineOverLapLength(float aMin, float aMax, float bMin, float bMax)
    {
        float len = 0;
        if (aMin > bMax || bMin > aMax)
            return len;
        if (aMin > bMin)
        {
            if (aMax > bMax)
                len = bMax - aMin;
            else
                len = aMax - aMin;
        }
        else
        {
            if (aMax > bMax)
                len = bMax - bMin;
            else
                len = aMax - bMin;
        }
        return len;
    }

    public static void SetButtonSpriteState(RedStone.UI.Button btn, Sprite highlightedSprite = null, Sprite pressedSprite = null, Sprite disabledSprite = null)
    {
        SpriteState spriteState = btn.spriteState;
        if (highlightedSprite != null)
            spriteState.highlightedSprite = highlightedSprite;
        if (pressedSprite != null)
            spriteState.pressedSprite = pressedSprite;
        if (disabledSprite != null)
            spriteState.disabledSprite = disabledSprite;
        btn.spriteState = spriteState;
    }

    //TODO:字符串拼接GC优化
    public static string GetSpriteName(int spriteId, string suffix = "", string prefix = "")
    {
        string name = null;
        TableIconSprite tableIconSprite = TableManager.instance.GetData<TableIconSprite>(spriteId);
        if (tableIconSprite != null)
        {
            name = prefix + System.IO.Path.GetFileName(tableIconSprite.path) + suffix;
        }
        return name;
    }

    public static void SetTextAuto(GameObject gameObject, object data)
    {
        var texts = ListPool<RedStone.UI.Text>.Get();
        gameObject.GetComponentsInChildren<RedStone.UI.Text>(true, texts);
        for (int i = 0; i < texts.Count; ++i)
        {
            var text = texts[i];
            if (!string.IsNullOrEmpty(text.textKey.Trim('#')))
            {
                text.text = LT.Get(text.textKey, data);
            }
            if (text.textColor != UIConfig.textColorNoneID)
            {
                Localization.instance.SetTextColor(text);
            }
        }
        texts.ReleaseToPool();
    }

    public static void ReloadFontStyle(GameObject gameObject)
    {
        if (gameObject == null)
            return;
        foreach (RedStone.UI.Text text in gameObject.GetComponentsInChildren<RedStone.UI.Text>(true))
        {
            LT.SetTextStyle(text);
        }
    }

    public static void SetParent(Transform parent, params Transform[] children)
    {
        RectTransform[] _children = new RectTransform[children.Length];
        for (int i = 0; i < _children.Length; i++)
            _children[i] = children[i].GetComponent<RectTransform>();
        SetParent(parent.GetComponent<RectTransform>(), false, _children);
    }

    public static void SetParent(RectTransform parent, bool keepRotation = false, params RectTransform[] children)
    {
        for (int i = 0; i < children.Length; i++)
        {
            RectTransform child = children[i];
            if (child != null)
            {
                Rect rect = child.rect;
                var rotation = child.localRotation;
                Vector3 vec = child.anchoredPosition3D;
                Vector2 sizeDelta = child.sizeDelta;

                child.SetParent(parent, true);
                child.localScale = Vector3.one;
                child.sizeDelta = sizeDelta;
                child.anchoredPosition3D = vec;
                if (keepRotation)
                    child.localRotation = rotation;
                child.rect.Set(rect.x, rect.y, rect.width, rect.height);
            }
        }
    }

    public static Regex EN_REG = new Regex("[a-zA-Z0-9]");
    public static Regex DE_REG = new Regex("[\u00C0-\u00FF]");
    public static Regex IT_REG = new Regex("[\u00C0-\u017F]");
    public static Regex FR_REG = new Regex("[\u00C0-\u017F]");
    public static Regex ES_REG = new Regex("[\u00C0-\u00FF]");
    public static Regex RU_REG = new Regex("[\u0400-\u052F]");
    public static Regex KO_REG = new Regex("[\u1100-\u11FF\u3130-\u318F\uAC00-\uD7AF]");
    public static Regex JA_REG = new Regex("[\u3040-\u30FF\u31F0-\u31FF]");
    public static Regex ZH_REG = new Regex("[\u4E00-\u9FBF]");
    public static Regex PT_BR_REG = new Regex("[\u00C0-\u00FF]");

    public static int GetLength(string str)
    {
        int len = 0;
        while (str.Length != 0)
        {
            string name_sub = str.Substring(0, 1);
            if (EN_REG.IsMatch(name_sub))
                len++;
            else if (DE_REG.IsMatch(name_sub))
                len++;
            else if (IT_REG.IsMatch(name_sub))
                len++;
            else if (FR_REG.IsMatch(name_sub))
                len++;
            else if (name_sub == " ")
                len++;
            else if (ES_REG.IsMatch(name_sub))
                len++;
            else if (RU_REG.IsMatch(name_sub))
                len++;
            else if (PT_BR_REG.IsMatch(name_sub))
                len++;
            else if (KO_REG.IsMatch(name_sub))
                len += 2;
            else if (JA_REG.IsMatch(name_sub))
                len += 2;
            else if (ZH_REG.IsMatch(name_sub))
                len += 2;
            else
                len += 2;
            str = str.Substring(1);
        }
        return len;
    }

    public const string INPUT_LIMIT = "[^a-zA-Z0-9\u00C0-\u017F\u0400-\u052F\u1100-\u11FF\u3130-\u318F\uAC00-\uD7AF\u3040-\u30FF\u31F0-\u31FF\u4E00-\u9FBF]";
    public static string RemoveIllegalCharacter(string str)
    {
        str = Regex.Replace(str, INPUT_LIMIT, "");
        return str;
    }

    public static string GetTimeAgo(long time)
    {
        TimeSpan ts = TimeSpan.FromMilliseconds(time);

        string strD = "";
        string strH = "";
        string strM = "";

        if (ts.Days != 0)
            strD = ts.Days + LT.GetText("CHAT_TIME_DAY");
        if (ts.Hours != 0)
            strH = ts.Hours + LT.GetText("CHAT_TIME_HOUR");
        if (ts.Minutes != 0)
            strM = ts.Minutes + LT.GetText("CHAT_TIME_MINUTE");

        if (ts.Days != 0)
        {
            strM = "";
        }

        if (ts.Days != 0 || ts.Hours != 0 || ts.Minutes != 0)
        {
            return strD + strH + strM + LT.GetText("CHAT_TIME_NOTICE");
        }
        else
        {
            return "";
        }
    }

    public const uint SEC_TO_MILLISEC = 1000;
    public const uint MINUTES_TO_SECONDS = 60;
    public const uint HOURS_TO_MINUTES = 60;
    public const uint DAYS_TO_HOURS = 24;
    public const uint WEEKS_TO_DAYS = 7;

    public const uint MINUTES_TO_MILLISEC = MINUTES_TO_SECONDS * SEC_TO_MILLISEC;
    public const uint HOURS_TO_MILLISEC = HOURS_TO_MINUTES * MINUTES_TO_MILLISEC;
    public const uint DAYS_TO_MILLISEC = DAYS_TO_HOURS * HOURS_TO_MILLISEC;
    public const uint WEEKS_TO_MILLISEC = WEEKS_TO_DAYS * DAYS_TO_MILLISEC;

    public const uint HOURS_TO_SECONDS = HOURS_TO_MINUTES * MINUTES_TO_SECONDS;
    public const uint DAYS_TO_SECONDS = DAYS_TO_HOURS * HOURS_TO_SECONDS;
    public const uint WEEKS_TO_SECONDS = WEEKS_TO_DAYS * DAYS_TO_SECONDS;

    /** 最个时间单d,h,m,s **/
    public static string GetShowTimeString(long time, uint timeLength = 2)
    {
        string time_string = "";
        uint time_count = 0;
        uint[] time_array = {DAYS_TO_MILLISEC, HOURS_TO_MILLISEC, MINUTES_TO_MILLISEC,
                SEC_TO_MILLISEC};//1000*60*60*24*7, 
        string[] string_array = { "d", "h", "m", "s" };
        if (time > 0)
        {
            for (int i = 0; i < time_array.Length; i++)
            {
                int time_this = Convert.ToInt32(time / time_array[i]);
                if (0 != time_this)
                {
                    if (Convert.ToBoolean(time_count))
                        time_string += " ";
                    time_count++;
                    if (timeLength == 1 && i != time_array.Length - 1)
                        time_this++;
                    time_string += time_this + string_array[i];
                    time -= time_this * time_array[i];
                }
                else if (Convert.ToBoolean(time_count))
                    time_count++;
                if (timeLength == time_count)
                    break;
            }
        }
        if (time_string == "")
            time_string = "1s";
        return time_string;
    }

    public static string GetShowTimeHoursString(long time)
    {
        string time_string = "";
        uint time_count = 0;
        uint[] time_array = {HOURS_TO_MILLISEC, MINUTES_TO_MILLISEC,
                SEC_TO_MILLISEC};//1000*60*60*24*7, 
        string[] string_array = { "h", "m", "s" };
        if (time > 0)
        {
            for (int i = 0; i < time_array.Length; i++)
            {
                int time_this = Convert.ToInt32(time / time_array[i]);

                if (0 != time_this || (time_this == 0 && time_string != null))
                {
                    /*
                    if (time_count != 0)
                        time_string += " ";
                     */
                    time_count++;
                    if (time_this < 10) time_string += "0";
                    time_string += time_this; // + string_array[i];
                    if (i != time_array.Length - 1) time_string += ":";
                    time -= time_this * time_array[i];
                }
            }
        }
        return time_string;
    }

    public static Camera mainCamera
    {
        get { return Camera.main; }
    }

    public static bool IsBlocked(Vector3 pos, int layer)
    {
        Vector3 toPos = pos;
        Vector3 formPos = mainCamera.transform.position;
        if (Physics.Linecast(formPos, toPos, layer))
        {
            return true;
        }
        return false;
    }

    public static TRect Player3DPanelPosBound
    {
        get
        {
            return new TRect();
        }
    }

    public static readonly Vector2 hangPointBoundSize = new Vector2(140, 80); //悬浮UI的基准点的浮动范围
    public static Vector2 hangPoint { get { return new Vector2(-320, -340) + UIConfig.virtualScreenCenter; } } //悬浮UI的基准点，默认的玩家挂点在屏幕的位置
    public static TRect hangRect { get { return new TRect(hangPoint, hangPointBoundSize); } }
    public static Vector2 GetHangLerp(Vector2 curHangUIPoint)
    {
        TRect cachedRect = hangRect;
        Vector2 point = hangRect.MakePosInside(curHangUIPoint);
        float lerpX = (point.x - cachedRect.left) / cachedRect.width;
        float lerpY = (point.y - cachedRect.bottom) / cachedRect.height;
        return new Vector2(lerpX, lerpY);
    }

    public static void CompareAndSetLocalPosition(Transform toSet, Vector3 pos, float difference = 0.5f)
    {
        if (Mathf.Abs(toSet.localPosition.x - pos.x) > difference
            || Mathf.Abs(toSet.localPosition.y - pos.y) > difference
            || Mathf.Abs(toSet.localPosition.y - pos.y) > difference)
        {
            toSet.localPosition = pos;
        }
    }

    public static void CompareAndSetSizeDelta(RectTransform toSet, Vector2 size, float difference = 0.5f)
    {
        if (Mathf.Abs(toSet.sizeDelta.x - size.x) > difference
            || Mathf.Abs(toSet.sizeDelta.y - size.y) > difference)
        {
            toSet.sizeDelta = size;
        }
    }

    public static bool GetPrefsIsShow(string name, bool defaultValue = false)
    {
        return bool.Parse(PlayerPrefs.GetString(name, defaultValue.ToString()));
    }

    public static bool TryGetPrefs(string name, out Vector3 pos)
    {
        if (!PlayerPrefs.HasKey(name))
        {
            pos = Vector3.zero;
            return false;
        }
        string value = PlayerPrefs.GetString(name);
        pos = TableManager.ParseVector3(value);
        return true;
    }

    public static bool TryGetPrefs(string name, out Vector2 pos)
    {
        if (!PlayerPrefs.HasKey(name))
        {
            pos = Vector2.zero;
            return false;
        }
        string value = PlayerPrefs.GetString(name);
        pos = TableManager.ParseVector2(value);
        return true;
    }

    public static bool TryGetPrefs(string name, out string sValue)
    {
        if (!PlayerPrefs.HasKey(name))
        {
            sValue = "";
            return false;
        }
        sValue = PlayerPrefs.GetString(name);
        return true;
    }

    public static void SetPrefs(string name, Vector3 value)
    {
        PlayerPrefs.SetString(name, value.ToString());
    }

    public static void SetPrefs(string name, Vector2 value)
    {
        PlayerPrefs.SetString(name, value.ToString());
    }

    public static void SetPrefs(string name, string value)
    {
        PlayerPrefs.SetString(name, value);
    }

    public static string GetQualityName(int quality)
    {
        return LT.GetText("GUN_QUALITY_{0}".FormatStr(quality));
    }

    private static Color[] s_qualityColor = new Color[] {
                FormatColor("FFFFFF"),/*-none-*/
                FormatColor("C7D2D6"),/*-white-*/
                FormatColor("218A30"),/*-green-*/
                FormatColor("06AFC3"),/*-blue-*/ 
                FormatColor("7C3A98"),/*-purple-*/ 
                FormatColor("DD4B18"),/*-orange-*/ 
            };

    public static Color GetQualityColor(int quality)
    {
        if (quality < s_qualityColor.Length)
            return s_qualityColor[quality];
        return Color.white;
    }

    private static string[] s_bluePrintQualityBorderImage = {
        "",
        "blueprint_border_blue",    //1
        "blueprint_border_gold",    //2
        "blueprint_border_purple",  //3
        "blueprint_border_purple",  //4
        "blueprint_border_purple",  //5
        "blueprint_border_purple",  //6
    };

    public static string GetQualityBorder(int quality)
    {
        if (quality < s_bluePrintQualityBorderImage.Length)
            return s_bluePrintQualityBorderImage[quality];
        return "";
    }

    public static TRect GetRect(Vector2 from, Vector2 to)
    {
        return new TRect((from + to) * 0.5f, new Vector2(Mathf.Abs(from.x - to.x), Mathf.Abs(from.y - to.y)));
    }

    public static int GetNearestColorType(Color color, float threshold = 0.1f)
    {
        int type = UIConfig.textColorNoneID;
        var tables = TableManager.instance.GetAllData<TableTextColor>().Values;
        float minThSq = threshold * threshold * 3;
        foreach (var table in tables)
        {
            if (table.id == UIConfig.textColorNoneID)
                continue;
            Color v = table.value;
            float cur = new Vector3(color.r - v.r, color.g - v.g, color.b - v.b).sqrMagnitude;
            if (cur < minThSq)
            {
                minThSq = cur;
                type = table.id;
            }
        }
        return type;
    }


    public static Vector2 GetUIPosition(Transform transform)
    {
        return UIWorldToUIPos(transform.position);
    }

    public static Vector2 UIWorldToUIPos(Vector3 worldPos)
    {
        GameObject go = GameManager.instance.UIRoot;
        Vector2 uiPos = worldPos / go.transform.localScale.x;
        return uiPos + UIConfig.virtualScreenCenter;
    }
}




