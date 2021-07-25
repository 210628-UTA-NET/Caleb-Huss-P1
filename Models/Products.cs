using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Products
    {
        public string Name{ get; set; }
        public decimal Price{ get; set; }
        public string Description{ get; set; }
        [Key]
        public int ProductID { get; set; }
        public List<Categories> Categories { get; set; }

        public override string ToString()
        {
            return $"Product Name: {Name}, Price: ${Price}\nDescription: {Description},\nProductID: {ProductID}";
        }
    }
}