using System;
using System.Collections.Generic;

#nullable disable

namespace DL.Entities
{
    public partial class Inventory
    {
        public int? ProductId { get; set; }
        public int? StoreNumber { get; set; }
        public int? Quantity { get; set; }
        public int InventoryEntry { get; set; }

        public virtual Product Product { get; set; }
        public virtual Store StoreNumberNavigation { get; set; }
    }
}
