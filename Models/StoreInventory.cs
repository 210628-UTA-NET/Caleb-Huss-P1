using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class StoreInventory
    {

        //These keys are here so that I can create a composite key in the DBContext file
        [ForeignKey("Inventory")]
        public int InventoryID { get; set; }
        [ForeignKey("Products")]
        public int ProductID { get; set; }
        public Inventory Inventory { get; set; }
        public Products Product { get; set; }
        public int Quantity { get; set; }
    }
}