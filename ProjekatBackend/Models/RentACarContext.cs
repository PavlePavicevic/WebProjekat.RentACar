using Models;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class RentACarContext : DbContext
    {
        public DbSet<Auto> Automobili {get; set;}

        public DbSet<Ekspozitura> Ekspoziture {get; set; }

        public DbSet<Klijent> Klijenti {get; set; }

        public DbSet<Iznajmljivanja> AutoEkspozitureKlijenti {get; set; }

        public RentACarContext(DbContextOptions options) : base(options)
        {

        }
    }
}