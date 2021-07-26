using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class StoreFrontController : Controller
    {
        private IStoreBL _storeBL;
        public StoreFrontController(IStoreBL p_storeBL)
        {
            _storeBL = p_storeBL;
        }
        public IActionResult Index()
        {
            return View(
                _storeBL.GetAllStores()
                .Select(rest => new StoreFrontVM(rest))
            ); ;
        }
    }
}
