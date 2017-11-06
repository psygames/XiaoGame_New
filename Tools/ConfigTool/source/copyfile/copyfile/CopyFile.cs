using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace copyfile
{
    class CopyFile
    {
        private static string ReadTextFromFile(string path)
        {
            if (!File.Exists(path))
            {
                return "";
            }
            return Tools.FileOpt.ReadText(path);
        }

        /// <summary>
        /// 清空文件下的所有子文件
        /// </summary>
        private void ClearFolder(string folderPath)
        {
            Console.WriteLine("清空目录 ==> " + folderPath);
            DirectoryInfo di = new DirectoryInfo(folderPath);
            if (di.Exists)
            {
                FileInfo[] fi = di.GetFiles("*.xml");
                for (int i = 0; i < fi.Length; i++)
                {
                    fi[i].Delete();
                }
            }

            Console.WriteLine("clear folder succeed :" + folderPath);
        }

        public bool CopyStart(string xmlListFile, string xmlSrcDir, string[] xmlDestDir, string extend)
        {
            foreach (var dest in xmlDestDir)
            {
                bool res = CopyStart(xmlListFile, xmlSrcDir, dest, extend);
                if (!res) return false;
            }
            return true;
        }

        public bool CopyStart(string xmlListFile, string xmlSrcDir, string xmlDestDir, string extend)
        {
            ClearFolder(xmlDestDir);
            string title = extend;
            string[] filenames = GetPropertiesList(xmlListFile, title);
            foreach (string filename in filenames)
            {
                string src = xmlSrcDir + "/" + filename + ".xml";
                string dest = xmlDestDir + "/" + filename + ".xml";
                if (!compare(src, dest))
                {
                    if (copy(src, dest))
                    {
                        Console.WriteLine("复制文件成功：" + title + "   " + filename);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool CopyStartLanguage(string srcDir, string destDir, string languageUpdateDir)
        {
            ClearFolder(destDir);
            DirectoryInfo di = new DirectoryInfo(srcDir);
            foreach (FileInfo fi in di.GetFiles("*.xml"))
            {
                string src = fi.FullName;
                string fileName = Path.GetFileName(src);
                string dest = destDir + "/" + fileName;
                if (fileName.Contains(SimplifyLanguage.UPDATE_SUFFIX))
                    dest = languageUpdateDir + "/" + fileName.Substring(0, fileName.LastIndexOf(SimplifyLanguage.UPDATE_SUFFIX)) + ".xml";
                if (!compare(src, dest))
                {
                    if (copy(src, dest))
                    {
                        Console.WriteLine("复制Language文件成功：" + dest);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool compare(string pathSrc, string pathDest)
        {
            string src = ReadTextFromFile(pathSrc);
            string dest = ReadTextFromFile(pathDest);
            if (src.Equals(dest))
                return true;
            return false;
        }

        public bool copy(string pathSrc, string pathDest)
        {
            try
            {
                File.Copy(pathSrc, pathDest, true);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }

        public static string[] GetPropertiesList(string filePath, string fileType = null)
        {
            string text = ReadTextFromFile(filePath);
            text = text.Replace("\r", "");
            string[] lists = text.Split('\n');
            int index = 0;
            while (fileType != null && lists[index].Trim('[', ']') != fileType)
                index++;

            List<string> result = new List<string>();

            for (int i = index + 1; i < lists.Length; i++)
            {
                if (string.IsNullOrEmpty(lists[i]) || result.Contains(lists[i]))
                    continue;
                if (fileType != null && lists[i].Contains("["))
                {
                    break;
                }
                result.Add(lists[i]);
            }
            return result.ToArray();
        }
    }
}
