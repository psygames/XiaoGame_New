using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Hotfire.UI
{
    [AddComponentMenu("Project UI/2D Rect Mask", 13)]
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RectTransform))]
    public class RectMask2D : UnityEngine.UI.RectMask2D
    {
    }
}
