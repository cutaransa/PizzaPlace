using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace PizzaPlace.Core.Models
{
    [Table("PizzaTypes", Schema = "Item")]
    public class PizzaType
    {
        [Key]
        public int TypeId { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        public string Ingredients { get; set; }

        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }

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
