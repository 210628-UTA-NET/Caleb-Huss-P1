using System;
using System.Collections.Generic;
using Models;

namespace DL
{
    /// <summary>
    /// This is responsible for accessing the StoreFront database (db)
    /// </summary>
    public interface IStoreRepository
    {
            /// <summary>
            /// Get a list of all stores from the db
            /// </summary>
            /// <returns></returns>
            List<StoreFront> GetAllStores();
            /// <summary>
            /// Gets a specific store based on search parameters from the db
            /// </summary>
            /// <param name="p_store">This storefront obj is used to get a desired store from the db</param>
            /// <returns> Returns the desired store obj</returns>
            StoreFront GetStore(StoreFront p_store);
            /// <summary>
            /// This will add a store to the db 
            /// </summary>
            /// <param name="p_store">This is the store being added to the db</param>
            /// <returns> The store obj just added to the db</returns>
            StoreFront AddStore(StoreFront p_store);
    }
}
