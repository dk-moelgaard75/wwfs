using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FuelLogUserBackend
{
    public class UserDataContext : DbContext
    {
        //partly based on
        //https://dev.to/azure/using-entity-framework-with-azure-functions-50aa
        
        public UserDataContext(DbContextOptions<UserDataContext> options) : base(options)
        {
            //this.Database.EnsureCreated();
            //this.Database.Migrate();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<UserModel>().ToTable("users");
        }
        public DbSet<UserModel> Users { get; set; }
        
    }
}
