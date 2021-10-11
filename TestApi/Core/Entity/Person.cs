using System.Collections.Generic;

namespace TestApi.Core.Entity
{
    public record Person
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
       
        public IList<Registration> Registrations { get; set; }

    }
}