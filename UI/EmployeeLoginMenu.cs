using System;
using Models;
namespace UI
{

    public class EmployeeLoginMenu : IMenu
    {
        private static UserLogin _userlog = new UserLogin();

        public void Menu()
        {
            Console.WriteLine("=== Employee Login ===");
            Console.WriteLine("Please enter your credentials: ");
            Console.WriteLine("1) Enter EmployeeID - " + _userlog.EmployeeID);
            Console.WriteLine("2) Enter password - " + _userlog.Password);
            Console.WriteLine("3) Login");
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
                Console.WriteLine("Please input EmployeeID");
                    _userlog.EmployeeID = int.Parse(Console.ReadLine());
                    return MenuType.EmployeeLoginMenu;
                case "2":
                Console.WriteLine("Please input your password");
                    _userlog.Password = Console.ReadLine();
                    return MenuType.EmployeeLoginMenu;
                case "3":
                    if (_userlog.CheckCreditials())
                    {
                        return MenuType.EmployeeStoreSelectorMenu;
                    }
                    else
                    {
                        Console.WriteLine("Invalid login info. Press enter to go back.");
                        Console.ReadLine();
                        return MenuType.EmployeeLoginMenu;
                    }
                default:
                    return MenuType.EmployeeLoginMenu;
            }
        }
    }
}