using System;
using System.Collections.Generic;
using BL;
using Models;

namespace UI
{

    public class CustomerViewOrderHistoryMenu : IMenu
    {
        private Customers _searchedCust = CustomerSelectCreateMenu._newCust;
        private StoreFront _searchedStore = CustomerStoreSelectorMenu._store;
        private static IOrderBL _orderBL;
        public CustomerViewOrderHistoryMenu(IOrderBL p_orderBL)
        {
            _orderBL = p_orderBL;
        }
        public void Menu()
        {
            Console.WriteLine("==== Order History Menu ====");
            Console.WriteLine($"What would you like to do {_searchedCust.Name}?");
            Console.WriteLine($"1) View Orders made at {_searchedStore.Name})");
            Console.WriteLine("2) View all orders made");
            Console.WriteLine("0) Go back");
        }

        public MenuType YourChoice()
        {
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "0":
                    return MenuType.CustomerCornerMenu;

                case "1":
                    Console.WriteLine(_searchedCust);
                    Console.WriteLine(_searchedStore);
                    List<Orders> _orders = _orderBL.GetAllOrders(_searchedStore, _searchedCust);
 
                    foreach (Orders order in _orders)
                    {
                        Console.WriteLine(order);
                    }
                    Console.WriteLine("Press enter to go back");
                    Console.ReadLine();
                    return MenuType.CustomerViewOrderHistoryMenu;

                case "2":
                    List<Orders> _allOrders = _orderBL.GetAllOrders(_searchedCust);
                    foreach (Orders order in _allOrders)
                    {
                        Console.WriteLine(order);
                    }
                    Console.WriteLine("Press enter to go back");
                    Console.ReadLine();
                    return MenuType.CustomerViewOrderHistoryMenu;
                    
                default:
                    return MenuType.CustomerViewOrderHistoryMenu;
            }
        }
    }
}