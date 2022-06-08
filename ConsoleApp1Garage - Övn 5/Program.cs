using System;

namespace ConsoleApp1Garage___Övn_5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsolUI consolUI = new ConsolUI();
            Manager manager = new Manager(consolUI);

            manager.Setup();
            manager.Run();

            Console.WriteLine("\nThank you ...");
            //Console.ReadKey();

        }

    }
}
