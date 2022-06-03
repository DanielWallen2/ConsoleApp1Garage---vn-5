using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1Garage___Övn_5
{
   
    internal class Garage<T> : IEnumerable<T> where T : Vehicle
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

        internal Garage(uint Capacity) 
        {
            capacity = Capacity; 
            vehicles = new T[Capacity];
        }

        internal bool AddVehicle(T vehicle)
        {
            for(int i = 0; i < vehicles.Length; i++)
            {
                if(vehicles[i] == null)         // Empty place
                {
                    vehicles[i] = vehicle;
                    nrOfParkedVehicles++;
                    return true;
                }
            }

            return false;                       // Garage is full
        }

        internal bool RemoveVehicle(T vehicle)
        {
            if(vehicle == null) return false;

            for (int i = 0; i < vehicles.Length; i++)
            {
                if (vehicles[i] == vehicle)
                {
                    vehicles[i] = null!;
                    nrOfParkedVehicles--;
                    return true;
                }
            }
            return false;

            //Vehicle? v2 = null;

            //foreach (T v in vehicles)
            //{
            //    if (v == vehicle)
            //    {
            //        v2 = v;
            //        break;
            //    }
            //}

            //if (v2 != null)
            //{
            //    v2 = null;
            //    nrOfParkedVehicles--;
            //    return true;
            //}

            //return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (T vehicle in vehicles)
            {
                if(vehicle != null)             // Du vill bara returnera faktiska instanser av fordon!
                    yield return vehicle;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
