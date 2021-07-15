using System;
using System.Collections.Generic;
using BL;
using Models;

namespace UI
{
    public class CustomerOrderMenu : IMenu
    {
        private static Orders _order = new Orders();
        private Products _product = new Products();
        private List<LineItems> _foundLineItems;
        private Customers _searchCust = new Customers();
        private Customers _searchedCust = CustomerSelectCreateMenu._newCust;
        private StoreFront _searchedStore = CustomerStoreSelectorMenu._store;
        private IOrderBL _orderBL;
        private IInventoryBL _invBL;

        public CustomerOrderMenu(IOrderBL p_orderBL, IInventoryBL p_invBL)
        {
            _orderBL = p_orderBL;
            _invBL = p_invBL;
        }


        public void Menu()
        {
            _order.Customer = _searchedCust;
            _order.StoreFront = _searchedStore;
            Console.WriteLine("==== Place an Order ====");
            Console.WriteLine("Search for and select an item");
            Console.WriteLine("and how many you want to order");
            Console.WriteLine("1) Search by product name");
            Console.WriteLine("2) Search by product ID");
            Console.WriteLine("3) Search by price");
            Console.WriteLine("4) Search by category");
            Console.WriteLine("5) View entire inventory");
            Console.WriteLine("6) Add product with ID");
            Console.WriteLine("7) Review order");
            Console.WriteLine("8) Place order");
            Console.WriteLine("9) Clear order");
            Console.WriteLine("0) Go back");
            Console.WriteLine("Order Total: $" + _order.TotalPrice.ToString("0.00"));
        }

        public MenuType YourChoice()
        {
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "0":
                    return MenuType.CustomerCornerMenu;

                case "1":
                    Console.WriteLine("Enter product name");
                    _product.Name = Console.ReadLine();
                    _foundLineItems = _invBL.GetSearchedInventory(_searchedStore, _product);
                    if (_foundLineItems.Count == 0)
                    {
                        Console.WriteLine("Product not found. Press enter and try again");
                        Console.ReadLine();
                        return MenuType.CustomerOrderMenu;
                    }
                    foreach (LineItems item in _foundLineItems)
                    {
                        Console.WriteLine(item);
                    }
                    Console.WriteLine("Press enter to go back");
                    Console.ReadLine();
                    return MenuType.CustomerOrderMenu;

                case "2":
                    Console.WriteLine("Enter product id");
                    _product.ProductID = int.Parse(Console.ReadLine());
                    _foundLineItems = _invBL.GetSearchedInventory(_searchedStore, _product);
                    if (_foundLineItems.Count == 0)
                    {
                        Console.WriteLine("Product not found. Press enter and try again");
                        Console.ReadLine();
                        return MenuType.CustomerOrderMenu;
                    }
                    foreach (LineItems item in _foundLineItems)
                    {
                        Console.WriteLine(item);
                    }
                    Console.WriteLine("Press enter to go back");
                    Console.ReadLine();
                    return MenuType.CustomerOrderMenu;

                case "3":
                    Console.WriteLine("Enter product price");
                    _product.Price = float.Parse(Console.ReadLine());
                    _foundLineItems = _invBL.GetSearchedInventory(_searchedStore, _product);
                    if (_foundLineItems.Count == 0)
                    {
                        Console.WriteLine("Product not found. Press enter and try again");
                        Console.ReadLine();
                        return MenuType.CustomerOrderMenu;
                    }
                    foreach (LineItems item in _foundLineItems)
                    {
                        Console.WriteLine(item);
                    }
                    Console.WriteLine("Press enter to go back");
                    Console.ReadLine();
                    return MenuType.CustomerOrderMenu;

                case "4":
                    Console.WriteLine("Enter product category");
                    _product.Category = Console.ReadLine();
                    _foundLineItems = _invBL.GetSearchedInventory(_searchedStore, _product);
                    if (_foundLineItems.Count == 0)
                    {
                        Console.WriteLine("Product not found. Press enter and try again");
                        Console.ReadLine();
                        return MenuType.CustomerOrderMenu;
                    }

                    foreach (LineItems item in _foundLineItems)
                    {
                        Console.WriteLine(item);
                    }
                    
                    Console.WriteLine("Press enter to go back");
                    Console.ReadLine();
                    return MenuType.CustomerOrderMenu;


                case "5":
                    List<LineItems> inv = _invBL.GetAllInventory(_searchedStore);
                    foreach (LineItems lineitem in inv)
                    {
                        Console.WriteLine(lineitem);
                    }
                    Console.WriteLine("Press enter to go back");
                    Console.ReadLine();
                    return MenuType.CustomerOrderMenu;

                case "6":
                    Console.WriteLine("Enter product id");
                    _product.ProductID = int.Parse(Console.ReadLine());
                    _foundLineItems = _invBL.GetSearchedInventory(_searchedStore, _product);
                    if (_foundLineItems.Count == 0)
                    {
                        Console.WriteLine("Product not found. Press enter and try again");
                        Console.ReadLine();
                        return MenuType.CustomerOrderMenu;
                    }
                    Console.WriteLine(_foundLineItems[0]);
                    Console.WriteLine("How many would you like to order?");
                    int _tempQuantity = int.Parse(Console.ReadLine());
                    if (_tempQuantity > _foundLineItems[0].Quantity)
                    {
                        Console.WriteLine("You can't order more than we have");
                        Console.ReadLine();
                        return MenuType.CustomerOrderMenu;
                    }
                    _order.AddLineItem(new LineItems(){
                        Product = _foundLineItems[0].Product,
                        Quantity = _tempQuantity
                    });         
                    return MenuType.CustomerOrderMenu;

                case "7":
                    Console.WriteLine("Products in your order");
                    foreach (LineItems item in _order.ItemsList)
                    {
                        Console.WriteLine(item);
                    }
                    Console.WriteLine("Press enter");
                    Console.ReadLine();

                    return MenuType.CustomerOrderMenu;

                case "8":
                    Orders ordered = _orderBL.AddOrder(_order);
                    Console.WriteLine(ordered);
                    Console.WriteLine("Press enter to go back");
                    Console.ReadLine();
                    return MenuType.CustomerOrderMenu;

                case "9":
                    _order.ItemsList = new List<LineItems>();
                    return MenuType.CustomerOrderMenu;

                default:
                    return MenuType.CustomerOrderMenu;
            }
        }
    }
}