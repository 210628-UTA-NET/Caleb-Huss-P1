using System;

namespace UI
{
    public class MainMenu : IMenu
    {
        public void Menu()
        {
            Console.WriteLine("====Main Menu====");
            Console.WriteLine("Where would you like to go?");
            Console.WriteLine("1) Customer Corner");
            Console.WriteLine("2) Employee Corner");
            Console.WriteLine("0) Exit");
        }

        public MenuType YourChoice()
        {
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "0":
                    return MenuType.Exit;
                case "1":
                    return MenuType.CustomerSelectCreateMenu;
                case "2":
                    return MenuType.EmployeeLoginMenu;
                default:
                    return MenuType.MainMenu;
            }


        }
    }
}