using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 

 namespace RedStone.UI
{
	[RequireComponent(typeof(Graphic))]
	[AddComponentMenu ("UI/Effects/Italic")]
	public class Italic : BaseMeshEffect
	{

		[SerializeField]
		protected float m_Offset = 0.15f;

		public float offset
		{
			get{
				return m_Offset;
			}
			set {
				m_Offset = value;
				if (graphic != null)
					graphic.SetVerticesDirty ();
			}
		}
		#if UNITY_EDITOR
		protected override void OnValidate()
		{
			offset = m_Offset;
			base.OnValidate();
		}
		#endif
		Text text;
		protected override void Awake ()
		{
			base.Awake ();
			text = GetComponent<Text> ();
		}
		UIVertex Move(UIVertex vert, int size, float maxY, float minY)
		{
			var delta = 1f / (maxY - minY);
			var y = vert.position.y;
			vert.position = vert.position + Vector3.right * size * offset * (y - minY) * delta;
			return vert;
		}
		public override void ModifyMesh (VertexHelper vh)
		{
			if (!enabled)
				return;
			var size = (text == null ? 1 : text.fontSize);
			if (text != null && text.resizeTextForBestFit && text.cachedTextGenerator != null && text.canvas != null)
			{
				size = (int)(text.cachedTextGenerator.fontSizeUsedForBestFit / text.canvas.scaleFactor);
			}

			var verts = ListPool<UIVertex>.Get ();
			vh.GetUIVertexStream (verts);
			if (verts.Count > 0)
			{
				var maxY = verts [0].position.y;
				var minY = maxY;
				var vertCount = verts.Count;
				for (int i = 0; i < vertCount; ++i)
				{
					var y = verts [i].position.y;
					maxY = Mathf.Max (maxY, y);
					minY = Mathf.Min (minY, y);
				}
				var delta = maxY - minY;
				if (delta > 0)
				{
					for (int i = 0; i < vertCount; ++i)
					{
						verts [i] = Move (verts [i], size, maxY, minY);
					}
				}

				vh.Clear ();
				vh.AddUIVertexTriangleStream (verts);
			}
			verts.ReleaseToPool ();
		}
	}

}