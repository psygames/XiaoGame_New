using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace xmltool
{
    class Program
    {
        static Process pro = new Process();
        const string TOOLS_PATH = @"../../../../../xmltools/";
        static string xmltoolsDir = @"xmltools/";
        static string exePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        static void Main(string[] args)
        {
#if DEBUG
            xmltoolsDir = TOOLS_PATH;
#endif

            if (runProcess("checker.exe", "-call")
                && runProcess("generator.exe", "-csharp -call")
                && runProcess("generator.exe", "-java -call")
                && runProcess("copyfile.exe", "-call"))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("All Finished!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error Locked!");
            }
            Console.ReadKey();
        }

        static bool runProcess(string processName, string args = "")
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Run --> " + processName + " " + args);
            File.Create(exePath + xmltoolsDir + ".lock").Close();
            pro.StartInfo.CreateNoWindow = false;
            pro.StartInfo.FileName = exePath + xmltoolsDir + processName;
            pro.StartInfo.Arguments = args;
            pro.Start();
            pro.WaitForExit();
            if (!File.Exists(exePath + xmltoolsDir + ".lock"))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Finish --> " + processName + " " + args);
                return true;
            }
            return false;
        }
    }
}
