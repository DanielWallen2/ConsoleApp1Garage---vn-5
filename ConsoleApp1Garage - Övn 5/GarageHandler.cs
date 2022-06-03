using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1Garage___Övn_5
{
    internal class GarageHandler
    {
        private Garage<Vehicle> garage;
        private string[] types = new string[12];

        internal GarageHandler()
        {
            garage = new Garage<Vehicle>(10);
        }

        internal void Populate()
        {
            InjectData();
            CreateTypesList();
        }

        internal bool AddCar(string RegNum, uint NrOfwheels, string FuelType, ConsoleColor colour = ConsoleColor.Black)
        {
            bool success = false;
            Car car = new Car(RegNum.ToUpper(), NrOfwheels, FuelType);
            car.Colour = colour;

            if (ValidateRegNum(RegNum) && ValidateNrOfWheels(NrOfwheels, car))
                success = garage.AddVehicle(car);

            return success;
        }

        internal bool AddMotorcycle(string RegNum, uint NrOfwheels, double CylinderVolume, ConsoleColor colour = ConsoleColor.Black)
        {
            bool success = false;
            Motorcycle mc = new Motorcycle(RegNum, NrOfwheels, CylinderVolume);
            mc.Colour = colour;

            if (ValidateRegNum(RegNum) && ValidateNrOfWheels(NrOfwheels, mc))
                success = garage.AddVehicle(mc);

            return success;
        }

        internal bool Bus(string RegNum, uint NrOfwheels, uint NrOfSeats, ConsoleColor colour = ConsoleColor.Black)
        {
            bool success = false;
            Bus bus = new Bus(RegNum, NrOfwheels, NrOfSeats);
            bus.Colour = colour;

            if (ValidateRegNum(RegNum) && ValidateNrOfWheels(NrOfwheels, bus))
                success = garage.AddVehicle(bus);

            return success;
        }

        internal bool Boat(string RegNum, uint NrOfwheels, double Lenght, ConsoleColor colour = ConsoleColor.Black)
        {
            bool success = false;
            Boat boat = new Boat(RegNum, NrOfwheels, Lenght);
            boat.Colour = colour;

            if (ValidateRegNum(RegNum) && ValidateNrOfWheels(NrOfwheels, boat))
                success = garage.AddVehicle(boat);

            return success;
        }

        internal bool Airplane(string RegNum, uint NrOfwheels, uint NrOfEngines, ConsoleColor colour = ConsoleColor.Black)
        {
            bool success = false;
            Boat boat = new Boat(RegNum, NrOfwheels, NrOfEngines);
            boat.Colour = colour;

            if (ValidateRegNum(RegNum) && ValidateNrOfWheels(NrOfwheels, boat))
                success = garage.AddVehicle(boat);

            return success;
        }

        internal bool RemoveVehicle(Vehicle vehicle)
        {
            bool result = garage.RemoveVehicle(vehicle);

            return result;
        }

        internal bool RemoveVehicle(string RegNum)
        {
            bool result;

            Vehicle? vehicle = FindVehicle(RegNum);
            result = garage.RemoveVehicle(vehicle!);

            return result;
        }

        internal Vehicle? FindVehicle(string RegNum)
        {
            RegNum = RegNum.ToUpper();
            if(ValidateRegNum(RegNum))
                foreach (var v in garage)
                    if(v.RegNum == RegNum) return v;

            return null;
        }

        internal Vehicle[] FindVehicles(string VehicleType, string NrOfWheels, string Colour)
        {
            var vehicles = garage.Where(v => v.GetType().Name == VehicleType);

            uint nrOfWheels;
            if(uint.TryParse(NrOfWheels, out nrOfWheels))
                vehicles = vehicles.Where(v => v.NrOfwheels == nrOfWheels);

            ConsoleColor myColour;
           
            for (uint i = 0; i < 16; i++)
            {
                myColour = (ConsoleColor)i;
                if(myColour.ToString() == Colour)
                    vehicles = vehicles.Where(v => v.NrOfwheels == nrOfWheels);
            }

            //uint size = sizeof(ConsoleColor);
            //var vehicles = vehicles.Where(v => v.Colour == "Red");
            //var vehicles2 = garage.Where(v => v is Car);

            return vehicles.ToArray();
        }

        internal (Type, uint)[] ListNrByType()
        {
            var vehicleTypes = garage.Select(v => v.GetType()).Distinct();

            var myarray = new (Type, uint)[vehicleTypes.Count()];

            uint nrOfType = 0; uint i = 0;
            foreach (var vType in vehicleTypes)
            {
                nrOfType = (uint)garage.Where(v => v.GetType() == vType).Count();
                myarray[i++] = (vType, nrOfType);
            }

            return myarray;

            //(Type, uint) typeCount;
            //typeCount = (vType, nrOfType);
            //myarray[i++] = typeCount;
            // var vehicleTypes = garage.Select(v => v.GetType().Name).Distinct().ToArray();
            // (double, int) t1 = (4.5, 3);


            //CreateTypesList();

            //string s = "";
            //foreach(var type in types)
            //{
            //    if(type != null)
            //    {
            //        uint count = 0;
            //        foreach(var v in garage)
            //            if(v.GetType().Name == type) count++;

            //        s += $"{type}:\t{count}";
            //    }
            //}

            //return s;

            // var res = garage.Select(v => v.GetType().Name).Distinct().ToArray();
        }

        internal string ListAll()
        {
            string s = "";
            foreach (var v in garage)
                s += $"Reg: {v.RegNum}\tNrOfWheels: {v.NrOfwheels}\tColour: {v.Colour}\tType: {v.GetType().Name}\n";

            return s;
        }

        private void CreateTypesList()
        {
            foreach (var v in garage)
            {
                bool exist = false;
                uint mark = 0;

                for(uint i = 0; i < types.Length; i++)
                {
                    if(types[i] != null)
                    {
                        if(types[i] == v.GetType().Name)
                        {
                            exist = true;
                            break;
                        }
                    }
                    else { mark = i; }      // Mark first empty place
                }

                if (!exist) { types[mark] = v.GetType().Name; }

            }
            
        }

        private void InjectData()
        {
            string regNum = "";

            regNum = MakeRegNum();
            Car car1 = new Car(regNum, 4, "Gasoline");
            car1.Colour = ConsoleColor.Blue;
            garage.AddVehicle(car1);

            regNum = MakeRegNum();
            Car car2 = new Car(regNum, 4, "Diesel");
            car2.Colour = ConsoleColor.Black;
            garage.AddVehicle(car2);

            regNum = MakeRegNum();
            Motorcycle mc1 = new Motorcycle(regNum, 2, 0.6);
            mc1.Colour = ConsoleColor.DarkBlue;
            garage.AddVehicle(mc1);

            regNum = MakeRegNum();
            Motorcycle mc2 = new Motorcycle(regNum, 2, 0.5);
            mc2.Colour = ConsoleColor.DarkRed;
            garage.AddVehicle(mc2);

            regNum = MakeRegNum();
            Bus bus = new Bus(regNum, 6, 50);
            bus.Colour = ConsoleColor.Green;
            garage.AddVehicle(bus);

            regNum = MakeRegNum();
            Boat boat = new Boat(regNum, 0, 2.5);
            boat.Colour = ConsoleColor.White;
            garage.AddVehicle(boat);

            regNum = MakeRegNum();
            Airplane airplane = new Airplane(regNum, 3, 2);
            airplane.Colour = ConsoleColor.White;
            garage.AddVehicle(airplane);

        }

        private string MakeRegNum()
        {
            var r = new Random();
            string s;

            do
            {
                s = "";
                for (int i = 0; i < 3; i++)
                    s += (char)r.Next(65, 91);
                for (int i = 0; i < 3; i++)
                    s += (char)r.Next(48, 58);

            } while (!ValidateRegNum(s));       // If found in garage keep looping

            return s;

        }

        private bool ValidateRegNum(string RegNum) //where T : class
        {
            if(string.IsNullOrWhiteSpace(RegNum) || 
                RegNum.Length != 6 || 
                RegNum.Contains(' ') || 
                !int.TryParse(RegNum.Substring(3, 3), out _) ||
                int.TryParse(RegNum.Substring(0, 1), out _) ||
                int.TryParse(RegNum.Substring(1, 1), out _) ||
                int.TryParse(RegNum.Substring(2, 1), out _) ) return false;

            foreach (var v in garage)
                if (v != null)
                    if (v.RegNum == RegNum) return false;   // Duplicate found

            return true;
        }

        private bool ValidateNrOfWheels(uint nrOfWheels, Vehicle vehicle)
        {
            switch (vehicle.GetType().Name)
            {
                case "Car":
                    if (nrOfWheels >= 3 && nrOfWheels <= 4) return true;
                    else return false;

                case "Motorcycle":
                    if (nrOfWheels >= 2 && nrOfWheels <= 3) return true;
                    else return false;

                case "Bus":
                    if (nrOfWheels >= 4 && nrOfWheels <= 6) return true;
                    else return false;

                case "Boat":
                    if (nrOfWheels == 0) return true;
                    else return false;

                case "Airplane":
                    if (nrOfWheels == 3) return true;
                    else return false;
            }

            return true;        // if type is vehicle or new unknown, allow nr be anything

        }
    }
}
