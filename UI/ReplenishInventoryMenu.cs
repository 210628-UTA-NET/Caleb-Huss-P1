using System;
using System.Collections.Generic;
using BL;
using Models;

namespace UI
{
    public class ReplenishInventoryMenu : IMenu
    {
        private IInventoryBL _invBL;
        private static LineItems _lineitem = new LineItems();
        private StoreFront _searchedstore = EmployeeStoreSelectorMenu._store;
        private static Products _Products = new Products();
        private static int _quantity;
        public ReplenishInventoryMenu(IInventoryBL p_invBL)
        {
            _invBL = p_invBL;
        }
        public void Menu()
        {
            Console.WriteLine($"==== Replenish {_searchedstore.Name}s inventory ====");
            Console.WriteLine("Choose what product to update and by how much");
            Console.WriteLine("1) Product ID - " + _Products.ProductID);
            Console.WriteLine("2) How many to add/subtract - " + _quantity);
            Console.WriteLine("3) Update Inventory");
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
                    Console.WriteLine("Enter the product ID");
                    _Products.ProductID = int.Parse(Console.ReadLine());
                    _lineitem = _invBL.GetSearchedInventory(_searchedstore, _Products)[0];
                    _Products = _lineitem.Product;
                    if (_Products.ProductID == 0)
                    {   
                        Console.WriteLine("Item not found. Press enter to continue");
                        Console.ReadLine();
                        return MenuType.ReplenishInventoryMenu;
                    }
                    Console.WriteLine(_lineitem);
                    Console.WriteLine("Press enter to go back");
                    Console.ReadLine();
                    return MenuType.ReplenishInventoryMenu;   
                case "2":
                    Console.WriteLine("How many items would you like to add or remove");
                    _quantity = int.Parse(Console.ReadLine());
                    if ((_lineitem.Quantity + _quantity) < 0)
                    {
                        _quantity = 0;
                        Console.WriteLine("Can't have negative product.");
                        Console.WriteLine("Press enter to continue");
                        Console.ReadLine();
                        return MenuType.ReplenishInventoryMenu;
                    }
                    return MenuType.ReplenishInventoryMenu;
                case "3":
                    _lineitem.Quantity += _quantity;
                    _invBL.ChangeInventory(_searchedstore, _lineitem);
                    _quantity = 0;
                    _lineitem= _invBL.GetSearchedInventory(_searchedstore, _Products)[0];
                    Console.WriteLine("Inventory Updated");
                    Console.WriteLine(_lineitem);
                    Console.WriteLine("Press enter to go back");
                    Console.ReadLine();
                    return MenuType.ReplenishInventoryMenu;
                default:
                    return MenuType.ReplenishInventoryMenu;
            }
        }
    }
}