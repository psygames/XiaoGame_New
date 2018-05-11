using System;
using IniParser.Parser;
using Core;
using IniParser.Model;

namespace RedStone
{
    public static class NetConfig
    {
        public static string LISTENER_IP { get; private set; }
        public static int LISTENER_PORT { get; private set; }

        public static string SERVER_IP { get; private set; }
        public static int SERVER_PORT { get; private set; }

        public static void Init()
        {
            IniDataParser parser = new IniDataParser();
            IniData data = parser.Parse(FileHelper.ReadText("Resource/Config.ini"));

            LISTENER_IP = data.Sections["Network"].GetKeyData("LISTENER_IP").Value;
            LISTENER_PORT = int.Parse(data.Sections["Network"].GetKeyData("LISTENER_PORT").Value);
            SERVER_IP = data.Sections["Network"].GetKeyData("SERVER_IP").Value;
            SERVER_PORT = int.Parse(data.Sections["Network"].GetKeyData("SERVER_PORT").Value);
        }
    }
}
