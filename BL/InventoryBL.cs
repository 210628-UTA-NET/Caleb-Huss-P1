using System;
using Models;
using DL;
using System.Collections.Generic;

namespace BL
{
    public class InventoryBL : IInventoryBL
    {   
        private readonly IInventoryRepository _repo;
        public InventoryBL(IInventoryRepository p_repo)
        {
            _repo = p_repo;
        }
        public LineItems ChangeInventory(StoreFront p_store, LineItems p_lineitem)
        {
            return _repo.ChangeInventory( p_store,p_lineitem);
        }

        public List<LineItems> GetAllInventory(StoreFront p_store)
        {
            return _repo.GetAllInventory(p_store);
        }

        public List<LineItems> GetSearchedInventory(StoreFront p_store, Products p_products)
        {
            return _repo.GetSearchedInventory(p_store, p_products);
        }
    }
}