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

            if (fileExist)
            {
                string[] sVehicles = File.ReadAllLines(path);

                for(int i = 0; i < sVehicles.Length; i++)
                {
                    string[] sVehicle = sVehicles[i].Split(';');

                    string[] prop;
                    string sType = "";
                    for (int j = 0; j < sVehicle.Length - 1; j++)
                    {
                        prop = sVehicle[j].Split(':');
                        if (j == 0) sType = prop[1];
                    }
                    
                    switch (sType)
                    {
                        case "car":
                            
                            //Car car = new Car();
                            break;
                    }

                }
            }
            else
            {
                if(!Directory.Exists(garageDirectory))
                    Directory.CreateDirectory(garageDirectory);

                MakeGarage();
            }

        }

        private void MakeGarage()
        {
            ui.DrawSetupMessage();
            ui.Prompt(" Enter how many parking slots: ");
            uint nrOfSlots = GetGarageSize();
            gh = new GarageHandler(nrOfSlots);
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


            //switch (choise)
            //{
            //    case "1":
            //        AddRandomVehicles();
            //        break;

            //    case "2":
            //        AddAVehicle();
            //        break;

            //    case "3":
            //        RemoveAVehicle();
            //        break;

            //    case "4":
            //        ShowVehicle();
            //        break;

            //    case "5":
            //        ListAllVehicles();
            //        break;

            //    case "6":
            //        ShowStatistics();
            //        break;

            //    case "7":
            //        QueryVehicles();
            //        break;

            //    case "Q":
            //        Quit();
            //        break;

            //    default:
            //        break;
            //}

        }

        private void Quit()
        {
            gh.SaveGarage(path);

            running = false;
            //Environment.Exit(0);
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
            ui.Prompt("RegNum: ");
            string sRegNum = ui.GetInput();
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

            ui.Prompt(" Type: ");
            string type = ui.GetInput();

            ui.Prompt(" Nr of wheels: ");
            string wheels = ui.GetInput();

            ui.Prompt(" Colour: ");
            string colour = ui.GetInput();

            IVehicle[] vehicles = gh.FindVehicles(type, wheels, colour);
            ui.DrawList(vehicles);
        }

        private void ShowStatistics()
        {
            ui.WriteMessage("\n Show garage statistics:");

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

            ui.Prompt(" RegNum: ");
            sRegNum = ui.GetInput();
            IVehicle? vehicle = gh.FindVehicle(sRegNum);
            if(vehicle != null) success = gh.RemoveVehicle(vehicle);
            else ui.ShowErrorMessage($" Could not find {sRegNum}");

            return success;
        }

        private bool AddVehicle()
        {
            ui.Prompt(" Vehicle type: ");
            string sType;
            bool typeOk;
            do
            {
                sType = ui.GetInput();
                typeOk = gh.ValidateType(sType);
                if (!typeOk) { ui.ShowErrorMessage(" Wrong input!"); }

            } while(!typeOk);

            ui.Prompt(" RegNum: ");
            string sRegNum;
            bool regNumOk;
            do
            {
                sRegNum = ui.GetInput().ToUpper();
                regNumOk = gh.ValidateRegNum(sRegNum);   
                if(!regNumOk) { ui.ShowErrorMessage(" Wrong input!"); }

            } while(!regNumOk);

            ui.Prompt(" How many wheels: ");
            uint nrOfWheels;
            bool wheelsOK;
            do
            {
                string sNrOfWheels = ui.GetInput();
                wheelsOK = uint.TryParse(sNrOfWheels, out nrOfWheels);
                if (wheelsOK) { wheelsOK = gh.ValidateNrOfWheels(nrOfWheels, sType); }
                if (!wheelsOK) { ui.ShowErrorMessage(" Wrong input!"); }

            } while (!wheelsOK);

            ui.Prompt(" Colour: ");
            ConsoleColor cColour = ConsoleColor.Black;
            bool colourOK;
            do
            {
                string sColour = ui.GetInput();
                if (sColour == "") colourOK = true;
                else colourOK = GarageHandler.ValiateColour(sColour, ref cColour);       // Set cColour if input is valid

            } while(!colourOK);

            bool success = false;
            switch (sType.ToLower())
            {
                case "car":
                    ui.Prompt(" Fuel type: ");
                    string sFuelType = ui.GetInput();
                    success = gh.AddCar(sRegNum, nrOfWheels, sFuelType, cColour);
                    break;

                case "motorcycle":
                    ui.Prompt(" Cylinder volume: ");
                    double cylinderVolume = 0.8;
                    string sCylinderVolume = ui.GetInput();
                    success = double.TryParse(sCylinderVolume, out cylinderVolume);
                    if (success) success = gh.AddMotorcycle(sRegNum, nrOfWheels, cylinderVolume, cColour);
                    break;

                case "bus":
                    ui.Prompt(" Number of seats: ");
                    uint nrOfSeats = 0;
                    string sNrOfSeats = ui.GetInput();
                    success = uint.TryParse(sNrOfSeats, out nrOfSeats);
                    if(success) success = gh.AddBus(sRegNum, nrOfWheels, nrOfSeats, cColour);
                    break;

                case "boat":
                    ui.Prompt(" Length: ");
                    double length = 0;
                    string sLength = ui.GetInput();
                    success = double.TryParse(sLength, out length);
                    if(success) success = gh.AddBoat(sRegNum, nrOfWheels, length, cColour);
                    break;

                case "airplane":
                    ui.Prompt(" Number of engines: ");
                    uint nrOfEngines = 0;
                    string sNrOfEngines = ui.GetInput();
                    success = uint.TryParse(sNrOfEngines, out nrOfEngines);
                    if(success) success = gh.AddAirplane(sRegNum, nrOfWheels, nrOfEngines, cColour);
                    break;
            }

            return success;
        }

        private uint GetPopulationSize()
        {
            uint size = 0;
            bool success = false;
            string errMsg = "";

            ui.Prompt(" How many vehicles? ");
            do
            {
                string sSize = ui.GetInput();
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
                string sSize = ui.GetInput();
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
