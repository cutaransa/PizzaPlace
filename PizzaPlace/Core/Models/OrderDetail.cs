using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace PizzaPlace.Core.Models
{
    [Table("OrderDetails", Schema = "Transaction")]
    public class OrderDetail
    {
        [Key]
        public int DetailId { get; set; }

        [Required]
        public int FileDetailId { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("Pizza")]
        public int? PizzaId { get; set; }
        public virtual Pizza Pizza { get; set; }

        [ForeignKey("Order")]
        public int? OrderId { get; set; }
        public virtual Order Order { get; set; }

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
