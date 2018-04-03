
using UnityEngine;
using RedStone;
using System.Collections;
using UnityEditor;
using System.IO;
using UnityEditor.Sprites;
using System;
using System.Collections.Generic;
using System.Text;

namespace EditorTools
{
    public class PathConfig
    {
        public const string localPath = "/ResourcesUI/";
        public const string localFullPath = "Assets/ResourcesUI/";
        public const string localFullPath2 = "Assets/ResourcesUI/";
        public const string atlasPath = "Assets/Resources/Atlas/";
    }

    [Serializable]
    public class AtlasTextureFormatItem
    {
        public string atlasName;
        public string iosFormat;
        public string androidFormat;
        public string standaloneFormat;
        public bool isTight = false;

        public TextureImporterFormat GetTextureFormat()
        {

            string format = standaloneFormat;
#if UNITY_IOS
			format = iosFormat;
#elif UNITY_ANDROID
			format = androidFormat;
#endif
            return Utility.ToEnum<TextureImporterFormat>(format, TextureImporterFormat.RGBA32);
        }
        public TextureImporterFormat GetTextureFormat(BuildTarget target)
        {
            string format = standaloneFormat;
            if (target == BuildTarget.iOS)
                format = iosFormat;
            else if (target == BuildTarget.Android)
                format = androidFormat;
            return Utility.ToEnum<TextureImporterFormat>(format, TextureImporterFormat.RGBA32);
        }
        public void SetTextureFormat(TextureImporterFormat format)
        {
            var formatStr = format.ToString();
#if UNITY_IOS
			iosFormat = formatStr;
#elif UNITY_ANDROID
			androidFormat = formatStr;
#else
            standaloneFormat = formatStr;
#endif
        }

        public void SetTextureFormat(TextureImporterFormat format, BuildTarget target)
        {
            var formatStr = format.ToString();
            if (target == BuildTarget.iOS)
                iosFormat = formatStr;
            else if (target == BuildTarget.Android)
                androidFormat = formatStr;
            else
                standaloneFormat = formatStr;
        }
    }

    [Serializable]
    public class AtlasTextureFormatList
    {
        [NonSerialized]
        public Dictionary<string, AtlasTextureFormatItem> dict = new Dictionary<string, AtlasTextureFormatItem>();

        [SerializeField]
        public List<AtlasTextureFormatItem> list = new List<AtlasTextureFormatItem>();

        public void Init()
        {
            if (list != null)
            {
                foreach (var item in list)
                {
                    dict.Add(item.atlasName, item);
                }
            }
        }

        public TextureImporterFormat GetTextureFormat(string atlasName, BuildTarget target)
        {
            AtlasTextureFormatItem item = null;
            if (dict.TryGetValue(atlasName, out item))
            {
                return item.GetTextureFormat(target);
            }
            return TextureImporterFormat.RGBA32;
        }
        public bool IsTight(string atlasName)
        {
            AtlasTextureFormatItem item = null;
            if (dict.TryGetValue(atlasName, out item))
            {
                return item.isTight;
            }
            return false;
        }
    }

    public class AtlasPackerHelper : AssetPostprocessor
    {
        public static AtlasTextureFormatList list = null;
        public const string ATLAS_TEXTURE_FORMAT_CONFIG_PATH = EditorConfig.CONFIG_PATH + "AtlasTextureFormat.json";
        public const int MAX_TEXTURE_SIZE = 4096;
        public static void Init()
        {
            if (File.Exists(ATLAS_TEXTURE_FORMAT_CONFIG_PATH))
            {
                string str = File.ReadAllText(ATLAS_TEXTURE_FORMAT_CONFIG_PATH);
                list = JsonUtility.FromJson<AtlasTextureFormatList>(str);
                if (list != null)
                    list.Init();
            }
        }

        static TextureImporterFormat GetFormat(string atlasName, BuildTarget target)
        {
            if (list == null)
            {
                Init();
            }
            return list == null ? TextureImporterFormat.RGBA32 : list.GetTextureFormat(atlasName, target);

        }

        void OnPostprocessTexture(Texture2D texture)
        {
            string AtlasName = new DirectoryInfo(Path.GetDirectoryName(assetPath)).Name;
            if (assetPath.StartsWith(PathConfig.localFullPath)/* && AtlasName.StartsWith("Atlas")*/)
            {
                TextureImporter textureImporter = assetImporter as TextureImporter;
                textureImporter.spritePackingTag = GetPackingTag(AtlasName);
                ApplyTexture(textureImporter, AtlasName);
            }
        }
        public static string GetPackingTag(string AtlasName)
        {
            if (list == null)
                Init();
            if (list != null && list.IsTight(AtlasName))
                return "[TIGHT]" + AtlasName;
            return AtlasName;
        }
        public static void OnPostprocessAllAssets(string[] importedAsset, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            return;
            if (importedAsset != null)
            {
                List<string> list = ListPool<string>.Get();
                foreach (var asset in importedAsset)
                {
                    var name = asset.Replace('\\', '/');
                    name = name.Substring(name.LastIndexOf('/'));
                    if (name.Contains(" ") && !name.Contains("Assets/External"))
                    {
                        list.Add(asset);
                    }
                }

                if (list.Count > 0)
                {
                    var strBuilder = new StringBuilder();
                    strBuilder.Append("以下文件名包含空格：\n");
                    foreach (var item in list)
                        strBuilder.Append(item).Append("\n");
                    EditorUtility.DisplayDialog("警告！", strBuilder.ToString(), "OK");
                }
            }
        }
        public static void ApplyTexture(TextureImporter textureImporter, string atlasName)
        {

            textureImporter.wrapMode = TextureWrapMode.Clamp;
            if (textureImporter.spriteImportMode == SpriteImportMode.None)
                textureImporter.spriteImportMode = SpriteImportMode.Single;
            var wrapmode = textureImporter.wrapMode;
            textureImporter.textureType = TextureImporterType.Sprite;
            var iosFormat = GetFormat(atlasName, BuildTarget.iOS);
            var androidFormat = GetFormat(atlasName, BuildTarget.Android);
            var standaloneFormat = GetFormat(atlasName, BuildTarget.StandaloneWindows);
            int textureSize = MAX_TEXTURE_SIZE;
            var curIOSFormat = iosFormat;
            var curAndroidFormat = androidFormat;
            var curStandAloneFormat = standaloneFormat;
            textureImporter.GetPlatformTextureSettings("iPhone", out textureSize, out curIOSFormat);
            bool needReimport = false;
            if (textureSize != MAX_TEXTURE_SIZE || curIOSFormat != iosFormat)
                needReimport = true;
            textureImporter.GetPlatformTextureSettings("Android", out textureSize, out curAndroidFormat);
            if (textureSize != MAX_TEXTURE_SIZE || curAndroidFormat != androidFormat)
                needReimport = true;
            if (textureSize != MAX_TEXTURE_SIZE || curStandAloneFormat != standaloneFormat)
                needReimport = true;

            textureImporter.GetPlatformTextureSettings("Standalone", out textureSize, out curAndroidFormat);
            textureImporter.SetPlatformTextureSettings("Standalone", 4096, standaloneFormat);
            textureImporter.SetPlatformTextureSettings("iPhone", 4096, iosFormat);
            textureImporter.SetPlatformTextureSettings("Android", 4096, androidFormat, 0, false);
            textureImporter.mipmapEnabled = false;
            textureImporter.wrapMode = wrapmode;
            if (needReimport)
                textureImporter.SaveAndReimport();
        }

        public static void ReApplyAllSprites()
        {
            int current = -1;
            int count = list.list.Count;
            for (int i = 0; i < count; ++i)
            {
                var atlasPath = list.list[i].atlasName;
                EditorUtility.DisplayProgressBar("process", atlasPath, (float)i / count);
                var sprites = AssetDatabase.FindAssets("t:Sprite", new string[] { PathConfig.localFullPath + atlasPath });
                foreach (var id in sprites)
                {
                    current++;
                    var path = AssetDatabase.GUIDToAssetPath(id);
                    var im = AssetImporter.GetAtPath(path) as TextureImporter;

                    ApplyTexture(im, atlasPath);
                }
            }

            EditorUtility.ClearProgressBar();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

    public class PackAtlas : EditorWindow
    {
        [MenuItem(EditorConfig.MENU_ROOT + "UI/PackAtlas", false, 100)]
        public static void Open()
        {
            GetWindow<PackAtlas>().Show();
        }

        static void Save()
        {
            AtlasPackerHelper.list.list = AtlasPackerHelper.list.dict.Values.ToListFromPool();
            File.WriteAllText(AtlasPackerHelper.ATLAS_TEXTURE_FORMAT_CONFIG_PATH, JsonUtility.ToJson(AtlasPackerHelper.list));
            AtlasPackerHelper.Init();
        }

        static void Read()
        {
            AtlasPackerHelper.Init();
            if (AtlasPackerHelper.list == null)
            {
                AtlasPackerHelper.list = new AtlasTextureFormatList();
            }
            var dirs = Directory.GetDirectories(PathConfig.localFullPath2);

            for (int i = 0; i < dirs.Length; ++i)
            {
                dirs[i] = dirs[i].Replace('\\', '/');
                dirs[i] = dirs[i].Substring(dirs[i].LastIndexOf('/') + 1);
            }
            if (AtlasPackerHelper.list.list != null)
            {
                foreach (var item in AtlasPackerHelper.list.list)
                {
                    if (dirs.First(t => t == item.atlasName) == null)
                    {
                        AtlasPackerHelper.list.dict.Remove(item.atlasName);
                    }
                }
            }

            Save();
            foreach (var dir in dirs)
            {
                if (AtlasPackerHelper.list.dict.ContainsKey(dir))
                    continue;
                var item = new AtlasTextureFormatItem();
                item.atlasName = dir;
                item.iosFormat = item.androidFormat = item.standaloneFormat = TextureImporterFormat.RGBA32.ToString();
                AtlasPackerHelper.list.dict.Add(dir, item);
            }
        }

        void Awake()
        {
            Init();
        }
        static void Init()
        {
            Read();
            Save();
        }
        BuildTarget target;
        Vector2 pos = Vector2.zero;
        void OnGUI()
        {
            if (AtlasPackerHelper.list == null || AtlasPackerHelper.list.list == null)
                Init();
            if ((int)target == 0)
                target = EditorUserBuildSettings.activeBuildTarget;
            target = (BuildTarget)EditorGUILayout.EnumPopup("PlatForm", target);
            EditorGUILayout.LabelField("IOS, Android Or PC!!!!");
            pos = EditorGUILayout.BeginScrollView(pos);
            for (int i = 0; i < AtlasPackerHelper.list.list.Count; ++i)
            {
                var item = AtlasPackerHelper.list.list[i];
                EditorGUILayout.BeginHorizontal();
                item.SetTextureFormat((TextureImporterFormat)EditorGUILayout.EnumPopup(item.atlasName, item.GetTextureFormat(target)), target);
                item.isTight = EditorGUILayout.ToggleLeft("Tight Policy", item.isTight, GUILayout.Width(100f));
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();
            EditorGUILayout.Space();
            if (GUILayout.Button("Save"))
                Save();
            EditorGUILayout.Space();
            if (GUILayout.Button("Restore"))
                Init();
            EditorGUILayout.Space();
            if (GUILayout.Button("Pack"))
                Pack();


        }

        public static void Pack()
        {
            Init();
            AtlasPackerHelper.ReApplyAllSprites();
            CreateAtlasSpriteHolder();
            Packer.RebuildAtlasCacheIfNeeded(EditorUserBuildSettings.activeBuildTarget, true, Packer.Execution.ForceRegroup);

            EditorUtility.ClearProgressBar();
        }

        private static void CreateAtlasSpriteHolder()
        {
            string path = Application.dataPath + PathConfig.localPath;
            string[] directories = Directory.GetDirectories(path);
            Array.Sort(directories);
            System.Collections.Generic.List<string> spriteList = new System.Collections.Generic.List<string>();
            List<string> atlasNames = new List<string>();
            Dictionary<string, string> spriteNameSet = new Dictionary<string, string>();
            for (int i = 0; i < directories.Length; i++)
            {
                EditorUtility.DisplayProgressBar("打包图集", "打包中...", 1f * i / directories.Length);

                string directoryPath = directories[i];
                directoryPath = directoryPath.Replace("\\", "/");
                string directory = directoryPath.Substring(directoryPath.LastIndexOf('/'));
                string AtlasName = Path.GetFileName(directoryPath);
                string[] files = Directory.GetFiles(directoryPath, "*.png");
                GameObject holder = new GameObject(AtlasName);
                SpriteHolder spriteHolder = holder.AddComponent<SpriteHolder>();
                spriteHolder.allSprites = new Sprite[files.Length];
                var oldGo = AssetDatabase.LoadAssetAtPath<GameObject>(PathConfig.atlasPath + AtlasName + ".prefab");
                SpriteHolder oldHolder = null;
                if (oldGo != null)
                    oldHolder = oldGo.GetComponent<SpriteHolder>();
                Array.Sort(files);
                spriteHolder.atlasName = AtlasName;
                atlasNames.Add(AtlasName);
                for (int k = 0; k < files.Length; k++)
                {
                    string filePath = files[k];
                    filePath = filePath.Replace('\\', '/');
                    int start = filePath.LastIndexOf(directory + '/') + 1;
                    string fileName = filePath.Substring(start);
                    //AssetDatabase.ImportAsset(PathConfig.localFullPath + fileName);

                    var assetImporter = AssetImporter.GetAtPath(PathConfig.localFullPath + fileName) as TextureImporter;
                    var packingTag = AtlasPackerHelper.GetPackingTag(AtlasName);

                    if (assetImporter != null && assetImporter.spritePackingTag != packingTag)
                    {
                        assetImporter.spritePackingTag = packingTag;
                        AtlasPackerHelper.ApplyTexture(assetImporter, AtlasName);
                        assetImporter.SaveAndReimport();
                    }

                    UnityEngine.Object[] objects = AssetDatabase.LoadAllAssetsAtPath(PathConfig.localFullPath + fileName);
                    foreach (var obj in objects)
                    {
                        var sprite = obj as Sprite;
                        if (sprite == null)
                        {
                            if (!(obj is Texture2D))
                                Debug.LogError(fileName + "is null");
                            continue;
                        }
                        var guid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(sprite));
                        spriteHolder.allSprites[k] = sprite;
                        start = fileName.LastIndexOf("/") + 1;
                        //fileName = sprite.name;// fileName.Substring(start, fileName.Length - start - ".png".Length);
                        if (spriteNameSet.ContainsKey(sprite.name))
                        {
                            Debug.LogErrorFormat("Sprite Name \"{0}\" Conflict in Atlas \"{1}\" and \"{2}\"", sprite.name, spriteNameSet[sprite.name], AtlasName);
                        }
                        else
                        {
                            spriteNameSet.Add(sprite.name, AtlasName);
                        }
                        spriteList.Add(guid + "." + sprite.name + "." + AtlasName);
                    }
                }
                bool changed = !(oldHolder != null && oldHolder.allSprites != null && oldHolder.allSprites.Length == spriteHolder.allSprites.Length && oldHolder.atlasName == spriteHolder.atlasName);
                if (!changed)
                {
                    for (int j = 0; j < oldHolder.allSprites.Length; ++j)
                    {
                        if (oldHolder.allSprites[j] != spriteHolder.allSprites[j])
                        {
                            changed = true;
                            break;
                        }
                    }
                }
                if (changed)
                    PrefabUtility.CreatePrefab(PathConfig.atlasPath + AtlasName + ".prefab", holder);
                DestroyImmediate(holder);

            }

            EditorUtility.DisplayProgressBar("打包图集", "打包中...", 1);

            GameObject go = new GameObject("SpritePath");
            TexturePathsHolder textureHolder = go.AddComponent<TexturePathsHolder>();
            textureHolder.files = new string[spriteList.Count];
            textureHolder.paths = new string[directories.Length];
            for (int i = 0; i < directories.Length; i++)
            {
                var holderName = directories[i].Substring(directories[i].IndexOf("ResourcesUI/") + "ResourcesUI/".Length);
                textureHolder.paths[i] = "Atlas/" + holderName;
            }
            for (int i = 0; i < spriteList.Count; ++i)
            {
                textureHolder.files[i] = spriteList[i];
            }
            var oldPaths = AssetDatabase.LoadAssetAtPath<GameObject>(PathConfig.atlasPath + "SpritePath" + ".prefab");
            TexturePathsHolder oldPathsHolder = null;
            if (oldPaths != null)
                oldPathsHolder = oldPaths.GetComponent<TexturePathsHolder>();
            bool pathChanged = oldPathsHolder == null
                || oldPathsHolder.files == null || oldPathsHolder.files.Length != textureHolder.files.Length
                || oldPathsHolder.paths == null || oldPathsHolder.paths.Length != textureHolder.paths.Length;

            if (!pathChanged)
            {
                for (int j = 0; j < oldPathsHolder.files.Length; ++j)
                {
                    if (oldPathsHolder.files[j] != textureHolder.files[j])
                    {
                        pathChanged = true;
                        break;
                    }
                }

                for (int i = 0; i < oldPathsHolder.paths.Length; i++)
                {
                    if (oldPathsHolder.paths[i] != textureHolder.paths[i])
                    {
                        pathChanged = true;
                        break;
                    }
                }
            }
            if (pathChanged)
                PrefabUtility.CreatePrefab(PathConfig.atlasPath + "SpritePath" + ".prefab", go);
            DestroyImmediate(go);
            AssetDatabase.SaveAssets();
            // Hotfire.SpriteProxy.instance.InitForEditor();
        }
    }
}
