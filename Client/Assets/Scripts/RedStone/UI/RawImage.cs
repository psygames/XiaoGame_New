using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Hotfire.UI
{
    /// <summary>
    /// If you don't have or don't wish to create an atlas, you can simply use this script to draw a texture.
    /// Keep in mind though that this will create an extra draw call with each RawImage present, so it's
    /// best to use it only for backgrounds or temporary visible graphics.
    /// </summary>
	[AddComponentMenu("Project UI/Raw Image", 12)]
	[RequireComponent(typeof(RectTransform))]
    public class RawImage : UnityEngine.UI.RawImage
    {
    }
}
