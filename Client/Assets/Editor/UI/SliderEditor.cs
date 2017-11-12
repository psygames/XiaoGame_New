using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

 namespace RedStone.UI
{
	[CustomEditor(typeof(Slider), true)]
    [CanEditMultipleObjects]
	public class SliderEditor : UnityEditor.UI.SliderEditor
    {
    }
}
