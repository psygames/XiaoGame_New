using UnityEditor;
using UnityEngine;
namespace Hotfire.UI
{
    /// <summary>
    /// Editor class used to edit UI Sprites.
    /// </summary>

    [CustomEditor(typeof(Image), true)]
    [CanEditMultipleObjects]
    public class ImageEditor : UnityEditor.UI.ImageEditor
    {


		protected override void OnEnable()
		{
			base.OnEnable();
		}
		protected override void OnDisable ()
		{
			base.OnDisable ();
		}

		public override void OnInspectorGUI()
		{  
			var img = target as Image;

			if (img == null)
				return;
			serializedObject.Update();
			serializedObject.ApplyModifiedProperties();
			EditorGUILayout.Space();
			img.enableGrey = EditorGUILayout.Toggle ("Enable Grey", img.enableGrey);

			img.Grey = EditorGUILayout.Toggle ("Grey", img.Grey);
			base.OnInspectorGUI();
			if (img != null)
			{
				img.dontUsePool = EditorGUILayout.Toggle ("Disable Pool", img.dontUsePool);
			}
		}

    }
}
