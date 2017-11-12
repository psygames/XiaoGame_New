using System;
using UnityEngine;
using UnityEngine.UI;
using RedStone;
 
using System.Collections.Generic;

[RequireComponent(typeof(Image))]
public class CircleTileEffect : BaseMeshEffect
{     
	public Vector3 pivot = Vector3.zero;

	[Range(0,1)]
	public float scaleX = 1f;
	[Range(0,1)]
	public float scaleY = 1f;
	[Range(0, 180)]
	public float angle = 30f;

	[Range(0, 30)]
	public int count = 1;

	[Range(0, 360)]
	public float startAngle = 0f;

	public bool perspective = false;
	private Image m_image;
	Vector3 Rotate(Vector3 v, Vector3 center, float angle, float yDelta)
	{
		Vector3 vertex = v; 

		var cos = Mathf.Cos (Mathf.Deg2Rad * angle);
		var sin = Mathf.Sin (Mathf.Deg2Rad * angle);
		vertex.x = center.x + (cos * (v.x - center.x) + sin * (v.y - center.y));
		vertex.y = center.y + yDelta * (-sin * (v.x - center.x) + cos * (v.y - center.y));
		return vertex;
	}
	public override void ModifyMesh(VertexHelper helper)
	{
		RectTransform rt = this.transform as RectTransform;
		var verts = ListPool<UIVertex>.Get ();
		var newVerts = ListPool<UIVertex>.Get ();
		helper.GetUIVertexStream (verts);
		if (m_image == null)
			m_image = GetComponent<Image> ();
		if (verts.Count > 0 && m_image.sprite != null)
		{
			helper.Clear ();

			var shapeCount = verts.Count / 6;
			var vertCount = verts.Count;

			var minPos = verts [0].position;
			var maxPos = verts [0].position;
			for (int i = 0; i < vertCount; ++i)
			{
				minPos.x = Mathf.Min (verts [i].position.x, minPos.x);
				maxPos.x = Mathf.Max (verts [i].position.x, maxPos.x);
				minPos.y = Mathf.Min (verts [i].position.y, minPos.y);
				maxPos.y = Mathf.Max (verts [i].position.y, maxPos.y);
			}

			var size = new Vector2 (Mathf.Abs (maxPos.x - minPos.x), Mathf.Abs (maxPos.y - minPos.y));
			float yDelta = size.y / size.x;
			var middleLeft = new Vector3 (minPos.x, (minPos.y + maxPos.y) * 0.5f);
			var rotatePivot = new Vector3 ((minPos.x + maxPos.x) * 0.5f, (minPos.y + maxPos.y) * 0.5f) + pivot;
			var tileAngle = angle;
			if (tileAngle == 0)
			{
				tileAngle = 360;
			}
			var tileCount = (int)Mathf.Min (count, 360 / tileAngle);
			var maxDistance = (minPos - pivot).magnitude;
			for (int j = 0; j < tileCount; ++j)
			{
				for (int i = 0; i < vertCount; ++i)
				{
					UIVertex uiVertex = verts [i];
					var position = uiVertex.position - middleLeft;
					position.x *= scaleX;
					position.y *= scaleY;
					var distance = perspective ? (position + middleLeft - rotatePivot).magnitude : maxDistance;
					uiVertex.position = Rotate (position * distance / maxDistance + middleLeft, rotatePivot, startAngle + tileAngle * j, yDelta);
					newVerts.Add (uiVertex);
				}
			}
			helper.AddUIVertexTriangleStream (newVerts);
		}
		verts.ReleaseToPool ();
		newVerts.ReleaseToPool ();
	}
}