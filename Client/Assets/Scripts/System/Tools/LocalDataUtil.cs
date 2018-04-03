using UnityEngine;
using System.Collections;
using Newtonsoft.Json;

namespace RedStone
{
    public static class LocalDataUtil
    {
        public static void Set(string category, int value)
        {
            PlayerPrefs.SetInt(category, value);
        }

        public static void Set(string category, string value)
        {
            PlayerPrefs.SetString(category, value);
        }

        public static void Set(string category, long value)
        {
            PlayerPrefs.SetString(category, value.ToString());
        }

        public static void Set(string category, float value)
        {
            PlayerPrefs.SetFloat(category, value);
        }

        public static void Set(string category, bool value)
        {
            PlayerPrefs.SetInt(category, value ? 1 : 0);
        }

        public static int Get(string category, int defaultValue)
        {
            return PlayerPrefs.GetInt(category, defaultValue);
        }
        public static long Get(string category, long defaultValue)
        {
            long ret = 0;
            if(long.TryParse(PlayerPrefs.GetString(category, ""), out ret))
            {
                return ret;
            }
            return defaultValue;
        }

        public static Vector2 Get(string name, Vector2 defaultPos)
        {
            string value = PlayerPrefs.GetString(name);
            return TableManager.ParseVector2(value);
        }
        public static void Set(string name, Vector2 pos)
        {
            PlayerPrefs.SetString(name, pos.ToString());
        }
        public static bool Get(string category, bool defaultValue)
        {
            return PlayerPrefs.GetInt(category, defaultValue ? 1 : 0) == 1 ? true : false;
        }

        public static float Get(string category, float defaultValue)
        {
            return PlayerPrefs.GetFloat(category, defaultValue);
        }
        public static string Get(string category, string defaultValue)
        {
            return PlayerPrefs.GetString(category, defaultValue);
        }

        public static bool Exists(string category)
        {
            return PlayerPrefs.HasKey(category);
        }
        public static void Delete(string category)
        {
            PlayerPrefs.DeleteKey(category);
        }
        public static bool ExistsFile(string category)
        {
            var path = FileHelper.GetPersistPath(System.IO.Path.Combine("LocalData", category));
            return (FileHelper.FileExists(path));
        }
        public static void DeleteFile(string category)
        {
            var path = System.IO.Path.Combine("LocalData", category);
            System.IO.File.Delete(path);
            
        }
        public static void Set<T>(string category, T obj)
            where T : class
        {
            var path = System.IO.Path.Combine("LocalData", category);
            FileHelper.WritePersistTextFile(path, ToJson(obj));
        }
        public static string ToJson<T>(T obj, bool pretty = false)
        {
            return JsonConvert.SerializeObject(obj, pretty ? Formatting.Indented : Formatting.None);
        }
        public static T FromJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
        public static T Get<T>(string category)
            where T : class
        {
            var path = FileHelper.GetPersistPath(System.IO.Path.Combine("LocalData" , category));
            if(FileHelper.FileExists(path))
                return FromJson<T>(FileHelper.ReadTextFile(path));
            else
                return default(T);
        }
    }
}
