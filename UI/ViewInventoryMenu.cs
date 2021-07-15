using System;
using System.Collections.Generic;
using BL;
using Models;

namespace UI
{

    public class ViewInventoryMenu : IMenu
    {
        private IInventoryBL _invBL;
        private static Products _product = new Products();
        private Customers _searchedCust = CustomerSelectCreateMenu._newCust;
        protected List<LineItems> _specificinventory = new List<LineItems>();
        private StoreFront _searchedstore = CustomerStoreSelectorMenu._store;
        public ViewInventoryMenu(IInventoryBL p_invBL)
        {
            _invBL = p_invBL;
        }
        public void Menu()
        {
            Console.WriteLine($"==== {_searchedstore.Name}s Inventory Menu ====");
            Console.WriteLine("Here you can search for specific items or view the entire inventory");
            Console.WriteLine("1) Search by product name");
            Console.WriteLine("2) Search by product ID");
            Console.WriteLine("3) Search by product price");
            Console.WriteLine("4) Search by category");
            Console.WriteLine("5) View entire inventory");
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
                    Console.WriteLine("Enter Product name");
                    _product.Name = Console.ReadLine();
                    _specificinventory = _invBL.GetSearchedInventory(_searchedstore, _product);
                    Console.WriteLine("================");
                    if (_specificinventory.Count == 0)
                    {
                        Console.WriteLine("Could not find any products");
                    }
                    foreach (LineItems lineitem in _specificinventory)
                    {
                        Console.WriteLine(lineitem);
                        Console.WriteLine("================");
                    }
                    Console.WriteLine("Press enter to go back");
                    Console.ReadLine();

                    return MenuType.ViewInventoryMenu;;
                    
                case "2":
                    Console.WriteLine("Enter product ID");
                    _product.ProductID = int.Parse(Console.ReadLine());
                    _specificinventory = _invBL.GetSearchedInventory(_searchedstore, _product);
                    Console.WriteLine("================");
                    if (_specificinventory.Count == 0)
                    {
                        Console.WriteLine("Could not find any products");
                    }
                    foreach (LineItems lineitem in _specificinventory)
                    {
                        Console.WriteLine(lineitem);
                        Console.WriteLine("================");
                    }
                    Console.WriteLine("Press enter to go back");
                    Console.ReadLine();

                    return MenuType.ViewInventoryMenu;;
                   
                case "3":
                    Console.WriteLine("Enter product price");
                    _product.Price = float.Parse(Console.ReadLine());
                    _specificinventory = _invBL.GetSearchedInventory(_searchedstore, _product);
                    Console.WriteLine("================");
                    if (_specificinventory.Count == 0)
                    {
                        Console.WriteLine("Could not find any products");
                    }
                    foreach (LineItems lineitem in _specificinventory)
                    {
                        Console.WriteLine(lineitem);
                        Console.WriteLine("================");
                    }
                    Console.WriteLine("Press enter to go back");
                    Console.ReadLine();

                    return MenuType.ViewInventoryMenu;;
                    
                case "4":
                    Console.WriteLine("Enter category");
                    _product.Category = Console.ReadLine();
                    _specificinventory = _invBL.GetSearchedInventory(_searchedstore, _product);
                    Console.WriteLine("================");
                    if (_specificinventory.Count == 0)
                    {
                        Console.WriteLine("Could not find any products");
                    }
                    foreach (LineItems lineitem in _specificinventory)
                    {
                        Console.WriteLine(lineitem);
                        Console.WriteLine("================");
                    }
                    Console.WriteLine("Press enter to go back");
                    Console.ReadLine();

                    return MenuType.ViewInventoryMenu;

                case "5":
                    List<LineItems> _allinventory = _invBL.GetAllInventory(_searchedstore);
                    Console.WriteLine("================");
                    if (_allinventory.Count == 0)
                    {
                        Console.WriteLine("This store has no inventory.");
                    }
                    foreach (LineItems lineitem in _allinventory)
                    {
                        Console.WriteLine(lineitem);
                        Console.WriteLine("================");
                    }
                    Console.WriteLine("Press enter to go back");
                    Console.ReadLine();
                    return MenuType.ViewInventoryMenu;
                default:
                    return MenuType.ViewInventoryMenu;

            }
        }
    }
}