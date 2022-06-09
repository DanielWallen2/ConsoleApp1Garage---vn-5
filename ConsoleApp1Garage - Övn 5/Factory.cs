using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1Garage___Övn_5
{
    internal class Factory
    {
        public Garage<IVehicle> MakeGarage(uint Capacity)
        {
            return new Garage<IVehicle>(Capacity);
        }
    }
}
