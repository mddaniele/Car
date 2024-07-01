namespace CarLibrary.Interfaces
{
    public interface ICarStockRepository
    {
        void AddCar(Car car);
        List<Car> GetInventory();
        void LoadCarsFromJson(string filePath);
    }
}