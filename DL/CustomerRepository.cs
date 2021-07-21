using System;
using System.Collections.Generic;
using System.Linq;
using Models;

namespace DL
{
    public class CustomerRepository : ICustomerRepository
    {

        private DBContext _context;
        public CustomerRepository(DBContext p_context)
        {
            _context = p_context;
        }
        public Customers AddCustomer(Customers p_cust)
        {
            _context.Customers.Add(p_cust);
            _context.SaveChanges();
            return p_cust;
        }

        public List<Customers> GetAllCustomers()
        {
            // returns all customers from db
            return _context.Customers.ToList();
        }

        public Customers GetCustomer(Customers p_cust)
        {
            //If provided the customer ID just return the customer associated with that ID.

            if (p_cust.CustomerID != 0)
            {
               return _context.Customers.Find(p_cust.CustomerID);;
            }
            
            var query = _context.Customers.AsQueryable();

            if (!String.IsNullOrWhiteSpace(p_cust.FirstName))
            {
               query = query.Where(a => a.FirstName == p_cust.FirstName);
            }
            if (!String.IsNullOrWhiteSpace(p_cust.LastName))
            {
               query = query.Where(a => a.LastName == p_cust.LastName);
            }
            if (!String.IsNullOrWhiteSpace(p_cust.Email))
            {
               query = query.Where(a => a.Email == p_cust.Email);
            }
            if (p_cust.PhoneNumber != 0)
            {
               query = query.Where(a => a.PhoneNumber == p_cust.PhoneNumber);
            }
            return query.FirstOrDefault();
        }

        public List<string> GetSaltAndHash(Customers p_cust)
        {
            throw new NotImplementedException();
        }
    }
}