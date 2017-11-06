using UnityEngine;
using System.Collections;

namespace Hotfire.UI
{
	public static class EffectHelper
	{
		public static void GetMinMaxVerts (System.Collections.Generic.List<UIVertex> verts, out Vector2 minVert, out Vector2 maxVert)
		{
			minVert = new Vector2 (verts [0].position.x, verts [0].position.y);
			maxVert = new Vector2 (verts [0].position.x, verts [0].position.y);
			for (int i = 0; i < verts.Count; ++i)
			{
				var x = verts [i].position.x;
				var y = verts [i].position.y;
				if (x < minVert.x)
					minVert.x = x;
				if (x > maxVert.x)
					maxVert.x = x;
				if (y < minVert.y)
					minVert.y = y;
				if (y > maxVert.y)
					maxVert.y = y;
			}
		}

	}
}
