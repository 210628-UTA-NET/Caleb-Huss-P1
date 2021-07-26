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
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<LineItems> LineItems { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<StoreInventory> StoreInventories { get; set; }
        public DbSet<UserLogin> UserLogin { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DBContext() : base()
        { }

        public DBContext(DbContextOptions options) : base(options)
        { }


        protected override void OnModelCreating(ModelBuilder p_modelBuilder)
        {
            p_modelBuilder.Entity<Customers>()
                .Property(cust => cust.CustomerID)
                .ValueGeneratedOnAdd();
            p_modelBuilder.Entity<StoreFront>()
                .Property(store => store.StoreNumber)
                .ValueGeneratedOnAdd();
            p_modelBuilder.Entity<LineItems>()
                .Property(li => li.LineItemID)
                .ValueGeneratedOnAdd();
            p_modelBuilder.Entity<Orders>()
                .Property(order => order.OrderNum)
                .ValueGeneratedOnAdd();
            p_modelBuilder.Entity<Products>()
                .Property(product => product.ProductID)
                .ValueGeneratedOnAdd();
            p_modelBuilder.Entity<Categories>()
                .Property(category => category.CategoryID)
                .ValueGeneratedOnAdd();
            p_modelBuilder.Entity<Inventory>()
                .Property(inv => inv.InventoryID)
                .ValueGeneratedOnAdd();
            p_modelBuilder.Entity<StoreInventory>()
                .HasKey(inv => new { inv.ProductID, inv.InventoryID });
            p_modelBuilder.Entity<Employee>()
                .Property(employee => employee.EmployeeID)
                .ValueGeneratedOnAdd();
        }
    }
}
