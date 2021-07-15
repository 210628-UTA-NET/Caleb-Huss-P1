using System.Collections.Generic;
using Models;

namespace BL
{
    public interface IOrderBL
    {
        List<Orders> GetAllOrders(Customers p_cust);
        List<Orders> GetAllOrders(StoreFront p_store);
        List<Orders> GetAllOrders(StoreFront p_store, Customers p_cust);
        Orders AddOrder(Orders p_order);
        

    }
}