namespace ConsoleApp1Garage___Övn_5
{
    internal class Manager
    {
        private ConsolUI ui;
        private GarageHandler gh;
        private bool running = true;

        internal Manager(ConsolUI consolUi)
        {
            ui = consolUi;
        }

        internal void Setup()
        {
            ui.DrawSetupMessage();
            ui.Prompt("Enter how many parking slots: ");
            uint nrOfSlots =  GetGarageSize();
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
            bool success = false;

            string choise = GetMenuChoise();
            switch (choise)
            {
                case "1":
                    ui.WriteMessage("\nAdd a number of random vehicles");
                    uint size = GetPopulationSize();
                    success = Populate(size);
                    if(success) ui.WriteMessage($"{size} vehicles added");
                    break;

                case "2":
                    ui.WriteMessage("\nAdd a vehicle");
                    success = AddVehicle();
                    if (success) ui.WriteMessage("Vehicle added");
                    else ui.ShowErrorMessage("Could not add vehicle");
                    break;

                case "3":
                    ui.WriteMessage("\nRemove a vehicle");
                    success = RemoveVehicle();
                    if(success) ui.WriteMessage("Vehicle removed");
                    break;

                case "4":
                    ui.WriteMessage("\nShow a vehicles properties");
                    ShowVehicle();
                    break;

                case "5":
                    ui.WriteMessage("\nList of all vehicles:");
                    ListAllVehicles();
                    break;

                case "6":
                    ui.WriteMessage("\nShow garage statistics:");
                    ShowStatistics();
                    break;

                case "7":
                    ui.WriteMessage("\nQuery vehicles");
                    QueryVehicles();
                    break;

                case "Q":
                    //Environment.Exit(0);
                    running = false;
                    break;

                default:
                    break;
            }




        }

        private void QueryVehicles()
        {
            string type = ui.GetInput();
            string wheels = ui.GetInput();
            string colour = ui.GetInput();

            Vehicle[] vehicles = gh.FindVehicles(type, wheels, colour);
            ui.DrawList(vehicles);
        }

        private void ListAllVehicles()
        {
            Vehicle[] vehicles = gh.ListAll();
            ui.DrawList(vehicles);
        }

        private void ShowStatistics()
        {
            uint capacity = gh.GarageCapacity;
            uint ocupied = gh.CountVehicles();
            uint available = capacity - ocupied;
            ui.WriteCapacity(capacity, ocupied, available);

            (Type, uint)[] typeCounts;
            typeCounts = gh.ListNrByType();
            ui.WriteTypeCount(typeCounts);
        }

        private bool ShowVehicle()
        {
            string sRegNum;
            bool success;

            ui.Prompt("RegNum: ");
            sRegNum = ui.GetInput();
            Vehicle? vehicle = gh.FindVehicle(sRegNum);
            success = (vehicle != null);
            if (success) ui.WriteVehicle(vehicle);
            else ui.ShowErrorMessage($"Could not find {sRegNum}");

            return success;
        }

        private bool RemoveVehicle()
        {
            string sRegNum = "";
            bool success = false;

            ui.Prompt("RegNum: ");
            sRegNum = ui.GetInput();
            Vehicle? vehicle = gh.FindVehicle(sRegNum);
            if(vehicle != null) success = gh.RemoveVehicle(vehicle);
            else ui.ShowErrorMessage($"Could not find {sRegNum}");

            return success;
        }

        private bool AddVehicle()
        {
            ui.Prompt("Vehicle type: ");
            string sType = "";
            bool typeOk = false;
            do
            {
                sType = ui.GetInput();
                typeOk = gh.ValidateType(sType);
                if (!typeOk) { ui.ShowErrorMessage("Wrong input!"); }

            } while(!typeOk);

            ui.Prompt("RegNum: ");
            string sRegNum = "";
            bool regNumOk = false;
            do
            {
                sRegNum = ui.GetInput();
                regNumOk = gh.ValidateRegNum(sRegNum);   
                if(!regNumOk) { ui.ShowErrorMessage("Wrong input!"); }

            } while(!regNumOk);

            ui.Prompt("How many wheels: ");
            uint nrOfWheels = 0;
            bool succes = false;
            do
            {
                string sNrOfWheels = ui.GetInput();
                succes = uint.TryParse(sNrOfWheels, out nrOfWheels);
                if (succes)
                    succes = gh.ValidateNrOfWheels(nrOfWheels, sType);

                if (!succes) { ui.ShowErrorMessage("Wrong input!"); }

            } while (!succes);

            ui.Prompt("Colour: ");
            ConsoleColor cColour = ConsoleColor.Black;
            string sColour = ui.GetInput();
            if(sColour != "") gh.ValiateColour(sColour, ref cColour);       // Set cColour if input valid

            bool success = false;
            switch (sType)
            {
                case "Car":
                    ui.Prompt("Fuel type: ");
                    string sFuelType = ui.GetInput();
                    success = gh.AddCar(sRegNum, nrOfWheels, sFuelType, cColour);
                    break;

                case "Motorcycle":
                    ui.Prompt("Cylinder volume: ");
                    double cylinderVolume = 0;
                    string sCylinderVolume = ui.GetInput();
                    success = double.TryParse(sCylinderVolume, out cylinderVolume);
                    if (success) success = gh.AddMotorcycle(sRegNum, nrOfWheels, cylinderVolume, cColour);
                    break;

                case "Bus":
                    ui.Prompt("Number of seats: ");
                    uint nrOfSeats = 0;
                    string sNrOfSeats = ui.GetInput();
                    success = uint.TryParse(sNrOfSeats, out nrOfSeats);
                    if(success) success = gh.AddBus(sRegNum, nrOfWheels, nrOfSeats, cColour);
                    break;

                case "Boat":
                    ui.Prompt("Length: ");
                    double length = 0;
                    string sLength = ui.GetInput();
                    success = double.TryParse(sLength, out length);
                    if(success) success = gh.AddBoat(sRegNum, nrOfWheels, length, cColour);
                    break;

                case "Airplane":
                    ui.Prompt("Number of engines: ");
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

            ui.Prompt("How many vehicles? ");
            do
            {
                string sSize = ui.GetInput();
                success = uint.TryParse(sSize, out size);
                if (!success) errMsg = "Input must be a number";
                if (success && (size == 0))
                {
                    errMsg = "Size must be grater than 0";
                    success = false;
                }
                if (success && (size > gh.GarageCapacity))
                {
                    errMsg = $"Size must not be grater than garage capacity ({gh.GarageCapacity})";
                    success = false;
                } 
                if (!success) ui.ShowErrorMessage("Wrong input! " + errMsg);

            } while (!success);

            return size;
        }

        private bool Populate(uint nrOfVehicles)
        {
            uint nrAdded = 0;
            nrAdded = gh.Populate(nrOfVehicles);
            if(nrAdded < nrOfVehicles) ui.ShowErrorMessage($"{nrAdded} vehicles added. Garage is Full.");

            return nrAdded == nrOfVehicles;
        }

        private string GetMenuChoise()
        {
            string sKey = "";

            bool succes = false;
            do
            {
                sKey = ui.GetMenuKey();
                if (sKey.ToUpper() == "Q") return sKey.ToUpper();
                uint result = 0; 
                succes = uint.TryParse(sKey.ToString(), out result);            // Check if numerical
                if (succes && (result == 0 || result > 7)) succes = false;      // Check if outside the menu
                if (!succes) ui.ShowErrorMessage("Wrong input!");

            } while (!succes);

            return sKey;
        }

        private uint GetGarageSize()
        {
            uint size = 0;
            bool succes = false;
            do
            {
                string sSize = ui.GetInput();
                succes = uint.TryParse(sSize, out size);
                if(succes && (size == 0 || size > 100)) succes = false;
                if(!succes) ui.ShowErrorMessage("Wrong input!");

            } while (!succes);

            return size;
        }

    }

}
