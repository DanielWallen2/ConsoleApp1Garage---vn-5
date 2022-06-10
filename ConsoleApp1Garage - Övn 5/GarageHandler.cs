using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1Garage___Övn_5
{
    internal class GarageHandler
    {
        private IGarage<IVehicle> garage;

        private uint garageCapasity;

        public uint GarageCapacity
        {
            get { return garageCapasity; }
        }

        internal GarageHandler(uint NrOfPlaces)
        {
            garage = new Garage<IVehicle>(NrOfPlaces);
            garageCapasity = NrOfPlaces;
        }

        internal bool SaveGarage(string path)
        {

            StringBuilder sb = new StringBuilder();

            foreach(var vehicle in garage)
            {
                sb.Append($"Type:{vehicle.GetType().Name};");

                foreach (PropertyInfo p in vehicle.GetType().GetProperties())
                    sb.Append($"{p.Name}:{p.GetValue(vehicle, null)};");

                sb.Append(Environment.NewLine);
            }

            File.WriteAllText(path, sb.ToString());

            return true;
        }





        internal uint Populate(uint nrOfVehicles)
        {
            return InjectRandomVehicles(nrOfVehicles);
        }

        internal bool AddCar(string RegNum, uint NrOfwheels, string FuelType, ConsoleColor colour = ConsoleColor.Black)
        {
            bool success = false;
            Car car = new Car(RegNum.ToUpper(), NrOfwheels, FuelType);
            car.Colour = colour;

            if (ValidateRegNum(RegNum) && ValidateNrOfWheels(NrOfwheels, car.GetType().Name))
                success = garage.AddVehicle(car);

            return success;
        }

        internal bool AddMotorcycle(string RegNum, uint NrOfwheels, double CylinderVolume, ConsoleColor colour = ConsoleColor.Black)
        {
            bool success = false;
            Motorcycle mc = new Motorcycle(RegNum, NrOfwheels, CylinderVolume);
            mc.Colour = colour;

            if (ValidateRegNum(RegNum) && ValidateNrOfWheels(NrOfwheels, mc.GetType().Name))
                success = garage.AddVehicle(mc);

            return success;
        }

        internal bool AddBus(string RegNum, uint NrOfwheels, uint NrOfSeats, ConsoleColor colour = ConsoleColor.Black)
        {
            bool success = false;
            Bus bus = new Bus(RegNum, NrOfwheels, NrOfSeats);
            bus.Colour = colour;

            if (ValidateRegNum(RegNum) && ValidateNrOfWheels(NrOfwheels, bus.GetType().Name))
                success = garage.AddVehicle(bus);

            return success;
        }

        internal bool AddBoat(string RegNum, uint NrOfwheels, double Lenght, ConsoleColor colour = ConsoleColor.Black)
        {
            bool success = false;
            Boat boat = new Boat(RegNum, NrOfwheels, Lenght);
            boat.Colour = colour;

            if (ValidateRegNum(RegNum) && ValidateNrOfWheels(NrOfwheels, boat.GetType().Name))
                success = garage.AddVehicle(boat);

            return success;
        }

        internal bool AddAirplane(string RegNum, uint NrOfwheels, uint NrOfEngines, ConsoleColor colour = ConsoleColor.Black)
        {
            bool success = false;
            Airplane airplane = new Airplane(RegNum, NrOfwheels, NrOfEngines);
            airplane.Colour = colour;

            if (ValidateRegNum(RegNum) && ValidateNrOfWheels(NrOfwheels, airplane.GetType().Name))
                success = garage.AddVehicle(airplane);

            return success;
        }

        internal bool RemoveVehicle(IVehicle vehicle)
        {
            bool result = garage.RemoveVehicle(vehicle);

            return result;
        }

        internal IVehicle? FindVehicle(string RegNum)
        {
            RegNum = RegNum.ToUpper();
            foreach (var v in garage)
                if(v.RegNum == RegNum) return v;

            return null;
        }

        internal IVehicle[] FindVehicles(string VehicleType, string NrOfWheels, string Colour)
        {
            IEnumerable<IVehicle> vehicles = (IEnumerable<IVehicle>)garage;
            if(VehicleType != "")
                vehicles = vehicles.Where(v => v.GetType().Name.ToLower() == VehicleType.ToLower());

            uint nrOfWheels;
            if(uint.TryParse(NrOfWheels, out nrOfWheels))
                vehicles = vehicles.Where(v => v.NrOfwheels == nrOfWheels);

            ConsoleColor cColour = ConsoleColor.Black;
            bool colourOk = ValiateColour(Colour, ref cColour);
            if(colourOk)
                vehicles = vehicles.Where(v => v.Colour == cColour);

            return vehicles.ToArray();
        }

        internal uint CountVehicles() => (uint)garage.Count();

        internal (Type, uint)[] ListNrByType()
        {
            var vehicleTypes = garage.Select(v => v.GetType()).Distinct();

            var myarray = new (Type, uint)[vehicleTypes.Count()];

            uint amount = 0; uint i = 0;
            foreach (var vType in vehicleTypes)
            {
                amount = (uint)garage.Where(v => v.GetType() == vType).Count();
                myarray[i++] = (vType, amount);
            }

            return myarray;
        }

        internal IVehicle[] ListAll() => garage.ToArray();

        private uint InjectRandomVehicles(uint NrOfVehicles)
        {
            var r = new Random();
            uint cnt = 0;

            for (uint i = 0; i < NrOfVehicles; i++)
            {
                int typeNr = r.Next(0, 5);
                int colourNr = r.Next(0, 16);

                Vehicle vehicle = MakeVehicle((uint)typeNr, (uint)colourNr);
                bool success = garage.AddVehicle(vehicle);                      // fail if array is full
                if (success) cnt++;
            }

            return cnt;                                                         // nr of successful adds
        }

        private Vehicle? MakeVehicle(uint typeNr, uint colourNr)
        {
            Vehicle? vehicle = null;

            switch (typeNr)
            {
                case 0:
                    vehicle = new Car(MakeRegNum(), 4, "Gasoline");
                    vehicle.Colour = (ConsoleColor)colourNr;
                    break;

                case 1:
                    vehicle = new Motorcycle(MakeRegNum(), 2, 0.82);
                    vehicle.Colour = (ConsoleColor)colourNr;
                    break;

                case 2:
                    vehicle = new Bus(MakeRegNum(), 6, 42);
                    vehicle.Colour = (ConsoleColor)colourNr;
                    break;

                case 3:
                    vehicle = new Boat(MakeRegNum(), 0, 7.5);
                    vehicle.Colour = (ConsoleColor)colourNr;
                    break;

                case 4:
                    vehicle = new Airplane(MakeRegNum(), 3, 2);
                    vehicle.Colour = (ConsoleColor)colourNr;
                   break;
            }

            return vehicle;

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

            } while(!ValidateRegNum(s));       // If found in garage keep looping

            return s;
        }

        internal bool ValidateRegNum(string RegNum) //where T : class
        {
            if (string.IsNullOrWhiteSpace(RegNum) ||
                RegNum.Length != 6 ||
                RegNum.Contains(' ')) return false;

            string s = RegNum.Substring(3, 3);
            if (!int.TryParse(RegNum.Substring(3, 3), out _) || 
                int.TryParse(RegNum.Substring(0, 1), out _) || 
                int.TryParse(RegNum.Substring(1, 1), out _) ||
                int.TryParse(RegNum.Substring(2, 1), out _)) return false;

            foreach (var v in garage)
                if (v != null)
                    if (v.RegNum == RegNum) return false;   // Duplicate found

            return true;
        }

        internal bool ValidateNrOfWheels(uint nrOfWheels, string Type)
        {
            switch (Type.ToLower())
            {
                case "car":
                    if (nrOfWheels >= 3 && nrOfWheels <= 4) return true;
                    else return false;

                case "motorcycle":
                    if (nrOfWheels >= 2 && nrOfWheels <= 4) return true;
                    else return false;

                case "bus":
                    if (nrOfWheels >= 4 && nrOfWheels <= 6) return true;
                    else return false;

                case "boat":
                    if (nrOfWheels == 0) return true;
                    else return false;

                case "airplane":
                    if (nrOfWheels == 3) return true;
                    else return false;
            }

            return true;        // if type is vehicle or new unknown, allow nr be anything
        }

        internal bool ValidateType(string Type)
        {
            string[] Types = {"car", "motorcycle", "bus", "boat", "airplane" };

            if(Types.Contains(Type.ToLower())) return true;
            else return false;
        }

        internal static bool ValiateColour(string Colour, ref ConsoleColor cColour)
        {
            ConsoleColor consoleColor;
            for(uint i = 0; i < 16; i++)
            {
                consoleColor = (ConsoleColor)i;
                if(consoleColor.ToString().ToLower() == Colour.ToLower())
                {
                    cColour = consoleColor;
                    return true;
                }
            }

            return false;
        }

    }

}
