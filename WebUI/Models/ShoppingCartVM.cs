using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace WebUI.Models
{
    public class ShoppingCartVM
    {
        public ShoppingCartVM(Cart p_cart)
        {
            Name = p_cart.Product.Name;
            Price = p_cart.Product.Price;
            ProductID = p_cart.Product.ProductID;
            Quantity = p_cart.Quantity;
        }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int ProductID { get; set; }
    }
}
