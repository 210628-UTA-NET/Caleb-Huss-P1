using System;
using Models;
using BL;
using System.Collections.Generic;

namespace UI
{
    public class CustomerStoreSelectorMenu : IMenu
    {

        public static StoreFront _store = new StoreFront();
        private IStoreBL _storeBL;
        private Customers searchStoreCust = CustomerSelectCreateMenu._newCust;
        private StoreFront _searchedstore = new StoreFront();
        public CustomerStoreSelectorMenu(IStoreBL p_storeBL)
        {
            _storeBL = p_storeBL;
        }

        public void Menu()
        {
            Console.WriteLine("==== Store Lookup ====");
            Console.WriteLine($"Welcome {searchStoreCust.Name},");
            Console.WriteLine("Please search for a store by its Store Number or Name");
            Console.WriteLine("If unsure please use the option to see all stores");
            Console.WriteLine("1) Search by Store Number");
            Console.WriteLine("2) Search by Store Name");
            Console.WriteLine("3) View list of all stores");
            Console.WriteLine("0) Go back");
        }

        public MenuType YourChoice()
        {
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "0":
                    return MenuType.CustomerSelectCreateMenu;
                case "1":
                    Console.WriteLine("Please enter the store number");
                    _searchedstore.StoreNumber = int.Parse(Console.ReadLine());
                    _store = _storeBL.GetStoreFront(_searchedstore);
                    if (_store.StoreNumber != 0)
                    {
                        Console.WriteLine("Store found. Press enter to continue");
                        Console.ReadLine();
                        return MenuType.CustomerCornerMenu;
                    }
                    Console.WriteLine("Could not find store. Press enter and try again");
                    Console.ReadLine();
                    return MenuType.CustomerStoreSelectorMenu;
                case "2":
                    Console.WriteLine("Please enter the store name");
                    _searchedstore.Name = (Console.ReadLine());
                    _store = _storeBL.GetStoreFront(_searchedstore);
                    if (_store.Name != null)
                    {
                        Console.WriteLine("Store found. Press enter to continue");
                        Console.ReadLine();
                        return MenuType.CustomerCornerMenu;
                    }
                    Console.WriteLine("Could not find store. Press enter and try again");
                    Console.ReadLine();
                    return MenuType.CustomerStoreSelectorMenu;
                case "3":
                    List<StoreFront> allStores = _storeBL.GetAllStores();
                    Console.WriteLine("===================");
                    foreach (StoreFront store in allStores)
                    {
                        Console.WriteLine(store);
                        Console.WriteLine("===================");
                    }
                    Console.WriteLine("Press enter to go back");
                    Console.ReadLine();
                    return MenuType.CustomerStoreSelectorMenu;
                default:
                    return MenuType.CustomerStoreSelectorMenu;
            }
        }
    }
}