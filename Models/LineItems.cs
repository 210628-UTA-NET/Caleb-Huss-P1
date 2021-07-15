using System;

namespace Models
{
    public class LineItems
    {
        private Products _product;
        private int _quantity;

        public int LineItemID { get; set; }
        public Products Product
        {
            get
            {
                return _product;
            }
            set
            {
                _product = value;
            }
        }
        public int Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                _quantity = value;
            }
        }
        public override string ToString()
        {
            
            return $@"
==============
{Product}, Quantity: {Quantity}";
        }
    }
}