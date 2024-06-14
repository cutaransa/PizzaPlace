using PizzaPlace.Core.Models;
using System.Collections.Generic;

namespace PizzaPlace.Core.Repositories
{
    public interface IOrderDetailRepository
    {
        IEnumerable<OrderDetail> GetOrderDetails();
        IEnumerable<OrderDetail> GetOrderDetailsByOrder(int orderId);
        OrderDetail GetOrderDetail(int orderDetailId);
        void Add(OrderDetail orderDetail);
        void Remove(OrderDetail orderDetail);
    }
}
