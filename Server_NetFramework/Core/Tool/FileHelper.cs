using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Core
{
    public class FileHelper
    {
        public static void ClearFolder(string folderPath, string searchPattern = "", string[] except = null)
        {
            DirectoryInfo di = new DirectoryInfo(folderPath);
            if (di.Exists)
            {
                FileInfo[] fi = di.GetFiles(searchPattern);
                for (int i = 0; i < fi.Length; i++)
                {
                    string ss = except.First((s) => { return fi[i].Name.Contains(s); });
                    if (ss == null)
                        fi[i].Delete();
                }
            }
        }

        public static string ReadText(String path)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            string text = sr.ReadToEnd();
            sr.Close();
            return text;
        }


        public static bool WriteText(string content, string path, bool append = false)
        {
            if (!File.Exists(path))
            {
                string dir = Path.GetDirectoryName(path);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                File.Create(path).Close();
            }

            if (!append)
            {
                StreamReader sr = new StreamReader(path, Encoding.UTF8);
                string oldContent = sr.ReadToEnd();
                sr.Close();
                if (oldContent.Equals(content))
                {
                    return false;
                }
            }

            StreamWriter sw = new StreamWriter(path, append, Encoding.UTF8);
            sw.Write(content);
            sw.Close();
            return true;
        }
    }
}
