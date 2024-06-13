using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace PizzaPlace.Core.Models
{
    [Table("RoleModules", Schema = "User")]
    public class RoleModule
    {
        [Key]
        public int RoleModuleId { get; set; }

        public string RoleId { get; set; }

        public int ModuleId { get; set; }

        public bool IsEnabled { get; set; }

        public virtual IdentityRole Role { get; set; }

        public virtual Module Module { get; set; }
    }
}
