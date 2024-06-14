using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace PizzaPlace.Core.Models
{
    [Table("Orders", Schema = "Transaction")]
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public int FileOrderId { get; set; }

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
