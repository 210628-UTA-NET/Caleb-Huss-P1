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

        [Theory]
        [InlineData("RootBeer1", 0, 0, null, 1)]
        [InlineData(null, 1, 0, null, 1)]
        [InlineData(null, 0, 3.99, null, 2)]
        [InlineData(null, 0, 0, "cat1", 1)]
        [InlineData(null, 0, 0, "cat2", 2)]
        public void GetSearchedInventoryGetSearchedInventory(string p_name, int p_pId, decimal p_price, string p_category, int p_expected)
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

                Products productToSearchFor = new Products()
                {
                        Name = p_name,
                        ProductID = p_pId,
                        Price = p_price,
                        Categories = new List<Categories> {
                            new Categories()
                            {
                                Category = p_category
                            }
                        }
                };

                //Act
                List<LineItems> foundInventory = invRepo.GetSearchedInventory(newStore, productToSearchFor);

                //Assert
                Assert.NotNull(foundInventory);
                Assert.Equal(p_expected, foundInventory.Count);
            }
        }

        [Fact]
        public void ChangeInventoryShouldChangeInventory()
        {
            using (var context = new DBContext(_options))
            {
                //Arrange
                StoreFront newStore = new StoreFront()
                {
                    Name = "StorePlace1",
                    Address = "123321 Road st",
                    City = "Kansas City",
                    State = "Florida",
                    StoreNumber = 1
                };
                LineItems lineItems = new LineItems()
                {
                    Product = new Products
                    {
                        Name = "RootBeer1",
                        Price = 3.99m,
                        Description = "desc 1",
                        ProductID = 1,
                    },
                    Quantity = 2
                };
                //Act

                //Assert
            }
        }

        private void Seed()
        {
            using (var context = new DBContext(_options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                Categories cat1 = new Categories
                {
                    Category = "cat1",
                    CategoryID = 1
                };

                Categories cat2 = new Categories
                {
                    Category = "cat2",
                    CategoryID = 2
                };

                Categories cat3 = new Categories
                {
                    Category = "cat3",
                    CategoryID = 3
                };
                context.Products.AddRange(
                    new Products
                    {
                        Name = "RootBeer1",
                        Price = 3.99m,
                        Description = "desc 1",
                        ProductID = 1,
                        Categories = new List<Categories>
                        {
                            cat1,
                            cat2
                        }
                    },
                    new Products
                    {
                        Name = "RootBeer2",
                        Price = 3.99m,
                        Description = "desc 2",
                        ProductID = 2,
                        Categories = new List<Categories>
                        {
                            cat2,
                            cat3
                        }

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
