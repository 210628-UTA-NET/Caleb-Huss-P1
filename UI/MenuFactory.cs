using System.IO;
using BL;
using DL;
using DL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace UI
{
    public class MenuFactory : IFactory
    {
        public IMenu GetMenu(MenuType p_menu)
        {
            //Get the configuration from the appsetting.json file
            var configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsetting.json")
                        .Build();

            // Get the connectionString from appsetting
            string connectionString = configuration.GetConnectionString("Reference2DB");
            DbContextOptions<DemoDBContext> options = new DbContextOptionsBuilder<DemoDBContext>()
                .UseSqlServer(connectionString)
                .Options;

            switch (p_menu)
            {
                case MenuType.MainMenu:
                    return new MainMenu();
                case MenuType.CustomerSelectCreateMenu:
                    return new CustomerSelectCreateMenu(new CustomerBL(new CustomerRepository(new DemoDBContext(options))));
                case MenuType.CustomerStoreSelectorMenu:
                    return new CustomerStoreSelectorMenu(new StoreBL(new StoreRepository(new DemoDBContext(options))));
                case MenuType.CustomerCornerMenu:
                    return new CustomerCornerMenu();
                case MenuType.CustomerOrderMenu:
                    return new CustomerOrderMenu(new OrderBL(new OrderRepository(new DemoDBContext(options))),new InventoryBL(new InventoryRepository(new DemoDBContext(options))));
                case MenuType.CustomerViewOrderHistoryMenu:
                    return new CustomerViewOrderHistoryMenu(new OrderBL(new OrderRepository(new DemoDBContext(options))));    
                case MenuType.ViewInventoryMenu:
                    return new ViewInventoryMenu(new InventoryBL(new InventoryRepository(new DemoDBContext(options))));
                case MenuType.EmployeeLoginMenu:
                    return new EmployeeLoginMenu();
                 case MenuType.EmployeeCornerMenu:
                    return new EmployeeCornerMenu();   
                case MenuType.EmployeeStoreSelectorMenu:
                    return new EmployeeStoreSelectorMenu(new StoreBL(new StoreRepository(new DemoDBContext(options))));
                case MenuType.ViewStoreOrderHistoryMenu:
                    return new ViewStoreOrderHistoryMenu(new CustomerBL(new CustomerRepository(new DemoDBContext(options))),new OrderBL(new OrderRepository(new DemoDBContext(options))));
                case MenuType.ReplenishInventoryMenu:
                    return new ReplenishInventoryMenu(new InventoryBL(new InventoryRepository(new DemoDBContext(options))));
                case MenuType.EmployeeViewInventoryMenu:
                    return new EmployeeViewInventoryMenu(new InventoryBL(new InventoryRepository(new DemoDBContext(options))));

                default:
                    return null;
            }
        }
    }
}