using PizzaPlace.Core.Models;
using PizzaPlace.Web.Core.Dtos.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PizzaPlace.Web.Core.Dtos.Report
{
    public class TransactionDto
    {
        public int detailId { get; set; }
        public int fileDetailId { get; set; }

        public int quantity { get; set; }

        public int? pizzaId { get; set; }
        public virtual ViewPizzaDto pizza { get; set; }

        public int? orderId { get; set; }
        public virtual ViewOrderDto order { get; set; }
    }
}