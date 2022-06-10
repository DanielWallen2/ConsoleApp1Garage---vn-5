using System;
using System.Linq;
using Xunit;

namespace ConsoleApp1Garage___Övn_5.Test
{
    public class GarageTest
    {
        [Fact]
        public void GarageCapacityTest()
        {
            //Arrange
            Garage<Vehicle> testGarage = new Garage<Vehicle>(3);

            //Assert
            Assert.True(3 == testGarage.Capacity);
        }

        [Fact]
        public void GarageIsEmptyTest()
        {
            //Arrange
            Garage<Vehicle> testGarage = new Garage<Vehicle>(1);

            //Assert
            Assert.True(testGarage.IsEmpty);

        }

        [Fact]
        public void GarageIsFullTest()
        {
            //Arrange
            Garage<Vehicle> testGarage = new Garage<Vehicle>(2);
            Car testCar1 = new Car("ABC123", 4, "Gasoline");
            Car testCar2 = new Car("DEF456", 4, "Gasoline");

            //Act
            testGarage.AddVehicle(testCar1);
            testGarage.AddVehicle(testCar2);

            //Assert
            Assert.True(testGarage.IsFull);
        }

        [Fact]
        public void AddVehicleTest()
        {
            //Arrange
            Garage<Vehicle> testGarage = new Garage<Vehicle>(3);
            Car testCar = new Car("ABC123", 4, "Gasoline");
            bool result;

            //Act
            result = testGarage.AddVehicle(testCar);

            //Assert
            Assert.True(result);

        }

        [Fact]
        public void AddNullVehicleTest()
        {
            //Arrange
            Garage<Vehicle> testGarage = new Garage<Vehicle>(3);
            bool result;

            //Act
            result = testGarage.AddVehicle(null);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void AddFullGarageTest()
        {
            //Arrange
            Garage<Vehicle> testGarage = new Garage<Vehicle>(1);
            Car testCar1 = new Car("ABC123", 4, "Gasoline");
            Car testCar2 = new Car("DEF456", 4, "Gasoline");
            testGarage.AddVehicle(testCar1);
            bool result;

            //Act
            result = testGarage.AddVehicle(testCar2);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void AddSameObjectTest()
        {
            //Arrange
            Garage<Vehicle> testGarage = new Garage<Vehicle>(3);
            Car testCar = new Car("ABC123", 4, "Gasoline");
            testGarage.AddVehicle(testCar);
            bool result;

            //Act
            result = testGarage.AddVehicle(testCar);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void RemoveVehicleTest()
        {
            //Arrange
            Garage<Vehicle> testGarage = new Garage<Vehicle>(3);
            Car testCar1 = new Car("ABC123", 4, "Gasoline");
            Car testCar2 = new Car("DEF456", 4, "Gasoline");
            testGarage.AddVehicle(testCar1);
            testGarage.AddVehicle(testCar2);
            bool result;

            //Act
            result = testGarage.RemoveVehicle(testCar1);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void RemoveNullVehicle()
        {
            //Arrange
            Garage<Vehicle> testGarage = new Garage<Vehicle>(3);
            bool result;

            //Act
            result = testGarage.RemoveVehicle(null);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void RemoveEmptyGarageTest()
        {
            //Arrange
            Garage<Vehicle> testGarage = new Garage<Vehicle>(3);
            Car testCar = new Car("ABC123", 4, "Gasoline");
            bool result;

            //Act
            result = testGarage.RemoveVehicle(testCar);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void GetEnumeratorCountTest()
        {
            //Arrange
            Garage<Vehicle> testGarage = new Garage<Vehicle>(3);
            Car testCar1 = new Car("ABC123", 4, "Gasoline");
            Car testCar2 = new Car("DEF456", 4, "Gasoline");
            testGarage.AddVehicle(testCar1);
            testGarage.AddVehicle(testCar2);

            //Act
            var actualCount = testGarage.Count();

            //Assert
            Assert.Equal(2, actualCount);

        }

        [Fact]
        public void GetEnumeratorFirstVehicleTest()
        {
            //Arrange
            Garage<Vehicle> testGarage = new Garage<Vehicle>(3);
            Car testCar1 = new Car("ABC123", 4, "Gasoline");
            Car testCar2 = new Car("DEF456", 4, "Gasoline");
            testGarage.AddVehicle(testCar1);
            testGarage.AddVehicle(testCar2);

            //Act
            Vehicle actual1st = testGarage.First();

            //Assert
            Assert.Equal(testCar1, actual1st);
            //Assert.IsType<Car>(actualVehicles);
        }

        [Fact]
        public void GetEnummeratorNullTest()
        {
            //Arrange
            Garage<Vehicle> testGarage = new Garage<Vehicle>(3);
            Car testCar1 = new Car("ABC123", 4, "Gasoline");
            Car testCar2 = new Car("DEF456", 4, "Gasoline");
            testGarage.AddVehicle(testCar1);
            testGarage.AddVehicle(testCar2);
            testGarage.RemoveVehicle(testCar1);

            //Act
            Vehicle actual = testGarage.ElementAt(0);

            //Assert
            Assert.True(actual != null);
        }

    }
}
