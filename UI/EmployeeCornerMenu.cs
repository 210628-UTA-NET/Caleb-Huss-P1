using System;

namespace UI
{
    public class EmployeeCornerMenu : IMenu
    {
        public void Menu()
        {
        
            Console.WriteLine("==== Employee Corner ====");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1) View store order history");
            Console.WriteLine("2) Replenish store inventory");
            Console.WriteLine("3) View store inventory");
            Console.WriteLine("0) Go back");
        }
        public MenuType YourChoice()
        {
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "0":
                    return MenuType.EmployeeStoreSelectorMenu;
                case "1":
                    return MenuType.ViewStoreOrderHistoryMenu;
                case "2":
                    return MenuType.ReplenishInventoryMenu;
                case "3":
                    return MenuType.EmployeeViewInventoryMenu;
                default:
                    return MenuType.EmployeeCornerMenu;
            }
        }
    }
}