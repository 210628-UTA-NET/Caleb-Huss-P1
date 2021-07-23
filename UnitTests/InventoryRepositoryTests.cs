using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;
using Xunit;
using Xunit.Abstractions;
using DL;



namespace UnitTests
{
    [Collection("Sequential")]
    public class InventoryRepositoryTests
    {
        private readonly DbContextOptions<DBContext> _options;
        private readonly ITestOutputHelper output;
        public InventoryRepositoryTests(ITestOutputHelper output)
        {
            this.output = output;
            _options = new DbContextOptionsBuilder<DBContext>().UseSqlite("Filename = Test.db").Options;
            this.Seed();
        }


        [Fact]
        public void GetAllInventoryShouldGetAllInventory()
        {
            using (var context = new DBContext(_options))
            {
                //Arrange
                IInventoryRepository invRepo = new InventoryRepository(context);
                StoreFront newStore = new StoreFront()
                {
                    Name = "StorePlace1",
                    Address = "123321 Road st",
                    City = "Kansas City",
                    State = "Florida",
                    StoreNumber = 1
                };

                //Act
                List<LineItems> storeInv = invRepo.GetAllInventory(newStore);

                //Assert
                Console.WriteLine(storeInv[0]);
                Assert.NotNull(storeInv);
                Assert.Equal(2, storeInv.Count);
                Assert.Equal("RootBeer1", storeInv[0].Product.Name);
                Assert.Equal("RootBeer2", storeInv[1].Product.Name);
                Assert.Equal(3.99m, storeInv[0].Product.Price);
                Assert.Equal(3.99m, storeInv[1].Product.Price);

            }
            

        }

        private void Seed()
        {
            using (var context = new DBContext(_options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Products.AddRange(
                    new Products
                    {
                        Name = "RootBeer1",
                        Price = 3.99m,
                        Description = "desc 1",
                        ProductID = 1,
                    },
                    new Products
                    {
                        Name = "RootBeer2",
                        Price = 3.99m,
                        Description = "desc 2",
                        ProductID = 2
                    });
 
                context.Inventories.Add(
                    new Inventory()
                    {
                        InventoryID = 1,
                        Store = new StoreFront()
                        {
                            Name = "StorePlace1",
                            Address = "123321 Road st",
                            City = "Kansas City",
                            State = "Florida",
                            StoreNumber = 1
                        }
                    });
                context.StoreInventories.AddRange(
                    new StoreInventory()
                    {
                        InventoryID = 1,
                        ProductID = 1,
                        Quantity = 4

                    },
                    new StoreInventory()
                    {
                        InventoryID = 1,
                        ProductID = 2,
                        Quantity = 7
                    });
                context.SaveChanges();
            }
        }
    }
}
