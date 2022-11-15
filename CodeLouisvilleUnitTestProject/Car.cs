using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeLouisvilleUnitTestProject
{
    public class Car : Vehicle
    {
        public int NumberOfPassengers { get; private set; }
        string baseUrl = "htp://vpic.nhtsa.dot.gov/api/";
        private HttpClient client;
       // Client.BaseAddrress = new Uri(baseUrl);


        public Car()
            : this(4, 0, "", "", 0)
        { }        

        public Car(double NumberOfTires, double gasTankCapacity, string make, string model, double milesPerGallon)
        {
            GasTankCapacity = gasTankCapacity;
            Make = make;
            Model = model;

        }
    }
}
