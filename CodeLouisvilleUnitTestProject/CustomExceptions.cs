namespace CodeLouisvilleUnitTestProject
{
    public class GasOverfillException : Exception
    {
        public GasOverfillException(double amountAdded, double capacity)
            : base($"Unable to add {amountAdded} gallons to tank " +
                  $"because it would exceed the capacity of {capacity} gallons")
        { }
    }

    public class NoTireToChangeException : Exception
        //made public from internal
    {
        public NoTireToChangeException()
            : base($"No flat tire to change")
        { }
    }

    public class NoCargoItemMatchingThatNameException : Exception
    {
        public NoCargoItemMatchingThatNameException()
            : base($"No Cargo found on List matching entered name")
        { }
    }
}
