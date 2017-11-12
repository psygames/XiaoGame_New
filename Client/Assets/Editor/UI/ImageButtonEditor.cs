using UnityEditor;
using UnityEngine.UI;

 namespace RedStone.UI
{
    [CustomEditor(typeof(ImageButton), true)]
    [CanEditMultipleObjects]
    public class ImageButtonEditor : ButtonEditor
	{
		SerializedProperty highlightedImage;
		SerializedProperty pressedImage;
		SerializedProperty disabledImage;
		SerializedProperty hideHighlight;
        protected override void OnEnable()
        {
			base.OnEnable();
			highlightedImage = serializedObject.FindProperty("highlightedObject");
			pressedImage = serializedObject.FindProperty("pressedObject");
			disabledImage = serializedObject.FindProperty("disabledObject");
			hideHighlight = serializedObject.FindProperty("hideHightlightWhenDisabled");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
			EditorGUILayout.PropertyField(highlightedImage);
            serializedObject.ApplyModifiedProperties();
			EditorGUILayout.PropertyField(pressedImage);
			serializedObject.ApplyModifiedProperties();
			EditorGUILayout.PropertyField(disabledImage);
			serializedObject.ApplyModifiedProperties();
			EditorGUILayout.PropertyField(hideHighlight);
			serializedObject.ApplyModifiedProperties();
			EditorGUILayout.Space();
			base.OnInspectorGUI();
        }
    }
}
