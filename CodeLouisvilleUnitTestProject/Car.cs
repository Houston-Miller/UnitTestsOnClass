using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CodeLouisvilleUnitTestProject
{
    public class Car : Vehicle
    {
        public int NumberOfPassengers { get; private set; }
        string baseUrl = "http://vpic.nhtsa.dot.gov/api/";
        private HttpClient _client = new HttpClient()
        {
            BaseAddress = new Uri("https://vpic.nhtsa.dot.gov/api/")
        };
        public int Numberoftires = 4;

        public Car()
            : this(0, "", "", 0)
        { }        

        public Car(double gasTankCapacity, string make, string model, double milesPerGallon)
        {
            GasTankCapacity = gasTankCapacity;
            Make = make;
            Model = model;
            MilesPerGallon = milesPerGallon;
            
        }

        public async Task<bool> IsValidModelForMakeAsync()
        {
            var model = this.Model;
            var make = this.Make;
            string urlsiffix = $"vehicles/getmodelsformake/{Make}?format=json";
            var response = await _client.GetAsync(urlsiffix);
            var rawJson = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<GetModelsForMakeResponseModel>(rawJson);
            if (data.Results != null)
            {
                return true;
            }
            else
            {
               return false;
            }
        }

        public async Task<bool> WasModelMadeInYearAsync(int year)
        {
            var model = this.Model;
            if (year < 1995)
                throw new ArgumentException("No data is available for years before 1995");
            string urlsuffix = $"vehicles/getmodelsformakeyear/make/{Make}/modelyear/{year}?format=json";
            var response = await _client.GetAsync(urlsuffix);
            var rawJson = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<GetModelsForMakeYearResponseModel>(rawJson);
            return data.Results.Any(r => r.Model_Name == Model);
            
        }

        public void AddPassengers(int passengersToAdd)
        {
            NumberOfPassengers = NumberOfPassengers + passengersToAdd;
            MilesPerGallon = MilesPerGallon - (passengersToAdd * .2);
            if (MilesPerGallon < 0)
            {
                MilesPerGallon = 0;
            }
        }

        public void RemovePassengers (int passengersToRemove)
        {
            //var milesPerGallon = MilesPerGallon;
            NumberOfPassengers -= passengersToRemove;
            MilesPerGallon = MilesPerGallon + (passengersToRemove * .2);
            if (NumberOfPassengers < 0 )
            {
                NumberOfPassengers = 0;
            }
            //if (milesPerGallon > MilesPerGallon)
            //{
            //    milesPerGallon = MilesPerGallon;
            //}    
        }
    }
}
