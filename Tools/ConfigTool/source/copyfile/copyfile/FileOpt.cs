using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Tools
{
    class FileOpt
    {
        public static void ClearFolder(string folderPath, string searchPattern = "", string[] except = null)
        {
            DirectoryInfo di = new DirectoryInfo(folderPath);
            if (di.Exists)
            {
                FileInfo[] fi = di.GetFiles(searchPattern);
                for (int i = 0; i < fi.Length; i++)
                {
                    if (except != null)
                    {
                        string ss = except.FirstOrDefault((s) => { return fi[i].Name.Contains(s); });
                        if (ss != null)
                            continue;
                    }
                    fi[i].Delete();
                }
            }

            Console.WriteLine("clear folder succeed :" + folderPath);
        }

        public static string ReadText(String path)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            string text = sr.ReadToEnd();
            sr.Close();
            return text;
        }


        public static bool WriteText(string path, string content)
        {
            if (!File.Exists(path))
            {
                if (!Directory.Exists(Path.GetDirectoryName(path)))
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                File.Create(path).Close();
            }
            StreamReader sr = new StreamReader(path, new UTF8Encoding(false));
            string oldContent = sr.ReadToEnd();
            sr.Close();
            if (oldContent.Equals(content))
            {
                return false;
            }

            if (File.Exists(path))
            {
                File.Delete(path);
            }
            File.Create(path).Close();

            StreamWriter sw = new StreamWriter(path, false, new UTF8Encoding(false));
            sw.Write(content);
            sw.Close();
            return true;
        }
    }
}
