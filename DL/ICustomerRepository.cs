using System;
using System.Collections.Generic;
using Models;

namespace DL
{
    /// <summary>
    /// This is responsible for accessing the Customer Database (db)
    /// </summary>
    public interface ICustomerRepository
    {
        /// <summary>
        /// Get a list of all customers in the db
        /// </summary>
        /// <returns>Returns a list of all customers</returns>
        List<Customers> GetAllCustomers();

        /// <summary>
        /// This will get a specific customer from the db
        /// </summary>
        /// <param name="p_cust"> This Customer object will be used to find the desired customer in the db</param>
        /// <returns>Returns the spceific customer</returns>
        Customers GetCustomer(Customers p_cust);
        /// <summary>
        /// Will get a list of customers based on search parameters
        /// </summary>
        /// <param name="p_cust">Search parameters wrapped in csutomer form</param>
        /// <returns>List of customers searched for</returns>
        List<Customers> GetCertainCustomers(Customers p_cust);
        /// <summary>
        /// This adds a customer to the db.
        /// </summary>
        /// <param name="p_cust">This is the customer obj that will be added</param>
        /// <returns>Return the customer added to the db</returns>
        Customers AddCustomer(Customers p_cust);

        /// <summary>
        /// This method gets the salt and hash for a certain customer
        /// </summary>
        /// <param name="p_cust">Customer to get salt and hash</param>
        /// <returns>returns the salt and hash for that customer</returns>
        List<String> GetSaltAndHash(Customers p_cust);
    }
}
