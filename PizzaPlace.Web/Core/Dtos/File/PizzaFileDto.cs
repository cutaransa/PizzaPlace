using System.ComponentModel.DataAnnotations;

namespace PizzaPlace.Web.Core.Dtos.File
{
    public class PizzaFileDto
    {


        [Required(ErrorMessage = "Pizza Id is required")]
        [Display(Name = "pizza_id")]
        public string PizzaId { get; set; }

        [Required(ErrorMessage = "Pizza Type Id is required")]
        [Display(Name = "pizza_type_id")]
        public string PizzaTypeId { get; set; }

        [Required(ErrorMessage = "Size is required")]
        [Display(Name = "size")]
        public string Size { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Display(Name = "price")]
        public string Price { get; set; }

        public string Remarks { get; set; }
    }
}