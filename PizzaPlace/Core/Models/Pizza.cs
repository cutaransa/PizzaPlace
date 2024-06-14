using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace PizzaPlace.Core.Models
{
    [Table("Pizzas", Schema = "Item")]
    public class Pizza
    {
        [Key]
        public int PizzaId { get; set; }
        [Required]
        public string Code { get; set; }
        [ForeignKey("PizzaType")]
        public int? TypeId { get; set; }
        public virtual PizzaType PizzaType { get; set; }
        [Required]
        public string Size { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        [ForeignKey("CreatedBy")]
        public string CreatedById { get; set; }
        public virtual Administrator CreatedBy { get; set; }
        [ForeignKey("ModifiedBy")]
        public string ModifiedById { get; set; }
        public virtual Administrator ModifiedBy { get; set; }
    }
}
