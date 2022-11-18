using CodeLouisvilleUnitTestProject;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit.Abstractions;

namespace CodeLouisvilleUnitTestProjectTests
{
    public class VehicleTests
    {

        //Verify the parameterless constructor successfully creates a new
        //object of type Vehicle, and instantiates all public properties
        //to their default values.
        [Fact]
        public void VehicleParameterlessConstructorTest()
        {
            //arrange
                      

            //throw new NotImplementedException();

            //act
            Vehicle vehicle = new Vehicle();

            //assert
            Assert.NotNull(vehicle);
        }

        //Verify the parameterized constructor successfully creates a new
        //object of type Vehicle, and instantiates all public properties
        //to the provided values.
        [Fact]
        public void VehicleConstructorTest()
        {
            //arrange
            //  throw new NotImplementedException();
            //act
            Vehicle vehicle = new Vehicle(4, 15, "Toyota", "Camry", 32);


            //assert
            vehicle.Should().NotBeNull();
        }

        //Verify that the parameterless AddGas method fills the gas tank
        //to 100% of its capacity
        [Fact]
        public void AddGasParameterlessFillsGasToMax()
        {
            //arrange
            Vehicle vehicle = new(4, 15, "Toyota", "Camry", 32);

            // throw new NotImplementedException();

            //act
            vehicle.AddGas();

            //assert
            vehicle.GasLevel.Should().Be("100%");
        
        }

        //Verify that the AddGas method with a parameter adds the
        //supplied amount of gas to the gas tank.
        [Fact]
        public void AddGasWithParameterAddsSuppliedAmountOfGas()
        {
            //arrange
            Vehicle vehicle = new(4, 15, "Toyota", "Camry", 32);

            //throw new NotImplementedException();
            //act
            vehicle.AddGas(3);


            //assert
            vehicle.GasLevel.Should().Be("20%");
        }

        //Verify that the AddGas method with a parameter will throw
        //a GasOverfillException if too much gas is added to the tank.
        [Fact]
        public void AddingTooMuchGasThrowsGasOverflowException()
        {
            //arrange
            Vehicle vehicle = new(4, 15, "Toyota", "Camry", 32);
            

            // throw new NotImplementedException();
            //act
            //vehicle.AddGas(16);


            //assert
            vehicle.Invoking(GasLevel => GasLevel.AddGas(16))
                .Should().Throw<GasOverfillException>()
                .WithMessage($"Unable to add {16} gallons to tank " +
                $"because it would exceed the capacity of {vehicle.GasTankCapacity} gallons");
        }

        //Using a Theory (or data-driven test), verify that the GasLevel
        //property returns the correct percentage when the gas level is
        //at 0%, 25%, 50%, 75%, and 100%.
        [Theory]
        [InlineData("0%", 0)]
        [InlineData("25%", 4)]
        [InlineData("50%", 8)]
        [InlineData("75%", 12)]
        [InlineData("100%", 16)]
        //public void GasLevelPercentageIsCorrectForAmountOfGas(params object[] yourParamsHere)
        public void GasLevelPercentageIsCorrectForAmountOfGas(string expectedGas, float FillGas)
        {
            //AddGas(FillGas);
            
            //arrange
            Vehicle vehicle = new(4, 16, "Toyota", "Camry", 32);

            //throw new NotImplementedException();
            //act
            vehicle.AddGas(FillGas);

            //assert
            vehicle.GasLevel.Should().Be(expectedGas);
        }

        /*
         * Using a Theory (or data-driven test), or a combination of several 
         * individual Fact tests, test the following functionality of the 
         * Drive method:
         *      a. Attempting to drive a car without gas returns the status 
         *      string “Cannot drive, out of gas.”.
         *      b. Attempting to drive a car with a flat tire returns 
         *      the status string “Cannot drive due to flat tire.”.
         *      c. Drive the car 10 miles. Verify that the correct amount 
         *      of gas was used, that the correct distance was traveled, 
         *      that GasLevel is correct, that MilesRemaining is correct, 
         *      and that the total mileage on the vehicle is correct.
         *      d. Drive the car 100 miles. Verify that the correct amount 
         *      of gas was used, that the correct distance was traveled,
         *      that GasLevel is correct, that MilesRemaining is correct, 
         *      and that the total mileage on the vehicle is correct.
         *      e. Drive the car until it runs out of gas. Verify that the 
         *      correct amount of gas was used, that the correct distance 
         *      was traveled, that GasLevel is correct, that MilesRemaining
         *      is correct, and that the total mileage on the vehicle is 
         *      correct. Verify that the status reports the car is out of gas.
        */
        [Theory]
        [InlineData(100, false)]
        [InlineData(100, true)]
        public void DriveNegativeTests(double milesToDrive, bool FlatTire)
        {
            //arrange
            Vehicle vehicle = new(4, 16, "Toyota", "Camry", 32);
            //act
            vehicle.AddGas(0);
            vehicle.hasFlatTire = FlatTire;
            var status = vehicle.Drive(milesToDrive);
            //assert
            vehicle.Drive(milesToDrive).Should().Be(status);
            
        }

        [Theory]
        [InlineData(10, "Drove 10 miles using 0.5 gallons of gas.", 310, 10, "96.875%")]
        [InlineData(100, "Drove 100 miles using 5 gallons of gas.", 220, 100, "68.75%")]
        [InlineData(320, "Drove 320 miles, then ran out of gas.", 0, 320, "0%")]
        public void DrivePositiveTests(double milesToDrive, String DriveStatus, double milesRemaining, double totalMilage, string gasRemaining)
        {
            //arrange
            Vehicle vehicle = new(4, 16, "Toyota", "Camry", 20);
            
            //act
            vehicle.AddGas();
            vehicle.hasFlatTire = false;
            var status = vehicle.Drive(milesToDrive);


            //assert
            using (new AssertionScope())
            {
                status.Should().Contain(DriveStatus);
                vehicle.MilesRemaining.Should().Be(milesRemaining);
                vehicle.Mileage.Should().Be(totalMilage);
                vehicle.GasLevel.Should().Be(gasRemaining);
            }

        }

        //Verify that attempting to change a flat tire using
        //ChangeTireAsync will throw a NoTireToChangeException
        //if there is no flat tire.
        [Fact]
        public async Task ChangeTireWithoutFlatTest()
        {
            //arrange
            Vehicle vehicle = new(4, 15, "Toyota", "Camry", 32);
            vehicle.hasFlatTire = false;
            //act
            
            //assert
            //vehicle.ChangeTireAsync().Should().Throw<NoTireToChangeException>;

            await vehicle.Invoking(async vehicle => await vehicle.ChangeTireAsync())
               .Should().ThrowAsync<NoTireToChangeException>();
                
        }

        //Verify that ChangeTireAsync can successfully
        //be used to change a flat tire
        [Fact]
        public async Task ChangeTireSuccessfulTest()
        {
            //arrange
            Vehicle vehicle = new(4, 15, "Toyota", "Camry", 32)
            {
                hasFlatTire = true
            };
            //act
            await vehicle.ChangeTireAsync();


            //assert
            vehicle.hasFlatTire.Should().Be(false);
        }

        //BONUS: Write a unit test that verifies that a flat
        //tire will occur after a certain number of miles.
        [Theory]
        [InlineData(320)]
        public void GetFlatTireAfterCertainNumberOfMilesTest(double milesToDrive)
        {
            //arrange
            Vehicle vehicle = new(4, 16, "Toyota", "Camry", 20);

            //act
            vehicle.Drive(milesToDrive);
            var status = vehicle.Drive(milesToDrive);

            //assert
            vehicle.Drive(milesToDrive).Should().Be(status);
        }
    }
}