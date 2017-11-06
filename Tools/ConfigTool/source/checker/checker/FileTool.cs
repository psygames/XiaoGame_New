using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace checker
{
    class FileTool
    {
        public static string ReadText(String path, Encoding encode)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            StreamReader sr = new StreamReader(fs, encode);
            string text = sr.ReadToEnd();
            sr.Close();
            return text;
        }

        public static string ReadText(string path)
        {
            return ReadText(path, Encoding.UTF8);
        }
    }
}
