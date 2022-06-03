using System;

namespace ConsoleApp1Garage___Övn_5
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Garage<Vehicle> garage = new Garage<Vehicle>(12);

            Garage<Car> cars = new Garage<Car>(10);

            

            InjectData(garage);

            

            foreach (var v in garage)
            {
                Console.WriteLine($"Reg: {v.RegNum}  Colour: {v.Colour}\tType: {v.GetType().Name}");
            }
            Console.WriteLine();


            Vehicle vehicle = new Vehicle("ABC123", 4);
            vehicle.Colour = ConsoleColor.White;
            bool success = garage.AddVehicle(vehicle);
            if (success) Console.WriteLine($"Added {vehicle.RegNum}");
            else Console.WriteLine($"Could not add {vehicle.RegNum}");

            //Vehicle vehicle2 = new Vehicle("ABC123", 4);
            //bool success2 = garage.AddVehicle(vehicle2);                    // Fail because of duplicate
            //if (success2) Console.WriteLine($"Added {vehicle2.RegNum}");
            //else Console.WriteLine($"Failed to add {vehicle2.RegNum}");

            Vehicle vehicle3 = new Vehicle("DEF456", 4);
            bool success3 = garage.AddVehicle(vehicle3);
            if (success3) Console.WriteLine($"Added {vehicle3.RegNum}");
            else Console.WriteLine($"Could not add {vehicle3.RegNum}");

            Car car = new Car("GHI789", 4, "Gasoline");
            //cars.AddVehicle(b);       // Funkar inte! Varför? Måste deklareras som car
            car.Colour = ConsoleColor.DarkYellow;
            bool success1 = garage.AddVehicle(car);
            if (success1) Console.WriteLine($"Added {car.RegNum}");
            else Console.WriteLine($"Could not add {car.RegNum}");
            Console.WriteLine();

            foreach (var v in garage)
                Console.WriteLine($"Reg: {v.RegNum}  Colour: {v.Colour}\tType: {v.GetType().Name}");
            Console.WriteLine();


            bool success4 = false;
            string searchString = "DEF456";
            foreach (var v in garage)
            {
                if(v.RegNum == searchString)
                {
                    success4 = garage.RemoveVehicle(v);
                    break;
                }
            }
            if (!success4) Console.WriteLine($"Could not find {searchString}\n");
            else Console.WriteLine($"Removed: {searchString}\n");


            Console.WriteLine("List of all vehicles:");
            foreach (var v in garage)
                Console.WriteLine($"Reg: {v.RegNum}  Colour: {v.Colour}\tType: {v.GetType().Name}");
            Console.WriteLine();

            Console.WriteLine("List of cars:");
            foreach (var v in garage)
                if(v.GetType().Name == "Car")
                    Console.WriteLine(v.RegNum);
            Console.WriteLine();

            Console.WriteLine("List of motorcycles:");
            foreach (var v in garage)
                if (v.GetType().Name == "Motorcycle")
                    Console.WriteLine(v.RegNum);
            Console.WriteLine();

            Console.WriteLine("List of indescribable vehicles:");
            foreach (var v in garage)
                if (v.GetType().Name == "Vehicle")
                    Console.WriteLine(v.RegNum);
            Console.WriteLine();


            void InjectData(Garage<Vehicle> garage)
            {
                string regNum = "";

                regNum = MakeRegNum(garage);
                Car car1 = new Car(regNum, 4, "Gasoline");
                car1.Colour = ConsoleColor.Blue;
                garage.AddVehicle(car1);

                regNum = MakeRegNum(garage);
                Car car2 = new Car(regNum, 4, "Diesel");
                car2.Colour = ConsoleColor.Black;
                garage.AddVehicle(car2);

                regNum = MakeRegNum(garage);
                Motorcycle mc1 = new Motorcycle(regNum, 2, 0.6);
                mc1.Colour = ConsoleColor.DarkBlue;
                garage.AddVehicle(mc1);

                regNum = MakeRegNum(garage);
                Motorcycle mc2 = new Motorcycle(regNum, 2, 0.5);
                mc2.Colour = ConsoleColor.DarkRed;
                garage.AddVehicle(mc2);

                regNum = MakeRegNum(garage);
                Bus bus = new Bus(regNum, 6, 50);
                bus.Colour = ConsoleColor.Green;
                garage.AddVehicle(bus);

                regNum = MakeRegNum(garage);
                Boat boat = new Boat(regNum, 0, 2.5);
                boat.Colour = ConsoleColor.White;
                garage.AddVehicle(boat);

                regNum = MakeRegNum(garage);
                Airplane airplane = new Airplane(regNum, 3, 2);
                airplane.Colour = ConsoleColor.White;
                garage.AddVehicle(airplane);

            }

            string MakeRegNum(Garage<Vehicle> garage)
            {
                var r = new Random();
                string s;
                bool loop;

                do
                {
                    loop = false;

                    s = "";
                    for (int i = 0; i < 3; i++)
                        s += (char)r.Next(65, 91);
                    for (int i = 0; i < 3; i++)
                        s += (char)r.Next(48, 58);

                    foreach (var v in garage)
                    {
                        if (v.RegNum == s)
                        {
                            loop = true;        // RegNum found in garage. Try again
                            break;
                        }
                    }

                } while (loop);

                return s;

            }


        }



    }
}
