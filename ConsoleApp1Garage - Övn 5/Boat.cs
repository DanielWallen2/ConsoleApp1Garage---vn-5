using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1Garage___Övn_5
{
    internal class Boat : Vehicle
    {
        private double lenght;

        public double Lenght
        {
            get { return lenght; }
            set { lenght = value; }
        }

        internal Boat(string RegNum, uint NrOfwheels, double Lenght) : base(RegNum, NrOfwheels) 
        {
            lenght = Lenght;
        }

    }
}
