using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaPlace.Web.Core.Dtos.Model
{
    public class OrderDto
    {
        public int orderId { get; set; }
        public int fileOrderId { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime modifiedDate { get; set; }
    }

    public class ViewOrderDto : OrderDto
    {

    }

    public class ListOrderDto : OrderDto
    {

    }

    public class AddOrderDto : OrderDto
    {

    }

    public class EditOrderDto : OrderDto
    {

    }
}