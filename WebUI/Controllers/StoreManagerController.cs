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
    public class StoreManagerController : Controller
    {
        private readonly IStoreBL _storeBL;
        private readonly IInventoryBL _inventoryBL;
        private readonly IOrderBL _orderBL;
        public StoreManagerController(IStoreBL p_storeBL, IInventoryBL p_inventoryBL, IOrderBL p_orderBL)
        {
            _storeBL = p_storeBL;
            _inventoryBL = p_inventoryBL;
            _orderBL = p_orderBL;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Stores()
        {
            return View(
                _storeBL.GetAllStores()
                .Select(rest => new StoreFrontVM(rest))
            );
        }
        public IActionResult Inventory(int p_num)
        {
            HttpContext.Session.SetInt32("StoreNum", p_num);
            return View(
                _inventoryBL.GetAllInventory(new StoreFront() { StoreNumber = p_num })
                .Select(inv => new InventoryVM(inv))
                .ToList()
                );
        }
        public IActionResult Edit(int p_id)
        {
            HttpContext.Session.SetInt32("ProdId", p_id)
;            return View();
        }
        [HttpPost]
        public IActionResult Edit(EditVM editVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                LineItems newLine = new LineItems()
                {
                Quantity = editVM.Quantity,
                Product = new Products()
                {
                    ProductID = (int)HttpContext.Session.GetInt32("ProdId")
                }
            };
            StoreFront newStore = new StoreFront()
            {
                StoreNumber = (int)HttpContext.Session.GetInt32("StoreNum")
            };
            _inventoryBL.ChangeInventory(newStore,newLine);
            return RedirectToAction(nameof(Index));
                }
            }catch (Exception)
            {
                return View();
            }
            return View();


            
        }
        public IActionResult Orders(int p_num)
        {
            StoreFront newStore = new StoreFront()
            {
                StoreNumber = p_num
            };
            List<Orders> getOrders = _orderBL.GetAllOrders(newStore);
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
                    //Cost = item.TotalPrice,
                    ItemCount = q,
                    Date = item.Date
                });

            }

            return View(ordervm);
        }

    }
}
