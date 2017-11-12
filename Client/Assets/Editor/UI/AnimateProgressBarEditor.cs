using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

 namespace RedStone.UI
{
	[CustomEditor(typeof(AnimateProgressBar), true)]
	[CanEditMultipleObjects]
	public class AnimateProgressBarEditor : SliderEditor
	{
		SerializedProperty m_animLength;

		protected override void OnEnable()
		{
			base.OnEnable();
			m_animLength = serializedObject.FindProperty("animLength");
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			EditorGUILayout.PropertyField(m_animLength);
			serializedObject.ApplyModifiedProperties();
			EditorGUILayout.Space();
			base.OnInspectorGUI();
		}
	}
}
