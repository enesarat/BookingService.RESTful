using BookingService.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.DataAccess
{
    public class BookingServiceDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Server=psql-mock-database-cloud.postgres.database.azure.com;Database=booking1661678861778msjijmdzuhimkuzz;Port=5432;Trust Server Certificate=true;Ssl Mode=Require;User Id=wthrndmdcjbjchnwqiwhfpdm@psql-mock-database-cloud;Password=hubzsrqeepttqzdaqjnplsob");

        public DbSet<Bookings> bookings { get; set; }
        public DbSet<Appartments> appartments { get; set; }
        public DbSet<Company> company { get; set; }
        public DbSet<Users> users { get; set; }

    }
}
