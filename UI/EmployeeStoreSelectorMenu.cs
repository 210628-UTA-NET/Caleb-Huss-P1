using System;
using Models;
using BL;
using System.Collections.Generic;

namespace UI
{
    public class EmployeeStoreSelectorMenu : IMenu
    {

        public static StoreFront _store = new StoreFront();
        private IStoreBL _storeBL;
        private StoreFront _searchedstore = new StoreFront();
        public EmployeeStoreSelectorMenu(IStoreBL p_storeBL)
        {
            _storeBL = p_storeBL;
        }

        public void Menu()
        {
            Console.WriteLine("==== Store Lookup ====");
            Console.WriteLine($"Store selector,");
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
                    return MenuType.MainMenu;
                case "1":
                    Console.WriteLine("Please enter the store number");
                    _searchedstore.StoreNumber = int.Parse(Console.ReadLine());
                    _store = _storeBL.GetStoreFront(_searchedstore);
                    if (_searchedstore.StoreNumber != 0)
                    {
                        Console.WriteLine("Store found press enter to continue");
                        Console.ReadLine();
                        return MenuType.EmployeeCornerMenu;
                    }
                    Console.WriteLine("Could not find store. Press enter and try again");
                    Console.ReadLine();
                    return MenuType.EmployeeStoreSelectorMenu;
                case "2":
                    Console.WriteLine("Please enter the store name");
                    _searchedstore.Name = (Console.ReadLine());
                    _store = _storeBL.GetStoreFront(_searchedstore);
                    if (_store.Name != null)
                    {
                        Console.WriteLine("Store found press enter to continue");
                        Console.ReadLine();
                        return MenuType.EmployeeCornerMenu;
                    }
                    Console.WriteLine("Could not find store. Press enter and try again");
                    Console.ReadLine();
                    return MenuType.EmployeeStoreSelectorMenu;
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
                    return MenuType.EmployeeStoreSelectorMenu;
                default:
                    return MenuType.EmployeeStoreSelectorMenu;
            }
        }
    }
}