using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DL
{
    public class InventoryRepository : IInventoryRepository
    {
        private DBContext _context;
        private List<LineItems> _inventory = new List<LineItems>();
        public InventoryRepository(DBContext p_context)
        {
            _context = p_context;
        }
        public LineItems ChangeInventory(StoreFront p_store, LineItems p_lineitem)
        {
            throw new NotImplementedException();
            // Get the specific inventory item needing to be changed
            //var result = (from i in _context.Inventories
            //                  where i.ProductId == p_lineitem.Product.ProductID &&
            //                  i.StoreNumber == p_store.StoreNumber
            //                  select i).SingleOrDefault();
            //    result.Quantity = p_lineitem.Quantity; // change the quantity here

            //    _context.Entry(result).State = EntityState.Modified;
            //    _context.SaveChanges();
            //    return new LineItems(){
            //      Quantity = (int)result.Quantity,
            //      Product = p_lineitem.Product
            //    };
            

        }

        public List<LineItems> GetAllInventory(StoreFront p_store)
        {
            List<LineItems> storeInventory = new List<LineItems>() ;
            storeInventory = (from sInv in _context.StoreInventories
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
            throw new NotImplementedException();
            // get selected inventory from store
            //if (p_product.Name != null)
            //    {
            //        // Get invertory of a certain store with certain parameters
            //        var _storeInventory = (from i in _context.Inventories
            //                               join p in _context.Products on i.ProductId equals p.ProductId
            //                               where i.StoreNumber == p_store.StoreNumber &&
            //                                p.Name.Contains(p_product.Name)
            //                               select new
            //                               {  //map query to variables and create a list
            //                                   StoreNumber = i.StoreNumber,
            //                                   Quantity = i.Quantity,
            //                                   ProductName = p.Name,
            //                                   Price = p.Price,
            //                                   Description = p.Description,
            //                                   Category = p.Category,
            //                                   ProductID = p.ProductId
            //                               }).ToList();
            //        // takes mapped query and applies them to lineitem models
            //        foreach (var item in _storeInventory)
            //        {
            //            LineItems _lineitem = new LineItems();
            //            _lineitem.Quantity = (int)item.Quantity;
            //            Products _item = new Products()
            //            {
            //                Name = item.ProductName,
            //                Price = (float)item.Price,
            //                Description = item.Description,
            //                Category = item.Category,
            //                ProductID = item.ProductID
            //            };
            //            _lineitem.Product = _item;
            //            _inventory.Add(_lineitem);
            //        }

            //    }
            //    else if (p_product.ProductID != 0)
            //    {
            //        // Get invertory of a certain store with certain parameters
            //        var _storeInventory = (from i in _context.Inventories
            //                               join p in _context.Products on i.ProductId equals p.ProductId
            //                               where i.StoreNumber == p_store.StoreNumber &&
            //                                p.ProductId == p_product.ProductID
            //                               select new
            //                               {  //map query to variables and create a list
            //                                   StoreNumber = i.StoreNumber,
            //                                   Quantity = i.Quantity,
            //                                   ProductName = p.Name,
            //                                   Price = p.Price,
            //                                   Description = p.Description,
            //                                   Category = p.Category,
            //                                   ProductID = p.ProductId
            //                               }).ToList();
            //        // takes mapped query and applies them to lineitem models
            //        foreach (var item in _storeInventory)
            //        {
            //            LineItems _lineitem = new LineItems();
            //            _lineitem.Quantity = (int)item.Quantity;
            //            Products _item = new Products()
            //            {
            //                Name = item.ProductName,
            //                Price = (float)item.Price,
            //                Description = item.Description,
            //                Category = item.Category,
            //                ProductID = item.ProductID
            //            };
            //            _lineitem.Product = _item;
            //            _inventory.Add(_lineitem);
            //        }

            //    }
            //    else if (p_product.Price != 0)
            //    {
            //        // Get invertory of a certain store with certain parameters
            //        var _storeInventory = (from i in _context.Inventories
            //                               join p in _context.Products on i.ProductId equals p.ProductId
            //                               where i.StoreNumber == p_store.StoreNumber &&
            //                               p.Price == (decimal)p_product.Price

            //                               select new
            //                               {  //map query to variables and create a list
            //                                   StoreNumber = i.StoreNumber,
            //                                   Quantity = i.Quantity,
            //                                   ProductName = p.Name,
            //                                   Price = p.Price,
            //                                   Description = p.Description,
            //                                   Category = p.Category,
            //                                   ProductID = p.ProductId
            //                               }).ToList();
            //        // takes mapped query and applies them to lineitem models
            //        foreach (var item in _storeInventory)
            //        {
            //            LineItems _lineitem = new LineItems();
            //            _lineitem.Quantity = (int)item.Quantity;
            //            Products _item = new Products()
            //            {
            //                Name = item.ProductName,
            //                Price = (float)item.Price,
            //                Description = item.Description,
            //                Category = item.Category,
            //                ProductID = item.ProductID
            //            };
            //            _lineitem.Product = _item;
            //            _inventory.Add(_lineitem);
            //        }

            //    }
            //    else if (p_product.Category != null)
            //    {
            //        // Get invertory of a certain store with certain parameters
            //        var _storeInventory = (from i in _context.Inventories
            //                               join p in _context.Products on i.ProductId equals p.ProductId
            //                               where i.StoreNumber == p_store.StoreNumber &&
            //                               p.Category.Contains(p_product.Category)
            //                               select new
            //                               {  //map query to variables and create a list
            //                                   StoreNumber = i.StoreNumber,
            //                                   Quantity = i.Quantity,
            //                                   ProductName = p.Name,
            //                                   Price = p.Price,
            //                                   Description = p.Description,
            //                                   Category = p.Category,
            //                                   ProductID = p.ProductId
            //                               }).ToList();
            //        // takes mapped query and applies them to lineitem models
            //        foreach (var item in _storeInventory)
            //        {
            //            LineItems _lineitem = new LineItems();
            //            _lineitem.Quantity = (int)item.Quantity;
            //            Products _item = new Products()
            //            {
            //                Name = item.ProductName,
            //                Price = (float)item.Price,
            //                Description = item.Description,
            //                Category = item.Category,
            //                ProductID = item.ProductID
            //            };
            //            _lineitem.Product = _item;
            //            _inventory.Add(_lineitem);
            //        }

            //    }


            //    return _inventory;




            
        }
    }
}