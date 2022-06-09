using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1Garage___Övn_5
{

    public class Garage<T> : /*IEnumerable<T>,*/ IGarage<T> where T : IVehicle
    {
        private T[] vehicles;

        private uint capacity;
        private uint nrOfParkedVehicles;

        public uint Capacity
        {
            get { return capacity; }
        }
        public uint NrOfParkedVehicles
        {
            get { return nrOfParkedVehicles; }
        }

        public Garage(uint Capacity)
        {
            capacity = Capacity;
            vehicles = new T[Capacity];
        }

        public bool AddVehicle(T vehicle)
        {
            for (int i = 0; i < vehicles.Length; i++)
            {
                if (vehicles[i] == null)         // Empty place
                {
                    vehicles[i] = vehicle;
                    nrOfParkedVehicles++;
                    return true;
                }
            }

            return false;                       // Garage is full
        }

        public bool RemoveVehicle(T vehicle)
        {
            if (vehicle == null) return false;

            for (int i = 0; i < vehicles.Length; i++)
            {
                if ((IVehicle)vehicles[i] == (IVehicle)vehicle)
                {
                    vehicles[i] = default(T);
                    nrOfParkedVehicles--;
                    return true;
                }
            }
            return false;

        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (T vehicle in vehicles)
            {
                if (vehicle != null)             // Du vill bara returnera faktiska instanser av fordon!
                    yield return vehicle;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
