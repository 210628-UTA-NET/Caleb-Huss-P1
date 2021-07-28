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
                prod.Quantity = prod.Quantity * -1;
                invRepo.ChangeInventory(p_order.StoreFront, prod);
            }

            return (from o in _context.Orders
                    join c in _context.Customers on o.Customer.CustomerID equals c.CustomerID
                    join s in _context.Stores on o.StoreFront.StoreNumber equals s.StoreNumber
                    where o.Customer.CustomerID == p_order.Customer.CustomerID &&
                          o.StoreFront.StoreNumber == p_order.StoreFront.StoreNumber
                    orderby o.OrderNum descending
                    select new Orders
                    {
                        OrderNum = o.OrderNum,
                        Customer = o.Customer,
                        StoreFront = o.StoreFront,
                        Date = o.Date,
                        ItemsList = o.ItemsList
                    }

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
            return (from o in _context.Orders
                    where o.OrderNum == p_orderNum
                    select new Orders
                    {
                        OrderNum = o.OrderNum,
                        Customer = o.Customer,
                        StoreFront = o.StoreFront,
                        Date = o.Date,
                        ItemsList = o.ItemsList
                    }
            ).FirstOrDefault();
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
        }

        public void MigrateCart(string p_email, string p_tempCartID)
        {
            var cartItems = (from c in _context.Carts
                             where c.CartID == p_tempCartID
                             select c
            ).ToList();

            foreach (var item in cartItems)
            {
                item.CartID = p_email;
            }
            _context.SaveChanges();
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
