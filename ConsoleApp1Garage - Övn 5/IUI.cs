
namespace ConsoleApp1Garage___Övn_5
{
    public interface IUI
    {
        void DrawList(IVehicle[] vehicles);
        void DrawMainMenu();
        void DrawSetupMessage();
        string GetMenuKey();
        void Pause();
        string Prompt(string Message);
        void ShowErrorMessage(string errorMessage);
        void WriteCapacity(uint Capacity, uint Ocupied, uint Available);
        void WriteMessage(string Message);
        void WriteTypeCount((Type, uint)[] typeCounts);
        void WriteVehicle(IVehicle vehicle);
    }
}