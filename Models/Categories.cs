using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Categories
    {
        [Key]
        public int CategoryID { get; set; }
        public string Category { get; set; }

        public List<Products> Products {get; set;}
    }
}