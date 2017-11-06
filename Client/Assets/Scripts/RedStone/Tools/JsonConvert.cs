using System;
using UnityEngine;

namespace Hotfire
{
    class JsonConvert
    {
        public static Vector3 JsonArrayToVector3(SimpleJSON.JSONArray jarray)
        {
            float x = jarray[0]["x"].AsFloat;
            float y = jarray[1]["y"].AsFloat;
            float z = jarray[2]["z"].AsFloat;
            return new Vector3(x, y, z);
        }

        public static Vector2 JsonArrayToVector2(SimpleJSON.JSONArray jarray)
        {
            float x = jarray[0]["x"].AsFloat;
            float y = jarray[1]["y"].AsFloat;
            return new Vector3(x, y);
        }

        public static SimpleJSON.JSONArray Vector3ToJsonArray(Vector3 vec)
        {
            SimpleJSON.JSONArray array = new SimpleJSON.JSONArray();
            array.Add(SimpleJSON.JSONNode.Parse("{x:" + vec.x + "}"));
            array.Add(SimpleJSON.JSONNode.Parse("{y:" + vec.y + "}"));
            array.Add(SimpleJSON.JSONNode.Parse("{z:" + vec.z + "}"));
            return array;
        }

        public static SimpleJSON.JSONArray Vector2ToJsonArray(Vector2 vec)
        {
            SimpleJSON.JSONArray array = new SimpleJSON.JSONArray();
            array.Add(SimpleJSON.JSONNode.Parse("{x:" + vec.x + "}"));
            array.Add(SimpleJSON.JSONNode.Parse("{y:" + vec.y + "}"));
            return array;
        }
    }
}
