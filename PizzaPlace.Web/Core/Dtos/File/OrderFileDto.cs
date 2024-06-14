using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PizzaPlace.Web.Core.Dtos.File
{
    public class OrderFileDto
    {

        [Required(ErrorMessage = "Order Id is required")]
        [Display(Name = "order_id")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [Display(Name = "date")]
        public string Date { get; set; }

        [Required(ErrorMessage = "Time is required")]
        [Display(Name = "time")]
        public string Time { get; set; }

        public string Remarks { get; set; }
    }
}