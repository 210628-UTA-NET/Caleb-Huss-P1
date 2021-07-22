using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL;
using Microsoft.EntityFrameworkCore;
using Models;
using Xunit;

namespace UnitTests
{
    public class CustomerRepositoryTest
    {
        private readonly DbContextOptions<DBContext> _options;
        public CustomerRepositoryTest()
        {
            //Create a db in our local storage to unit test DL
            _options = new DbContextOptionsBuilder<DBContext>().UseSqlite("Filename = Test.db").Options;
            this.Seed();
        }

        [Fact]
        public void GetAllCustomersShouldGetAllCustomers()
        {
            using (var context = new DBContext(_options))
            {
                //Arrange
                ICustomerRepository custRepo = new CustomerRepository(context);
                List<Customers> Customers;

                //Act
                Customers = custRepo.GetAllCustomers();

                //Assert
                Assert.NotNull(Customers);
                Assert.Equal(5,Customers.Count); // Should also assert and check if the whole thing is there.
            }
            
        }
        // This test is pretty robust and should check the different combinations when "building" a query
        [Theory]
        [InlineData("Caleb","Huss", "caleb.huss@gmail.gov", 7851231234,1)] //Full test
        [InlineData("Caleb", "Huss", "caleb.huss@gmail.gov", 7851231234,0)] // No customer id
        [InlineData("Caleb", "Huss", "caleb.huss@gmail.gov", 0,0)] // no id or number
        [InlineData("Caleb", "Huss", null, 0, 0)] // Just full name. Not really unique but should return one right now 
        [InlineData(null, "Huss", null, 7851231234, 0)] // Should probably be unique
        [InlineData(null, null, "caleb.huss@gmail.gov", 0, 0)] // Should be unique
        public void GetCustomerShouldGetASpecificCustomer(String p_fname, String p_lname, String p_email, long p_phonenumber, int p_custID)
        {
            using(var context = new DBContext(_options))
            {
                //Arrange
                ICustomerRepository custRepo = new CustomerRepository(context);
                Customers custSearch = new Customers()
                {
                    FirstName = p_fname,
                    LastName = p_lname,
                    Email = p_email,
                    PhoneNumber = p_phonenumber,
                    CustomerID = p_custID
                };

                Customers custToFind = new Customers()
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
                Customers found = custRepo.GetCustomer(custSearch);
        
                //Assert
                Assert.NotNull(found);
                // I realize these are backwords and is fixed in other tests
                Assert.Equal(found.FirstName, custToFind.FirstName);
                Assert.Equal(found.LastName, custToFind.LastName);
                Assert.Equal(found.Address, custToFind.Address);
                Assert.Equal(found.City, custToFind.City);
                Assert.Equal(found.State, custToFind.State);
                Assert.Equal(found.Email, custToFind.Email);
                Assert.Equal(found.PhoneNumber, custToFind.PhoneNumber);
                Assert.Equal(found.CustomerID, custToFind.CustomerID);
            }
        }

        //Another pretty robust test but obviously doesnt test every combo
        //By my calculations that would be over 40000 tests
        [Theory]
        [InlineData("Caleb", "Huss", "caleb.huss@gmail.gov", 7851231234, 1, "123 main st","Kansas","Kansas City",1)]//full test
        [InlineData("Caleb", null, null, 0, 0, null, null, null, 2)] // look for Caleb's
        [InlineData(null, "Huss", null, 0, 0, null, null, null, 2)] // look for Huss's
        [InlineData("Caleb", "Huss", null, 0, 0, null, null, null, 1)] // look for Caleb Huss's
        [InlineData(null, null, null, 0, 0, null, "Kansas", null, 3)] // People in Kansas
        [InlineData(null, null, null, 0, 0, null, "Kansas", "Kansas City", 2)] // People in kansas city, kansas
        [InlineData(null, null, null, 0, 0, null, null, "Kansas City", 2)]
        public void GetCertainCustomersShouldGetCertainCustomers(String p_fname, String p_lname, String p_email, long p_phonenumber, int p_custID, String p_address, String p_state, String p_city,int p_expected)
        {
            using (var context = new DBContext(_options))
            {
                //Arrange
                ICustomerRepository custRepo = new CustomerRepository(context);
                Customers custSearch = new Customers()
                {
                    FirstName = p_fname,
                    LastName = p_lname,
                    Email = p_email,
                    PhoneNumber = p_phonenumber,
                    CustomerID = p_custID,
                    Address = p_address,
                    State = p_state,
                    City = p_city
                };
                List<Customers> foundCusts;
                //Act
                
                foundCusts = custRepo.GetCertainCustomers(custSearch);

                //Assert
                Assert.NotNull(foundCusts);
                Assert.Equal(p_expected, foundCusts.Count);
            }       
        }

        [Fact]
        public void AddCustomerShouldAddCustomer()
        {
            using (var context = new DBContext(_options))
            {
                //Arrange
                ICustomerRepository custRepo = new CustomerRepository(context);
                Customers newCustomer = new Customers()
                {
                    FirstName = "Ryan",
                    LastName = "Husss",
                    Address = "123 main sts",
                    City = "Kansas Citys",
                    State = "Kansass",
                    Email = "test@gmail.com",
                    PhoneNumber = 7855231234,
                    CustomerID = 100
                };
                //Act
                custRepo.AddCustomer(newCustomer);
                Customers found = custRepo.GetCustomer(newCustomer);

                //Assert
                Assert.NotNull(found);
                Assert.Equal(found.FirstName, newCustomer.FirstName);
                Assert.Equal(found.LastName, newCustomer.LastName);
                Assert.Equal(found.Address, newCustomer.Address);
                Assert.Equal(found.City, newCustomer.City);
                Assert.Equal(found.State, newCustomer.State);
                Assert.Equal(found.Email, newCustomer.Email);
                Assert.Equal(found.PhoneNumber, newCustomer.PhoneNumber);
                Assert.Equal(found.CustomerID, newCustomer.CustomerID);
            }
        }
        private void Seed()
        {
            using (var context = new DBContext(_options))
            {
                //Makes sure in memory db is deleted before another test case uses it
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Customers.AddRange(
                    new Customers
                    {
                        FirstName = "Caleb",
                        LastName = "Huss",
                        Address = "123 main st",
                        City = "Kansas City",
                        State = "Kansas",
                        Email = "caleb.huss@gmail.gov",
                        PhoneNumber = 7851231234,
                        CustomerID = 1
                    },
                    new Customers
                    {
                        FirstName = "Caleb",
                        LastName = "Buss",
                        Address = "45554 E Stree Roud",
                        City = "La City",
                        State = "Kansas",
                        Email = "caleb.buss@gmail.gov",
                        PhoneNumber = 1235559762,
                        CustomerID = 2
                    },
                    new Customers
                    {
                        FirstName = "Rueb",
                        LastName = "Huss",
                        Address = "9658 SW east Way",
                        City = "Kansas City",
                        State = "Kansas",
                        Email = "rueb.huss@gmail.gov",
                        PhoneNumber = 2596453825,
                        CustomerID = 3
                    },
                    new Customers
                    {
                    
                        FirstName = "Tyler",
                        LastName = "Jay",
                        Address = "321 back rd",
                        City = "Jamestown",
                        State = "Ohio",
                        Email = "TJ332@gmail.com",
                        PhoneNumber = 7778546845,
                        CustomerID = 10
                    },
                    new Customers
                    {
                        FirstName = "Baileeeeigh",
                        LastName = "Mills",
                        Address = "401 S waynes wrld",
                        City = "Veil",
                        State = "Florida",
                        Email = "BailMill@yahoo.com",
                        PhoneNumber = 8679511234,
                        CustomerID = 11
                    });
                context.SaveChanges();
            }
        }
    }
}
