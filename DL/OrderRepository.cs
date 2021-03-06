using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DL
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DBContext _context;
        public OrderRepository(DBContext p_context)
        {
            _context = p_context;
        }
        public Orders AddOrder(Orders p_order)
        {
            IInventoryRepository invRepo = new InventoryRepository(_context);
            _context.Orders.Add(p_order);
            _context.Customers.Attach(p_order.Customer);
            foreach (LineItems prod in p_order.ItemsList)
            {
                _context.Products.Attach(prod.Product);
            }
            _context.Stores.Attach(p_order.StoreFront);

            _context.SaveChanges();
            foreach (LineItems prod in p_order.ItemsList)
            {
                LineItems newLI = new LineItems(){ Product = prod.Product, Quantity = prod.Quantity * -1 };

                invRepo.ChangeInventory(p_order.StoreFront, newLI);
            }

            return (from o in _context.Orders
                    where o.Customer.CustomerID == p_order.Customer.CustomerID
                    orderby o.OrderNum descending
                    select o
            ).FirstOrDefault();
        }

        public void AddToCart(LineItems p_lineitem, string p_cartId)
        {
            var cartItem = _context.Carts.SingleOrDefault(
                c => c.CartID == p_cartId
                && c.ProductID == p_lineitem.Product.ProductID
            );
            if (cartItem == null)
            {

                Cart newCartItem = new Cart()
                {
                    CartID = p_cartId,
                    ProductID = p_lineitem.Product.ProductID,
                    Quantity = p_lineitem.Quantity,
                    DateMade = DateTime.Now
                };
                _context.Carts.Add(newCartItem);
            }
            else
            {
                cartItem.Quantity += p_lineitem.Quantity;
            }
            _context.SaveChanges();
        }

        public void EmptyCart(string p_cartId)
        {
            var cartItem = (from c in _context.Carts
                            where c.CartID == p_cartId
                            select c
            ).ToList();
            foreach (var item in cartItem)
            {
                _context.Carts.Remove(item);
            }
            _context.SaveChanges();
        }

        public Orders GetAnOrder(int p_orderNum)
        {
             Orders foundOrder = (from o in _context.Orders
                    where o.OrderNum == p_orderNum
                    select new Orders
                    {
                        OrderNum = o.OrderNum,
                        Customer = o.Customer,
                        StoreFront = o.StoreFront,
                        Date = o.Date
                    }).FirstOrDefault();
            int oNum = foundOrder.OrderNum;
            foundOrder.ItemsList = (from li in _context.LineItems
                                   join p in _context.Products on li.Product.ProductID equals p.ProductID
                                   where li.OrdersOrderNum == oNum
                                   select new LineItems()
                                   {
                                     Product = new  Products(){
                                     Name = p.Name,
                                     Description = p.Description,
                                     Price = p.Price,
                                     ProductID = p.ProductID
                                     },
                                     Quantity = li.Quantity
                                   }
                ).ToList();
            return foundOrder;
        }

        public List<Cart> GetCartItems(string p_cartId)
        {
            return (from c in _context.Carts
                    join p in _context.Products on c.ProductID equals p.ProductID
                    where c.CartID == p_cartId
                    select new Cart()
                    {
                        CartID = p_cartId,
                        Quantity = c.Quantity,
                        Product = new Products()
                        {
                            Name = p.Name,
                            Price = p.Price,
                            ProductID = p.ProductID
                        }
                    }
                    ).ToList();
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
                                        }
            ).ToList();
            foreach (Orders order in ordersFound)
            {
                order.ItemsList = (from li in _context.LineItems
                                   join p in _context.Products on li.Product.ProductID equals p.ProductID
                                   where li.OrdersOrderNum == order.OrderNum
                                   select new LineItems()
                                   {
                                     Product = li.Product,
                                     Quantity = li.Quantity
                                   }
                ).ToList();
            }
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
                                        }
            ).ToList();
            foreach (Orders order in ordersFound)
            {
                order.ItemsList = (from li in _context.LineItems
                                   join p in _context.Products on li.Product.ProductID equals p.ProductID
                                   where li.OrdersOrderNum == order.OrderNum
                                   select new LineItems()
                                   {
                                     Product = li.Product,
                                     Quantity = li.Quantity
                                   }
                ).ToList();
            }

            return ordersFound;
        }

        public List<Orders> GetOrders(StoreFront p_store, Customers p_cust)
        {
            // try just getting order with customer ID
            //_context.Include(item => item).ThenInclude(item => item.product)
            //return _context.Orders.Include(order => order.Items).ThenInclude(item => item.Product).Where(order => order.StoreFrontID == p_storeID).ToList();


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
                                        }
            ).ToList();
            foreach (Orders order in ordersFound)
            {
                order.ItemsList = (from li in _context.LineItems
                                   join p in _context.Products on li.Product.ProductID equals p.ProductID
                                   where li.OrdersOrderNum == order.OrderNum
                                   select new LineItems()
                                   {
                                     Product = li.Product,
                                     Quantity = li.Quantity
                                   }
                ).ToList();
            }
            return ordersFound;
        }

        public void RemoveFromCart(int p_productid, string p_cartId)
        {
            var cartItem = (from c in _context.Carts
                            where c.CartID == p_cartId
                            && c.ProductID == p_productid
                            select c
            ).FirstOrDefault();
            if (cartItem != null)
            {
                _context.Carts.Remove(cartItem);
                _context.SaveChanges();
            }
        }
        
    }
}
