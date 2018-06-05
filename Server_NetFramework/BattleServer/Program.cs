using System;

namespace RedStone
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Battle Server";
            ConfigLogger();
            GameManager.CreateInstance().Start();
            while (true)
            {
                System.Threading.Thread.Sleep(10000);
            }
        }

        private static void ConfigLogger()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                Exception excp = (Exception)e.ExceptionObject;
                Logger.Log(excp.Message + "\n" + excp.StackTrace);
            };
            Logger.SetFilePath("BattleServer.log");
        }
    }
}
