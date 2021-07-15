using System;
using System.Collections.Generic;

#nullable disable

namespace DL.Entities
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int StoreNumber { get; set; }
        public int CustomerId { get; set; }
        public int LineItemId { get; set; }
        public int InvKey { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Store StoreNumberNavigation { get; set; }
    }
}
