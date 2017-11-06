using System;
using System.Threading;

namespace RedStone
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main Server Start Up...");
            GameManager.CreateInstance().Start();
            while(true)
            {
                Thread.Sleep(10000);
            }
        }
    }
}
