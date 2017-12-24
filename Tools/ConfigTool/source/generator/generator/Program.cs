using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace generator
{
    class Program
    {
        const string TOOLS_PATH = @"../../../../../xmltools/";

        static void Main(string[] args)
        {
            try
            {
                string exePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
#if DEBUG
                args = new string[] { "-csharp" };
                exePath = TOOLS_PATH;
#endif

                if (args.Length == 0)
                {
                    Console.WriteLine("please append with -java/-csharp");
                    Console.ReadKey();
                    return;
                }

                IniFiles ini = new IniFiles(GetPath(exePath, "config.ini"));

                string xmlDir = GetPath(exePath, ini.ReadString("Generator", "XmlDir"));
                string xmlListFile = GetPath(exePath, ini.ReadString("Generator", "XmlListFile"));
                string csOutDir = GetPath(exePath, ini.ReadString("Generator", "CsOutDir"));
                string csOutDir1 = GetPath(exePath, ini.ReadString("Generator", "CsOutDir1"));
                string csOutDir2 = GetPath(exePath, ini.ReadString("Generator", "CsOutDir2"));
                string javaOutDir = GetPath(exePath, ini.ReadString("Generator", "JavaOutDir"));
                string languageXMLPath = xmlDir + "../Languages/languages.xml";
                string colorXMLPath = xmlDir + "/TableTextColor.xml";
                string param = args[0];
                Generator gen = new Generator();
                ConstGenerator constGen = new ConstGenerator();
                LanguageGenerator lanGen = new LanguageGenerator();
                TextColorGenerator colorGen = new TextColorGenerator();

                if (param == "-java"
                    && gen.GeneJava(xmlDir, xmlListFile, "", javaOutDir))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("succeed!!!");
                    File.Delete(exePath + ".lock");
                    if (args.Length <= 1)
                        Console.ReadKey();
                }
                else if (param == "-csharp"
                    && gen.GeneCs(xmlDir, xmlListFile, csOutDir)
                    && gen.GeneCs(xmlDir, xmlListFile, csOutDir1)
                    && gen.GeneCs(xmlDir, xmlListFile, csOutDir2)
                    )
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("succeed!!!");
                    File.Delete(exePath + ".lock");
                    if (args.Length <= 1)
                        Console.ReadKey();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Failed");
                    Console.ReadKey();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "    " + e.StackTrace);
                Console.ReadKey();
            }
        }

        private static string GetPath(string exePath, string iniPath)
        {
            if (string.IsNullOrEmpty(iniPath))
            {
                return "";
            }
            return exePath + iniPath;
        }
    }
}
