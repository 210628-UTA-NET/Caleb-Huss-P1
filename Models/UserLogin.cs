using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
namespace Models
{
    public class UserLogin
    {
        [Key]
        [ForeignKey("Customers")]
        public int CustomerID { get; set; }
        public string hash { get; set; }
        public string salt { get; set; }
        public Customers Customer { get; set; }
    }
}