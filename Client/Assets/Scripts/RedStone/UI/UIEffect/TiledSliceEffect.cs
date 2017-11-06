using UnityEngine;
using Coolfish.System;
using System;

namespace Hotfire.UI
{
	[AddComponentMenu("UI/Effects/TiledSlice")]
	[RequireComponent(typeof(Image))]
	[DisallowMultipleComponent]
	public class TiledSliceEffect : UnityEngine.UI.BaseMeshEffect
	{
		public enum TileType
		{
			None,
			Vertical,
			Horizontal,
			Both,
		}
        [SerializeField]
        protected TileType m_borderTileType = TileType.Both;

        [SerializeField]
        protected TileType m_centerTileType = TileType.Both;

        [SerializeField]
        protected bool m_changeBorderColor = false;
        [SerializeField]
        protected Color m_borderColor = Color.white;

		[SerializeField]
        protected bool m_changeCenterColor = false;

        [SerializeField]
        protected Color[] m_customCenterColor = new Color[4];
        [NonSerialized]
        protected Image m_image;
        protected TiledSliceEffect() { }

		protected virtual Material LoadMaterial()
		{
			return Resources.Load<Material>("UIMaterial/TileUI");
        }
		protected override void OnEnable()
		{
            base.OnEnable();
        

			if(m_image == null)
                m_image = GetComponent<Image>();
			if(m_image != null)
			{
                m_image.type = UnityEngine.UI.Image.Type.Sliced;
				#if UNITY_EDITOR
				if (!Application.isPlaying)
				{
                    m_image.material = LoadMaterial();
                }	
				#endif
            }
		}

		#if UNITY_EDITOR
		protected override void OnValidate()
		{
            BorderTileType = m_borderTileType;
            CenterTileType = m_centerTileType;
            base.OnValidate();
		}
		#endif

		public TileType BorderTileType
		{
			get { return m_borderTileType; }
			set
			{
				if (m_borderTileType == value) return;
				m_borderTileType = value;
				if (graphic != null) graphic.SetVerticesDirty();
			}
		}
		public TileType CenterTileType
		{
			get { return m_centerTileType; }
			set
			{
				if (m_centerTileType == value) return;
				m_centerTileType = value;
				if (graphic != null) graphic.SetVerticesDirty();
			}
		}
		public bool ChangeCenterColor
		{
			get { return m_changeCenterColor; }
			set
			{
				if (m_changeCenterColor == value) return;
				m_changeCenterColor = value;
				if (graphic != null) graphic.SetVerticesDirty();
			}
		}
		public Color GetCustomCenterColor(int index)
		{
			if(0 <= index && index < 4)
			{
                return m_customCenterColor[index];
            }
            return Color.clear;
        }
		public void SetCustomCenterColor(int index, Color color)
		{
			if(0 <= index && index < 4)
			{
                m_customCenterColor[index] = color;
            }
        }
		public bool IsBorderTileVertical
		{
			get {return (BorderTileType == TileType.Vertical || BorderTileType == TileType.Both);}
		}
		public bool IsBorderTileHorizontal
		{
			get {return (BorderTileType == TileType.Horizontal || BorderTileType == TileType.Both);}
		}
		public bool IsCenterTileVertical
		{
			get {return (CenterTileType == TileType.Vertical || CenterTileType == TileType.Both);}
		}
		public bool IsCenterTileHorizontal
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
                leftTopUV = new Vector2((int)(leftTopUV.x * 10000), (int)(leftTopUV.y * 10000));
                rightBottomUV = new Vector2((int)(rightBottomUV.x * 10000), (int)(rightBottomUV.y * 10000));
                var rectBorder = verts[6 * i + 4].position - verts[6 * i + 1].position;
                rectBorder = new Vector3(Mathf.Abs(rectBorder.x), Mathf.Abs(rectBorder.y), Mathf.Abs(rectBorder.z));
                bool bTileVertical = m_image.fillCenter ? (IsBorderTileVertical && (i == 1 || i == 7) || IsCenterTileVertical && i == 4) : IsBorderTileVertical && (i == 1 || i == 6);
				bool bTileHorizontal = m_image.fillCenter ? (IsBorderTileHorizontal && (i == 3 || i == 5) || IsCenterTileHorizontal && i == 4) : IsBorderTileHorizontal && (i == 3 || i == 4);
                for (int j = 0; j < 6; ++j)
				{
                    var index = 6 * i + j;
                    var vert = verts[index];
                    var uv0 = vert.uv0;
                    var uv1 = Vector2.zero;
                   	if(bTileVertical && tileHeight != 0)
					{
						uv1.y = ((tileHeight == 0 || tileHeight >= rectBorder.y) ? 1f : tileHeight / rectBorder.y);
					}
                    if (bTileHorizontal && tileWidth != 0)
                    {
						uv1.x = ((tileWidth == 0 || tileWidth >= rectBorder.x) ? 1f : tileWidth / rectBorder.x);
                    }
                    uv0 *= 0.5f;
                    uv0.x += (uv0.x * leftTopUV.x) < 0 ? -leftTopUV.x : leftTopUV.x;
                    uv0.y += (uv0.y * leftTopUV.y) < 0 ? -leftTopUV.y : leftTopUV.y;
                    uv1 *= 0.5f;
                    uv1.x += rightBottomUV.x < 0 ? -rightBottomUV.x : rightBottomUV.x;
                    uv1.y += rightBottomUV.y < 0 ? -rightBottomUV.y : rightBottomUV.y;
                    vert.uv0 = uv0;
                    vert.uv1 = uv1;
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
