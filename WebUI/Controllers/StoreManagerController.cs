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
        public StoreManagerController(IStoreBL p_storeBL, IInventoryBL p_inventoryBL)
        {
            _storeBL = p_storeBL;
            _inventoryBL = p_inventoryBL;
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
            return View(
                _inventoryBL.GetAllInventory(new StoreFront() { StoreNumber = p_num })
                .Select(inv => new InventoryVM(inv))
                .ToList()
                );
        }

    }
}
