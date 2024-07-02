namespace CarLibrary.Interfaces
{
    public interface ICarStockService
    {
        string PrintInventory();
        void LoadCarsFromJson(string filePath);
    }
}