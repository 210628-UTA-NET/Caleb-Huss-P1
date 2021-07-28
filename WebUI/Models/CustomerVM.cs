using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace WebUI.Models
{
    public class CustomerVM
    {
        public CustomerVM()
        { }

        public CustomerVM(Customers p_cust)
        {
            FirstName = p_cust.FirstName;
            LastName = p_cust.LastName;
            Address = p_cust.Address;
            City = p_cust.City;
            State = p_cust.State;
            Email = p_cust.Email;
            PhoneNumber = p_cust.PhoneNumber;
            CustomerID = p_cust.CustomerID;

        }
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Email { get; set; }
        public long PhoneNumber { get; set; }
    }
}
