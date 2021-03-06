using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DL
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly DBContext _context;
        public InventoryRepository(DBContext p_context)
        {
            _context = p_context;
        }
        public LineItems ChangeInventory(StoreFront p_store, LineItems p_lineitem)
        {
            var result = (from i in _context.StoreInventories
                          join p in _context.Products on i.ProductID equals p.ProductID
                          join c in _context.Inventories on i.InventoryID equals c.InventoryID
                          where c.Store.StoreNumber == p_store.StoreNumber &&
                           p.ProductID == p_lineitem.Product.ProductID
                          select i
                          ).FirstOrDefault();
            result.Quantity += p_lineitem.Quantity;
            _context.Entry(result).State = EntityState.Modified;
            _context.SaveChanges();
            return new LineItems()
            {
                Product = result.Product,
                Quantity = result.Quantity
            };
        }

        public List<LineItems> GetAllInventory(StoreFront p_store)
        {
            List<LineItems> storeInventory;
            storeInventory = (from sInv in _context.StoreInventories
                              join inv in _context.Inventories on sInv.InventoryID equals inv.InventoryID
                              join s in _context.Stores on inv.Store.StoreNumber equals s.StoreNumber
                              join p in _context.Products on sInv.ProductID equals p.ProductID
                              where inv.Store.StoreNumber == p_store.StoreNumber
                              select new LineItems
                              {
                                  Product = sInv.Product,
                                  Quantity = sInv.Quantity
                              }
            ).ToList();
            return storeInventory;
        }

        public List<LineItems> GetSearchedInventory(StoreFront p_store, Products p_product)
        {
            List<LineItems> _inventory = new List<LineItems>();


            if (p_product.ProductID != 0)
            {
                _inventory = (from i in _context.StoreInventories
                              join p in _context.Products on i.ProductID equals p.ProductID
                              join c in _context.Inventories on i.InventoryID equals c.InventoryID
                              where c.Store.StoreNumber == p_store.StoreNumber &&
                               p.ProductID == p_product.ProductID
                              select new LineItems
                              {
                                  Product = i.Product,
                                  Quantity = i.Quantity
                              }).ToList();
            }
            else if (p_product.Name != null)
            {
                // Get invertory of a certain store with certain parameters
                _inventory = (from i in _context.StoreInventories
                              join p in _context.Products on i.ProductID equals p.ProductID
                              join c in _context.Inventories on i.InventoryID equals c.InventoryID
                              where c.Store.StoreNumber == p_store.StoreNumber &&
                               p.Name.Contains(p_product.Name)
                              select new LineItems
                              {  //map query to variables and create a list
                                  Product = i.Product,
                                  Quantity = i.Quantity
                              }).ToList();
            }
            else if (p_product.Price != 0)
            {
                _inventory = (from i in _context.StoreInventories
                              join p in _context.Products on i.ProductID equals p.ProductID
                              join c in _context.Inventories on i.InventoryID equals c.InventoryID
                              where c.Store.StoreNumber == p_store.StoreNumber &&
                              p.Price == p_product.Price

                              select new LineItems
                              {
                                  Product = i.Product,
                                  Quantity = i.Quantity
                              }).ToList();
            }

            else if (p_product.Categories != null)
            {
                var cat = p_product.Categories[0].Category;
                _inventory = (from p in _context.Products
                              join sInv in _context.StoreInventories on p.ProductID equals sInv.ProductID
                              join inv in _context.Inventories on sInv.InventoryID equals inv.InventoryID
                              where p.Categories.Any(c => c.Category == cat) &&
                              inv.Store.StoreNumber == p_store.StoreNumber

                              select new LineItems
                              {
                                  Product = sInv.Product,
                                  Quantity = sInv.Quantity
                              }).ToList();
            }
            return _inventory;
        }
    }
}