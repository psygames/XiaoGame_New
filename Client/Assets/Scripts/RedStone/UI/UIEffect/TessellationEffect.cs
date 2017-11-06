using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Coolfish.System;
using System;

namespace Hotfire.UI
{
	[AddComponentMenu("UI/Effects/Curve")]
	[DisallowMultipleComponent]
	public class TessellationEffect : BaseMeshEffect
	{
		[SerializeField]
		private int m_Fineness = 1;


		protected TessellationEffect() { }


		#if UNITY_EDITOR
		protected override void OnValidate()
		{
			Fineness = m_Fineness;
			base.OnValidate();
		}
		#endif

		public int Fineness
		{
			get { return m_Fineness; }
			set
			{
				if (m_Fineness == value) return;
				m_Fineness = value;
				if (graphic != null) graphic.SetVerticesDirty();
			}
		}
		public Vector2 Min(Vector2 v, Vector2 v1)
		{
			return new Vector2(Mathf.Min(v.x, v1.x), Mathf.Min(v.y, v1.y));
		}

		public Vector2 Max(Vector2 v, Vector2 v1)
		{
			return new Vector2(Mathf.Max(v.x, v1.x), Mathf.Max(v.y, v1.y));
		}
		public Vector3 Min(Vector3 v, Vector3 v1)
		{
			return new Vector3(Mathf.Min(v.x, v1.x), Mathf.Min(v.y, v1.y), v.z);
		}

		public Vector3 Max(Vector3 v, Vector3 v1)
		{
			return new Vector3(Mathf.Max(v.x, v1.x), Mathf.Max(v.y, v1.y), v.z);
		}

		public override void ModifyMesh(UnityEngine.UI.VertexHelper helper)
		{
			var verts = ListPool<UIVertex>.Get ();
			var newVerts = ListPool<UIVertex>.Get ();
			helper.GetUIVertexStream (verts);
			helper.Clear ();
			int count = verts.Count;
			int shapeCount = count / 6;
			int shapeIndex = 0;
			for (int i = 0; i < shapeCount; ++i) 
			{
				int curIndex = i * 6;
				bool isUVVertical = (GetComponent<Graphic>() is Text) && (verts [curIndex].uv0.x == verts [curIndex + 1].uv0.x);
				Vector2 minUV = verts [curIndex + 0].uv0;
				Vector2 maxUV = verts [curIndex + 2].uv0;
				Vector3 minPos = verts [curIndex + 0].position;
				Vector3 maxPos = verts [curIndex + 2].position;

				Vector3 lengthPos = (maxPos - minPos) / m_Fineness;
				Vector2 lengthUV = (maxUV - minUV) / m_Fineness;
				for(int j = 0; j < m_Fineness ; ++j)
				{
					for (int k = 0; k < 6; ++k) 
					{
						var vert = verts [curIndex + k];
						float x = 0;
						float y = 0;
						float uvx = 0;
						float uvy = 0;
						bool isX = (k == 0 || k == 4 || k == 5);
						bool isY = (k == 0 || k == 1 || k == 5);
						if ((isUVVertical && isX) || (!isUVVertical && isY)) {
							uvy = isUVVertical ? minUV.y + lengthUV.y * j : minUV.y;
							y = minPos.y;
						} else {
							uvy = isUVVertical ? minUV.y + lengthUV.y * (j + 1) : maxUV.y;
							y = maxPos.y;
						}
						y = (isY ? minPos.y : maxPos.y);
						if ((isUVVertical && isY) || (!isUVVertical && isX)) {
							uvx = isUVVertical ? minUV.x : minUV.x + lengthUV.x * j;
						} else {
							uvx = isUVVertical ? maxUV.x : minUV.x + lengthUV.x * (j + 1);
						}
						x = (isX ? minPos.x + lengthPos.x * j : minPos.x + lengthPos.x * (j + 1));
						vert.position = new Vector3 (x, y, minPos.z);
						vert.uv0 = new Vector2 (uvx, uvy);
						newVerts.Add (vert);
					}
				}
				shapeIndex += m_Fineness;
			}
			helper.Clear ();
			helper.AddUIVertexTriangleStream(newVerts);
			newVerts.ReleaseToPool ();
			verts.ReleaseToPool ();
		}
	}
}
