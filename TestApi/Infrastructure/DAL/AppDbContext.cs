using Microsoft.EntityFrameworkCore;
using TestApi.Core.Entity;

namespace TestApi.Infrastructure.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options):base(options)
        {}
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Organisation> Organisations { get; set; }
        public DbSet<Registration> Registrations { get; set; }
    }
}