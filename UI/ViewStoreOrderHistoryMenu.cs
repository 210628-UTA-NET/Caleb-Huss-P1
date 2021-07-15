using System;
using Models;
using BL;
using System.Collections.Generic;

namespace UI
{
    public class ViewStoreOrderHistoryMenu : IMenu
    {
        private static Customers _cust = new Customers();
        private Customers _searchedCust;
        private ICustomerBL _custBL;
        private IOrderBL _orderBL;
        private StoreFront _searchedstore = EmployeeStoreSelectorMenu._store;
        public ViewStoreOrderHistoryMenu(ICustomerBL p_custBL, IOrderBL p_orderBL)
        {
                _custBL = p_custBL;
                _orderBL = p_orderBL;   
        }
        public void Menu()
        {
           Console.WriteLine($"==== {_searchedstore.Name}s Order History ====");
           Console.WriteLine("View all orders for this store or just from select customers");
           Console.WriteLine("1) Customer ID - " + _cust.CustomerId);
           Console.WriteLine("2) Search orders");
           Console.WriteLine("3) Get all orders");
           Console.WriteLine("0) Go back");
        }

        public MenuType YourChoice()
        {
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "0":
                    return MenuType.EmployeeCornerMenu;
                case "1":
                    Console.WriteLine("Please enter the customers id");
                    _cust.CustomerId = int.Parse(Console.ReadLine());
                    return MenuType.ViewStoreOrderHistoryMenu;

                case "2":
                    _searchedCust = _custBL.GetCustomer(_cust);
                    // Console.WriteLine(_searchedCust);
                    // Console.WriteLine(_searchedstore);
                    if (_searchedCust.CustomerId == 0)
                    {
                        Console.WriteLine("Customer not found. Press enter and try again");
                        Console.ReadLine();
                        return MenuType.ViewStoreOrderHistoryMenu;
                    }
                    List<Orders> _searchedStoreOrders = _orderBL.GetAllOrders(_searchedstore, _searchedCust);
                    if (_searchedStoreOrders.Count == 0)
                    {
                        Console.WriteLine("No orders found");
                    }
                    foreach (Orders _orders in _searchedStoreOrders)
                    {
                        Console.WriteLine(_orders);
                    }
                    Console.WriteLine("Press enter to go back");
                    Console.ReadLine();
                    return MenuType.ViewStoreOrderHistoryMenu;
                case "3":
                    List<Orders> _storeOrders = _orderBL.GetAllOrders(_searchedstore);
                    if (_storeOrders.Count == 0)
                    {
                        Console.WriteLine("No orders found");
                    }
                    foreach (Orders _order in _storeOrders)
                    {
                        Console.WriteLine(_order);
                    }
                    Console.WriteLine("Press enter to go back");
                    Console.ReadLine();
                    return MenuType.ViewStoreOrderHistoryMenu;
                default:    
                    return MenuType.ViewStoreOrderHistoryMenu;
            }
        }
    }
}