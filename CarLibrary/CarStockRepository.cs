using System.Text.Json;
using CarLibrary.Dto;
using CarLibrary.Interfaces;

namespace CarLibrary
{
    public class CarStockRepository : ICarStockRepository
    {
        private List<Car> _cars = [];

        public void AddCar(Car car)
        {
            _cars.Add(car);
        }

        // loading from json for convenience reasons
        public void LoadCarsFromJson(string filePath)
        {
            var json = File.ReadAllText(filePath);
            var carDtos = JsonSerializer.Deserialize<List<CarDto>>(json);

            foreach (var carDto in carDtos)
            {
                Car car = carDto.Brand switch
                {
                    "Ford" => new Ford(carDto.Year),
                    "VW" => new VW(carDto.Year),
                    _ => throw new InvalidOperationException("Unbekannte Marke") //assumption: unkown brands should lead to an exception
                };
                AddCar(car);
            }
        }

        public List<Car> GetInventory()
        {
            return _cars;
        }
    }
}
