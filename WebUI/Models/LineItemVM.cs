using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace WebUI.Models
{
    public class LineItemVM
    {
        public LineItemVM(LineItems p_lineItem)
        {
            Name = p_lineItem.Product.Name;
            Description = p_lineItem.Product.Description;
            Price = p_lineItem.Product.Price;
            Quantity = p_lineItem.Quantity;

        }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
