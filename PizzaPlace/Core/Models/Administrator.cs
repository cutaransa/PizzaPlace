using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PizzaPlace.Core.Models
{
    [Table("Administrators", Schema = "User")]
    public class Administrator
    {
        [Key]
        [ForeignKey("Login")]
        public string AdministratorId { get; set; }

        [Required]
        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public string ApiKey { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public bool IsDefault { get; set; }

        public ApplicationUser Login { get; set; }
    }
}
