using UnityEngine;
using UnityEngine.UI;
using UnityEditor.AnimatedValues;
using UnityEditor;

namespace Hotfire.UI
{
    [CustomEditor(typeof(ScrollRect), true)]
    [CanEditMultipleObjects]
    public class ScrollRectEditor : UnityEditor.UI.ScrollRectEditor
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
            base.OnInspectorGUI ();
            EditorGUILayout.Space ();
			EditorGUI.BeginChangeCheck ();
	
			EditorGUILayout.Space ();
			Rect btnResizeRect = EditorGUILayout.GetControlRect ();
			if (GUI.Button (btnResizeRect, "Change Into List View")) 
			{
				GameObject go = (target as Component).gameObject;
				var item = go.GetComponentInChildren<GridLayoutGroup> (true);
				var vItem = go.GetComponentInChildren<VerticalLayoutGroup> (true);
				var hItem = go.GetComponentInChildren<HorizontalLayoutGroup> (true);
				var scrollRect = go.GetComponent<ScrollRect> ();

				var direction = (scrollRect.vertical ? ListLayoutGroup.Axis.Vertical : ListLayoutGroup.Axis.Horizontal);

				if (item != null) {
					var cellSize = item.cellSize;
					var spacing = item.spacing;
					var padding = item.padding;
					var childaliment = item.childAlignment;
					var startCorner = item.startCorner;
					var constraint = (ListLayoutGroup.Constraint)item.constraint;
					var constraintCount = item.constraintCount;
					GameObject child = item.gameObject;
					DestroyImmediate (item);
					var listItem = child.AddComponent<ListLayoutGroup> ();
					listItem.padding = padding;
					listItem.cellSize = cellSize;
					listItem.spacing = spacing;
					listItem.childAlignment = childaliment;
					listItem.constraintCount = constraintCount;
				} else if (vItem != null) {
					var spacing = vItem.spacing;
					var cellSize = Vector2.zero;
					var padding = vItem.padding;
					var childalignment = vItem.childAlignment;
					GameObject child = vItem.gameObject;
					DestroyImmediate (vItem);

					var listItem = child.AddComponent<ListLayoutGroup> ();
					listItem.padding = padding;
					listItem.cellSize = cellSize;
					listItem.spacing = Vector2.one * spacing;
					listItem.childAlignment = childalignment;
					listItem.constraintCount = 1;

				}
				else if (hItem != null) {
					var spacing = hItem.spacing;
					var cellSize = Vector2.zero;
					var padding = hItem.padding;
					var childalignment = hItem.childAlignment;
					GameObject child = hItem.gameObject;
					DestroyImmediate (hItem);

					var listItem = child.AddComponent<ListLayoutGroup> ();
					listItem.padding = padding;
					listItem.cellSize = cellSize;
					listItem.spacing = Vector2.one * spacing;
					listItem.childAlignment = childalignment;
					listItem.constraintCount = 1;

				}
			}

		}
    }
}
