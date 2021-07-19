using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Inventory
    {
        // public int ProductID { get; set; }
        // public int StoreID { get; set; }
        [Key]        
        public int InventoryID { get; set; }
        
        [ForeignKey("StoreFront")]
        public StoreFront Store { get; set; }
        // public virtual List<Products> Product { get; set; }
        // public int Quantity { get; set; }
    }
}