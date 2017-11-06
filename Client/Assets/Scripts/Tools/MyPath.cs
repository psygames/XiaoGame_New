using System;
using UnityEngine;
namespace RedStone
{
    public class MyPath
    {
        public static string PROJECT
        {
            get { return Application.dataPath + "/../../"; }
        }

        public static string RES_UI
        {
            get { return "Prefabs/UI/"; }
        }

        public static string RES_PROTO_NUM
        {
            get { return "Config/protoNum"; }
        }
    }
}
