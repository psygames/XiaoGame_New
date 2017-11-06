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
                args = new string[] { "-java" };
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
                string javaOutDir = GetPath(exePath, ini.ReadString("Generator", "JavaOutDir"));

                string constXmlPath = xmlDir + "/TableConst.xml";

                string constCsOutDir = GetPath(exePath, ini.ReadString("Generator", "ConstCsOutDir"));

                List<string> constJavaDirs = new List<string>();
                List<string> constJavaServerName = new List<string>();
                foreach (var key in ini.ReadSection("Generator"))
                {
                    if (key.StartsWith("ConstJavaOutDir"))
                    {
                        var dir = GetPath(exePath, ini.ReadString("Generator", key));
                        constJavaDirs.Add(dir);
                        constJavaServerName.Add(key.Substring("ConstJavaOutDir-".Length));
                    }
                }

                string languageXMLPath = xmlDir + "../Languages/languages.xml";
                string colorXMLPath = xmlDir + "/TableTextColor.xml";
                string param = args[0];
                Generator gen = new Generator();
                ConstGenerator constGen = new ConstGenerator();
                LanguageGenerator lanGen = new LanguageGenerator();
                TextColorGenerator colorGen = new TextColorGenerator();

                if (param == "-java"
                    && gen.GeneJava(xmlDir, xmlListFile, "", javaOutDir)
                    && constGen.GeneJava(constJavaServerName.ToArray()
                    , constJavaDirs.ToArray(), constXmlPath, "Color"))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("succeed!!!");
                    File.Delete(exePath + ".lock");
                    if (args.Length <= 1)
                        Console.ReadKey();
                }
                else if (param == "-csharp"
                    && gen.GeneCs(xmlDir, xmlListFile, csOutDir)
                    && constGen.GeneCs(constXmlPath, constCsOutDir)
                    && lanGen.GeneCs(languageXMLPath, constCsOutDir)
                    && colorGen.GeneCs(colorXMLPath, constCsOutDir))
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
