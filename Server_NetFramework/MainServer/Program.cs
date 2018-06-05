using System;
using System.Threading;

namespace RedStone
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Main Server";
            ConfigLogger();
            GameManager.CreateInstance().Start();
            while(true)
            {
                Thread.Sleep(10000);
            }
        }

        private static void ConfigLogger()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender,e)=>
            {
                Exception excp = (Exception)e.ExceptionObject;
                Logger.Log(excp.Message + "\n" + excp.StackTrace);
            };
            Logger.SetFilePath("MainServer.log");
        }
    }
}
