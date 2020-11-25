using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FuelLogVehicleBackend
{
    public class VehicleDataContext : DbContext
    {
        //partly based on
        //https://dev.to/azure/using-entity-framework-with-azure-functions-50aa
        
        public VehicleDataContext(DbContextOptions<VehicleDataContext> options) : base(options)
        {
            //this.Database.EnsureCreated();
            //this.Database.Migrate();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<VehicleModel>().ToTable("vehicles");
        }
        public DbSet<VehicleModel> Vehicles { get; set; }
        
    }
}
