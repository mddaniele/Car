namespace CarLibrary.Interfaces
{
    public interface ICarStockService
    {
        IEnumerable<string> GetPrintableLines();
        string PrintInventory();
        void LoadCarsFromJson(string filePath);
    }
}