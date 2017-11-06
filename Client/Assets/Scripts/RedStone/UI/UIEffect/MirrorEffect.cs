using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using Coolfish.System;

[AddComponentMenu ("UI/Effects/Mirror")]
public class MirrorEffect : BaseMeshEffect
{
	public enum MirrorType
	{
		Top,
		Bottom,
		Left,
		Right,
	}
	[Range(0f,1f)]
	public float mirrorScale = 0.5f;
	public MirrorType mirrorType = MirrorType.Right;
	public override void ModifyMesh (VertexHelper vh)
	{
		var verts = ListPool<UIVertex>.Get ();
		var _scale =  Mathf.Clamp (mirrorScale, 0f, 1f);
		vh.GetUIVertexStream (verts);
		var count = verts.Count;
		if (count > 0)
		{
			var min = verts [0].position;
			var max = verts [0].position;

			for (int i = 0; i < count; ++i)
			{
				var vert = verts [i];
				min.x = Mathf.Min (vert.position.x, min.x);
				max.x = Mathf.Max (vert.position.x, max.x);
				min.y = Mathf.Min (vert.position.y, min.y);
				max.y = Mathf.Max (vert.position.y, max.y);
				verts.Add (vert);
			}
			var newMax = max;
			var newMaxMirror = max;
			var newMin = min;
			var newMinMirror = min;
			if (mirrorType == MirrorType.Top)
			{
				newMax.y = (max.y - min.y) * _scale + min.y;
				newMinMirror.y = (max.y - min.y) * (_scale) + min.y;

			} else if (mirrorType == MirrorType.Bottom)
			{
				newMin.y = (max.y - min.y) * _scale + min.y;
				newMaxMirror.y = (max.y - min.y) * _scale + min.y;
			}
			if (mirrorType == MirrorType.Right)
			{
				newMax.x = (max.x - min.x) * _scale + min.x;
				newMinMirror.x = (max.x - min.x) * _scale + min.x;
			} else if (mirrorType == MirrorType.Left)
			{
				newMin.x = (max.x - min.x) * _scale + min.x;
				newMaxMirror.x = (max.x - min.x) * _scale + min.x;
			}
			for (int i = 0; i < count; ++i)
			{
				var vert = verts [i];
				var pos = vert.position;
				var mirrorVert = verts [i + count];
				var mirrorPos = mirrorVert.position;
				if (mirrorType == MirrorType.Right || mirrorType == MirrorType.Left)
				{
					pos.x = (pos.x - min.x) / Mathf.Abs(max.x - min.x) * Mathf.Abs(newMax.x - newMin.x) + newMin.x;
					mirrorPos.x = (max.x - mirrorPos.x) / Mathf.Abs (max.x - min.x) * Mathf.Abs (newMaxMirror.x - newMinMirror.x) + newMinMirror.x;
				}
				else if (mirrorType == MirrorType.Top || mirrorType == MirrorType.Bottom)
				{
					pos.y = (pos.y - min.y) / Mathf.Abs(max.y - min.y) * Mathf.Abs(newMax.y - newMin.y) + newMin.y;
					mirrorPos.y = (max.y - mirrorPos.y) / Mathf.Abs (max.y - min.y) * Mathf.Abs (newMaxMirror.y - newMinMirror.y) + newMinMirror.y;
				}
				mirrorVert.position = mirrorPos;
				verts [i + count] = mirrorVert;
					vert.position = pos;
					verts [i] = vert;
			}
			vh.Clear ();
			vh.AddUIVertexTriangleStream (verts);
		}
		verts.ReleaseToPool ();
	}
}

