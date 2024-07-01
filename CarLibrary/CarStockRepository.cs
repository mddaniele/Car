using System.Text.Json;
using CarLibrary.Dto;

namespace CarLibrary
{
    public class CarStock
    {
        private List<Car> _cars = [];

        public void AddCar(Car car)
        {
            _cars.Add(car);
        }

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
                    _ => throw new InvalidOperationException("Unbekannte Marke")
                };
                AddCar(car);
            }
        }

        public IEnumerable<string> PrintInventory()
        {
            yield return "Marke\tJahrgang\tMax Speed";
            foreach (var car in _cars)
            {
                yield return car.ToString();
            }
        }

        public List<Car> GetInventory()
        {
            return _cars;
        }
    }
}
