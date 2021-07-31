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
        public DbSet<Cart> Carts { get; set; }
        public DBContext() : base()
        { }

        public DBContext(DbContextOptions options) : base(options)
        { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customers>()
                .Property(cust => cust.CustomerID)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<StoreFront>()
                .Property(store => store.StoreNumber)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<LineItems>()
                .Property(li => li.LineItemID)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Orders>()
                .Property(order => order.OrderNum)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Products>()
                .Property(product => product.ProductID)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Categories>()
                .Property(category => category.CategoryID)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Inventory>()
                .Property(inv => inv.InventoryID)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<StoreInventory>()
                .HasKey(inv => new { inv.ProductID, inv.InventoryID });
            modelBuilder.Entity<Employee>()
                .Property(employee => employee.EmployeeID)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Cart>()
                .Property(cart => cart.RecordID)
                .ValueGeneratedOnAdd();
        }
    }
}
