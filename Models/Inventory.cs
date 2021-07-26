using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Inventory
    {
        [Key]        
        public int InventoryID { get; set; }
        
        [ForeignKey("StoreFront")]
        public StoreFront Store { get; set; }
    }
}