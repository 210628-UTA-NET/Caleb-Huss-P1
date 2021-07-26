using System;
using Models;
using DL;
using System.Collections.Generic;

namespace BL
{
    public class StoreBL : IStoreBL
    {
        private readonly IStoreRepository _repo;

        public StoreBL(IStoreRepository p_repo)
        {
            _repo = p_repo;
        }
        public StoreFront AddStore(StoreFront p_store)
        {
            return _repo.AddStore(p_store);
        }

        public List<StoreFront> GetAllStores()
        {
            return _repo.GetAllStores();
        }

        public StoreFront GetStoreFront(StoreFront p_store)
        {
            return _repo.GetStore(p_store);
        }
    }
}