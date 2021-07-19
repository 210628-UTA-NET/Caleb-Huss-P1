using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Customers
    {
       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [Required]
        public string Email { get; set; }
        public long PhoneNumber{ get; set; }
        [Key]
        public int CustomerID { get; set; }
        public List<Orders> OrderList { get; set; }
        public virtual UserLogin UserLogin { get; set; }

        public void AddOrder(Orders p_order)
        {
            OrderList.Add(p_order);
        }
        public override string ToString()
        {
            return $"Name: {FirstName} {LastName}, Phonenumber: {PhoneNumber}, Email: {Email}, CustomerID: {CustomerID}, \nAddress: {Address}, City: {City}, State: {State}";
        }
    }
}