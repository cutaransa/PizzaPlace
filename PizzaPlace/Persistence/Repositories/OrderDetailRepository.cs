using PizzaPlace.Core.Models;
using PizzaPlace.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;

namespace PizzaPlace.Persistence.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private ApplicationDbContext _context;

        public OrderDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<OrderDetail> GetOrderDetails()
        {
            return _context.OrderDetails
                .Include(x => x.Order)
                .Include(x => x.Pizza)
                .Include(x => x.Pizza.PizzaType)
                .Include(x => x.Pizza.PizzaType.Category)
                .ToList();
        }

        public IEnumerable<OrderDetail> GetOrderDetailsByOrder(int orderId)
        {
            return _context.OrderDetails
                .ToList();
        }

        public OrderDetail GetOrderDetail(int orderDetailId)
        {
            return _context.OrderDetails
                .Include(x => x.Order)
                .Include(x => x.Pizza)
                .Include(x => x.Pizza.PizzaType)
                .Include(x => x.Pizza.PizzaType.Category)
                .SingleOrDefault(m => m.DetailId == orderDetailId);
        }

        public void Add(OrderDetail orderDetail)
        {
            _context.OrderDetails.Add(orderDetail);
        }

        public void Remove(OrderDetail orderDetail)
        {
            _context.OrderDetails.Remove(orderDetail);
        }
    }
}
