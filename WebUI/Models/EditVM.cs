using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class EditVM
    {
        [Required]
        public int Quantity { get; set; }
    }
}
