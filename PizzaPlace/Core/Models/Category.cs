using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace PizzaPlace.Core.Models
{
    [Table("Categories", Schema = "Item")]
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsActive { get; set; }

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
