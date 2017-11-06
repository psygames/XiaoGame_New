using UnityEngine;
using UnityEditor;
using System.Collections;
using Hotfire.UI;

[CustomEditor(typeof(Hotfire.UI.SpriteFrameAnimator), true)]
[CanEditMultipleObjects]
public class SpriteFrameAnimatorEditor : UnityEditor.Editor
{

	System.Collections.Generic.List<string> str = new System.Collections.Generic.List<string>();
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI ();		
		if (str.Count == 0)
		{
			str.Add ("icon_diamond");
			str.Add ("icon_fund");
			str.Add ("icon_add");
		}
		Rect btnResizeRect = EditorGUILayout.GetControlRect();
		if (GUI.Button (btnResizeRect, "Test Animator")) {
			(target as SpriteFrameAnimator).Init (str.ToArray());
		}

	}
}

