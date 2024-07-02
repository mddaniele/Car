using CarLibrary.Interfaces;
using NSubstitute;
using Xunit;

namespace CarLibrary.Test
{
    public class CarStockTests
    {
        #region Car Tests
        [Fact]
        public void Car_ShouldHaveFourWheelsAtInitialization()
        {
            // Arrange
            var ford = new Ford(1885);

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
        public void Car_AddWheel_ShouldThrowExceptionIfMoreThanFourWheels()
        {
            // Arrange
            var vw = new VW(2024);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => vw.AddWheel("Ersatzrad"));
        }

        [Fact]
        public void Car_RemoveWheel_ShouldRemoveWheel()
        {
            // Arrange
            var vw = new VW(2024);

            // Act
            vw.RemoveWheel("Vorne rechts");

            // Assert
            Assert.Equal(3, vw.Wheels.Count);
            Assert.DoesNotContain("Vorne rechts", vw.Wheels);
        }

        [Fact]
        public void Car_ShouldThrowExceptionIfNewerThanToday()
        {
            // Arrange
            var year = DateTime.Today.AddYears(1).Year;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Ford(year));
        }

        [Fact]
        public void Car_ShouldThrowExceptionIfOlderThan1885()
        {
            // Arrange
            var year = 1884;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Ford(year));
        }
        #endregion

        #region CarStockRepository Tests
        [Fact]
        public void CarStockRepository_AddCar_ShouldAddCarToInventory()
        {
            // Arrange
            var carStock = new CarStockRepository();
            var ford = new Ford(1964);

            // Act
            carStock.AddCar(ford);

            // Assert
            Assert.Contains(ford, carStock.GetInventory());
        }

        [Fact]
        public void CarStockRepository_LoadCarsFromJson_ShouldAddCarsToInventory()
        {
            // Arrange
            var carStock = new CarStockRepository();
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
        public void CarStockRepository_LoadCarsFromJson_ShouldThrowExceptionIfUnknownBrand()
        {
            // Arrange
            var carStock = new CarStockRepository();
            var json = "[{\"Brand\":\"Skoda\",\"Year\":2022}]";
            var filePath = "test_cars.json";
            File.WriteAllText(filePath, json);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => carStock.LoadCarsFromJson(filePath));
            File.Delete(filePath);
        }

        [Fact]
        public void CarStockRepository_LoadCarsFromJson_ShouldThrowExceptionIfEmpttyJson()
        {
            // Arrange
            var carStock = new CarStockRepository();
            var json = "[]";
            var filePath = "test_cars.json";
            File.WriteAllText(filePath, json);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => carStock.LoadCarsFromJson(filePath));
            File.Delete(filePath);
        }
        #endregion

        #region CarStockService Tests
        [Fact]
        public void CarStockService_PrintInventory_ShouldOutputCorrectFormat()
        {
            // Arrange
            var repo = Substitute.For<ICarStockRepository>();
            repo.GetInventory().Returns([new VW(1983), new Ford(1964)]);

            var carStockService = new CarStockService(repo);

            // Act
            var output = carStockService.PrintInventory();

            // Assert
            Assert.Contains("Marke\tJahrgang\tMax Speed", output);
            Assert.Contains("Ford\t1964\t250 km/h", output);
            Assert.Contains("VW\t1983\t180 km/h", output);
        }
        #endregion
    }
}