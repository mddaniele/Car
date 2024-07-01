using CarLibrary;

class Program
{
    static void Main(string[] args)
    {
        var carstock = new CarStock();

        // Autos aus JSON-Datei laden
        carstock.LoadCarsFromJson("cars.json");

        // Inventurliste ausgeben
        foreach (var line in carstock.PrintInventory())
        {
            Console.WriteLine(line);
        }
    }
}