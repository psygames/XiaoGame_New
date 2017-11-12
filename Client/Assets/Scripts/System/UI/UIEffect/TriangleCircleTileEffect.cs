using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 
using RedStone;
using System;

public class TriangleCircleTileEffect : BaseMeshEffect
{
	public int count = 1;
	[Range(1, 180)]
	public float angle = 30f;
	[Range(0, 90)]
	public float startAngle = 0f;
	[Range(0, 360)]
	public float rotation = 0f;
	public float uvScale = 0f;
	public float uvCurve = 1f;
	public EPivot pivot = EPivot.Middle;
	[Range(0,1)]
	public float fill = 1f;
	public enum EPivot
	{
		RightBottom,
		RightMiddle,
		Middle,
		//UpMiddle,

	};
	Vector3 Rotate(Vector3 v, Vector3 center, float angle, float yDelta)
	{
		Vector3 vertex = v; 

		var cos = Mathf.Cos (Mathf.Deg2Rad * angle);
		var sin = Mathf.Sin (Mathf.Deg2Rad * angle);
		vertex.x = center.x + (cos * (v.x - center.x) + sin * (v.y - center.y));
		vertex.y = center.y + yDelta * (-sin * (v.x - center.x) + cos * (v.y - center.y));
		return vertex;
	}
	Vector2 Rotate(Vector2 v, Vector2 center, float angle, float yDelta)
	{
		Vector2 vertex = v; 
		var cos = Mathf.Cos (Mathf.Deg2Rad * angle);
		var sin = Mathf.Sin (Mathf.Deg2Rad * angle);
		vertex.x = center.x + (cos * (v.x - center.x) + sin * (v.y - center.y));
		vertex.y = center.y + yDelta * (-sin * (v.x - center.x) + cos * (v.y - center.y));
		return vertex;
	}
	public override void ModifyMesh (VertexHelper vh)
	{
		var verts = ListPool<UIVertex>.Get ();
		var newVerts = RedStone.ListPool<UIVertex>.Get ();
		vh.GetUIVertexStream (verts);
		if (verts.Count >= 6)
		{
			vh.Clear ();
			var pivotVert = verts [4];
			var startVert = verts [0];
			var delta = verts [2].position - verts [0].position;
			var yDelta = Mathf.Abs (delta.y / delta.x);
			var uvDelta = verts [2].uv0 - verts [0].uv0;
			var uvYDelta = Mathf.Abs (uvDelta.y / uvDelta.x) * uvCurve;
			pivotVert.position = (verts[0].position + verts[2].position) * 0.5f;
			startVert.position = (verts [0].position + verts [1].position) * 0.5f;
			var uvStartAngle = startAngle;
			var uvAngle = angle;

			var vert0 = startVert;
			var vert1 = startVert;
			if (pivot == EPivot.Middle)
			{
				pivotVert.uv0 = (verts [0].uv0 + verts [2].uv0) * 0.5f;
			} else if (pivot == EPivot.RightMiddle)
			{
				pivotVert.uv0 = (verts [3].uv0 + verts [4].uv0) * 0.5f;
			} /*else if (pivot == EPivot.UpMiddle)
			{
				pivotVert.uv0 = (verts [1].uv0 + verts [2].uv0) * 0.5f;
			}*/
			if (pivot == EPivot.RightMiddle || pivot == EPivot.Middle)
			{
				startVert.uv0 = (verts [0].uv0 + verts [1].uv0) * 0.5f;



			}/* else if (pivot == EPivot.UpMiddle)
			{
				startVert.uv0 = (verts [0].uv0 + verts [4].uv0) * 0.5f;
			}*/
			startVert.uv0 = pivotVert.uv0 + (startVert.uv0 - pivotVert.uv0) * uvScale;
			if (pivot == EPivot.RightBottom)
			{
				if (uvAngle > 90)
					uvAngle = 90;
				if ((uvStartAngle + uvAngle) > 90)
				{
					uvStartAngle = 90 - uvAngle;
				}

			} 
			else
			{
				uvStartAngle =-uvAngle * 0.5f;
			}
			vert0.uv0 = Rotate (startVert.uv0, pivotVert.uv0, uvStartAngle, uvYDelta);
			vert1.uv0 = Rotate (startVert.uv0, pivotVert.uv0, uvStartAngle + uvAngle, uvYDelta);
			var tileCount = (int)Mathf.Min (count, 360 / uvAngle);
			var totalAngle = (tileCount * uvAngle * fill);
			var tileFull = (int)(totalAngle / uvAngle);
			var restAngle = totalAngle - tileFull * uvAngle;
			for (int i = 0; i < tileFull; ++i)
			{
				vert0.position = Rotate (startVert.position, pivotVert.position, rotation + uvAngle * i, yDelta); 
				vert1.position = Rotate (startVert.position, pivotVert.position, rotation + uvAngle * (i + 1), yDelta);
				newVerts.Add (vert0);
				newVerts.Add (vert1); 
				newVerts.Add (pivotVert);
			}
			if (restAngle > 0 && !Mathf.Approximately(restAngle, 0))
			{
				vert0.position = Rotate (startVert.position, pivotVert.position, rotation + uvAngle * tileFull, yDelta); 
				vert1.position = Rotate (startVert.position, pivotVert.position, rotation + totalAngle, yDelta);
				vert1.uv0 = Rotate (startVert.uv0, pivotVert.uv0, uvStartAngle + restAngle, uvYDelta);
				newVerts.Add (vert0);
				newVerts.Add (vert1); 
				newVerts.Add (pivotVert);
			}
			vh.AddUIVertexTriangleStream (newVerts);
		}
		verts.ReleaseToPool ();
		newVerts.ReleaseToPool ();
	}
}

