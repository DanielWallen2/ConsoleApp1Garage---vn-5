namespace ConsoleApp1Garage___Övn_5
{
    public interface IVehicle
    {
        uint NrOfwheels { get; }
        string RegNum { get; }
        ConsoleColor Colour { get; set; }
    }
}