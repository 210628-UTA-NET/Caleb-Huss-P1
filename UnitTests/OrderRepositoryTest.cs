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
    public class OrderRepositoryTest
    {
        private readonly DbContextOptions<DBContext> _options;
        public OrderRepositoryTest()
        {
            _options = new DbContextOptionsBuilder<DBContext>().UseSqlite("Filename = Test.db").Options;
            this.Seed();
        }

        [Fact]
        public void GetOrdersWithStoreAndCustomerShouldGetStoreandCustomerOrders()
        {
            using (var context = new DBContext(_options))
            {
                //Arrange
                IOrderRepository orderRepo = new OrderRepository(context);
                StoreFront newStore = new StoreFront()
                {
                    Name = "StorePlace1",
                    Address = "123321 Road st",
                    City = "Kansas City",
                    State = "Florida",
                    StoreNumber = 1
                };
                Customers newCustomer = new Customers()
                {
                    FirstName = "Caleb",
                    LastName = "Huss",
                    Address = "123 main st",
                    City = "Kansas City",
                    State = "Kansas",
                    Email = "caleb.huss@gmail.gov",
                    PhoneNumber = 7851231234,
                    CustomerID = 1
                };

                //Act
                List<Orders> foundOrders = orderRepo.GetOrders(newStore, newCustomer);

                //Assert
                Assert.NotNull(foundOrders[0]);
                Assert.NotNull(foundOrders[0].StoreFront);
                Assert.Single(foundOrders);
                Assert.Equal(1, foundOrders[0].StoreFront.StoreNumber);
                Assert.Equal(1, foundOrders[0].Customer.CustomerID);
                Assert.Equal(2, foundOrders[0].ItemsList.Count);
            }
        }
        [Fact]
        public void GetStoreOrdersShouldGetStoreOrders()
        {
            using (var context = new DBContext(_options))
            {
                //Arrange
                IOrderRepository orderRepo = new OrderRepository(context);
                StoreFront newStore = new StoreFront()
                {
                    Name = "StorePlace1",
                    Address = "123321 Road st",
                    City = "Kansas City",
                    State = "Florida",
                    StoreNumber = 1
                };

                //Act
                List<Orders> foundOrders = orderRepo.GetOrders(newStore);

                //Assert
                Assert.NotNull(foundOrders[0]);
                Assert.NotNull(foundOrders[0].StoreFront);
                Assert.Single(foundOrders);
                Assert.Equal(1, foundOrders[0].StoreFront.StoreNumber);
                Assert.Equal(1, foundOrders[0].Customer.CustomerID);
                Assert.Equal(2, foundOrders[0].ItemsList.Count);
            }
        }
        [Fact]
        public void GetCustomerOrderShouldGetCustomerOrders()
        {
            using (var context = new DBContext(_options))
            {
                //Arrange
                IOrderRepository orderRepo = new OrderRepository(context);
                Customers newCustomer = new Customers()
                {
                    FirstName = "Caleb",
                    LastName = "Huss",
                    Address = "123 main st",
                    City = "Kansas City",
                    State = "Kansas",
                    Email = "caleb.huss@gmail.gov",
                    PhoneNumber = 7851231234,
                    CustomerID = 1
                };

                //Act
                List<Orders> foundOrders = orderRepo.GetOrders(newCustomer);

                //Assert
                Assert.NotNull(foundOrders[0]);
                Assert.NotNull(foundOrders[0].StoreFront);
                Assert.Single(foundOrders);
                Assert.Equal(1, foundOrders[0].StoreFront.StoreNumber);
                Assert.Equal(1, foundOrders[0].Customer.CustomerID);
                Assert.Equal(2, foundOrders[0].ItemsList.Count);
            }
        }
        [Fact]
        public void AddOrderShouldAddOrder()
        {
            using (var context = new DBContext(_options))
            {
                //Arrange
                IOrderRepository orderRepo = new OrderRepository(context);
                StoreFront store1 = new StoreFront()
                {
                    Name = "StorePlace1",
                    Address = "123321 Road st",
                    City = "Kansas City",
                    State = "Florida",
                    StoreNumber = 1
                };

                Customers cust1 = new Customers()
                {
                    FirstName = "Caleb",
                    LastName = "Huss",
                    Address = "123 main st",
                    City = "Kansas City",
                    State = "Kansas",
                    Email = "caleb.huss@gmail.gov",
                    PhoneNumber = 7851231234,
                    CustomerID = 1
                };
                Products prod1 = new Products
                {
                    Name = "RootBeer1",
                    Price = 3.99m,
                    Description = "desc 1",
                    ProductID = 1
                };
                Products prod2 = new Products
                {
                    Name = "RootBeer2",
                    Price = 3.99m,
                    Description = "desc 2",
                    ProductID = 2
                };
                Orders newOrder = new Orders()
                {
                    Date = new DateTime(2021, 7, 21, 5, 0, 0),
                    StoreFront = store1,
                    Customer = cust1,
                    ItemsList = new List<LineItems>()
                    {
                        new LineItems()
                        {
                            Product = prod1,
                            Quantity = 2
                        },
                        new LineItems()
                        {
                            Product = prod2,
                            Quantity = 3
                        }
                    }
                };

                //Act
                Orders orderAdded = orderRepo.AddOrder(newOrder);

                //Assert
                Assert.NotNull(orderAdded);
                Assert.Equal(2, orderAdded.OrderNum);
                Assert.Equal(1, orderAdded.StoreFront.StoreNumber);
                Assert.Equal(1, orderAdded.Customer.CustomerID);
                Assert.Equal(2, orderAdded.ItemsList.Count);
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
                Products prod1 = new Products
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
                };
                Products prod2 = new Products
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
                };
                StoreFront store1 = new StoreFront()
                {
                    Name = "StorePlace1",
                    Address = "123321 Road st",
                    City = "Kansas City",
                    State = "Florida",
                    StoreNumber = 1
                };
                Inventory inv1 = new Inventory()
                {
                    InventoryID = 1,
                    Store = store1
                };
                StoreInventory sInv1 = new StoreInventory()
                {
                    InventoryID = 1,
                    ProductID = 1,
                    Quantity = 4
                };
                StoreInventory sInv2 = new()
                {
                    InventoryID = 1,
                    ProductID = 2,
                    Quantity = 7
                };
                Customers cust1 = new Customers()
                {
                    FirstName = "Caleb",
                    LastName = "Huss",
                    Address = "123 main st",
                    City = "Kansas City",
                    State = "Kansas",
                    Email = "caleb.huss@gmail.gov",
                    PhoneNumber = 7851231234,
                    CustomerID = 1
                };
                Orders order1 = new Orders()
                {
                    Date = new DateTime(2021, 7, 20, 5, 0, 0),
                    OrderNum = 1,
                    StoreFront = store1,
                    Customer = cust1,
                    ItemsList = new List<LineItems>()
                    {
                        new LineItems()
                        {
                            LineItemID = 1,
                            Product = prod1,
                            Quantity = 1
                        },
                        new LineItems()
                        {   
                            LineItemID = 2,
                            Product = prod2,
                            Quantity = 3
                        }
                    }
                };
                Orders order0 = new Orders()
                {
                    Date = new DateTime(2021, 7, 20, 5, 0, 0),
                    OrderNum = 0,
                    StoreFront = store1,
                    Customer = cust1,
                    ItemsList = new List<LineItems>()
                    {
                        new LineItems()
                        {
                            LineItemID = 0,
                            Product = prod1,
                            Quantity = 1
                        },
                        new LineItems()
                        {
                            LineItemID = 3,
                            Product = prod2,
                            Quantity = 3
                        }
                    }
                };

                context.Products.AddRange(prod1, prod2);
                context.Inventories.Add(inv1);
                context.StoreInventories.AddRange(sInv1, sInv2);
                context.Orders.Add(order1);
                context.SaveChanges();
            }
        }
    }
}
