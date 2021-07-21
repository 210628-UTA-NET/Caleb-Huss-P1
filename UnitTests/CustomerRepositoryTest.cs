﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL;
using Models;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Collections;

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
                Assert.Equal(3,Customers.Count); // Should also assert and check if the whole thing is there.
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
                context.Customers.Add(newCustomer);
                context.SaveChanges();
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
                    
                        FirstName = "Tyler",
                        LastName = "Jay",
                        Address = "321 back rd",
                        City = "Jamestown",
                        State = "Ohio",
                        Email = "TJ332@gmail.com",
                        PhoneNumber = 7778546845
                    },
                    new Customers
                    {
                        FirstName = "Baileeeeigh",
                        LastName = "Mills",
                        Address = "401 S waynes wrld",
                        City = "Veil",
                        State = "Florida",
                        Email = "BailMill@yahoo.com",
                        PhoneNumber = 8679511234
                    });
                context.SaveChanges();
            }
        }
    }
}
