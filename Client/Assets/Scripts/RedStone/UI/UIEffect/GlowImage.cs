using UnityEngine;
using System.Collections;
using System;

namespace Hotfire.UI
{
	public class GlowImage : RawImage
	{
		[NonSerialized]
		public Camera glowCamera;

		[NonSerialized]
		private Text m_text = null;

		[NonSerialized]
		private TextGlow m_textGlow = null;

		private bool m_spriteChanged = false;

		[NonSerialized]
		public Vector3 screenLeftTop = Vector3.zero;
		[NonSerialized]
		public Vector3 screenRightBottom = Vector3.zero;
		[NonSerialized]
		public Vector3 worldLeftTop = Vector3.zero;
		[NonSerialized]
		public Vector3 worldRightBottom = Vector3.zero;
		public void Init(Text text)
		{
			m_text = text;
			if(text != null)
				m_textGlow = text.GetComponent<TextGlow> ();
			raycastTarget = false;
		}

		public void SetGlowTexture(Texture tex)
		{
			m_spriteChanged = (tex != texture);
			texture = tex;
		}

		void LateUpdate()
		{
			if (m_spriteChanged)
			{
				m_spriteChanged = false;
				OnGlowChanged ();
			}
		}

		Vector3[] worldCorners = new Vector3[4];
		public void OnGlowChanged()
		{

			if (m_text == null || m_textGlow == null || glowCamera == null)
				return;
			gameObject.SetActive (true);
			m_text.rectTransform.GetWorldCorners (worldCorners);
			worldCorners [0] -= new Vector3(m_textGlow.glowUVExpand.x, m_textGlow.glowUVExpand.y, 0f);
			worldCorners [2] +=  new Vector3(m_textGlow.glowUVExpand.x, m_textGlow.glowUVExpand.y, 0f);
			rectTransform.pivot = Vector2.one * 0.5f;
			screenLeftTop = glowCamera.WorldToScreenPoint(worldCorners [0]);
			screenRightBottom = glowCamera.WorldToScreenPoint(worldCorners[2]);
			var leftTop = m_text.rectTransform.InverseTransformPoint (worldCorners [0]);
			var rightBottom = m_text.rectTransform.InverseTransformPoint (worldCorners [2]);
			//m_text.rectTransform.GetLocalCorners (localCorners);
			var localDelta = (rightBottom - leftTop);
			var textPosition = (worldCorners [0] + worldCorners [2]) * 0.5f;

			rectTransform.position = textPosition;
			rectTransform.rotation = Quaternion.identity;
			rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Abs(localDelta.x));
			rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Abs(localDelta.y));

			var delta = screenRightBottom - screenLeftTop;
			this.uvRect = new Rect(new Vector2((screenLeftTop.x + m_textGlow.glowUVOffset.x) / Screen.width, (screenLeftTop.y + m_textGlow.glowUVOffset.y)/ Screen.height), new Vector2(delta.x / Screen.width, delta.y / Screen.height));
		}
		#if UNITY_EDITOR
		void Update()
		{
			if (m_textGlow != null && m_textGlow.debugInfomation)
			{
			}
		}
		void OnGUI()
		{if (m_textGlow != null && m_textGlow.debugInfomation)
			{
				GUILayout.Space (50f);
				GUILayout.BeginVertical ("box");
				GUILayout.Label (worldCorners [0].ToString ());
				GUILayout.Label (worldCorners [2].ToString ());
				GUILayout.Label (screenLeftTop.ToString ());
				GUILayout.Label (screenRightBottom.ToString ());
				GUILayout.EndVertical ();
			}
		}
		#endif
	}

}