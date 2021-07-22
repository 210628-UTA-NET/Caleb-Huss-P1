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

        [Theory]
        [InlineData(1,null)]
        [InlineData(0,"StorePlace1")]
        public void GetStoreGetsAStore(int p_storeID, string p_name)
        {
            using (var context = new DBContext(_options))
            {
                //Arrange
                IStoreRepository storeRepo = new StoreRepository(context);
                StoreFront storeToFind = new StoreFront () //This is the store being searched for
                {
                    Name = "StorePlace1",
                    Address = "123321 Road st",
                    City = "Kansas City",
                    State = "Florida",
                    StoreNumber = 1
                };

                StoreFront searchStore = new StoreFront()// applying the search parameters
                {
                    Name = p_name,
                    StoreNumber = p_storeID 
                };
                StoreFront foundStore;
                //Act

                foundStore = storeRepo.GetStore(searchStore);

                //Assert
                Assert.NotNull(foundStore);
                Assert.Equal(storeToFind.Name , foundStore.Name);
                Assert.Equal(storeToFind.Address, foundStore.Address);
                Assert.Equal(storeToFind.City, foundStore.City);
                Assert.Equal(storeToFind.State, foundStore.State);
                Assert.Equal(storeToFind.StoreNumber, foundStore.StoreNumber);
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
