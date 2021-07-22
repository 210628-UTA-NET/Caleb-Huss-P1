using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;
using Xunit;
using DL;

namespace UnitTests
{
    public class StoreRepositoryTests
    {
        private readonly DbContextOptions<DBContext> _options;
        public StoreRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<DBContext>().UseSqlite("Filename = Test.db").Options;
            this.Seed();
        }
        [Fact]
        public void GetAllStoresShouldGetAllStores()
        {
            using (var context = new DBContext(_options))
            {
                //Arrange
                IStoreRepository storeRepo = new StoreRepository(context);
                List<StoreFront> Stores;

                //Act
                Stores = storeRepo.GetAllStores();

                //Assert
                Assert.NotNull(Stores);
                Assert.Equal(3, Stores.Count);
            }
        }











        private void Seed()
        {
            using (var context = new DBContext(_options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Stores.AddRange(
                    new StoreFront
                    { 
                        Name = "StorePlace1",
                        Address = "123321 Road st",
                        City = "Kansas City",
                        State = "Florida",
                        StoreNumber = 1
                    },
                    new StoreFront
                    {
                        Name = "StorePlace2",
                        Address = "862 Hometown blvd",
                        City = "Hello",
                        State = "Texas",
                        StoreNumber = 2
                    },
                    new StoreFront
                    {
                        Name = "StorePlace3",
                        Address = "7747 sw test ln",
                        City = "New York",
                        State = "New York",
                        StoreNumber = 3
                    }
                    );
                context.SaveChanges();

            }
        }
    }
}
