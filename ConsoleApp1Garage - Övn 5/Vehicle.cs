using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1Garage___Övn_5
{
    internal class Vehicle : IVehicle
    {
        private string regnum;
        private uint nrOfwheels;
        private ConsoleColor colour;

        public string RegNum
        {
            get { return regnum; }
        }
        public uint NrOfwheels
        {
            get => nrOfwheels;
        }
        public ConsoleColor Colour
        {
            get { return colour; }
            set { colour = value; }
        }

        internal Vehicle(string RegNum, uint NrOfwheels)
        {
            regnum = RegNum;
            nrOfwheels = NrOfwheels;
        }

    }
}
