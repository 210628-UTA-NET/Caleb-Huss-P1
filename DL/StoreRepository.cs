using System;
using System.Collections.Generic;
using System.Linq;
using Models;

namespace DL
{
    public class StoreRepository : IStoreRepository
    {
        private readonly DBContext _context;
        public StoreRepository(DBContext p_context)
        {
            _context = p_context;
        }
        public StoreFront AddStore(StoreFront p_store)
        {
            _context.Stores.Add(p_store);
            _context.SaveChanges();
            return p_store;
        }

        public List<StoreFront> GetAllStores()
        {
            return _context.Stores.ToList();
        }

        public StoreFront GetStore(StoreFront p_store)
        {
            var query = _context.Stores.AsQueryable();
            if (!String.IsNullOrWhiteSpace(p_store.Name))
            {
               query = query.Where(a => a.Name == p_store.Name); 
            }
            if (p_store.StoreNumber > 0)
            {
               query = query.Where(a => a.StoreNumber == p_store.StoreNumber);
            } 
            return query.FirstOrDefault();
        }
    }
}