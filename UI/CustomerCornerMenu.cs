using System;

namespace UI
{
    public class CustomerCornerMenu : IMenu
    {
    
        public void Menu()
        {
            Console.WriteLine("==== Customer Corner ====");
            Console.WriteLine("Welcome to the Customer Corner,");
            Console.WriteLine("what would you like to do?");
            Console.WriteLine("1) View Store Inventory");
            Console.WriteLine("2) Place an order");
            Console.WriteLine("3) View order history");
            Console.WriteLine("0) Go back");
        }

        public MenuType YourChoice()
        {
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "0":
                    return MenuType.CustomerStoreSelectorMenu;
                case "1":
                    return MenuType.ViewInventoryMenu;
                case "2":
                    return MenuType.CustomerOrderMenu;
                case "3":
                    return MenuType.CustomerViewOrderHistoryMenu;
                default:
                    return MenuType.CustomerCornerMenu;
            }

        }
    }
}