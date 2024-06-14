using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PizzaPlace.Web.Core.Dtos.File
{
    public class TypeFileDto
    {

        [Required(ErrorMessage = "Pizza Type Id is required")]
        [Display(Name = "pizza_type_id")]
        public string PizzaTypeId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "category")]
        public string Category { get; set; }

        [Display(Name = "ingredients")]
        public string Ingredients { get; set; }

        public string Remarks { get; set; }
    }
}