using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using RedStone.UI;
using System.Reflection;
 

 namespace RedStone
{
    public class Localization : Core.Singleton<Localization>
    {
        private int m_language = GetPrefsLanguageID();

        public const string LANGUAGE_PATH_ROOT = "Languages/";
        public const string LANGUAGE_UPDATE_PATH_ROOT = "LanguageUpdate/";
        public const string FONT_PATH_ROOT = "Font/";

        public const string defaultFontName = "Arial";
        public const string defaultFontBoldName = "Arial";

        private Dictionary<string, TextData> m_textDict = new Dictionary<string, TextData>();
        private Dictionary<int, FontStyleData> m_fontStyleDict = new Dictionary<int, FontStyleData>();
        private Dictionary<string, Font> m_fontDict = new Dictionary<string, Font>();
        private Dictionary<string, Font> m_defaultFontDict = new Dictionary<string, Font>();

#if UNITY_EDITOR
        public class TextTree
        {
            public string path;
            public string text;
            public Dictionary<string, TextTree> children = new Dictionary<string, TextTree>();
            public void Add(string[] texts, string text, int layer = -1)
            {
                if (layer == texts.Length - 1)
                {
                    this.text = text;
                }
                else if (layer < texts.Length - 1)
                {
                    if (layer >= 0)
                        path = texts[layer];
                    ++layer;
                    TextTree child = null;
                    children.TryGetValue(texts[layer], out child);
                    if (child == null)
                    {
                        child = new TextTree();
                        children.Add(texts[layer], child);
                    }
                    child.Add(texts, text, layer);
                }
            }
        }
        public TextTree textTree = new TextTree();
#endif
        private static Font s_systemDefaultFont = Font.CreateDynamicFontFromOSFont("Arial", 16);

        private bool m_fontStyleLoaded = false;
        private bool m_languageLoaded = false;
        private bool m_languageFontLoaded = false;
        private bool m_languageDefaultFontLoaded = false;


        public static int GetPrefsLanguageID()
        {
            return PlayerPrefs.GetInt("languageID", GetDefaultLangID());
        }

        private static void SetPrefsLanguageID(int id)
        {
            PlayerPrefs.SetInt("languageID", id);
        }

        public static int GetDefaultLangID()
        {
            return 1;
        }

        #region Editor Load
#if UNITY_EDITOR
        public static void CreateAndLoad()
        {
            TableManager.CreateAndLoad();
            if (instance == null)
                CreateInstance();
            instance.InitSync();
            instance.InitTextTree();
            Debug.Log("Init Localization");
        }

        void InitTextTree()
        {
            foreach (var text in m_textDict)
            {
                if (!string.IsNullOrEmpty(text.Value.text))
                {
                    var texts = text.Key.Split('_');
                    textTree.Add(texts, text.Key);
                }
            }
        }
        public void SetLanguageSync(int id)
        {
            m_language = id;
            InitSync();
        }

        private void InitSync()
        {
            ClearAll();
            LoadLanguageTableSync();
            LoadDefaultFontSync();
            LoadLanguageFontSync();
            LoadFontStyleSync();
        }

        private void LoadDefaultFontSync()
        {
            TableLanguage languageTable = TableManager.instance.GetData<TableLanguage>(m_language);
            List<string> pathList = new List<string>
            {
                FONT_PATH_ROOT + defaultFontName,
                FONT_PATH_ROOT + defaultFontBoldName,
            };
            LoadFontSync(pathList, a =>
            {
                var it = a.GetEnumerator();
                while (it.MoveNext())
                {
                    if (m_defaultFontDict.ContainsKey(it.Current.Key))
                        m_defaultFontDict[it.Current.Key] = it.Current.Value;
                    else
                        m_defaultFontDict.Add(it.Current.Key, it.Current.Value);
                }
            });
        }

        private void LoadLanguageFontSync()
        {
            TableLanguage languageTable = TableManager.instance.GetData<TableLanguage>(m_language);
            List<string> pathList = new List<string>
            {
                GetFontPath(languageTable.fontName),
                GetFontPath(languageTable.fontBoldName),
            };
            LoadFontSync(pathList, a =>
            {
                var it = a.GetEnumerator();
                while (it.MoveNext())
                {
                    if (m_fontDict.ContainsKey(it.Current.Key))
                        m_fontDict[it.Current.Key] = it.Current.Value;
                    else
                        m_fontDict.Add(it.Current.Key, it.Current.Value);

                }
            });
        }

        private void LoadFontSync(List<string> pathList, Action<Dictionary<string, Font>> completed)
        {
            Dictionary<string, Font> fontDict = new Dictionary<string, Font>();
            for (int i = 0; i < pathList.Count; i++)
            {
                Font font = ResourceManager.instance.GetResourceByPath(pathList[i]) as Font;
                if (font != null)
                    fontDict.Add(font.name, font);
                completed.Invoke(fontDict);
            }
        }

        private void LoadLanguageTableSync()
        {
            string fileName = TableManager.instance.GetData<TableLanguage>(m_language).fileName;
            string path = LANGUAGE_PATH_ROOT + fileName;
            TextAsset textAsset = ResourceManager.instance.GetResourceByPath(path) as TextAsset;
            InitLanguageTable(textAsset);
        }

        private void LoadFontStyleSync()
        {
            string fileName = TableManager.instance.GetData<TableLanguage>(m_language).fontStyleName;
            string path = LANGUAGE_PATH_ROOT + fileName;
            TextAsset textAsset = ResourceManager.instance.GetResourceByPath(path) as TextAsset;
            InitFontStyle(textAsset);
        }
#endif
        #endregion

        public void SetLanguage(int id)
        {
            m_language = id;
            SetPrefsLanguageID(id);
            Init();
            CheckLanguageLoadCompleted();
        }

        private IEnumerator CheckLanguageLoadCompleted()
        {
            while (!Localization.instance.IsReady())
                yield return null;
        }

        public Font GetFont(bool isBold)
        {
            TableLanguage languageTable = TableManager.instance.GetData<TableLanguage>(m_language);
            string fontName = isBold ? languageTable.fontBoldName : languageTable.fontName;
            if (m_fontDict.ContainsKey(fontName))
                return m_fontDict[fontName];
            fontName = isBold ? defaultFontBoldName : defaultFontName;
            if (m_defaultFontDict.ContainsKey(fontName))
                return m_defaultFontDict[fontName];
            return s_systemDefaultFont;
        }

        public string GetText(string id, params object[] placeholderStr)
        {
            string value = id;
            if (m_textDict.ContainsKey(id))
            {
                TextData tmp = m_textDict[id];
                if (placeholderStr == null)
                    value = tmp.text;
                else
                    value = ReplaceTextHolder(tmp.text, placeholderStr);
            }
            return value;
        }


        private string ReplaceTextHolder(string text, params object[] holder)
        {
            char[] ts = text.ToCharArray();
            StringBuilder sb = new StringBuilder();
            StringBuilder sbHolder = new StringBuilder();
            bool isHolder = false;
            for (int i = 0; i < ts.Length; i++)
            {
                if (isHolder)
                {
                    if (ts[i] == '}')
                    {
                        isHolder = false;
                        string hStr = sbHolder.ToString();
                        int spIndex = hStr.IndexOf(":");
                        int index = 0;
                        bool bParse = false;
                        if (spIndex >= 0)
                            bParse = int.TryParse(hStr.Substring(0, spIndex), out index);
                        if (bParse && index < holder.Length)
                        {
                            sb.Append(holder[index]);
                        }
                        else
                        {
                            sb.Append(hStr);
                        }
                    }
                    if (isHolder)
                        sbHolder.Append(ts[i]);
                }
                else
                {
                    if (ts[i] == '{')
                    {
                        isHolder = true;
                        sbHolder.Remove(0, sbHolder.Length);
                    }
                    if (!isHolder)
                        sb.Append(ts[i]);
                }
            }
            return sb.ToString();
        }

        public ICollection<FontStyleData> GetAllFontStyles()
        {
            return m_fontStyleDict.Values;
        }

        public FontStyleData GetFontStyleData(Text text)
        {
            FontStyleData data;
            if (m_fontStyleDict.TryGetValue(text.textStyle, out data))
            {
                return data;
            }
            return null;
        }

        public void SetTextStyle(Text text, bool force = false)
        {
#if UNITY_EDITOR
            if (!Application.isPlaying && !force)
                SetTextStyle(text, text.textStyle);
            else
#endif
            {
                var size = SetTextStyle(text, text.textStyle);
                if (text.verticalOverflow != VerticalWrapMode.Overflow
                    && !text.multiLine && text.textStyle != UIConfig.textStyleNoneID)
                {
                    text.resizeTextMaxSize = size;
                    text.resizeTextForBestFit = true;
                }
            }
        }

        void SetTextStyleByFontStyle(Text text)
        {
            Font font = GetFont(text.fontStyle == FontStyle.Bold || text.fontStyle == FontStyle.BoldAndItalic);
            if (text.font != font)
            {
                text.font = font;
            }
        }

        public int SetTextStyle(Text text, int style)
        {
            int size = text.fontSize;
            if (style != UIConfig.textStyleNoneID)
            {
                FontStyleData data;
                if (m_fontStyleDict.TryGetValue(style, out data))
                {
                    Font font = GetFont(data.isBold);
                    if (text.font != font)
                    {
                        text.font = font;
                    }
                    text.fontSize = data.fontSize;
                    text.fontStyle = data.unityFontStyle;
                    size = text.fontSize;
                }
                else
                {
                    SetTextStyleByFontStyle(text);
                }
            }
            else
            {
                SetTextStyleByFontStyle(text);
            }
            return size;
        }
        public void SetTextColor(Text text)
        {
            if (text.textColor != UIConfig.textColorNoneID)
            {
                text.color = TableManager.instance.GetData<TableTextColor>(text.textColor).value;
            }
        }

        private void LoadFontStyle()
        {
            string fileName = TableManager.instance.GetData<TableLanguage>(m_language).fontStyleName;
            string path = LANGUAGE_PATH_ROOT + fileName;
            ResourceManager.instance.GetResourceByPathAsync(path, (AsyncResource ar) =>
            {
                InitFontStyle(ar.loadedAsset as TextAsset);
                m_fontStyleLoaded = true;
                ResourceManager.instance.UnloadResouce(path);
            });
        }

        private void InitFontStyle(TextAsset textAsset)
        {
            Dictionary<int, object> dict = (Dictionary<int, object>)TableManager.instance.LoadOneTable(typeof(FontStyleData), textAsset);
            var it = dict.GetEnumerator();
            while (it.MoveNext())
            {
                FontStyleData data = (it.Current.Value) as FontStyleData;
                m_fontStyleDict.Add(it.Current.Key, data);
            }
        }

        private void LoadLanguageTable()
        {
            string fileName = TableManager.instance.GetData<TableLanguage>(m_language).fileName;
            List<string> pathList = new List<string>() { LANGUAGE_PATH_ROOT + fileName, LANGUAGE_UPDATE_PATH_ROOT + fileName };
            ResourceManager.instance.GetResourceByPathAsync(pathList, (arList) =>
            {
                foreach (var ar in arList)
                {
                    if (ar.Value == null || ar.Value.loadedAsset == null)
                        continue;
                    InitLanguageTable(ar.Value.loadedAsset as TextAsset);
                    m_languageLoaded = true;
                }
            });
        }

        private void LoadDefaultFont()
        {
            if (m_defaultFontDict.ContainsKey(defaultFontName)
                && m_defaultFontDict.ContainsKey(defaultFontBoldName))
                return;
            List<string> pathList = new List<string>
            {
                FONT_PATH_ROOT + defaultFontName,
                FONT_PATH_ROOT + defaultFontBoldName,
            };
            LoadFontAsync(pathList, a =>
            {
                var it = a.GetEnumerator();
                while (it.MoveNext())
                {
                    m_defaultFontDict.Add(it.Current.Key, it.Current.Value);
                }
                m_languageDefaultFontLoaded = true;
            });
        }
        private string GetFontPath(string fontName)
        {
            string fontFamilyPath = fontName.Substring(0, Mathf.Max(0, fontName.LastIndexOf('-')));
            return "{0}{1}/{2}".FormatStr(FONT_PATH_ROOT, fontFamilyPath, fontName);
        }
        private void LoadLanguageFont()
        {
            TableLanguage languageTable = TableManager.instance.GetData<TableLanguage>(m_language);
            string fontPath = GetFontPath(languageTable.fontName);
            string fontBoldPath = GetFontPath(languageTable.fontBoldName);
            if (m_fontDict.ContainsKey(languageTable.fontName) && m_fontDict.ContainsKey(languageTable.fontName))
            {
                m_languageFontLoaded = true;
                return;
            }
            ClearFont();
            List<string> pathList = new List<string>();
            pathList.Add(fontPath);
            if (fontPath != fontBoldPath)
                pathList.Add(fontBoldPath);
            LoadFontAsync(pathList, a =>
            {
                var it = a.GetEnumerator();
                while (it.MoveNext())
                {
                    m_fontDict.Add(it.Current.Key, it.Current.Value);
                }
                m_languageFontLoaded = true;
            });
        }

        private void LoadFontAsync(List<string> pathList, Action<Dictionary<string, Font>> completed)
        {
            Dictionary<string, Font> fontDict = new Dictionary<string, Font>();
            var count = pathList.Count;
            int loadedFont = 0;
            for (int i = 0; i < count; ++i)
            {
                var fontName = pathList[i];
                ResourceManager.instance.GetResourceByPathAsync(fontName, (ar) =>
                {
                    if (ar.allLoadedAssets != null)
                    {
                        for (int j = 0; j < ar.allLoadedAssets.Length; ++j)
                        {
                            Font font = ar.allLoadedAssets[j] as Font;
                            if (font != null && !fontDict.ContainsKey(font.name))
                                fontDict.Add(font.name, font);
                        }
                    }
                    else if (ar.loadedAsset != null)
                    {
                        Font font = ar.loadedAsset as Font;
                        if (font != null && !fontDict.ContainsKey(font.name))
                            fontDict.Add(font.name, font);
                    }
                    loadedFont++;
                    if (loadedFont == pathList.Count)
                        completed(fontDict);

                });
            }
        }

        private void InitLanguageTable(TextAsset textasset)
        {
            Dictionary<string, object> dict = (Dictionary<string, object>)TableManager.instance.LoadOneTable(typeof(TextData), textasset);
            var it = dict.GetEnumerator();
            while (it.MoveNext())
            {
                TextData textData = (it.Current.Value) as TextData;
                textData.text = textData.text.Replace("\\n", "\n"); // 处理文本\\n转义

                m_textDict[it.Current.Key] = textData;
            }
        }

        public bool IsReady()
        {
            return m_languageLoaded
                && m_fontStyleLoaded
                && m_languageFontLoaded
                && m_languageDefaultFontLoaded;
        }

        private void Init()
        {
            ClearAll();
            LoadLanguageTable();
            LoadDefaultFont();
            LoadLanguageFont();
            LoadFontStyle();
        }

        private void ClearAll()
        {
            m_textDict.Clear();
            m_fontStyleDict.Clear();
            //ClearFont();
            m_languageLoaded = false;
            m_fontStyleLoaded = false;
            m_languageFontLoaded = false;
            m_languageDefaultFontLoaded = false;
        }

        public int GetFitTextStyle(Text text)
        {
            var textWidth = text.preferredWidth;
            var textHeight = text.preferredHeight;
            if (textWidth > 0 && textHeight > 0)
            {
                int size = text.fontSize;
                var sizeDelta = text.rectTransform.sizeDelta;
                int minDelta = (int)Mathf.Min(sizeDelta.x, sizeDelta.y);
                size = (int)Mathf.Min(size * sizeDelta.y / textHeight, size * sizeDelta.x / textWidth);
                size = Mathf.Min(minDelta, size);
                var list = instance.GetAllFontStyles().ToListFromPool(a => text.fontStyle == FontStyle.Bold ? a.isBold : !a.isBold);
                for (int i = list.Count - 1; i >= 0; --i)
                {
                    if (size >= list[i].fontSize)
                    {
                        return list[i].id;
                    }
                }
                list.ReleaseToPool();
            }
            return text.textStyle;
        }

        public static int GetNearlyTextStyle(float size, bool isBold)
        {
            int lastSize = 0;
            int lastStyleID = UIConfig.textStyleNoneID;
            foreach (var style in instance.GetAllFontStyles())
            {
                if (style.isBold != isBold)
                    continue;
                int curSize = style.fontSize;
                if (curSize > size)
                {
                    return (curSize - size) > (size - lastSize) ? lastStyleID : style.id;
                }
                lastStyleID = style.id;
                lastSize = curSize;
            }
            return lastStyleID;
        }

        private void ClearFont()
        {
            var itr = m_fontDict.GetEnumerator();
            while (itr.MoveNext())
            {
                Resources.UnloadAsset(itr.Current.Value);
            }
            m_fontDict.Clear();
        }
    }

    public class TextData
    {
        public TextData(IDictionary dict)
        {
            this.id = (string)dict["id"];
            this.description = (string)dict["description"];
            this.text = (string)dict["text"];
        }

        public string id;
        public string text;
        public string description;
    }

    public class FontStyleData
    {
        public FontStyleData(IDictionary dict)
        {
            this.id = (int)dict["id"];
            this.name = (string)dict["name"];
            this.fontSize = (int)dict["fontSize"];
            this.fontStyle = (int)dict["fontStyle"];
        }

        public bool isBold { get { return fontStyle == 1 || fontStyle == 3; } }
        public FontStyle unityFontStyle { get { return (FontStyle)fontStyle; } }
        public int id;
        public string name;
        public int fontSize;
        public int fontStyle;
    }
}