using System.Collections.Generic;

namespace TestApi.Core.Entity
{
    public record Organisation
    {
        public int  Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public IList<Registration> Registrations { get; set; }

    }
}