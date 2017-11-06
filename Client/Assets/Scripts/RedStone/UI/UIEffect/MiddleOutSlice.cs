using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Coolfish.System;

namespace Hotfire.UI
{
	[DisallowMultipleComponent]
	[ExecuteInEditMode]
	[RequireComponent (typeof(Image))]
	[AddComponentMenu ("UI/Effects/Middle Out Slice")]
	//TODO : Need Refactor!!!!!!
	public class MiddleOutSlice : BaseMeshEffect
	{
		UnityEngine.UI.Image m_image;
		const int maxShapeCount = 9;
		const int vertexCountInOneShape = 6;

		[Range(0, 2)]
		public float offsetX = 1;
		[Range(0, 2)]
		public float offsetY = 1;

		public int drawIndex = 0;

		protected override void Awake ()
		{
			base.Awake ();

			if (m_image == null)
			{
				m_image = GetComponent<UnityEngine.UI.Image> (); 
			}
		}

		protected override void OnEnable ()
		{
			base.OnEnable ();
		}




		public override void ModifyMesh (VertexHelper vh)
		{
			var sprite = m_image.overrideSprite;
			if (sprite == null)
				return;
			
			if (!m_image.hasBorder)
				return;
			
			var verts = ListPool<UIVertex>.Get ();
			var newVert = ListPool<UIVertex>.Get ();
			vh.GetUIVertexStream (verts);
			var shapeCount = verts.Count / vertexCountInOneShape;

			if (shapeCount == maxShapeCount || shapeCount == maxShapeCount - 1)
			{
				Vector2 minVert, maxVert;
				EffectHelper.GetMinMaxVerts (verts, out minVert, out maxVert);

				var rect = m_image.GetPixelAdjustedRect ();
				var spriteRect = sprite.rect;
				Vector2 middleSize = new Vector2 ((spriteRect.width - sprite.border.x - sprite.border.z) / m_image.pixelsPerUnit, (spriteRect.height - sprite.border.y - sprite.border.w) / m_image.pixelsPerUnit);

				middleSize = Vector2.Min (new Vector2(rect.width, rect.height), middleSize);
				var positionLB = minVert + Vector2.Scale (((maxVert - minVert) - middleSize) * 0.5f, new Vector2 (offsetX, offsetY));
				var positionRT = positionLB + middleSize;
				var hasCenter = (maxShapeCount == shapeCount);

				for (int i = 0; i < shapeCount; ++i)
				{
					//if (i > drawIndex)//这两行不知道干什么用的，引起了bug,先注释掉了
					//	break;

					bool isLeftRect = (i == 0 || i == 1 || i == 2);
					bool isRightRect = ((i == 6 || i == 7) || (hasCenter ? i == 8 : i == 5));
					bool isTopRect = (i == 2 || (hasCenter ? (i == 5 || i == 8) : (i == 4 || i == 7)));
					bool isBottomRect = (i == 0 || i == 3 || (hasCenter ? i == 6 : i == 5));
					bool isXCenterRect = (i == 1 || (hasCenter ? (i == 4 || i == 7) : i == 6));
					bool isYCenterRect = (i == 3 || (hasCenter ? (i == 4 || i == 5) : i == 4));
					for (int j = 0; j < vertexCountInOneShape; ++j)
					{
						var index = i * vertexCountInOneShape + j;
						var vert = verts [index];
						var pos = vert.position;
						bool isLeftBorder = (j == 0 || j == 1 || j == 5);
						bool isRightBorder = (j == 2 || j == 3 || j == 4);
						bool isTopBorder = (j == 1 || j == 2 || j == 3);
						bool isBottomBorder = (j == 0 || j == 4 || j == 5);
						if (isLeftRect && isRightBorder || isYCenterRect && isLeftBorder)
						{
							pos.x = positionLB.x;
						}
						if (isRightRect && isLeftBorder || isYCenterRect && isRightBorder)
						{
							pos.x = positionRT.x;
						}
						if (isTopRect && isBottomBorder || isXCenterRect && isTopBorder)
						{
							pos.y = positionRT.y;
						}
						if (isBottomRect && isTopBorder || isXCenterRect && isBottomBorder)
						{
							pos.y = positionLB.y;
						}
						vert.position = pos;
						newVert.Add (vert);
					}
				}
				vh.Clear ();
				vh.AddUIVertexTriangleStream (newVert);
			}
			newVert.ReleaseToPool ();
			verts.ReleaseToPool ();
		}
	}
}

