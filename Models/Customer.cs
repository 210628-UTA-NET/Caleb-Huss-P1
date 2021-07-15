using System;
using System.Collections.Generic;

namespace Models
{
    public class Customers
    {
        private string _name;
        private string _address;
        private string _city;
        private string _state;
        private string _email;
        private long _phonenumber;
        private int _customerid;
        private List<Orders> _orderlist = new List<Orders>();

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
            }
        }
        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value;
            }
        }
        public string State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
            }
        }
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
            }

        }
        public long PhoneNumber
        {
            get
            {
                return _phonenumber;
            }
            set
            {
                _phonenumber = value;
            }
        }

        public int CustomerId { get { return _customerid; } set { _customerid = value; } }
        public List<Orders> OrderList
        {
            get
            {
                return _orderlist;
            }
            set
            {
                _orderlist = value;
            }
        }
        public void AddOrder(Orders p_order)
        {
            _orderlist.Add(p_order);
        }
        public override string ToString()
        {
            return $"Name: {Name}, Phonenumber: {PhoneNumber}, Email: {Email}, CustomerID: {CustomerId}, \nAddress: {Address}, City: {City}, State: {State}";
        }
    }
}