using System;
using System.Collections.Generic;
using System.Linq;
using Models;

namespace DL
{
    public class CustomerRepository : ICustomerRepository
    {

        private Entities.DemoDBContext _context;
        public CustomerRepository(Entities.DemoDBContext p_context)
        {
            _context = p_context;
        }
        public Customers AddCustomer(Customers p_cust)
        {  
            _context.Customers.Add(new Entities.Customer
            {
                Name = p_cust.Name,
                Address = p_cust.Address,
                City = p_cust.City,
                State = p_cust.State,
                Email = p_cust.Email,
                PhoneNumber = p_cust.PhoneNumber,
                CustomerId = p_cust.CustomerId
            });
            _context.SaveChanges();
            return p_cust;
        }

        public List<Customers> GetAllCustomers()
        {
            return _context.Customers.Select(
                cust =>
                    new Customers()
                    {
                        CustomerId = cust.CustomerId,
                        Name = cust.Name,
                        City = cust.City,
                        State = cust.State,
                        Address = cust.Address,
                        Email = cust.Email,
                        PhoneNumber = (long)cust.PhoneNumber
                    }
            ).ToList();
        }

        public Customers GetCustomer(Customers p_cust)
        {
            //If provided the customer ID just return the customer associated with that ID.

            if (p_cust.CustomerId != 0)
            {
                Customers _foundcust = new Customers();
                var query1 = _context.Customers.Find(p_cust.CustomerId);
                if (query1 != null)
                {
                    _foundcust.CustomerId = query1.CustomerId;
                    _foundcust.Name = query1.Name;
                    _foundcust.City = query1.City;
                    _foundcust.State = query1.State;
                    _foundcust.Address = query1.Address;
                    _foundcust.PhoneNumber = (long)query1.PhoneNumber;
                    _foundcust.Email = query1.Email;
                }

                return _foundcust;
            }
            
            var query = _context.Customers.AsQueryable();

            if (!String.IsNullOrWhiteSpace(p_cust.Name))
            {
                query = query.Where(a => a.Name == p_cust.Name);
            }
            if (!String.IsNullOrWhiteSpace(p_cust.Email))
            {
                query = query.Where(a => a.Email == p_cust.Email);
            }
            if (p_cust.PhoneNumber != 0)
            {
                query = query.Where(a => a.PhoneNumber == p_cust.PhoneNumber);
            }
            return query.Select(
                cust =>
                    new Customers()
                    {
                        CustomerId = cust.CustomerId,
                        Name = cust.Name,
                        City = cust.City,
                        State = cust.State,
                        Address = cust.Address,
                        Email = cust.Email,
                        PhoneNumber = (long)cust.PhoneNumber
                    }
            ).FirstOrDefault();


        }
    }
}