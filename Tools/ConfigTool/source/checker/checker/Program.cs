using System;
using System.Collections.Generic;
using System.Text;
using System.Security;
using System.IO;

namespace checker
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
                exePath = TOOLS_PATH;
#endif
                IniFile ini = new IniFile(exePath + "config.ini");
                string xmlDir = exePath + ini.ReadValue("Checker", "XmlDir");
                string languageXmlDir = exePath + ini.ReadValue("Checker", "LanguageXmlDir");
                CheckerManager mgr = new CheckerManager();
                CheckResult result = mgr.CheckDirectory(xmlDir);
                if (result.isSucceed
                    && (result = mgr.CheckDirectory(languageXmlDir)).isSucceed)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(result);
                    File.Delete(exePath + ".lock");
                    if (args.Length == 0)
                        Console.ReadKey();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(result);
                    Console.ReadKey();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "    " + e.StackTrace);
                Console.ReadKey();
            }
        }
    }
}
