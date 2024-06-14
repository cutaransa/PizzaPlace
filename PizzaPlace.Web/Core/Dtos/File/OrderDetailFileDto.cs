using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PizzaPlace.Web.Core.Dtos.File
{
    public class OrderDetailFileDto
    {

        [Required(ErrorMessage = "Order Detail Id is required")]
        [Display(Name = "order_details_id")]
        public int OrderDetailId { get; set; }

        [Required(ErrorMessage = "Order Id is required")]
        [Display(Name = "order_id")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Pizza Id is required")]
        [Display(Name = "pizza_id")]
        public string PizzaId { get; set; }

        public int Quantity { get; set; }

        public string Remarks { get; set; }
    }
}