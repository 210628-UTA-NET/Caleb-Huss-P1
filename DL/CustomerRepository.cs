using System;
using System.Collections.Generic;
using System.Linq;
using Models;

namespace DL
{
    public class CustomerRepository : ICustomerRepository
    {

        private readonly DBContext _context;
        public CustomerRepository(DBContext p_context)
        {
            _context = p_context;
        }

        public UserLogin AddCredentials(UserLogin p_userLogin)
        {
            _context.UserLogin.Add(p_userLogin);
            _context.SaveChanges();
            return p_userLogin;
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

        public List<Customers> GetCertainCustomers(Customers p_cust)
        {
            List<Customers> custsFound = new List<Customers>();
            if (p_cust.CustomerID != 0)
            {   Customers found = _context.Customers.Find(p_cust.CustomerID);
               custsFound.Add(found);
               return custsFound;
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
            if(!String.IsNullOrWhiteSpace(p_cust.City))
            {
                query = query.Where(a => a.City == p_cust.City);
            }
            if(!String.IsNullOrWhiteSpace(p_cust.State))
            {
               query = query.Where(a => a.State == p_cust.State);
            }
            if(!String.IsNullOrWhiteSpace(p_cust.Address))
            {
               query = query.Where(a => a.Address == p_cust.Address);
            }
            if (p_cust.PhoneNumber != 0)
            {
               query = query.Where(a => a.PhoneNumber == p_cust.PhoneNumber);
            }
            
            custsFound = query.ToList();
            return custsFound;
        }

        public UserLogin GetCredentials(Customers p_cust)
        {
            UserLogin userLogin = (from uLog in _context.UserLogin
                                 join c in _context.Customers on uLog.CustomerID equals c.CustomerID
                                 where p_cust.CustomerID == uLog.CustomerID
                                 select uLog
            ).FirstOrDefault();
            return userLogin;
        }

        public Customers GetCustomer(Customers p_cust)
        {
            //If provided the customer ID just return the customer associated with that ID.

            if (p_cust.CustomerID != 0)
            {
               return _context.Customers.Find(p_cust.CustomerID);
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

    }
}