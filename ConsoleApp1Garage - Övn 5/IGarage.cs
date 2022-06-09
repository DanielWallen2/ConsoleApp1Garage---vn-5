
namespace ConsoleApp1Garage___Övn_5
{
    public interface IGarage<T> : IEnumerable<T>  where T : IVehicle
    {
        uint Capacity { get; }
        uint NrOfParkedVehicles { get; }

        IEnumerator<T> GetEnumerator();
        bool RemoveVehicle(T vehicle);
        bool AddVehicle(T vehicle);

    }
}