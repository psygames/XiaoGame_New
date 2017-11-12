using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;
using System.Linq;
using System.Collections.Generic;

[CustomEditor(typeof(RectTransform))]
public class RectTransformEditor : DecoratorEditor
{
	private Type[] types = null;
	private string[]typeStr = null;
	int m_selectType = 0;
	public RectTransformEditor() : base("UnityEditor.RectTransformEditor")
	{
	}
	void OnEnable()
	{
		var transform = target as RectTransform;
		if (transform == null || serializedObject == null)
			return;
		if(types == null)
		{
			var assUnity = Assembly.GetAssembly (typeof(RedStone.UI.TweenEffectAttribute));
			List<Type> typeList = new List<Type>();
			List<string> typeStrList = new List<string>();
			foreach (var type in assUnity.GetTypes())
			{
				if (type.GetCustomAttributes (typeof(RedStone.UI.TweenEffectAttribute), false).FirstOrDefault () != null)
				{
					typeList.Add (type);
					typeStrList.Add (type.Name);
				}
			}
			types = typeList.ToArray ();
			typeStr = typeStrList.ToArray ();
		}
	}

	public override void OnInspectorGUI()
	{
		var transform = target as RectTransform;
		if (transform == null || serializedObject == null)
			return;

		base.OnInspectorGUI ();
		if (GUILayout.Button ("Reset Position/Scale/Rotation"))
		{
			transform.anchoredPosition3D = Vector3.zero;
			transform.rotation = Quaternion.identity;
			transform.localScale = Vector3.one;
		}
		EditorGUILayout.BeginVertical ("box");
		EditorGUILayout.EndVertical ();
		if (types != null && typeStr != null && types.Length > 0 && types.Length == typeStr.Length)
		{
			EditorGUILayout.BeginHorizontal ();
			m_selectType = EditorGUILayout.Popup (m_selectType, typeStr);
			if(0 <= m_selectType && m_selectType < types.Length)
			{
				if (transform.GetComponent(types[m_selectType]) == null && GUILayout.Button ("Add"))
				{
					transform.gameObject.AddComponent (types [m_selectType]);
				}
			}
			EditorGUILayout.EndHorizontal ();
		}
		// ...
	}
}