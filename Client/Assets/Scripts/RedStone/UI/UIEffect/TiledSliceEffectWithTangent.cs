using UnityEngine;
using Coolfish.System;
using System;

namespace Hotfire.UI
{
	[AddComponentMenu("UI/Effects/TiledSliceWithTangent")]
	[RequireComponent(typeof(Image))]
	[DisallowMultipleComponent]
	public class TiledSliceEffectWithTangent : TiledSliceEffect
	{
		
		protected override Material LoadMaterial()
		{
			return Resources.Load<Material>("UIMaterial/TileUIWithTangent");
        }
		public override void ModifyMesh(UnityEngine.UI.VertexHelper helper)
		{
			var verts = ListPool<UIVertex>.Get ();
			if(m_image == null)
                return;
            helper.GetUIVertexStream (verts);
			int count = verts.Count;
			
            var shapeCount = m_image.fillCenter ? 9 : 8;
            if(count < shapeCount * 6 || m_image.overrideSprite == null)
                return;
			helper.Clear ();
            var overrideSprite = m_image.overrideSprite;
            var border = overrideSprite.border;
            var spriteSize = overrideSprite.rect.size;
            float tileWidth = (spriteSize.x - border.x - border.z) / m_image.pixelsPerUnit;
            float tileHeight = (spriteSize.y - border.y - border.w) / m_image.pixelsPerUnit;
            for (int i = 0; i < shapeCount; ++i) 
			{
                var leftTopUV = verts[6 * i + 1].uv0;
                var rightBottomUV = verts[6 * i + 4].uv0;
                var rectBorder = verts[6 * i + 4].position - verts[6 * i + 1].position;
                rectBorder = new Vector3(Mathf.Abs(rectBorder.x), Mathf.Abs(rectBorder.y), Mathf.Abs(rectBorder.z));
                bool bTileVertical = m_image.fillCenter ? (IsBorderTileVertical && (i == 1 || i == 7) || IsCenterTileVertical && i == 4) : IsBorderTileVertical && (i == 1 || i == 6);
				bool bTileHorizontal = m_image.fillCenter ? (IsBorderTileHorizontal && (i == 3 || i == 5) || IsCenterTileHorizontal && i == 4) : IsBorderTileHorizontal && (i == 3 || i == 4);
                for (int j = 0; j < 6; ++j)
				{
                    var index = 6 * i + j;
                    var vert = verts[index];
                    var uv1 = Vector2.zero;
                   	if(bTileVertical && tileHeight != 0)
					{
						uv1.y = (tileHeight == 0 ? 0f : rectBorder.y / tileHeight);
					}
                    if (bTileHorizontal && tileWidth != 0)
                    {
						uv1.x = (tileWidth == 0 ? 0f : rectBorder.x / tileWidth);
                    }
                    vert.uv1 = uv1;
                    var tangent = m_image.rectTransform.InverseTransformDirection(leftTopUV.x, leftTopUV.y, rightBottomUV.x);
                    
                    vert.tangent = new Vector4(tangent.x, tangent.y, tangent.z, rightBottomUV.y);
                    if(m_changeBorderColor && !(m_image.fillCenter && i == 4))
                        vert.color *= m_borderColor;
					if(m_changeCenterColor && (m_image.fillCenter && i == 4))
					{
						if(j == 0 || j == 5)
                            vert.color = vert.color * m_customCenterColor[0];
						else if(j == 1)
                            vert.color = vert.color * m_customCenterColor[1];
						else if(j == 2 || j == 3)
                            vert.color = vert.color * m_customCenterColor[2];
						else if(j == 4)
                            vert.color = vert.color * m_customCenterColor[3];
                    }
                    verts[index] = vert;
                }
            }
			helper.Clear ();
			helper.AddUIVertexTriangleStream(verts);
			verts.ReleaseToPool ();
        }
	}
}
