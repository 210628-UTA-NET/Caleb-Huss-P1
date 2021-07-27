using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace WebUI.Models
{
    public class InventoryVM
    {
        public InventoryVM()
        {  }
        public InventoryVM(LineItems p_lineitem)
        {
            ProductID = p_lineitem.Product.ProductID;
            Name = p_lineitem.Product.Name;
            Description = p_lineitem.Product.Description;
            Price = p_lineitem.Product.Price;
            Quantity = p_lineitem.Quantity;
        }
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
