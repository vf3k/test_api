using System;

namespace TestApi.DTO
{
    public class PersonDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AddressDto Address { get; set; }
    }

    public class AddressDto
    {
        public int Id { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string City { get; set; }
        public string CountryIsoCode { get; set; }
        public string Locale { get; set; }
        public string Postcode { get; set; }
        public string State { get; set; }
    }
    public class OrganisationDto
    {
        public int  Id { get; set; }
        public string Name { get; set; }
        public AddressDto Address { get; set; }
        
    }

    public class GetRegistrationResponse
    {
        public Guid Id { get; set; }
        public string Locale { get; set; }
        public DateTime RegistrationDate { get; set; }
        public OrganisationDto Organisation { get; set; }
        public PersonDto Person { get; set; }
    }
}