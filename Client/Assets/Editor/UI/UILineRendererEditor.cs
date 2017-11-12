using UnityEngine;
using System.Collections;
using UnityEditor;
using RedStone.UI;

[CustomEditor(typeof(UILineRenderer))]
public class UILineRendererEditor : Editor
{

	public override void OnInspectorGUI ()
	{
		var line = target as UILineRenderer;
		if (line == null)
			return;
		base.OnInspectorGUI ();
		if (GUILayout.Button ("Set Transform Size"))
		{
			line.rectTransform.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, line.sizeDelta.x);
			line.rectTransform.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, line.sizeDelta.y);
		}

	}
}

