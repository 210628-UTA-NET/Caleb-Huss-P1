using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class OrderVM
    {

        public int OrderNumber { get; set; }
        public int ItemCount { get; set; }
        //public decimal Cost { get; set; }
        public DateTime Date { get; set; }
    }
}
