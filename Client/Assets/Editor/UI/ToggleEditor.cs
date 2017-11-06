using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Hotfire.UI
{
    [CustomEditor(typeof(Toggle), true)]
    [CanEditMultipleObjects]
    public class ToggleEditor : UnityEditor.UI.ToggleEditor
    {
        SerializedProperty m_Event;

        protected override void OnEnable()
        {
            base.OnEnable();
            m_Event = serializedObject.FindProperty("m_Event");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(m_Event);
            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.Space();
            base.OnInspectorGUI();
        }
    }
}
