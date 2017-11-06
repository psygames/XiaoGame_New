using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Coolfish.System;
using System;

namespace Hotfire.UI
{
	[AddComponentMenu("UI/Effects/Text Glow")]
	[DisallowMultipleComponent]
	[RequireComponent(typeof(Text))]
	public class TextGlow : BaseMeshEffect
	{
		[SerializeField]
		private Color m_glowColor = Color.white;

		[SerializeField]
		private float m_expand = 0f;

		private MeshRenderer m_meshRenderer = null;

		private MeshFilter m_meshFilter = null;

		private Mesh m_mesh;

		[SerializeField]
		private GlowImage m_glowImage = null;

		private Text m_text = null;


		protected TextGlow() { }

		protected Vector3[] vertices;
		protected int[] triangles;
		protected Vector2[] uvs;
		protected Color[] colors;


		[SerializeField]
		private Vector2 m_glowUVExpand = Vector2.one;

		[SerializeField]
		private Vector2 m_glowUVOffset = Vector2.zero;

		[SerializeField]
		private bool m_addGlowImage = true;

		private bool m_spriteChanged = false;

		[NonSerialized]
		public bool debugInfomation = false;

		#if UNITY_EDITOR
		protected override void OnValidate()
		{
			glowColor = m_glowColor;
			glowImage = m_glowImage;
			expand = m_expand;
			glowUVExpand = m_glowUVExpand;
			addGlowImage = m_addGlowImage;
			glowUVOffset = m_glowUVOffset;
			base.OnValidate();
		}
		#endif

		public Vector2 glowUVOffset
		{
			get { return m_glowUVOffset; }
			set {
				if (m_glowUVOffset == value)
					return;
				m_glowUVOffset = value;
				if (graphic != null)
					graphic.SetVerticesDirty ();
			}
		}
		public Vector2 glowUVExpand
		{
			get { return m_glowUVExpand; }
			set {
				if (m_glowUVExpand == value)
					return;
				m_glowUVExpand = value;
				if (graphic != null)
					graphic.SetVerticesDirty ();
			}
		}

		public bool addGlowImage
		{
			get { return m_addGlowImage; }
			set {
				if (m_addGlowImage == value)
					return;
				m_addGlowImage = value;
				if (graphic != null)
					graphic.SetVerticesDirty ();
			}
		}

		public GlowImage glowImage
		{
			get { return m_glowImage; }
			set
			{
				if (m_glowImage == value) return;
				m_glowImage = value;
				if (graphic != null) graphic.SetVerticesDirty();
			}
		}
		public Color glowColor
		{
			get { return m_glowColor; }
			set
			{
				if (m_glowColor == value) return;
				m_glowColor = value;
				if (graphic != null) graphic.SetVerticesDirty();
			}
		}
		public float expand
		{
			get { return m_expand; }
			set
			{
				if (m_expand == value) return;
				m_expand = value;
				if (graphic != null) graphic.SetVerticesDirty();
			}
		}

		public void GetArray<T>(ref T[] t, int count)
		{
			if(t == null)
				t = new T[count];
			else
				System.Array.Resize (ref t, count);
		}

		public void InitGlowImage()
		{
			#if UNITY_EDITOR
			if(!Application.isPlaying && m_glowImage == null && addGlowImage)
			{
				var glowImageGO = new GameObject (gameObject.name + "_glow");
				var _glowImage = glowImageGO.AddComponent<GlowImage> ();
				_glowImage.transform.SetParent (transform.parent, false);
				_glowImage.transform.SetSiblingIndex (transform.GetSiblingIndex ());
				m_glowImage = _glowImage;
			}
			#endif

			if (m_glowImage != null)
			{
				m_glowImage.Init (m_text);
				m_glowImage.gameObject.SetActive (false);
			}
		}
		protected override void Awake ()
		{
			base.Awake ();
			if (m_text == null)
				m_text = GetComponent<Text> ();
			InitGlowImage ();

		}
		protected override void OnDisable ()
		{
			base.OnDisable ();
			if (m_glowImage != null)
				m_glowImage.gameObject.SetActive (false);
		}
		void LateUpdate()
		{
			if (m_glowImage == null || m_glowImage.texture == null)
				return;
			if (m_spriteChanged || this.transform.hasChanged)
			{
				m_spriteChanged = false;
				m_glowImage.OnGlowChanged ();
			}
		}

		private void CreateMeshRenderer()
		{
			if(m_meshRenderer == null)
			m_meshRenderer = GetComponent<MeshRenderer> ();
			if (m_meshRenderer == null)
				m_meshRenderer = gameObject.AddComponent<MeshRenderer> ();
			if(m_meshFilter == null)
				m_meshFilter = GetComponent<MeshFilter> ();
			if (m_meshFilter == null)
				m_meshFilter = gameObject.AddComponent<MeshFilter> ();
			if(m_mesh == null)
				m_mesh = new Mesh();
		}
		public override void ModifyMesh(UnityEngine.UI.VertexHelper helper)
		{
			if (!IsActive ())
				return;
			#if UNITY_EDITOR
			if(!Application.isPlaying)
				return;
			#endif
			List<UIVertex> verts = ListPool<UIVertex>.Get();  
			helper.GetUIVertexStream (verts);
			CreateMeshRenderer ();
			m_mesh.Clear ();
			m_meshRenderer.material = GetComponent<Text> ().font.material;
			m_meshFilter.mesh = m_mesh;
			m_meshRenderer.enabled = false;
			GetArray (ref vertices, verts.Count);
			GetArray (ref triangles, verts.Count);
			GetArray (ref colors, verts.Count);
			GetArray (ref uvs, verts.Count);
			for (int i = 0; i < verts.Count; ++i) {
				var vert = verts [i];
				vertices [i] = vert.position;
				if (i % 6 == 0 || i % 6 == 5)
					vertices[i] = vert.position + new Vector3 (-m_expand, m_expand);
				else if (i % 6 == 1)
					vertices[i] = vert.position + new Vector3 (m_expand, m_expand);
				else if (i % 6 == 2 || i % 6 == 3)
					vertices[i] = vert.position + new Vector3 (m_expand, -m_expand);
				else
					vertices[i] = vert.position + new Vector3 (-m_expand, -m_expand);
				uvs [i] = verts [i].uv0;
				triangles [i] = i;
				colors [i] = m_glowColor;
			}
			m_mesh.vertices = vertices;
			m_mesh.uv = uvs;
			m_mesh.triangles = triangles;
			m_mesh.colors = colors;
			verts.ReleaseToPool ();

			m_spriteChanged = true;

		}
	}
}
