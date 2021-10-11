using System.Collections.Generic;

namespace TestApi.Core.Entity
{
    public record Address
    {
        public Address()
        {
            Persons = new List<Person>();
            Organisations = new List<Organisation>();
        }
        public int Id { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string City { get; set; }
        public string CountryIsoCode { get; set; }
        public string Locale { get; set; }
        public string Postcode { get; set; }
        public string State { get; set; }

        public IList<Person> Persons { get; set; }
        public IList<Organisation> Organisations { get; set; }
        
    }
}