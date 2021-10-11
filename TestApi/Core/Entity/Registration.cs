using System;

namespace TestApi.Core.Entity
{
    public record Registration
    {
        public Guid Id { get; set; }
        public string Locale { get; set; }
        public DateTime RegistrationDate { get; set; }
        public Organisation Organisation { get; set; }
        public Person Person { get; set; }
    }
}