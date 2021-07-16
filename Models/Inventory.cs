using System;

namespace Models
{
    public class Inventory
    {
        public StoreFront Store { get; set; }
        public Products Product { get; set; }
        public int Quantity { get; set; }
    }
}