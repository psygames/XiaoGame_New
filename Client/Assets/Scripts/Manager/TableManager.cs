using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Reflection;
using System.Xml;
using Mono.Xml;
 
using System.Security;

namespace RedStone
{
    public sealed class TableManager : Core.Singleton<TableManager>
    {
#if UNITY_EDITOR
        public static void CreateAndLoad()
        {
            if (instance == null)
                CreateInstance();

            instance.InitSync();
        }

        public void InitSync()
        {
            LoadAllTableSync();
            Debug.Log("Init Table");
        }

        private void LoadAllTableSync()
        {
            m_tables.Clear();
            List<string> lists = LoadTableNameListSync(TABLE_LIST_FILE_PATH, TABLE_FILETER_TYPE);
            foreach (string filename in lists)
            {
                Type type = Type.GetType("RedStone." + filename);
                string path = TABLE_FOLDER_PATH + filename;
                TextAsset textAsset = ResourceManager.instance.GetResourceByPath(path) as TextAsset;
                IDictionary dict = LoadOneTable(type, textAsset);
                if (dict != null)
                    m_tables.Add(type, dict);
            }
        }
#endif
        private Dictionary<Type, IDictionary> m_tables = new Dictionary<Type, IDictionary>();
        private Dictionary<Type, IDictionary> m_cachedTables4All = new Dictionary<Type, IDictionary>();

        #region GetValue
        public T GetData<T>(Func<T, bool> predicate)
        {
            Dictionary<int, T> table = GetAllData<int, T>();
            return table.Values.First<T>(predicate);
        }

        public T GetData<T>(int id)
        {
            return GetData<int, T>(id);
        }

        public T GetData<T>(string id)
        {
            return GetData<string, T>(id);
        }

        private TValue GetData<TKey, TValue>(TKey id)
        {
            Type type = typeof(TValue);
            IDictionary dict = m_tables[type];
            Dictionary<TKey, object> table = m_tables[type] as Dictionary<TKey, object>;
            if (table != null)
            {
                object value;
                if (table.TryGetValue(id, out value))
                    return (TValue)value;
            }
            return default(TValue);
        }

        public Dictionary<TKey, TValue> GetAllData<TKey, TValue>()
        {
            Type type = typeof(TValue);
            IDictionary dict = null;
            if (m_cachedTables4All.TryGetValue(type, out dict))
            {

            }
            else
            {
                Dictionary<TKey, object> table = m_tables[type] as Dictionary<TKey, object>;
                dict = new Dictionary<TKey, TValue>();
                var itr = table.GetEnumerator();
                while (itr.MoveNext())
                {
                    dict.Add(itr.Current.Key, itr.Current.Value);
                }
                m_cachedTables4All.Add(type, dict);
            }
            return m_cachedTables4All[typeof(TValue)] as Dictionary<TKey, TValue>;
        }

        public Dictionary<int, TValue> GetAllData<TValue>()
        {
            return GetAllData<int, TValue>();
        }
        #endregion

        public void Init()
        {
            LoadAllTable();
        }

        public const string TABLE_FOLDER_PATH = "xml/";
        public const string TABLE_LIST_FILE_PATH = "xmlList";
        public const string TABLE_FILETER_TYPE = "[ForCSharp]";
        public const string TABLE_NAME_SPACE = "Hotfire";
        private bool m_tableLoaded = false;
        private void LoadAllTable()
        {
            LoadTableNameList(TABLE_LIST_FILE_PATH, TABLE_FILETER_TYPE, (List<string> lists) =>
            {
                List<string> xmlPath = new List<string>();
                Dictionary<string, Type> typeMap = new Dictionary<string, Type>();
                foreach (string filename in lists)
                {
                    Type type = Type.GetType("RedStone." + filename);
                    xmlPath.Add(TABLE_FOLDER_PATH + filename);
                    typeMap.Add(TABLE_FOLDER_PATH + filename, type);
                }
                ResourceManager.instance.GetResourceByPathAsync(xmlPath, (Dictionary<string, AsyncResource> ar) =>
                {
                    foreach (var key in ar.Keys)
                    {
                        IDictionary dict = LoadOneTable(typeMap[key], (TextAsset)(ar[key].loadedAsset));
                        if (dict != null)
                            m_tables.Add(typeMap[key], dict);
                        ResourceManager.instance.UnloadResouce(key, false);
                    }
                    m_tableLoaded = true;
                });
            });
        }

        private List<string> LoadTableNameListSync(string filePath, string fileType)
        {
            UnityEngine.Object res = ResourceManager.instance.GetResourceByPath(filePath);
            TextAsset textAsset = res as TextAsset;
            return GetTableNameList(textAsset, fileType);
        }

        private List<string> GetTableNameList(TextAsset textAsset, string fileType)
        {
            string text = textAsset.text;
            text = text.Replace("\r", "");
            string[] lists = text.Split('\n');
            int index = 0;
            while (!lists[index].Contains(fileType))
                index++;

            List<string> result = new List<string>();

            for (int i = index + 1; i < lists.Length; i++)
            {
                if (String.IsNullOrEmpty(lists[i]))
                    continue;
                if (lists[i].Contains("["))
                {
                    break;
                }
                result.Add(lists[i]);
            }
            return result;
        }

        public bool IsReady()
        {
            return m_tableLoaded;
        }


        private void LoadTableNameList(string filePath, string fileType, Action<List<string>> onLoaded)
        {
            ResourceManager.instance.GetResourceByPathAsync(filePath, (AsyncResource ar) =>
            {
                List<string> result = GetTableNameList(ar.loadedAsset as TextAsset, fileType);
                onLoaded(result);
            });
        }

        public IDictionary LoadOneTable(Type tableType, TextAsset texAsset)
        {
            if (texAsset == null)
            {
                Debug.LogError(tableType + "(.xml) File Not Exist!!!");
                return null;
            }
            string content = LoadTableContent(texAsset);
            return LoadOneTableByContent(tableType, content);
        }

        private string LoadTableContent(TextAsset texAsset)
        {
            return texAsset.text;
        }

        private IDictionary LoadOneTableByContent(Type tableType, string content)
        {
            return LoadTableWithSecurityParser(tableType, content);
        }

        private const string TAG_TYPE = "type";
        private const string TAG_DATA = "data";
        private const string TAG_ITEM = "item";
        private IDictionary LoadTableWithSecurityParser(Type tableType, string content)
        {
            IDictionary dict = null;
            Hashtable typeMap = null;
            SecurityParser parser = new SecurityParser();
            parser.LoadXml(content);
            SecurityElement element = parser.ToXml();
            for (int i = 0; i < element.Children.Count; i++)
            {
                SecurityElement subElement = element.Children[i] as SecurityElement;
                if (subElement.Tag == TAG_TYPE)
                {
                    #region type line
                    SecurityElement item = subElement.Children[0] as SecurityElement;
                    if (item.Tag == TAG_ITEM)
                    {
                        typeMap = item.Attributes;
                        if (typeMap["id"].ToString() == "int")
                        {
                            dict = new Dictionary<int, object>();
                        }
                        else if (typeMap["id"].ToString() == "string")
                        {
                            dict = new Dictionary<string, object>();
                        }
                        else
                        {
                            Debug.LogError("wrong ID type : " + typeMap["id"]);
                        }
                    }
                    else
                    {
                        Debug.LogError("The TAG " + item.Tag + " is not a valid Tag");
                    }
                    #endregion
                }
                else if (subElement.Tag == TAG_DATA)
                {
                    for (int j = 0; subElement.Children != null && j < subElement.Children.Count; j++)
                    {
                        SecurityElement item = subElement.Children[j] as SecurityElement;
                        if (item.Tag == TAG_ITEM)
                        {
                            Hashtable hashTable = item.Attributes;
                            Hashtable paramMap = new Hashtable();
                            var itr = hashTable.GetEnumerator();
                            while (itr.MoveNext())
                            {
                                string key = itr.Key.ToString();
                                string value = itr.Value.ToString();
                                string type = typeMap[key].ToString();
                                paramMap.Add(key, ParseValue(type, value));
                            }
                            object obj = Activator.CreateInstance(tableType, paramMap);
                            object id = ParseValue(typeMap["id"].ToString(), hashTable["id"].ToString());
                            if (dict.Contains(id))
                                Debug.LogError("Table has the repeat ID : " + tableType + " --> " + id);
                            else
                                dict.Add(id, obj);
                        }
                        else
                        {
                            Debug.LogError("The TAG " + item.Tag + " is not a valid Tag");
                        }
                    }
                }
                else
                {
                    Debug.LogError("The TAG " + subElement.Tag + " is not a valid Tag");
                }
            }
            return dict;
        }

        public static object ParseValue(string type, string value)
        {
            object obj = null;
            switch (type)
            {
                case "float":
                    obj = float.Parse(value);
                    break;
                case "int":
                    obj = int.Parse(value);
                    break;
                case "string":
                    obj = value;
                    break;
                case "bool":
                    obj = bool.Parse(value);
                    break;
                case "Vector2":
                    obj = ParseVector2(value);
                    break;
                case "Vector3":
                    obj = ParseVector3(value);
                    break;
                case "Color":
                    obj = ParseColor(value);
                    break;
                case "Date":
                    obj = ParseDate(value);
                    break;
                case "bool[]":
                    obj = ParseArray<bool>(value);
                    break;
                case "string[]":
                    obj = ParseArray<string>(value);
                    break;
                case "int[]":
                    obj = ParseArray<int>(value);
                    break;
                case "float[]":
                    obj = ParseArray<float>(value);
                    break;
                case "Vector3[]":
                    obj = ParseArray<Vector3>(value);
                    break;
                case "Color[]":
                    obj = ParseArray<Color>(value);
                    break;
                case "Date[]":
                    obj = ParseArray<DateTime>(value);
                    break;
                default:
                    throw new ArgumentException("参数类型错误：" + type);
            }
            return obj;
        }

        public static Vector2 ParseVector2(string sv2)
        {
            sv2 = sv2.Trim('(');
            sv2 = sv2.Trim(')');
            Vector3 vec = new Vector3();
            string[] sv2s = sv2.Split(',');
            vec.x = float.Parse(sv2s[0]);
            vec.y = float.Parse(sv2s[1]);
            return vec;
        }

        public static Vector3 ParseVector3(string sv3)
        {
            sv3 = sv3.Trim('(');
            sv3 = sv3.Trim(')');
            Vector3 vec = new Vector3();
            string[] sv3s = sv3.Split(',');
            vec.x = float.Parse(sv3s[0]);
            vec.y = float.Parse(sv3s[1]);
            vec.z = float.Parse(sv3s[2]);
            return vec;
        }

        public static Color ParseColor(string color)
        {
            color = color.Trim('#');
            float max = 255;
            Color c = new Color();
            if (color.Length == 8)
            {
                c.a = Int32.Parse(color.Substring(6, 2), System.Globalization.NumberStyles.HexNumber) / max;
            }
            else if (color.Length == 6)
            {
                c.a = 1;
            }
            else
                Debug.LogError("Font Color Error : " + color);
            c.r = Int32.Parse(color.Substring(0, 2), System.Globalization.NumberStyles.HexNumber) / max;
            c.g = Int32.Parse(color.Substring(2, 2), System.Globalization.NumberStyles.HexNumber) / max;
            c.b = Int32.Parse(color.Substring(4, 2), System.Globalization.NumberStyles.HexNumber) / max;
            return c;
        }

        public static DateTime ParseDate(string date)
        {
            DateTime dt;
            if (!DateTime.TryParse(date, out dt))
            {
                Debug.LogError("Date Parse Error : " + date);
            }
            return dt;
        }

        public static T[] ParseArray<T>(string val)
        {
            bool isClass = false;
            if (val.Contains("("))
                isClass = true;
            val = val.Replace("[", "");
            val = val.Replace("]", "");
            val = val.Replace("),", ")|");
            val = val.Replace("(", "");
            val = val.Replace(")", "");
            val = val.Trim(' ');

            char splitTag = ',';
            if (isClass) splitTag = '|';
            string[] arrays = { };
            if (!string.IsNullOrEmpty(val))
                arrays = val.Split(splitTag);
            T[] tArray = new T[arrays.Length];

            string type = typeof(T).Name;

            for (int i = 0; i < arrays.Length; i++)
            {
                switch (type)
                {
                    case "Single":
                        tArray[i] = (T)(object)float.Parse(arrays[i]);
                        break;
                    case "Int32":
                        tArray[i] = (T)(object)int.Parse(arrays[i]);
                        break;
                    case "String":
                        tArray[i] = (T)(object)arrays[i];
                        break;
                    case "Boolean":
                        tArray[i] = (T)(object)bool.Parse(arrays[i]);
                        break;
                    case "Vector2":
                        tArray[i] = (T)(object)ParseVector2(arrays[i]);
                        break;
                    case "Vector3":
                        tArray[i] = (T)(object)ParseVector3(arrays[i]);
                        break;
                    case "Color":
                        tArray[i] = (T)(object)ParseColor(arrays[i]);
                        break;
                    case "Date":
                        tArray[i] = (T)(object)ParseDate(arrays[i]);
                        break;
                    default:
                        throw new ArgumentException("数组中，参数类型错误：" + arrays[i]);
                }
            }
            return tArray;
        }
    }
}