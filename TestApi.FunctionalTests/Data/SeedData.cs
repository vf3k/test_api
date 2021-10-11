using System;
using Microsoft.Win32;
using TestApi.Core.Entity;

namespace TestApi.FunctionalTests.Data
{
    public class SeedData
    {
        public static Registration TestRegistration() => new()
        {
            Locale = "en",
            RegistrationDate = DateTime.Now,
            Organisation = new Organisation
            {
                Name = "Org1",
                Address = new()
                {
                    Locale = "en",
                    AddressLine1 = "Gateway House",
                    AddressLine2 = "28 The Quadrant",
                    City = "Richmond",
                    CountryIsoCode = "GBR",
                    Postcode = "TW9 1DN",
                    State = "Surrey"
                },
            },
            Person = new Person()
            {
                Email = "test@email.com",
                FirstName = "FN",
                LastName = "LN",
                Address = new Address
                {
                    Locale = "en",
                    AddressLine1 = "Gateway House",
                    AddressLine2 = "28 The Quadrant",
                    City = "Richmond",
                    CountryIsoCode = "GBR",
                    Postcode = "TW9 1DN",
                    State = "Surrey"
                },
            }
        };
        public static Registration TestRegistrationWithoutOrganisation() 
        {
            var tmp = TestRegistration();
            tmp.Organisation = null;
            return tmp;
        }

        public static Registration TestRegistrationWithoutPerson()
        {
            var tmp = TestRegistration();
            tmp.Person = null;
            return tmp;
        }
        public static Registration TestRegistrationPersonAddressEmpty()
        {
            var tmp = TestRegistration();
            tmp.Person.Address.AddressLine1="";
            return tmp;
        }

        
    }
}