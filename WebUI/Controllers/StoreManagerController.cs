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
        private readonly ICustomerBL _customerBL;
        public StoreManagerController(IStoreBL p_storeBL, IInventoryBL p_inventoryBL, IOrderBL p_orderBL, ICustomerBL p_customerBL)
        {
            _storeBL = p_storeBL;
            _inventoryBL = p_inventoryBL;
            _orderBL = p_orderBL;
            _customerBL = p_customerBL;
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
        public IActionResult Orders(int p_num, string p_sort)
        {
            ViewBag.StoreNumberOrder = p_num;
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
        public IActionResult Customers()
        {
            List<Customers> allCusts = _customerBL.GetAllCustomers();
            List<CustomerVM> allCustsVM = new List<CustomerVM>();
            foreach (Customers cust in allCusts)
            {
                allCustsVM.Add(new CustomerVM(cust));
            }
            return View(allCustsVM);
        }
        public IActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Search(CustomerVM p_custVM)
        {
            List<Customers> foundCusts = _customerBL.GetCertainCustomers(new Customers() { FirstName = p_custVM.FirstName, LastName = p_custVM.LastName });
            List<CustomerVM> allCustsVM = new List<CustomerVM>();
            foreach (Customers cust in foundCusts)
            {
                allCustsVM.Add(new CustomerVM(cust));
            }
            return View("Customers",allCustsVM);
        }
    }
}
