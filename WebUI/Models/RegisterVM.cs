using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Models
{
    public class RegisterVM
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [Required]
        public string Email { get; set; }
        [Range(1000000000,9999999999)]
        public long PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
