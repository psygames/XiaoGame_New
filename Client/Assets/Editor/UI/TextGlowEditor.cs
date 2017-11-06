using UnityEngine;
using System.Collections;
using Hotfire.UI;
using UnityEditor;

[CustomEditor (typeof(TextGlow))]
public class TextGlowEditor : UnityEditor.Editor
{
	
	float m_textureHeight = 100f;

	public override void OnInspectorGUI ()
	{
		var glow = target as TextGlow;
		glow.debugInfomation = EditorGUILayout.Toggle ("Show Debug Information", glow.debugInfomation);
		if (glow == null)
			return;
		if (glow.glowImage != null && glow.glowImage.texture != null)
		{
			RawImage rawImage = glow.glowImage;
			m_textureHeight = EditorGUILayout.FloatField ("Preview Height", m_textureHeight);
			EditorGUILayout.Space ();
			var rect = EditorGUILayout.GetControlRect (GUILayout.Width (rawImage.rectTransform.sizeDelta.x / rawImage.rectTransform.sizeDelta.y * m_textureHeight), GUILayout.Height (m_textureHeight));
			Texture tex = rawImage.texture;

			if (tex == null)
				return;

			GUI.DrawTextureWithTexCoords (rect, rawImage.texture, rawImage.uvRect);
		}
		base.OnInspectorGUI ();


		if (!glow.addGlowImage && glow.glowImage != null)
		{
			var go = glow.glowImage.gameObject;
			glow.glowImage = null;
			GameObject.DestroyImmediate (go);
		}


		else
		if (glow.addGlowImage && glow.glowImage == null)
		{
			glow.InitGlowImage ();
		}

		EditorGUILayout.LabelField ("Forward : " + glow.transform.forward);
		EditorGUILayout.LabelField ("Up : " + glow.transform.up);
		EditorGUILayout.LabelField ("Right : " + glow.transform.right);

	}
}