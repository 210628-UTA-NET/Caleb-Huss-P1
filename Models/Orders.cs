using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Orders
    {
        public decimal TotalPrice
        {
            get
            {   decimal _totalprice = 0;
                foreach (LineItems lineitem in ItemsList)
                {
                    _totalprice += lineitem.Product.Price * lineitem.Quantity;
                }
                return _totalprice;
            }
        }
        public DateTime Date { get; set; }
        [Key]
        public int OrderNum{ get; set; }
        public List<LineItems> ItemsList{ get; set; }
        public StoreFront StoreFront { get; set; }
        public Customers Customer { get; set; }
        public void AddLineItem(Products p_product, int p_quantity){
            LineItems newListItem = new LineItems()
            {
                Product = p_product,
                Quantity = p_quantity
            };
            List<LineItems> tempList = this.ItemsList;
            tempList.Add(newListItem);
            this.ItemsList = tempList;
        }
        public void AddLineItem(LineItems p_lineItem)
        {
            List<LineItems> tempList = this.ItemsList;
            tempList.Add(p_lineItem);
            this.ItemsList = tempList;
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