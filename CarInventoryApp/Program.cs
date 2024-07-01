using CarLibrary;
using CarLibrary.Interfaces;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static void Main(string[] args)
    {
        var services = new ServiceCollection()
            .AddScoped<ICarStockService, CarStockService>()
            .AddScoped<ICarStockRepository, CarStockRepository>()
            .BuildServiceProvider();

        var carstockService = services.GetService<ICarStockService>();

        // Autos aus JSON-Datei laden
        carstockService.LoadCarsFromJson("cars.json");

        // Inventurliste ausgeben
        Console.WriteLine(carstockService.PrintInventory());
    }
}