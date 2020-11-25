using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FuelLogFuelingBackend
{
    public class FuelingDataContext : DbContext
    {
        //https://dev.to/azure/using-entity-framework-with-azure-functions-50aa

        public FuelingDataContext(DbContextOptions<FuelingDataContext> options) : base(options)
        {
            //this.Database.EnsureCreated();
            //this.Database.Migrate();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<FuelingModel>().ToTable("fuelings");
        }
        public DbSet<FuelingModel> Fuelings { get; set; }

    }
}
