using CarLibrary.Interfaces;

namespace CarLibrary
{
    public class CarStockService(ICarStockRepository carStockRepository) : ICarStockService
    {
        private readonly ICarStockRepository _carStockRepository = carStockRepository;

        public string PrintInventory()
        {
            var lines = GetPrintableLines();
            var writer = new StringWriter();
            foreach (var line in lines)
            {
                writer.WriteLine(line);
            }

            return writer.ToString();
        }

        public IEnumerable<string> GetPrintableLines()
        {
            yield return "Marke\tJahrgang\tMax Speed";
            foreach (var car in _carStockRepository.GetInventory())
            {
                yield return car.ToString();
            }
        }

        public void LoadCarsFromJson(string filePath)
        {
            _carStockRepository.LoadCarsFromJson(filePath);
        }
    }
}
