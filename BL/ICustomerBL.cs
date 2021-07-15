using System;
using System.Collections.Generic;
using Models;

namespace BL
{
    /// <summary>
    /// Handles all the BL for the customer model
    /// </summary>
    public interface ICustomerBL
    {
        /// <summary>
        /// Gets all the customers from the database
        /// </summary>
        /// <returns>Returns a list of all the customers</returns>
        List<Customers> GetAllCustomers();
        
        /// <summary>
        /// Used to get a specific customer
        /// </summary>
        /// <param name="p_cust">This object will be used to find an existing cust in db</param>
        /// <returns>Returns the customer being searched for</returns>
        Customers GetCustomer(Customers p_cust);
        
        /// <summary>
        /// Adds a customer to the database
        /// </summary>
        /// <param name="p_cust"> This is the object that represent the customer to be added</param>
        /// <returns>Just returns the customer added</returns>
        Customers AddCustomer(Customers p_cust);

    }
}
