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
        /// <summary>
        /// Gets orderhistory for certain customer at certain store
        /// </summary>
        /// <param name="p_store">store to get order for</param>
        /// <param name="p_cust">customer to get order for</param>
        /// <returns>returns orders for a customer at a store</returns>
        List<Orders> GetOrders(StoreFront p_store, Customers p_cust);

        /// <summary>
        /// Add order to the database
        /// </summary>
        /// <param name="p_order">The order to be added to the db</param>
        /// <returns>the order added to the db</returns>
        Orders AddOrder(Orders p_order);

        /// <summary>
        /// Adds a lineitem to the cart
        /// </summary>
        /// <param name="p_lineitem">line item to be added</param>
        /// <param name="p_cartId">the cart to add it to</param>
        void AddToCart(LineItems p_lineitem, string p_cartId);
        
        void RemoveFromCart(int p_productid, string p_cartId);
        void EmptyCart(string p_cartId);
        List<Cart> GetCartItems(string p_cartId);

        void MigrateCart(string p_email, string p_tempCartID);
    }
}