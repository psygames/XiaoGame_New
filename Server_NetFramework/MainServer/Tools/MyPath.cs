using System;
namespace RedStone
{
    public class MyPath
    {
        public const string RES = "Resource/";

        public static string PROJECT
        {
            get { return RES + "/../../"; }
        }

        public static string RES_UI
        {
            get { return "Prefabs/UI/"; }
        }

        public static string RES_PROTO_NUM
        {
            get { return RES + "protonum.txt"; }
        }
    }
}
