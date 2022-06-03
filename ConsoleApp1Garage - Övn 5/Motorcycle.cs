using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1Garage___Övn_5
{
    internal class Motorcycle : Vehicle
    {
        private double cylinderVolume;

        public double CylinderVolume
        {
            get { return cylinderVolume; }
        }

        internal Motorcycle(string RegNum, uint NrOfwheels, double CylinderVolume) : base(RegNum, NrOfwheels)
        {
            cylinderVolume = CylinderVolume;
        }

    }
}
