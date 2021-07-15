using System;
using System.Collections.Generic;


namespace Models
{
    public class Orders
    {
        private List<LineItems> _itemslist = new List<LineItems>();
        private double _totalprice;
        private int _ordernum;
        public double TotalPrice
        {
            get
            {   _totalprice = 0;
                foreach (LineItems lineitem in _itemslist)
                {
                    _totalprice += lineitem.Product.Price * lineitem.Quantity;
                }
                return _totalprice;
            }
        }
        public int OrderNum
        {
            get
            {
                return _ordernum;
            }
            set
            {
                _ordernum = value;
            }
        }
        public List<LineItems> ItemsList
        {
            get
            {
                return _itemslist;
            }
            set
            {
                _itemslist = value;
            }
        }
        public StoreFront StoreFront { get; set; }
        public Customers Customer { get; set; }
        public void AddLineItem(Products p_product, int p_quantity){
            LineItems _newListItem = new LineItems()
            {
                Product = p_product,
                Quantity = p_quantity
            };
            _itemslist.Add(_newListItem);
        }
        public void AddLineItem(LineItems p_lineItem)
        {
            _itemslist.Add(p_lineItem);
        }
        public override string ToString()
        {
            string returner =($@"
========================
Beginning of Order
========================
====== Store Info ======
{StoreFront}
====== Customer Info ======
{Customer}
====== Order Info =====
Order number: {OrderNum}
Total Cost: ${TotalPrice.ToString("0.00")}");
            foreach (LineItems item in ItemsList)
            {
                returner += "\n" + item.ToString() ;
            }
            return returner;

        }

    }
}