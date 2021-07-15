using System.Collections.Generic;
using Models;

namespace BL
{
    public interface IInventoryBL
    {
        /// <summary>
        /// Gets all the inventory for a certain store
        /// </summary>
        /// <param name="p_store">This is the store to get the inventory from</param>
        /// <returns>Returns a list of all products</returns>
        List<LineItems> GetAllInventory(StoreFront p_store);
        
        /// <summary>
        /// Changes the inventory because an order happened to the inventory is replenished
        /// </summary>
        /// <param name="p_store"> The store that whill have its inventory changed</param>
        /// <param name="p_lineitem">The product to change</param>
        /// <returns>returns the lineitem passed to it</returns>
        LineItems ChangeInventory(StoreFront p_store, LineItems p_lineitem);
        List<LineItems> GetSearchedInventory(StoreFront p_store, Products p_products);
    }
}