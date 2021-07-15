using System;
using Models;
using BL;
namespace UI
{
    public class CustomerSelectCreateMenu : IMenu
    {
        public static Customers _newCust = new Customers();
        private ICustomerBL _custBL;
        private Customers searchedCust = new Customers();
        public CustomerSelectCreateMenu(ICustomerBL p_custBL)
        {
            _custBL = p_custBL;
        }
        public void Menu()
        {
            Console.WriteLine("==== Customer Corner ====");
            Console.WriteLine("Welcome to the Customer Corner");
            Console.WriteLine("Please fill out the information below");
            Console.WriteLine("Note, if searching up profile only some info is needed");
            Console.WriteLine("1) Name - " + _newCust.Name);
            Console.WriteLine("2) Address - " + _newCust.Address);
            Console.WriteLine("3) City - " + _newCust.City);
            Console.WriteLine("4) State - " + _newCust.State);
            Console.WriteLine("5) Email - " + _newCust.Email);
            Console.WriteLine("6) PhoneNumber - " + _newCust.PhoneNumber);
            Console.WriteLine("7) Search with provided parameters");
            Console.WriteLine("8) Search with Customer ID");
            Console.WriteLine("9) Create Customer Profile");
            Console.WriteLine("10) Clear Search");
            Console.WriteLine("0) Go back to the main menu");
        }

        public MenuType YourChoice()
        {
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "0":
                    return MenuType.MainMenu;
                case "1":
                    Console.WriteLine("Enter Name:");
                    _newCust.Name = Console.ReadLine();
                    return MenuType.CustomerSelectCreateMenu;
                case "2":
                    Console.WriteLine("Enter Address:");
                    _newCust.Address = Console.ReadLine();
                    return MenuType.CustomerSelectCreateMenu;
                case "3":
                    Console.WriteLine("Enter City:");
                    _newCust.City = Console.ReadLine();
                    return MenuType.CustomerSelectCreateMenu;
                case "4":
                    Console.WriteLine("Enter State:");
                    _newCust.State = Console.ReadLine();
                    return MenuType.CustomerSelectCreateMenu;
                case "5":
                    Console.WriteLine("Enter Email:");
                    _newCust.Email = Console.ReadLine();
                    return MenuType.CustomerSelectCreateMenu;
                case "6":
                    Console.WriteLine("Enter PhoneNumber:");
                    _newCust.PhoneNumber = long.Parse(Console.ReadLine());
                    return MenuType.CustomerSelectCreateMenu;
                case "7":
                    searchedCust = _custBL.GetCustomer(_newCust);
                    if(searchedCust == null){
    
                    }else if (searchedCust.CustomerId != 0)
                    {
                       _newCust = searchedCust; 
                       return MenuType.CustomerStoreSelectorMenu;
                    }
                    Console.WriteLine("No customer found. Please try again");
                        Console.ReadLine();
                    return MenuType.CustomerSelectCreateMenu;
                case "8":
                    Console.WriteLine("Enter CustomerID");
                    _newCust.CustomerId = int.Parse(Console.ReadLine());
                    searchedCust = _custBL.GetCustomer(_newCust);
                    if (searchedCust.CustomerId != 0)
                    {
                        _newCust = searchedCust;
                        return MenuType.CustomerStoreSelectorMenu;

                    }
                    else
                    {   
                        Console.WriteLine("No customer found with that ID.");
                        Console.ReadLine();
                        return MenuType.CustomerSelectCreateMenu;
                    }

                    
                case "9":
                    _custBL.AddCustomer(_newCust);
                    return MenuType.CustomerStoreSelectorMenu;
                case "10":
                _newCust = new Customers();
                    return MenuType.CustomerSelectCreateMenu;
                default:
                    return MenuType.CustomerSelectCreateMenu;
            }
        }
    }
}