using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 

 namespace RedStone.UI
{
	[RequireComponent (typeof(Graphic))]
	[AddComponentMenu ("UI/Effects/Gradient")]
	public class Gradient : BaseMeshEffect
	{
		public Color color1 = Color.white;
		public Color color2 = Color.white;
		public Color color3 = Color.white;
		public Color color4 = Color.white;


		public bool overrideTargetColor = true;

		Graphic targetGraphic;

		protected override void Awake ()
		{
			targetGraphic = GetComponent<Graphic> ();
		}

		public override void ModifyMesh (VertexHelper vh)
		{
			int count = vh.currentVertCount;
			if (!IsActive () || count == 0)
			{
				return;
			}
			var verts = ListPool<UIVertex>.Get ();
			vh.GetUIVertexStream (verts);
			Vector2 minVert = new Vector2 (verts [0].position.x, verts [0].position.y);
			Vector2 maxVert = new Vector2 (verts [0].position.x, verts [0].position.y);
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
			var distant = maxVert - minVert;
			var distant2 = distant.x * distant.y;
			for (int i = 0; i < verts.Count; ++i)
			{
				var vert = verts [i];
				var pos = vert.position;
				var curColor1 = color1 * (maxVert.x - pos.x) * (maxVert.y - pos.y);
				var curColor2 = color2 * (pos.x - minVert.x) * (maxVert.y - pos.y);
				var curColor3 = color3 * (pos.x - minVert.x) * (pos.y - minVert.y);
				var curColor4 = color4 * (maxVert.x - pos.x) * (pos.y - minVert.y);
				var color = (curColor1 + curColor2 + curColor3 + curColor4) / distant2;
				if (overrideTargetColor)
					vert.color = color;
				else
					vert.color *= color;
				verts [i] = vert;
			}
			vh.Clear ();
			vh.AddUIVertexTriangleStream (verts);
			verts.ReleaseToPool ();
		}

        public void SetVertical(Color top, Color bottom)
        {
            color1 = bottom;
            color2 = bottom;
            color3 = top;
            color4 = top;
        }

        public void SetHorizontal(Color left, Color right)
        {
            color1 = left;
            color2 = right;
            color3 = right;
            color4 = left;
        }
	}
}
