using System;
using System.Collections.Generic;
using Models;

namespace DL
{
    public interface IOrderRepository
    {
        /// <summary>
        /// Get the order history from a certain store
        /// </summary>
        /// <param name="p_store">The store to get the order history from</param>
        /// <returns>Return the order history</returns>
        List<Orders> GetOrders(StoreFront p_store);

        /// <summary>
        /// Get the orderhistory for a certain customer
        /// </summary>
        /// <param name="p_cust"> The customer to get the order history for</param>
        /// <returns>Returns the order history</returns>
        List<Orders> GetOrders(Customers p_cust);
        List<Orders> GetOrders(StoreFront p_store, Customers p_cust);

        /// <summary>
        /// Add order to the database
        /// </summary>
        /// <param name="p_order">The order to be added to the db</param>
        /// <returns>the order added to the db</returns>
        Orders AddOrder(Orders p_order);
    }
}