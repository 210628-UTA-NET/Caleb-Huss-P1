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
    public class ShoppingCartController : Controller
    {
        private readonly IOrderBL _orderBL;
        private readonly ICustomerBL _custBL;
        public ShoppingCartController(IOrderBL p_orderBL, ICustomerBL p_custBL)
        {
            _orderBL = p_orderBL;
            _custBL = p_custBL;
        }
        public IActionResult Index()
        {
            return View(
                _orderBL.GetCartItems(HttpContext.Session.GetString("UserEmail"))
                .Select(cart => new ShoppingCartVM(cart)).ToList());
        }
        public IActionResult RemoveItem(int p_id)
        {
            _orderBL.RemoveFromCart(p_id, HttpContext.Session.GetString("UserEmail"));
            return RedirectToAction(nameof(Index));
        }
        public IActionResult PlaceOrder()
        {
            List<Cart> cartItems = _orderBL.GetCartItems(HttpContext.Session.GetString("UserEmail"));
            Orders newOrder = new Orders();
            newOrder.Date = DateTime.Now;
            newOrder.Customer = _custBL.GetCustomer(new Customers()
            {
                Email = HttpContext.Session.GetString("UserEmail")
            });
            foreach (Cart item in cartItems)
            {
                LineItems newLine = new LineItems()
                {
                    Product = item.Product,
                    Quantity = item.Quantity
                };
                newOrder.AddLineItem(newLine);
            }
            _orderBL.AddOrder(newOrder);
            _orderBL.EmptyCart(HttpContext.Session.GetString("UserEmail"));
            return View();
        }
    }
}
