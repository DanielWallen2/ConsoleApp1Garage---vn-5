using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1Garage___Övn_5
{
    internal class ConsolUI
    {
        internal void DrawSetupMessage()
        {
            Console.Clear();
            Console.WriteLine("Create garage");
        }

        internal void DrawMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Garage menu:");
            Console.WriteLine("" +
                "1. Populate garage\n" +
                "2. Add a vehicle\n" +
                "3. Remove a vehicle\n" +
                "4. Show a vehicles properties\n" +
                "5. List all vehicles\n" +
                "6. Show garage statistics\n" +
                "7. Query vehicles\n" +
                "Q. Quit");
        }

        internal string GetMenuKey()
        {
            string key = Console.ReadKey(intercept: true).KeyChar.ToString();
            return key;
        }

        internal string GetInput()
        {
            return Console.ReadLine();
        }

        internal void ShowErrorMessage(string errorMessage) => Console.WriteLine(errorMessage);

        internal void WriteMessage(string Message) => Console.WriteLine(Message);

        internal void Prompt(string Message) => Console.Write(Message);

        internal void DrawList(Vehicle[] vehicles)
        {
            Console.WriteLine("Type        RegNum   Wheels Colour");
            foreach(Vehicle vehicle in vehicles)
                WriteVehicle(vehicle);
        }

        internal void WriteVehicle(Vehicle vehicle)
        {
            string output = "" +
                vehicle.GetType().Name + new string(' ', 12 - vehicle.GetType().Name.Length) +
                vehicle.RegNum + "   " +
                vehicle.NrOfwheels + "      " +
                vehicle.Colour;
            
            Console.WriteLine(output);
        }

        internal void WriteTypeCount((Type, uint)[] typeCounts)
        {
            Console.WriteLine("\nType        Count");

            string type = "";
            uint count = 0;
            string output = "";

            foreach (var tc in typeCounts)
            {
                type = tc.Item1.Name;
                count = tc.Item2;

                output = "" +
                   type + new string(' ', 12 - type.Length) +
                   count;

                Console.WriteLine(output);
            }
           
        }

        internal void WriteCapacity(uint Capacity, uint Ocupied, uint Available)
        {
            string output = $"\nGarage capacity   \t {Capacity}\n" +
                $"Number of vehicles\t {Ocupied}\n" +
                $"Available slots   \t {Available}";

            Console.WriteLine(output);
        }

        internal void Pause()
        {
            Console.ReadKey(intercept: true);             // Pause here
        }


    }




}
