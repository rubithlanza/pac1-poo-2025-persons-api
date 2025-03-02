using Microsoft.EntityFrameworkCore;
using Persons.API.Database.Entities;

namespace Persons.API.Database
{
    public class PersonsDbContext : DbContext
    {
        public PersonsDbContext(DbContextOptions options) : base(options) //Para crearlo con ctrl + punto
        {
        }

        //Aqui se crea el mapa
        public DbSet <PersonEntity> Persons { get; set; }
        public DbSet<CountryEntity> countries { get; set; }
    }
}
