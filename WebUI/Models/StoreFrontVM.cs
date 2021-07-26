using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace WebUI.Models
{
    public class StoreFrontVM
    {
        public StoreFrontVM()
        { }
        public StoreFrontVM(StoreFront p_store)
        {
            StoreNumber = p_store.StoreNumber;
            Name = p_store.Name;
            Address = p_store.Address;
            City = p_store.City;
            State = p_store.State;
        }

        public int StoreNumber { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
