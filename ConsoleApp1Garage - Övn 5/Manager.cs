using System.Reflection;

namespace ConsoleApp1Garage___Övn_5
{
    internal class Manager
    {
        private IUI ui;
        private GarageHandler gh;
        private bool running = true;

        private string garageDirectory;
        private string garageFile = "MyGarage";
        private string path;

        internal Manager(IUI consolUi)
        {
            ui = consolUi;
        }

        internal void Setup()
        {
            garageDirectory = Environment.CurrentDirectory;
            path = @$"{garageDirectory}\{garageFile}";

            bool fileExist = File.Exists(path);
            // ToDo: Check if file is empty
            
            if (fileExist)
            {
                string[] sVehicles = File.ReadAllLines(path);

                uint size;
                string[] gp = sVehicles[0].Split(';');
                (string key, string sSize) = GetProperty(gp[0]);
                bool ok = uint.TryParse(sSize, out size);
                if(ok) gh = new GarageHandler(size);

                for (int i = 0; i < sVehicles.Length; i++)
                {
                    (string, string)[] props;
                    props = GetProperties(sVehicles[i]);

                    Vehicle? vehicle = CreateSpecificVehicle(props);
                    if(vehicle != null)
                        gh.AddVehicle(vehicle);
                }
            }
            else
            {
                if(!Directory.Exists(garageDirectory))
                    Directory.CreateDirectory(garageDirectory);

                MakeGarage();
            }

        }

        internal void Run()
        {
            do 
            {
                ui.DrawMainMenu();
                DoChoise();
                if(running) ui.Pause();

            } while(running);

        }

        internal void DoChoise()
        {
            string choise = GetMenuChoise();

            var menu = new Dictionary<string, Action> 
            {
                { "1", AddRandomVehicles },
                { "2", AddAVehicle },
                { "3", RemoveAVehicle },
                { "4", ShowVehicle },
                { "5", ListAllVehicles },
                { "6", ShowStatistics },
                { "7", QueryVehicles },
                { "Q", Quit }
            };

            if(menu.ContainsKey(choise)) menu[choise]?.Invoke();

        }

        private void Quit()
        {
            //Environment.Exit(0);
            gh.SaveGarage(path);
            running = false;
        }

        private ConsoleColor ParseColour(string Colour)
        {
            ConsoleColor colour = ConsoleColor.Black;

            for (int j = 0; j < 16; j++)
            {
                colour = (ConsoleColor)j;
                string sColour = colour.ToString();
                if (sColour == Colour) break;
            }

            return colour;
        }

        private void MakeGarage()
        {
            ui.DrawSetupMessage();
            uint nrOfSlots = GetGarageSize();
            gh = new GarageHandler(nrOfSlots);
        }

        private (string, string)[] GetProperties(string Vehicle)
        {
            string[] sProperties = Vehicle.Split(';');

            (string, string)[] props = { ("", ""), ("", ""), ("", ""), ("", ""), ("", "") };
            for (int j = 0; j < sProperties.Length - 1; j++)
                props[j] = GetProperty(sProperties[j]);

            return props;
        }

        private (string, string) GetProperty(string Property)
        {
            string[] prop = Property.Split(':');
            return (prop[0], prop[1]);
        }

        private Vehicle? CreateSpecificVehicle((string, string)[] Properties)
        {
            uint nrOfWheels;
            ConsoleColor colour;

            uint.TryParse(Properties[3].Item2, out nrOfWheels);
            colour = ParseColour(Properties[4].Item2);

            switch (Properties[0].Item2)
            {
                case "Car":
                    Car car = new Car(Properties[2].Item2, nrOfWheels, Properties[1].Item2);
                    car.Colour = colour;
                    return car;

                case "Motorcycle":
                    double cylinderVolume;
                    double.TryParse(Properties[1].Item2, out cylinderVolume);
                    Motorcycle mc = new Motorcycle(Properties[2].Item2, nrOfWheels, cylinderVolume);
                    mc.Colour = colour;
                    return mc;

                case "Bus":
                    uint nrOfSeats;
                    uint.TryParse(Properties[1].Item2, out nrOfSeats);
                    Bus bus = new Bus(Properties[2].Item2, nrOfWheels, nrOfSeats);
                    bus.Colour = colour;
                    return bus;

                case "Boat":
                    double length;
                    double.TryParse(Properties[1].Item2, out length);
                    Boat boat = new Boat(Properties[2].Item2, nrOfWheels, length);
                    boat.Colour = colour;
                    return boat;

                case "Airplane":
                    uint nrOfEngines;
                    uint.TryParse(Properties[1].Item2, out nrOfEngines);
                    Airplane airplane = new Airplane(Properties[2].Item2, nrOfWheels, nrOfEngines);
                    airplane.Colour = colour;
                    return airplane;

                default:
                    return null;

            }
        }

        private void ListAllVehicles()
        {
            ui.WriteMessage("\n List of all vehicles:");
            IVehicle[] vehicles = gh.ListAll();
            ui.DrawList(vehicles);
        }

        private void ShowVehicle()
        {
            ui.WriteMessage("\n Show a vehicles properties");
            string sRegNum = ui.Prompt("RegNum: ");
            IVehicle? vehicle = gh.FindVehicle(sRegNum);
            if (vehicle != null) ui.WriteVehicle(vehicle);
            else ui.ShowErrorMessage($"Could not find {sRegNum}");
        }

        private void RemoveAVehicle()
        {
            ui.WriteMessage("\n Remove a vehicle");
            bool success = RemoveVehicle();
            if (success) ui.WriteMessage(" Vehicle removed");
        }

        private void AddAVehicle()
        {
            ui.WriteMessage("\n Add a vehicle");
            bool success = AddVehicle();
            if (success) ui.WriteMessage(" Vehicle added");
            else ui.ShowErrorMessage(" Could not add vehicle");
        }

        private void AddRandomVehicles()
        {
            ui.WriteMessage("\n Add a number of random vehicles");
            uint size = GetPopulationSize();
            bool success = Populate(size);
            if (success) ui.WriteMessage($" {size} vehicles added");
        }


        private void QueryVehicles()
        {
            ui.WriteMessage("\n Query vehicles");

            string type = ui.Prompt(" Type: ");
            string wheels = ui.Prompt(" Nr of wheels: ");
            string colour = ui.Prompt(" Colour: ");

            IVehicle[] vehicles = gh.FindVehicles(type, wheels, colour);
            ui.DrawList(vehicles);
        }

        private void ShowStatistics()
        {
            ui.WriteMessage("\n Show garage statistics:\n" +
                              " -----------------------");

            uint capacity = gh.GarageCapacity;
            uint ocupied = gh.CountVehicles();
            uint available = capacity - ocupied;
            ui.WriteCapacity(capacity, ocupied, available);

            (Type, uint)[] typeCounts;
            typeCounts = gh.ListNrByType();
            ui.WriteTypeCount(typeCounts);
        }

        private bool RemoveVehicle()
        {
            string sRegNum;
            bool success = false;
            
            sRegNum = ui.Prompt(" RegNum: ");
            IVehicle? vehicle = gh.FindVehicle(sRegNum);
            if(vehicle != null) success = gh.RemoveVehicle(vehicle);
            else ui.ShowErrorMessage($" Could not find {sRegNum}");

            return success;
        }

        private bool AddVehicle()
        {
            bool success = false;
            string sType, sRegNum;
            uint nrOfWheels;

            do
            {
                sType = ui.Prompt(" Vehicle type: ");
                success = gh.ValidateType(sType);
                if (!success) { ui.ShowErrorMessage(" Wrong input!"); }

            } while(!success);
            
            do
            {
                sRegNum = ui.Prompt(" RegNum: ").ToUpper();
                success = gh.ValidateRegNum(sRegNum);   
                if(!success) { ui.ShowErrorMessage(" Wrong input!"); }

            } while(!success);

            do
            {
                string sNrOfWheels = ui.Prompt(" How many wheels: ");
                success = uint.TryParse(sNrOfWheels, out nrOfWheels);
                if (success) { success = gh.ValidateNrOfWheels(nrOfWheels, sType); }
                if (!success) { ui.ShowErrorMessage(" Wrong input!"); }

            } while (!success);
            
            ConsoleColor cColour = ConsoleColor.Black;
            do
            {
                string sColour = ui.Prompt(" Colour: ");
                if (sColour == "") success = true;
                else success = GarageHandler.ValiateColour(sColour, ref cColour);       // Set cColour if input is valid

            } while(!success);

            switch (sType.ToLower())
            {
                case "car":
                    string sFuelType = ui.Prompt(" Fuel type: ");
                    success = gh.AddCar(sRegNum, nrOfWheels, sFuelType, cColour);
                    break;

                case "motorcycle":
                    string sCylinderVolume = ui.Prompt(" Cylinder volume: ");
                    double cylinderVolume;
                    success = double.TryParse(sCylinderVolume, out cylinderVolume);
                    if (success) success = gh.AddMotorcycle(sRegNum, nrOfWheels, cylinderVolume, cColour);
                    break;

                case "bus":
                    string sNrOfSeats = ui.Prompt(" Number of seats: ");
                    uint nrOfSeats;
                    success = uint.TryParse(sNrOfSeats, out nrOfSeats);
                    if(success) success = gh.AddBus(sRegNum, nrOfWheels, nrOfSeats, cColour);
                    break;

                case "boat":
                    string sLength = ui.Prompt(" Length: ");
                    double length;
                    success = double.TryParse(sLength, out length);
                    if(success) success = gh.AddBoat(sRegNum, nrOfWheels, length, cColour);
                    break;

                case "airplane":
                    string sNrOfEngines = ui.Prompt(" Number of engines: ");
                    uint nrOfEngines = 0;
                    success = uint.TryParse(sNrOfEngines, out nrOfEngines);
                    if(success) success = gh.AddAirplane(sRegNum, nrOfWheels, nrOfEngines, cColour);
                    break;
            }

            return success;
        }

        private uint GetPopulationSize()
        {
            uint size;
            bool success;
            string errMsg = "";

            do
            {
                string sSize = ui.Prompt(" How many vehicles? ");
                success = uint.TryParse(sSize, out size);

                if (!success) errMsg = "Input must be a number";
                if (success && (size == 0)) { errMsg = "Size must be grater than 0"; success = false; }
                if (success && (size > gh.GarageCapacity))
                {
                    errMsg = $"Size must not be grater than garage capacity ({gh.GarageCapacity})";
                    success = false;
                } 
                if (!success) ui.ShowErrorMessage(" Wrong input! " + errMsg);

            } while (!success);

            return size;
        }

        private bool Populate(uint nrOfVehicles)
        {
            uint nrAdded;
            nrAdded = gh.Populate(nrOfVehicles);
            if(nrAdded < nrOfVehicles) ui.ShowErrorMessage($" {nrAdded} vehicles added. Garage is full.");
            return nrAdded == nrOfVehicles;
        }

        private string GetMenuChoise()
        {
            string sKey;
            string errMsg = "";
            bool success;

            do
            {
                sKey = ui.GetMenuKey();
                if (sKey.ToUpper() == "Q") return sKey.ToUpper();
                success = uint.TryParse(sKey.ToString(), out uint result);                                  // Check if numerical
                if (!success) errMsg = "Key must be a number";
                if (success && (result == 0)) { success = false; errMsg = "Key can not be 0"; }             // Check if 0
                if (success && (result > 7)) { success = false; errMsg = "Key can not be more than 7"; }    // Check if outside the menu
                if (!success) ui.ShowErrorMessage(" Wrong input! " + errMsg);

            } while (!success);

            return sKey;
        }

        private uint GetGarageSize()
        {
            uint size = 0;
            string errMsg = "";
            bool success = false;

            do
            {
                string sSize = ui.Prompt(" Enter how many parking slots: ");
                success = uint.TryParse(sSize, out size);
                if (!success) errMsg = "Input must be numerical";
                if (success && (size == 0)) { success = false; errMsg = "Size can not be 0"; }
                if (success && (size > 100)) { success = false; errMsg = "Size can not be more than 100"; }
                if(!success) ui.ShowErrorMessage(" Wrong input! " + errMsg);

            } while (!success);

            return size;
        }

    }

}
