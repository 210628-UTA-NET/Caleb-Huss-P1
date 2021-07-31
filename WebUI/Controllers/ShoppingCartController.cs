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
        private readonly IStoreBL _storeBL;
        public ShoppingCartController(IOrderBL p_orderBL, ICustomerBL p_custBL, IStoreBL p_storeBL)
        {
            _orderBL = p_orderBL;
            _custBL = p_custBL;
            _storeBL = p_storeBL;
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
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                return RedirectToAction(nameof(Index));
            }
            List<Cart> cartItems = _orderBL.GetCartItems(HttpContext.Session.GetString("UserEmail"));
            Orders newOrder = new Orders();
            newOrder.Date = DateTime.Now;
            newOrder.Customer = _custBL.GetCustomer(new Customers()
            {
                Email = HttpContext.Session.GetString("UserEmail")
            });
            newOrder.StoreFront = _storeBL.GetStoreFront(new StoreFront() { StoreNumber = 1 });
            List<LineItems> tempList = new List<LineItems>();
            foreach (Cart item in cartItems)
            {
                LineItems newLine = new LineItems()
                {
                    Product = item.Product,
                    Quantity = item.Quantity
                };
                tempList.Add(newLine);
            }
            newOrder.ItemsList = tempList;
            _orderBL.AddOrder(newOrder);
            _orderBL.EmptyCart(HttpContext.Session.GetString("UserEmail"));
            return View();
        }
    }
}
