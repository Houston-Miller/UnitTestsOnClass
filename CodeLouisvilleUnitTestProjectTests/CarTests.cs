using CodeLouisvilleUnitTestProject;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit.Abstractions;

namespace CodeLouisvilleUnitTestProjectTests
{
    public class CarTests
    {

        //Constructor: verify that newly created Car instances are also Vehicles
        //and have 4 tires
        [Fact]
        public void VehicleParameterlessConstructorTest()
        {
            Car car = new Car();

            using (new AssertionScope())
            {
                car.Numberoftires.Should().Be(4);
                car.Should().BeAssignableTo<Vehicle>();
            }
        }

        //Test that a Make of Honda and a Model of Civic is valid.
        //Test that a Make of Honda and a Model of Camry is not.
        //You may use two Facts or one Theory for this test.
        [Fact]
        public void IsValidModelForMakeAsyncPositive()
        {
            //arrange
            Car car = new Car(12, "Honda", "Civic", 20);
            //act
            Task<bool> validModel = car.IsValidModelForMakeAsync();
            //assert
            validModel.Should().Be(true);
        }
        [Fact]
        public void IsValidModelForMakeAsyncNegative()
        {
            throw new NotImplementedException();
        }

        //Negative Test: Test that passing a value for
        //year that is before 1995 throws a System.ArgumentException.
        [Fact]
        public async void WasModelMadeInYearAsyncNegative()
        {
            //arrange
            Car car = new Car(12, "Honda", "Civic", 20);
            //act
            Func<Task> result = async () => { await car.WasModelMadeInYearAsync(1990); };
            //assert
            await result.Should().ThrowAsync<ArgumentException>();
            
        }

        //Positive Tests: Test that each of these values return the expected result(using a Theory would be a good idea):
        //A Make that does not exist at all returns false (regardless of model/year).
        //Make Honda, Model Camry returns false (regardless of year).
        //Make Subaru, Model WRX returns true for year 2020.
        //Make Subaru, Model WRX returns false for year 2000.
        [Theory]
        [InlineData("Honda", "Camry", 2035, false)]
        [InlineData("Subaru", "WRX", 2020, true)]
        [InlineData("Subaru", "WRX", 2000, false)]
        public void WasModelMadeInYearAsyncPositive(string make, string model, int year, bool returnResult)
        {
            //arrange
            Car car = new Car(12, make, model, 20);

            //act
            Task<bool> result = car.WasModelMadeInYearAsync(year);
            //assert
            result.Should().Be(returnResult);
        }

        //Test that adding passengers to the car reduces the fuel economy of the car by .2 per passenger.
        //Test that removing the passengers then adds back the fuel economy.
        [Fact]
        public void AddPassengersReducesFuelEconomy()
        {
            //arrange
            Car car = new Car(12, "Honda", "Civic", 20);
            //act
            car.AddPassengers(5);
            //assert
            car.MilesPerGallon.Should().Be(19);
        }
        [Fact]
        public void RemovePassengersRestoresFuelEconomy()
        {
            //arrange
            Car car = new Car(12, "Honda", "Civic", 20);
            car.AddPassengers(5);
            //act
            car.RemovePassengers(5);
            //assert
            car.MilesPerGallon.Should().Be(20);
        }

        //Using a Theory, test the following:
        //Create a Car with 5 passengers that gets 21 MPG.Remove 3 passengers from the car.
        //Verify the car now has 2 passengers and gets 20.6 MPG.
        //Create a Car with 5 passengers that gets 21 MPG.Remove 5 passengers from the car.
        //Verify the car now has 0 passengers and gets 21 MPG.
        //Create a Car with 5 passengers that gets 21 MPG.Remove 25 passengers from the car.
        //Verify the car now has 0 passengers and gets 21 MPG.
        [Theory]
        [InlineData(3, 20.6)]
        [InlineData(5, 21)]
        [InlineData(25,21)]
        public void RemovePAssengersDoesNotDropMPGBelowZero(int passengersToRemove, double expectedMPG)
        {
            //arrange
            Car car = new Car(12, "Honda", "Civic", 21);
            car.AddPassengers(5);
            //act
            car.RemovePassengers(passengersToRemove);
            //assert
            car.MilesPerGallon.Should().Be(expectedMPG);
        }
    }
}
