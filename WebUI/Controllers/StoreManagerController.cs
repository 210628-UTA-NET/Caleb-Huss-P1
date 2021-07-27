using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    public class StoreManagerController : Controller
    {
        // GET: StoreManagerController
        public ActionResult Index()
        {
            return View();
        }

        // GET: StoreManagerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StoreManagerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StoreManagerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StoreManagerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StoreManagerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StoreManagerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StoreManagerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
