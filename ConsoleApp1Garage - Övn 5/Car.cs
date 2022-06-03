using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1Garage___Övn_5
{
    internal class Car : Vehicle
    {
        private string fuelType;

        public string FuelType
        {
            get { return fuelType; }
        }

        internal Car(string RegNum, uint NrOfwheels, string FuelType) : base(RegNum, NrOfwheels)
        {
            fuelType = FuelType;
        }
    }
}
