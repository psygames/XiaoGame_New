using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace copyfile
{
    class Program
    {
        static string xmlTmpSimplifyDir = "_t__e___m____p_____/";// 防止勿删文件
        static string xmlTmpSimplifyDir_Language = "_t__e___m____p_____1/";
        const string TOOLS_PATH = "../../../../../xmltools/";
        static void Main(string[] args)
        {
            try
            {

                string exePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
#if DEBUG
                exePath = TOOLS_PATH;
#endif

                IniFiles ini = new IniFiles(exePath + "config.ini");

                string xmlSrcDir = exePath + ini.ReadString("CopyFile", "XmlSrcDir");
                string xmlListFile = exePath + ini.ReadString("CopyFile", "XmlListFile");
                string destXmlListFile = exePath + ini.ReadString("CopyFile", "DestXmlListFile");
                string destXmlListFile1 = exePath + ini.ReadString("CopyFile", "DestXmlListFile1");
                string destXmlListFile2 = exePath + ini.ReadString("CopyFile", "DestXmlListFile2");

                string xmlDestDirClinet = exePath + ini.ReadString("CopyFile", "XmlDestDirClient");
                string xmlDestDirClinet1 = exePath + ini.ReadString("CopyFile", "XmlDestDirClient1");
                string xmlDestDirClinet2 = exePath + ini.ReadString("CopyFile", "XmlDestDirClient2");

                List<string> xmlServerDirs = new List<string>();
                foreach (var key in ini.ReadSection("CopyFile"))
                {
                    if (key.StartsWith("XmlDestDirServer"))
                    {
                        var dir = exePath + ini.ReadString("CopyFile", key);
                        xmlServerDirs.Add(dir);
                    }
                }

                string xmlDestDirBattleServer = exePath + ini.ReadString("CopyFile", "XmlDestDir-BattleServer");
                string xmlDestDirGameServer = exePath + ini.ReadString("CopyFile", "XmlDestDir-GameServer");
                string xmlDestDirBattleManager = exePath + ini.ReadString("CopyFile", "XmlDestDir-BattleManager");

                string languageSrcDir = exePath + ini.ReadString("CopyFile", "LanguageSrcDir");
                string languageDestDir = exePath + ini.ReadString("CopyFile", "LanguageDestDir");
                string languageUpdateDir = exePath + ini.ReadString("CopyFile", "LanguageUpdateDir");
                CopyFile cop = new CopyFile();
                Simplify sp = new Simplify();
                Simplify spLangs = new SimplifyLanguage();
                if (cop.copy(xmlListFile, destXmlListFile)
                    && cop.copy(xmlListFile, destXmlListFile1)
                    && cop.copy(xmlListFile, destXmlListFile2)
                    && sp.SimplifyStart(xmlSrcDir, xmlTmpSimplifyDir, xmlListFile)
                    && cop.CopyStart(xmlListFile, xmlTmpSimplifyDir, xmlDestDirClinet, "ForCSharp")
                    && cop.CopyStart(xmlListFile, xmlTmpSimplifyDir, xmlDestDirClinet1, "ForCSharp")
                    && cop.CopyStart(xmlListFile, xmlTmpSimplifyDir, xmlDestDirClinet2, "ForCSharp")
                    )
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Succeed!");
                    System.IO.File.Delete(exePath + ".lock");
                    ClearTempFolder();
                    if (args.Length == 0)
                        Console.ReadKey();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Failed!");
                    ClearTempFolder();
                    Console.ReadKey();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "    " + e.StackTrace);
                Console.ReadKey();
            }
            finally
            {
                ClearTempFolder();
            }
        }

        static void ClearTempFolder()
        {
            try
            {
                if (Directory.Exists(xmlTmpSimplifyDir))
                    Directory.Delete(xmlTmpSimplifyDir, true);
                if (Directory.Exists(xmlTmpSimplifyDir_Language))
                    Directory.Delete(xmlTmpSimplifyDir_Language, true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "    " + e.StackTrace);
            }
        }
    }
}
