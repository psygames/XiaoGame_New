using UnityEditor;
using UnityEngine.UI;
using UnityEngine;
using System;
using Coolfish.System;

namespace Hotfire.UI
{
    [CustomEditor(typeof(Button), true)]
    [CanEditMultipleObjects]
    public class ButtonEditor : UnityEditor.UI.ButtonEditor
	{
		SerializedProperty m_Event;
		SerializedProperty m_showModuleName;
		SerializedProperty m_clickType;
		SerializedProperty m_nativeSize;
		SerializedProperty m_normalSpriteName;
		SerializedProperty m_pressedSpriteName;
		SerializedProperty m_disabledSpriteName;
		SerializedProperty m_normalSpriteGUID;
		SerializedProperty m_pressedSpriteGUID;
		SerializedProperty m_disabledSpriteGUID;


        protected override void OnEnable()
        {
			base.OnEnable();
			m_Event = serializedObject.FindProperty("m_Event");
			m_clickType = serializedObject.FindProperty("m_clickType");
			m_showModuleName = serializedObject.FindProperty("m_showModuleName");
			m_nativeSize = serializedObject.FindProperty("autoSetNativeSize");
			m_normalSpriteName = serializedObject.FindProperty("m_spriteNameNormal");
			m_pressedSpriteName = serializedObject.FindProperty("m_spriteNamePressed");
			m_disabledSpriteName = serializedObject.FindProperty("m_spriteNameDisabled");
			m_normalSpriteGUID = serializedObject.FindProperty("m_spriteGUIDNormal");
			m_pressedSpriteGUID = serializedObject.FindProperty("m_spriteGUIDPressed");
			m_disabledSpriteGUID = serializedObject.FindProperty("m_spriteGUIDDisabled");
		}
        public override void OnInspectorGUI()
        {
			var btn = (target as Button);

			serializedObject.Update();
			EditorGUILayout.PropertyField(m_clickType);
			serializedObject.ApplyModifiedProperties();
			var clickType = (EClickType)m_clickType.enumValueIndex;
			if (clickType == EClickType.HandleByParent || clickType == EClickType.SendEvent)
			{
				EditorGUILayout.PropertyField (m_Event);
				serializedObject.ApplyModifiedProperties ();
			}
			bool changed = false;
			changed = (IsSpriteChanged(btn.spriteState.highlightedSprite, m_normalSpriteName) || IsSpriteChanged(btn.spriteState.pressedSprite, m_pressedSpriteName) || IsSpriteChanged(btn.spriteState.disabledSprite, m_disabledSpriteName)); 
			if (!Application.isPlaying && btn.image != null && (btn.spriteState.highlightedSprite != btn.image.sprite || changed))
			{
				if(m_nativeSize.boolValue)
					btn.image.SetNativeSize ();
			}
			EditorGUILayout.Space();
			EditorGUILayout.PropertyField(m_nativeSize);
			serializedObject.ApplyModifiedProperties();
			EditorGUILayout.Space();

			if (GUILayout.Button ("Clear Sprite"))
			{
			}
			string spriteName = FindTransisionSprites (btn);
			if (string.IsNullOrEmpty(spriteName))
			{
				EditorGUILayout.LabelField ("No Transision Sprites");
			}
			else if (GUILayout.Button ("Find Transision Sprite"))
			{;
				btn.transition = Selectable.Transition.SpriteSwap;
			}
			base.OnInspectorGUI();
			EditorGUILayout.BeginVertical("box");
			EditorGUILayout.LabelField ("Sprite Names:");
			EditorGUILayout.LabelField ("normal", "{0}[{1}]".FormatStr(m_normalSpriteName.stringValue, m_normalSpriteGUID.stringValue));
			EditorGUILayout.LabelField ("pressed", "{0}[{1}]".FormatStr(m_pressedSpriteName.stringValue, m_pressedSpriteGUID.stringValue));
			EditorGUILayout.LabelField ("disabled", "{0}[{1}]".FormatStr(m_disabledSpriteName.stringValue, m_disabledSpriteGUID.stringValue));
			EditorGUILayout.EndVertical();
			if (GUILayout.Button ("Add Sound"))
			{
			}
        }
		bool IsSpriteChanged(Sprite sprite, SerializedProperty name)
		{
			return (sprite == null && !string.IsNullOrEmpty(name.stringValue)) || (sprite != null && sprite.name != name.stringValue);
		}
		static string SubStr(string name, string suffix)
		{
			if (name.EndsWith (suffix))
			{
				return name.Substring (0, name.LastIndexOf (suffix));
			}
			return null;
		}
		public static string FindTransisionSprites(Button btn)
		{
			string spriteName = null;
			if (btn.spriteState.highlightedSprite != null)
			{
				spriteName = SubStr (btn.spriteState.highlightedSprite.name, "_normal");
				if(string.IsNullOrEmpty (spriteName))
				{
					spriteName = SubStr (btn.spriteState.highlightedSprite.name, "_pressed");
				}
				if (string.IsNullOrEmpty (spriteName))
				{
					spriteName = SubStr (btn.spriteState.highlightedSprite.name, "_disabled");
				}
			}
			return spriteName;
		}
    }
}
