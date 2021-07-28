using System.Collections.Generic;
using Models;

namespace BL
{
    public interface IOrderBL
    {
        /// <summary>
        /// Get all orders for a customer
        /// </summary>
        /// <param name="p_cust">Customer to get order history for</param>
        /// <returns>List of orders for a customer</returns>
        List<Orders> GetAllOrders(Customers p_cust);
        /// <summary>
        /// Get all orders for a certain store
        /// </summary>
        /// <param name="p_store">Store to get orders for</param>
        /// <returns>List of orers for a store</returns>
        List<Orders> GetAllOrders(StoreFront p_store);
        /// <summary>
        /// Gets all orders for a customer at a store
        /// </summary>
        /// <param name="p_store">store to get orders</param>
        /// <param name="p_cust">customer to get orders</param>
        /// <returns>list of orders</returns>
        List<Orders> GetAllOrders(StoreFront p_store, Customers p_cust);
        /// <summary>
        /// Adds a order to the database
        /// </summary>
        /// <param name="p_order">order to be added</param>
        /// <returns>the order added</returns>
        Orders AddOrder(Orders p_order);

        /// <summary>
        /// Transfers any items in the cart over to the users cart when they login
        /// </summary>
        /// <param name="p_email">The email of the user logged in</param>
        void MigrateCart(string p_email);
        /// <summary>
        /// Get a specific order based on order numver
        /// </summary>
        /// <param name="p_orderNum">Order to get</param>
        /// <returns>Order with certain order number</returns>
        Orders GetAnOrder(int p_orderNum);

        void AddToCart(LineItems p_lineitem, string p_cartId);
        
        void RemoveFromCart(int p_productid, string p_cartId);
        void EmptyCart(string p_cartId);
        List<Cart> GetCartItems(string p_cartId);

    }
}