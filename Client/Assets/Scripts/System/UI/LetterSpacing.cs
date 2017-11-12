using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
 
 namespace RedStone.UI
{
	[AddComponentMenu("UI/Effects/Single Line Letter Spacing")]
	[DisallowMultipleComponent]
	[RequireComponent(typeof(Text))]
	public class LetterSpacing : BaseMeshEffect
	{
		[SerializeField]
		private float m_spacing = 6f;


		protected LetterSpacing() { }

		#if UNITY_EDITOR
		protected override void OnValidate()
		{
			spacing = m_spacing;
			base.OnValidate();
		}
		#endif

		public float spacing
		{
			get { return m_spacing; }
			set
			{
				if (m_spacing == value) return;
				m_spacing = value;
				if (graphic != null) graphic.SetVerticesDirty();
			}
		}


		public override void ModifyMesh(UnityEngine.UI.VertexHelper helper)
		{
			if (!IsActive()) return;
			List<UIVertex> verts = ListPool<UIVertex>.Get ();
			helper.GetUIVertexStream (verts);
			Text text = GetComponent<Text>();
			if (text == null)
			{
				Debug.LogWarning("LetterSpacing: Missing Text component");
				return;
			}
			Vector3 pos;
			float letterOffset = spacing * (float)text.fontSize / 100f;
			float alignmentFactor = 0;
			int glyphIdx = 0;

			switch (text.alignment)
			{
			case TextAnchor.LowerLeft:
			case TextAnchor.MiddleLeft:
			case TextAnchor.UpperLeft:
				alignmentFactor = 0f;
				break;

			case TextAnchor.LowerCenter:
			case TextAnchor.MiddleCenter:
			case TextAnchor.UpperCenter:
				alignmentFactor = 0.5f;
				break;

			case TextAnchor.LowerRight:
			case TextAnchor.MiddleRight:
			case TextAnchor.UpperRight:
				alignmentFactor = 1f;
				break;
			}

			//for (int lineIdx=0; lineIdx < lines.Length; lineIdx++)
			{
				string line = text.text;
				float lineOffset = (line.Length -1) * letterOffset * alignmentFactor;
				for (int charIdx = 0; charIdx < line.Length; charIdx++)
				{
					int idx1 = glyphIdx * 6 + 0;
					int idx2 = glyphIdx * 6 + 1;
					int idx3 = glyphIdx * 6 + 2;
					int idx4 = glyphIdx * 6 + 3;
					int idx5 = glyphIdx * 6 + 4;
					int idx6 = glyphIdx * 6 + 5;


					// Check for truncated text (doesn't generate verts for all characters)
					if (idx4 > verts.Count - 1) return;

					UIVertex vert1 = verts[idx1];
					UIVertex vert2 = verts[idx2];
					UIVertex vert3 = verts[idx3];
					UIVertex vert4 = verts[idx4];
					UIVertex vert5 = verts[idx5];
					UIVertex vert6 = verts[idx6];


					pos = Vector3.right * (letterOffset * charIdx - lineOffset);

					vert1.position += pos;
					vert2.position += pos;
					vert3.position += pos;
					vert4.position += pos;
					vert5.position += pos;
					vert6.position += pos;


					verts[idx1] = vert1;
					verts[idx2] = vert2;
					verts[idx3] = vert3;
					verts[idx4] = vert4;
					verts[idx5] = vert5;
					verts[idx6] = vert6;


					glyphIdx++;
				}

				// Offset for carriage return character that still generates verts
				glyphIdx++;
			}

			helper.Clear ();
			helper.AddUIVertexTriangleStream(verts);
			verts.ReleaseToPool ();
		}
	}
}
