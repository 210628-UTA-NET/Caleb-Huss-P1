using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DL
{
    public class OrderRepository : IOrderRepository
    {
        private DBContext _context;
        private List<Orders> _getorders = new List<Orders>();
        private Orders currentOrder = new Orders();
        public OrderRepository(DBContext p_context)
        {
            _context = p_context;
        }
        public Orders AddOrder(Orders p_order)
        {
            throw new NotImplementedException();
            ////Get the highest order number
            //var result = (from o in _context.Orders
            //                  orderby o.OrderId descending
            //                  select o).FirstOrDefault();
            //    p_order.OrderNum = result.OrderId + 1;
            //    var result2 = (from l in _context.LineItems
            //                   orderby l.LineItemId descending
            //                   select l).FirstOrDefault();
            //    int lastLineItemId = result2.LineItemId;
            //    foreach (LineItems item in p_order.ItemsList)
            //    {
            //        lastLineItemId += 1;
            //        item.LineItemID = lastLineItemId;
            //        _context.LineItems.Add(new Entities.LineItem
            //        {
            //            ProductId = item.Product.ProductID,
            //            Quantity = item.Quantity

            //        });
            //        _context.Orders.Add(new Entities.Order
            //        {
            //            OrderId = p_order.OrderNum,
            //            StoreNumber = p_order.StoreFront.StoreNumber,
            //            CustomerId = p_order.Customer.CustomerId,
            //            LineItemId = lastLineItemId
            //        });
            //    }
            //    _context.SaveChanges();
                
            //    foreach (LineItems item in p_order.ItemsList)
            //    {
            //        var result3 = (from i in _context.Inventories
            //                  where i.ProductId == item.Product.ProductID &&
            //                  i.StoreNumber == p_order.StoreFront.StoreNumber
            //                  select i).SingleOrDefault();
            //    result3.Quantity -= item.Quantity; // change the quantity here

            //    _context.Entry(result).State = EntityState.Modified;
            //    _context.SaveChanges();
            //    }


            //    return p_order;
        
        }

        public List<Orders> GetOrders(StoreFront p_store)
        {
            List<Orders> ordersFound = (from o in _context.Orders
                                        join c in _context.Customers on o.Customer.CustomerID equals c.CustomerID
                                        join s in _context.Stores on o.StoreFront.StoreNumber equals s.StoreNumber
                                        where o.StoreFront.StoreNumber == p_store.StoreNumber
                                        select new Orders
                                        {
                                            OrderNum = o.OrderNum,
                                            Customer = o.Customer,
                                            StoreFront = o.StoreFront,
                                            Date = o.Date,
                                            ItemsList = o.ItemsList
                                        }
            ).ToList();
            return ordersFound;
        }

        public List<Orders> GetOrders(Customers p_cust)
        {
            List<Orders> ordersFound = (from o in _context.Orders
                                        join c in _context.Customers on o.Customer.CustomerID equals c.CustomerID
                                        join s in _context.Stores on o.StoreFront.StoreNumber equals s.StoreNumber
                                        where o.Customer.CustomerID == p_cust.CustomerID
                                        select new Orders
                                        {
                                            OrderNum = o.OrderNum,
                                            Customer = o.Customer,
                                            StoreFront = o.StoreFront,
                                            Date = o.Date,
                                            ItemsList = o.ItemsList
                                        }
            ).ToList();
            return ordersFound;
        }

        public List<Orders> GetOrders(StoreFront p_store, Customers p_cust)
        {
            List<Orders> ordersFound = (from o in _context.Orders
                                        join c in _context.Customers on o.Customer.CustomerID equals c.CustomerID
                                        join s in _context.Stores on o.StoreFront.StoreNumber equals s.StoreNumber
                                        where o.Customer.CustomerID == p_cust.CustomerID &&
                                              o.StoreFront.StoreNumber == p_store.StoreNumber
                                        select new Orders
                                        {
                                            OrderNum = o.OrderNum,
                                            Customer = o.Customer,
                                            StoreFront = o.StoreFront,
                                            Date = o.Date,
                                            ItemsList = o.ItemsList
                                        }
            ).ToList();
            return ordersFound;
            //var result = (from o in _context.Orders
            //                  join l in _context.LineItems on o.LineItemId equals l.LineItemId
            //                  join p in _context.Products on l.ProductId equals p.ProductId
            //                  where o.StoreNumber == p_store.StoreNumber &&
            //                        o.CustomerId == p_cust.CustomerId
            //                 orderby o.OrderId ascending
            //                  select new
            //                  {
            //                      OrderNum = o.OrderId,
            //                      ProductName = p.Name,
            //                      ProductID = p.ProductId,
            //                      ProductPrice = p.Price,
            //                      ProductDesc = p.Description,
            //                      ProductCat = p.Category,
            //                      Quantity = l.Quantity
            //                  }).ToList();
            //    if (result.Count == 0)
            //    {
            //        Console.WriteLine("No orders found");
            //        return new List<Orders>();
            //    }
            //    int currentOrderNum = result[0].OrderNum;

            //    foreach (var item in result)
            //    {
            //        if (currentOrderNum != item.OrderNum)
            //        {
            //            currentOrderNum = item.OrderNum;
            //            _getorders.Add(currentOrder);
            //            currentOrder = new Orders();
            //        }
            //        currentOrder.Customer = p_cust;
            //        currentOrder.OrderNum = item.OrderNum;
            //        currentOrder.StoreFront = p_store;
            //        currentOrder.AddLineItem(new Products()
            //        {
            //            Name = item.ProductName,
            //            Price = (float)item.ProductPrice,
            //            Description = item.ProductDesc,
            //            Category = item.ProductCat,
            //            ProductID = item.ProductID
            //        }, (int)item.Quantity);
            //    }
            //    _getorders.Add(currentOrder);
            //    return _getorders;
            
        }
    }
}
