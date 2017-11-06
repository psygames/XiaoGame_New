using UnityEngine;
using Coolfish.System;
using System;

namespace Hotfire.UI
{
	[AddComponentMenu("UI/Effects/SimpleTile")]
	[RequireComponent(typeof(Image))]
	[DisallowMultipleComponent]
	public class SimpleTileEffect : UnityEngine.UI.BaseMeshEffect
	{
		public enum TileType
		{
			None,
			Vertical,
			Horizontal,
			Both,
		}
        [NonSerialized]
        protected Image m_image;

        [SerializeField]
        protected TileType m_tileType;

		Vector4 border = Vector4.zero;

        protected SimpleTileEffect() { }

		protected virtual Material LoadMaterial()
		{
			return new Material (Shader.Find ("Custom/UI/SimpleTile"));
        }

		protected virtual void SetImageType()
		{
                m_image.type = UnityEngine.UI.Image.Type.Simple;
		}
		protected override void OnEnable()
		{
            base.OnEnable();
        

			if (m_image == null)
			{
				m_image = GetComponent<Image> ();
				if (m_image != null)
				{
					m_image.material = LoadMaterial();
				}
			}
		}

		#if UNITY_EDITOR
		protected override void OnValidate()
		{
            CenterTileType = m_tileType;
            base.OnValidate();
		}
		#endif
		public TileType CenterTileType
		{
			get { return m_tileType; }
			set
			{
				if (m_tileType == value) return;
				m_tileType = value;
				if (graphic != null) graphic.SetVerticesDirty();
			}
		}
		public bool IsTileVertical
		{
			get {return (CenterTileType == TileType.Vertical || CenterTileType == TileType.Both);}
		}
		public bool IsTileHorizontal
		{
			get {return (CenterTileType == TileType.Horizontal || CenterTileType == TileType.Both);}
		}
		public override void ModifyMesh(UnityEngine.UI.VertexHelper helper)
		{
			var verts = ListPool<UIVertex>.Get ();
			if(m_image == null)
                return;
            helper.GetUIVertexStream (verts);
			int count = verts.Count;
            helper.Clear ();
            var overrideSprite = m_image.overrideSprite;
            var spriteSize = overrideSprite.rect.size;
            float tileWidth = spriteSize.x / m_image.pixelsPerUnit;
            float tileHeight = spriteSize.y / m_image.pixelsPerUnit;
			var leftTopUV = Vector2.Min(verts[1].uv0, verts[4].uv0);
			var rightBottomUV = Vector2.Max(verts[1].uv0, verts[4].uv0);
			border = new Vector4(leftTopUV.x, leftTopUV.y, rightBottomUV.x, rightBottomUV.y);
           // for (int i = 0; i < shapeCount; ++i) 
			{
                var rectBorder = verts[1].position - verts[4].position;
                rectBorder = new Vector3(Mathf.Abs(rectBorder.x), Mathf.Abs(rectBorder.y), Mathf.Abs(rectBorder.z));
                bool bTileVertical = IsTileVertical;
                bool bTileHorizontal = IsTileHorizontal;
                for (int j = 0; j < 6; ++j)
				{
                    var index = j;
                    var vert = verts[index];
                    var uv0 = vert.uv0;
					//uv0.x = (uv0.x - leftTopUV.x) * rectBorder.x / tileWidth;
					var uv1 = Vector2.one;
                   	if(bTileVertical && tileHeight != 0)
					{
						uv1.y = ((tileHeight == 0 || tileHeight >= rectBorder.y) ? 1f : rectBorder.y / tileHeight);
					}
                    if (bTileHorizontal && tileWidth != 0)
                    {
						uv1.x = ((tileWidth == 0 || tileWidth >= rectBorder.x) ? 1f : rectBorder.x / tileWidth);
                    }
                    vert.uv0 = uv0;
                    vert.uv1 = uv1;
                    verts[index] = vert;
                }
			}
			m_image.material.SetVector ("_Border", border);
			helper.Clear ();
			helper.AddUIVertexTriangleStream(verts);
			verts.ReleaseToPool ();
        }
	}
}
