using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PizzaPlace.Core.Models
{
    [Table("Modules", Schema = "User")]
    public class Module
    {
        [Key]
        public int ModuleId { get; set; }

        [Required]
        public string Controller { get; set; }

        [Required]
        public string Action { get; set; }
    }
}
