using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class StoreFront
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [Key]
        public int StoreNumber { get; set; }
        public Inventory StoreInventory { get; set; }
        public List<Orders> StoreOrders { get; set; }
        
        public override string ToString()
        {
            return $"Name: {Name}, Store Number: {StoreNumber}, \nAddress: {Address}, City: {City}, State: {State}";
        }
    }
}