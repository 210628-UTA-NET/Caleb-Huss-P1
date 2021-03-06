using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class LineItems
    {
        [Key]
        public int LineItemID { get; set; }
        public Products Product{ get; set; }
        public int Quantity{ get; set; }
        [ForeignKey("Orders")]
        public int OrdersOrderNum { get; set; }
        
        public override string ToString()
        {
            
            return $@"
==============
{Product}, Quantity: {Quantity}";
        }
    }
}