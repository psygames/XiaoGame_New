using System;
 
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

 namespace RedStone.UI
{
    /// <summary>
    /// Labels are graphics that display text.
    /// </summary>

    [RequireComponent(typeof(RectTransform))]
    [AddComponentMenu("Project UI/Text", 10)]
    [ExecuteInEditMode]
    public class Text : UnityEngine.UI.Text
    {
        [SerializeField]
        protected int m_TextStyle = UIConfig.textStyleNoneID;
        [NonSerialized]
        public MonoBehaviour LTParent = null;
        public bool multiLine = false;
        public bool autoUpdateTranslate = false;
        public virtual int textStyle
        {
            get { return m_TextStyle; }
            set
            {
                m_TextStyle = value;
                LT.SetTextStyle(this);
            }
        }

        protected override void Awake()
        {
            if (Localization.instance != null)
                Localization.instance.SetTextStyle(this);

        }

        [SerializeField]
        protected int m_TextColor = UIConfig.textColorNoneID;
        public virtual int textColor
        {
            get { return m_TextColor; }
            set
            {
                m_TextColor = value;
                LT.SetTextColor(this);
            }
        }

        public void SetColor(Color color)
        {
            this.color = color;
        }

        public void SetKey(string key, params object[] param)
        {
            text = LT.GetText(key, param);
        }
        public void SetKeyWithObject(string key, MonoBehaviour obj)
        {
            LTParent = obj;
            text = LT.Get(key, LTParent);
        }

        [TextArea(3, 10)]
        [SerializeField]
        protected string m_TextKey = "";
        public virtual string textKey
        {
            get
            {
                return m_TextKey;
            }
            set
            {
                m_TextKey = value;
            }
        }

        private static readonly Regex m_spriteTagRegex =
            new Regex(@"<quad name=(.+?)( +size=(.+?))?( +width=(.+?))? */>", RegexOptions.Singleline);
        private static readonly Regex m_emojiRegex =
            new Regex(@"[\uD83C-\uD83E][\uDC00-\uDFFF]", RegexOptions.Singleline);

        public struct TSpriteTag
        {
            public int index;
            public string name;
            public int size;
            public float width;
            public Vector3 position;
            public bool active;
        }
        public List<TSpriteTag> spriteTagList = new List<TSpriteTag>();
        public bool spriteTagChanged = false;

        public float spriteYOffset = -0.1f;
        List<RedStone.UI.Image> m_imageList = new List<RedStone.UI.Image>();

        protected override void Start()
        {
            base.Start();
            InitImage();
        }

        protected void InitImage()
        {
            var list = GetComponentsInChildren<RedStone.UI.Image>(true);
            m_imageList.Clear();
            if (list != null)
            {
                for (int i = 0; i < list.Length; ++i)
                {
                    m_imageList.Add(list[i]);
                }
            }
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            InitImage();
            if (autoUpdateTranslate)
                SetKey(this.m_TextKey);
        }

        void LateUpdate()
        {
            if (spriteTagChanged)
            {
                while (spriteTagList.Count > m_imageList.Count)
                {
                    var image = new GameObject("Image").AddComponent<RedStone.UI.Image>();
                    m_imageList.Add(image);
                    UIHelper.SetParent(transform, image.transform);
                    image.transform.localRotation = Quaternion.identity;
                    image.gameObject.SetActive(false);
                }
                for (int i = 0; i < m_imageList.Count; ++i)
                {
                    if (i >= spriteTagList.Count || !spriteTagList[i].active)
                        m_imageList[i].gameObject.SetActive(false);
                    else
                    {
                        m_imageList[i].gameObject.SetActive(true);
                        m_imageList[i].SetSprite(spriteTagList[i].name);
                        var rectTransform = m_imageList[i].GetComponent<RectTransform>();
                        var size = spriteTagList[i].size;
                        var _fontSize = resizeTextForBestFit ? cachedTextGenerator.fontSizeUsedForBestFit : fontSize;

                        rectTransform.sizeDelta = new Vector2(spriteTagList[i].width, 1) * (size == 0 ? _fontSize : size);

                        rectTransform.localPosition = spriteTagList[i].position + Vector3.up * rectTransform.sizeDelta.y * spriteYOffset;
                    }
                }
            }
            spriteTagChanged = false;
        }
        StringBuilder builder = new StringBuilder();
        static Dictionary<uint, string> m_emojiDict;
        static string GetEmojiName(uint code)
        {
            if (m_emojiDict == null)
                m_emojiDict = new Dictionary<uint, string>();
            string str = null;
            m_emojiDict.TryGetValue(code, out str);
            if (string.IsNullOrEmpty(str))
                return "icon_add";
            return str;
        }
        public override void SetVerticesDirty()
        {
            base.SetVerticesDirty();
            //解析标签属性  
            spriteTagList.Clear();

            var emojiMatches = m_emojiRegex.Matches(text);
            for (int i = 0; i < emojiMatches.Count; ++i)
            {
                uint code = 0;
                var match = emojiMatches[i].Groups[0].Value;
                var bytes = System.Text.Encoding.Unicode.GetBytes(match);
                builder.Length = 0;
                for (int j = bytes.Length - 1; j >= 0; --j)
                    code = code << 8 | bytes[j];
                TSpriteTag spriteTag = new TSpriteTag();
                spriteTag.name = GetEmojiName(code);
                spriteTag.index = emojiMatches[i].Index - i;
                spriteTag.size = 0;
                spriteTag.width = 1;
                spriteTag.active = false;
                spriteTagList.Add(spriteTag);
            }
            if (supportRichText)
            {
                var matches = m_spriteTagRegex.Matches(text);
                for (int i = 0; i < matches.Count; ++i)
                {
                    var match = matches[i];
                    TSpriteTag spriteTag = new TSpriteTag();
                    spriteTag.name = match.Groups[1].Value;
                    spriteTag.index = match.Index;
                    spriteTag.size = 0;
                    spriteTag.active = true;
                    if (match.Groups.Count > 3 && match.Groups[3].Success)
                    {
                        int.TryParse(match.Groups[3].Value, out spriteTag.size);
                    }
                    spriteTag.width = 1f;
                    if (match.Groups.Count > 5 && match.Groups[5].Success)
                    {
                        float.TryParse(match.Groups[5].Value, out spriteTag.width);
                    }
                    spriteTagList.Add(spriteTag);
                }
            }
        }
        protected override void OnPopulateMesh(UnityEngine.UI.VertexHelper vh)
        {
            base.OnPopulateMesh(vh);
            var verts = ListPool<UnityEngine.UIVertex>.Get();
            vh.GetUIVertexStream(verts);
            var spriteTagCount = spriteTagList.Count;
            for (int i = 0; i < spriteTagCount; ++i)
            {
                var spriteTag = spriteTagList[i];
                spriteTag.active = true;
                var vertStart = spriteTag.index * 6;
                if (vertStart >= verts.Count)
                {
                    spriteTag.active = false;
                    continue;
                }
                var posLT = verts[vertStart].position;
                var posRB = verts[vertStart + 2].position;
                spriteTag.position = (posLT + posRB) * 0.5f;
                for (int j = 0; j < 6; ++j)
                {
                    int index = spriteTag.index * 6 + j;
                    var vert = verts[index];
                    vert.uv0 = Vector2.zero;
                    vert.color = new Color(0, 0, 0, 0);
                    verts[index] = vert;
                }
                spriteTagList[i] = spriteTag;
            }

            spriteTagChanged = true;

            vh.Clear();
            vh.AddUIVertexTriangleStream(verts);
            verts.ReleaseToPool();
        }

        public void SetAlpha(float alpha)
        {
            UIHelper.SetImageAlpha(this, alpha);
        }
    }
}
