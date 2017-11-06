using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

namespace Hotfire.UI
{
    [AddComponentMenu("Project UI/Mask", 13)]
    [ExecuteInEditMode]
    [RequireComponent(typeof(RectTransform))]
    [DisallowMultipleComponent]
    public class Mask : UnityEngine.UI.Mask
    {
    }
}
