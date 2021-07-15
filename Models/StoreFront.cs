using System;
using System.Collections.Generic;

namespace Models
{
    public class StoreFront
    {
        private string _name;
        private string _address;
        private string _city;
        private string _state;

        private List<LineItems> _inventory = new List<LineItems>();
        private List<Orders> _orderslist = new List<Orders>();
        private int _storenum;

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
        public int StoreNumber
        {
            get
            {
                return _storenum;
            }
            set
            {
                _storenum = value;
            }
        }
        public List<LineItems> Inventory
        {
            get
            {
                return _inventory;
            }
            set
            {
                _inventory = value;
            }
        }
        public List<Orders> OrdersList
        {
            get
            {
                return _orderslist;
            }
            set
            {
                _orderslist = value;
            }
        }
        public void AddOrder(Orders p_order)
        {
            _orderslist.Add(p_order);
        }
        public void AddInventory(LineItems p_item)
        {
            _inventory.Add(p_item);
        }
        public override string ToString()
        {
            return $"Name: {Name}, Store Number: {StoreNumber}, \nAddress: {Address}, City: {City}, State: {State}";
        }
    }
}