using NSubstitute;
using Xunit;

namespace CarLibrary.Test
{
    public class CarStockTests
    {
        [Fact]
        public void AddCar_ShouldAddCarToInventory()
        {
            // Arrange
            var carStock = new CarStock();
            var ford = new Ford(1964);

            // Act
            carStock.AddCar(ford);

            // Assert
            Assert.Contains(ford, carStock.GetInventory());
        }

        [Fact]
        public void AddCar_ShouldThrowExceptionIfNewerThanToday()
        {
            // Arrange
            var carStock = new CarStock();
            var year = DateTime.Today.AddYears(1).Year;
            
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Ford(year));
        }

        [Fact]
        public void AddCar_ShouldThrowExceptionIfOlderThan1885()
        {
            // Arrange
            var carStock = new CarStock();
            var year = 1884;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Ford(year));
        }

        [Fact]
        public void PrintInventory_ShouldOutputCorrectFormat()
        {
            // Arrange
            var carStock = new CarStock();
            var ford = new Ford(1964);
            var vw = new VW(1983);
            carStock.AddCar(ford);
            carStock.AddCar(vw);

            // Act
            var output = carStock.PrintInventory();

            // Assert
            Assert.Contains("Marke\tJahrgang\tMax Speed", output);
            Assert.Contains("Ford\t1964\t250 km/h", output);
            Assert.Contains("VW\t1983\t180 km/h", output);
        }

        [Fact]
        public void Car_ShouldHaveFourWheelsAtInitialization()
        {
            // Arrange
            var ford = new Ford(2020);

            // Act
            var wheels = ford.Wheels;

            // Assert
            Assert.Equal(4, wheels.Count);
            Assert.Contains("Vorne rechts", wheels);
            Assert.Contains("Vorne links", wheels);
            Assert.Contains("Hinten rechts", wheels);
            Assert.Contains("Hinten links", wheels);
        }

        [Fact]
        public void AddWheel_ShouldThrowExceptionIfMoreThanFourWheels()
        {
            // Arrange
            var ford = new Ford(2020);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => ford.AddWheel("Ersatzrad"));
        }

        [Fact]
        public void RemoveWheel_ShouldRemoveWheel()
        {
            // Arrange
            var ford = new Ford(2020);

            // Act
            ford.RemoveWheel("Vorne rechts");

            // Assert
            Assert.Equal(3, ford.Wheels.Count);
            Assert.DoesNotContain("Vorne rechts", ford.Wheels);
        }

        [Fact]
        public void LoadCarsFromJson_ShouldAddCarsToInventory()
        {
            // Arrange
            var carStock = new CarStock();
            var json = "[{\"Brand\":\"Ford\",\"Year\":1964},{\"Brand\":\"VW\",\"Year\":1983}]";
            var filePath = "test_cars.json";
            File.WriteAllText(filePath, json);

            // Act
            carStock.LoadCarsFromJson(filePath);

            // Assert
            var inventory = carStock.GetInventory();
            Assert.Equal(2, inventory.Count);
            Assert.IsType<Ford>(inventory[0]);
            Assert.IsType<VW>(inventory[1]);
            File.Delete(filePath);
        }

        [Fact]
        public void LoadCarsFromJson_ShouldThrowExceptionIfUnkownBrand()
        { 
            // Arrange
            var carStock = new CarStock();
            var json = "[{\"Brand\":\"Skoda\",\"Year\":2022}]";
            var filePath = "test_cars.json";
            File.WriteAllText(filePath, json);
            
            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => carStock.LoadCarsFromJson(filePath));
            File.Delete(filePath);
        }
    }
}