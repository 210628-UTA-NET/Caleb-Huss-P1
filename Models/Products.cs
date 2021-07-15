using System;

namespace Models
{
    public class Products
    {
        private string _name;
        private float _price;
        private string _desc = "No description set";
        private string _category = "No category set";

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
        public float Price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
            }
        }
        public string Description
        {
            get
            {
                return _desc;
            }
            set
            {
                _desc = value;
            }
        }
        public string Category
        {
            get
            {
                return _category;
            }
            set
            {
                _category = value;
            }
        }
        public int ProductID { get; set; }
        public override string ToString()
        {
            return $"Product Name: {Name}, Price: ${Price}\nDescription: {Description},\nCategory: {Category}, ProductID: {ProductID}";
        }
    }
}