using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL;
using WebUI.Models;
using Models;
using Microsoft.AspNetCore.Http;
namespace WebUI.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerBL _custBL;
        public CustomerController(ICustomerBL p_custBL)
        {
            _custBL = p_custBL;
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
                    if (_custBL.CheckCredentials(logVm.Email, logVm.Password))
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
                    Email = regVM.Email,
                    PhoneNumber = regVM.PhoneNumber
                    };

                    Customers addedCust = _custBL.AddCustomer(newCust, regVM.Password);
                    if(addedCust.Email != null)
                    {
                        HttpContext.Session.SetString("UserEmail",addedCust.Email);
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
            HttpContext.Session.SetString("UserEmail", null);
            return RedirectToAction(nameof(Index));

        }
        public IActionResult OrderHistory()
        {
            return View();
        }
    }
}
