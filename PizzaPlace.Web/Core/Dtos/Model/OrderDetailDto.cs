using PizzaPlace.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PizzaPlace.Web.Core.Dtos.Model
{
    public class OrderDetailDto
    {
        public int detailId { get; set; }

        public int quantity { get; set; }

        public int? pizzaId { get; set; }
        public virtual ViewPizzaDto pizza { get; set; }

        public int? orderId { get; set; }
        public virtual ViewOrderDto order { get; set; }
    }

    public class ViewOrderDetailDto : OrderDetailDto
    {

    }

    public class ListOrderDetailDto : OrderDetailDto
    {

    }

    public class AddOrderDetailDto : OrderDetailDto
    {

    }

    public class EditOrderDetailDto : OrderDetailDto
    {

    }
}