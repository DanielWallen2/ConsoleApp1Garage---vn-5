using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1Garage___Övn_5
{
    public class ConsolUI : IUI
    {
        public void DrawSetupMessage()
        {
            Console.Clear();
            Console.WriteLine("\n Create garage");
        }

        public void DrawMainMenu()
        {
            Console.Clear();
            Console.WriteLine("\n Garage menu:\n" +
                " -----------------------------");
            Console.WriteLine("" +
                " 1. Populate garage\n" +
                " 2. Add a vehicle\n" +
                " 3. Remove a vehicle\n" +
                " 4. Show a vehicles properties\n" +
                " 5. List all vehicles\n" +
                " 6. Show garage statistics\n" +
                " 7. Query vehicles\n" +
                " Q. Quit");
        }

        public string GetMenuKey()
        {
            Console.Write(" ");
            string key = Console.ReadKey(intercept: true).KeyChar.ToString();
            return key;
        }

        public string GetInput() => Console.ReadLine();

        public void ShowErrorMessage(string errorMessage)
        {
            Console.WriteLine(errorMessage);
            Console.Write(" ");
        }

        public void WriteMessage(string Message) => Console.WriteLine(Message);

        public string Prompt(string Message)
        {
            Console.Write(Message);
            return GetInput();
        }

        public void DrawList(IVehicle[] vehicles)
        {
            Console.WriteLine(" Type        RegNum   Wheels Colour\n" +
                              " ----------------------------------");
            foreach (Vehicle vehicle in vehicles)
                WriteVehicle(vehicle);
        }

        public void WriteVehicle(IVehicle vehicle)
        {
            string output = " " +
                vehicle.GetType().Name + new string(' ', 12 - vehicle.GetType().Name.Length) +
                vehicle.RegNum + "   " +
                vehicle.NrOfwheels + "      " +
                vehicle.Colour;

            Console.WriteLine(output);
        }

        public void WriteTypeCount((Type, uint)[] typeCounts)
        {
            Console.WriteLine("\n Type        Count\n" +
                                " -----------------");

            string type;
            uint count;
            string output;

            foreach (var tc in typeCounts)
            {
                type = tc.Item1.Name;
                count = tc.Item2;

                output = " " +
                   type + new string(' ', 12 - type.Length) +
                   count;

                Console.WriteLine(output);
            }

        }

        public void WriteCapacity(uint Capacity, uint Ocupied, uint Available)
        {
            string output = $"\n" +
                $" Garage capacity   \t {Capacity}\n" +
                $" Number of vehicles\t {Ocupied}\n" +
                $" Available slots   \t {Available}";

            Console.WriteLine(output);
        }

        public void Pause()
        {
            Console.Write(" ");
            Console.ReadKey(intercept: true);    // Pause here
        }
    }




}
