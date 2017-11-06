using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Hotfire.UI
{
    [CustomEditor(typeof(Scrollbar), true)]
    [CanEditMultipleObjects]
    public class ScrollbarEditor : UnityEditor.UI.ScrollbarEditor
    {
    }
}
