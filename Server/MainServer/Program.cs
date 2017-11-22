using System;
using System.Threading;

namespace RedStone
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Main Server";
            GameManager.CreateInstance().Start();
            while(true)
            {
                Thread.Sleep(10000);
            }
        }
    }
}
