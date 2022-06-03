using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1Garage___Övn_5
{
    internal class Bus : Vehicle
    {
        private uint nrOfSeats;

        public uint NrOfSeats
        {
            get { return nrOfSeats; }
        }

        internal Bus(string RegNum, uint NrOfwheels, uint NrOfSeats) : base(RegNum, NrOfwheels)
        {
            nrOfSeats = NrOfSeats;
        }



    }
}
