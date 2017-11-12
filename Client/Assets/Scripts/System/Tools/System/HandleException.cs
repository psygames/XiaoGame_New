using System;
using System.Collections.Generic;

using System.Text;

namespace RedStone
{
    public class UnhandledExceptionHandler : Core.Singleton<UnhandledExceptionHandler>
    {
        public UnhandledExceptionHandler()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(ExceptionHandler);
        }

        private static void ExceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            Console.WriteLine("UnhandledException caught : " + e.Message);
            Console.WriteLine("Runtime terminating: {0}", args.IsTerminating);
        }
    }
}
