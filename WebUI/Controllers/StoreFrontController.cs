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
    public class StoreFrontController : Controller
    {
        private readonly IStoreBL _storeBL;
        private readonly IInventoryBL _inventoryBL;
        private readonly IOrderBL _orderBL;
        public StoreFrontController(IStoreBL p_storeBL, IInventoryBL p_inventoryBL,IOrderBL p_orderBL)
        {
            _storeBL = p_storeBL;
            _inventoryBL = p_inventoryBL;
            _orderBL = p_orderBL;
        }
        public IActionResult Index()
        {
            return View(
                _storeBL.GetAllStores()
                .Select(rest => new StoreFrontVM(rest))
            );
        }
        public IActionResult Inventory(int p_num)
        {
            return View(
                _inventoryBL.GetAllInventory(new StoreFront() { StoreNumber = p_num })
                .Select(inv => new InventoryVM(inv))
                .ToList()
                );
        }
        public IActionResult AddCart(int p_id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                return RedirectToAction("Login","Customer");
            }
            LineItems newLine = new LineItems()
            {
                Quantity = 1,
                Product = new Products
                {
                    ProductID = p_id
                }
            };
            _orderBL.AddToCart(newLine, HttpContext.Session.GetString("UserEmail"));
            return RedirectToAction(nameof(Index));
        }
    }
}
