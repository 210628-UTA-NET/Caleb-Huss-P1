using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;


namespace DL
{
    public class DBContext : DbContext
    {
        public DbSet<Customers> Customers { get; set; }

        public DbSet<StoreFront> Stores { get; set; }
        public DBContext() : base()
        { }

        public DBContext(DbContextOptions options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder p_options)
        { 
            p_options.UseSqlServer("Server=tcp:calebhrev.database.windows.net,1433;Initial Catalog=DemoDB;Persist Security Info=False;User ID=CalebHuss;Password=8675CBL!!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=20;");
        }
            protected override void OnModelCreating(ModelBuilder p_modelBuilder)
        {
            p_modelBuilder.Entity<Customers>()
                .Property(cust => cust.CustomerId)
                .ValueGeneratedOnAdd();
            p_modelBuilder.Entity<StoreFront>()
                .Property(store => store.StoreNumber)
                .ValueGeneratedOnAdd();
        }
    }
}
