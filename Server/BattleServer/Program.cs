using System;

namespace RedStone
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Battle Server";
            GameManager.CreateInstance().Start();
            while (true)
            {
                System.Threading.Thread.Sleep(10000);
            }
        }
    }
}
