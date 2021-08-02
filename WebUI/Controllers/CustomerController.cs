using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL;
using WebUI.Models;
using Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace WebUI.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerBL _custBL;
        private readonly IOrderBL _orderBL;
        private readonly ILogger _logger;
        public CustomerController(ICustomerBL p_custBL, IOrderBL p_orderBL, ILogger<CustomerController> logger)
        {
            _custBL = p_custBL;
            _orderBL = p_orderBL;
            _logger = logger;
        }
        public IActionResult Index()
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                return View("Login");
            }
            return View("CustomerCorner");
        }

        public IActionResult CustomerCorner()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginVM logVm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_custBL.CheckCredentials(logVm.Email.ToLower(), logVm.Password))
                    {
                        HttpContext.Session.SetString("UserEmail", logVm.Email);
                        return View("CustomerCorner");
                    }
                } 
            }
            catch (Exception)
            {
                return View();
            }
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterVM regVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Customers newCust = new Customers() { 
                    FirstName = regVM.FirstName,
                    LastName = regVM.LastName,
                    Address = regVM.Address,
                    City = regVM.City,
                    State = regVM.State,
                    Email = regVM.Email.ToLower(),
                    PhoneNumber = regVM.PhoneNumber
                    };

                    Customers addedCust = _custBL.AddCustomer(newCust, regVM.Password);
                    if(addedCust.Email != null)
                    {
                        HttpContext.Session.SetString("UserEmail",addedCust.Email.ToLower());
                        return View("CustomerCorner");
                    }
                }
            }
            catch(Exception)
            {
                return View();
            }
            

            
            return View();
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.SetString("UserEmail", "");
            return RedirectToAction(nameof(Index));

        }
        public IActionResult OrderHistory(string p_sort)
        {
            Customers cust = new Customers()
            {
                Email = HttpContext.Session.GetString("UserEmail")
            };
            Customers getCust = _custBL.GetCustomer(cust);

            List<Orders> getOrders = _orderBL.GetAllOrders(getCust);
            List<OrderVM> ordervm = new List<OrderVM>();
            int q = 0;
       
            foreach (Orders item in getOrders)
            {
                q = 0;
                foreach (LineItems li in item.ItemsList)
                {
                    q += li.Quantity;

                }
                ordervm.Add(new OrderVM()
                {
                    OrderNumber = item.OrderNum,
                    Cost = item.TotalPrice,
                    ItemCount = q,
                    Date = item.Date
                });

            }
            switch (p_sort)
            {
                case "date_asc":
                    ordervm = ordervm.OrderBy(s => s.Date).ToList();
                    break;
                case "date_desc":
                    ordervm = ordervm.OrderByDescending(s => s.Date).ToList();
                    break;
                case "price_asc":
                    ordervm = ordervm.OrderBy(s => s.Cost).ToList();
                    break;
                case "price_desc":
                    ordervm = ordervm.OrderByDescending(s => s.Cost).ToList();
                    break;
                default:
                    ordervm = ordervm.OrderBy(s => s.Cost).ToList();
                    break;
            }


            return View(ordervm);
        }
        public IActionResult OrderDetails(int p_orderNum)
        {
            Orders sOrder = _orderBL.GetAnOrder(p_orderNum);
            List<LineItemVM> lineitemsVM = new List<LineItemVM>();
            foreach (LineItems item in sOrder.ItemsList)
            {
                lineitemsVM.Add(new LineItemVM(item));
            }
            
            return View(lineitemsVM);

        }
    }
}
