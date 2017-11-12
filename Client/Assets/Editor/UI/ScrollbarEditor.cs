using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

 namespace RedStone.UI
{
    [CustomEditor(typeof(Scrollbar), true)]
    [CanEditMultipleObjects]
    public class ScrollbarEditor : UnityEditor.UI.ScrollbarEditor
    {
    }
}
