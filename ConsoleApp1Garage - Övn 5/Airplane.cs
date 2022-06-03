using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1Garage___Övn_5
{
    internal class Airplane : Vehicle
    {
        private uint nrOfEngines;

        public uint NrOfEngines
        {
            get { return nrOfEngines; }
            set { nrOfEngines = value; }
        }

        internal Airplane(string RegNum, uint NrOfwheels, uint NrOfEngines) : base(RegNum, NrOfwheels)
        {
            nrOfEngines = NrOfEngines;
        }

    }
}
