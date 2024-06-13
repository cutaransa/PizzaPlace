using PizzaPlace.Core.PasswordValidations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PizzaPlace.Web.Core.Dtos.Model
{
    public class AdministratorDto
    {
        public bool isActive { get; set; }

        public UserDto login { get; set; }
    }

    public class ViewAdministratorDto : AdministratorDto
    {
        public string administratorId { get; set; }

        public string name { get; set; }

        public string userName
        {
            get
            {
                return (login != null ? login.userName : string.Empty);
            }
        }

        public string emailAddress
        {
            get
            {
                return (login != null ? login.email : string.Empty);
            }
        }

        public bool isDefault { get; set; }

        public string roleId { get; set; }

        public RoleDto role { get; set; }

        public IEnumerable<ListRoleDto> roles { get; set; }
    }

    public class ListAdministratorDto : AdministratorDto
    {
        public string administratorId { get; set; }

        public string name { get; set; }

        public string userName
        {
            get
            {
                return (login != null ? login.userName : string.Empty);
            }
        }

        public string emailAddress
        {
            get
            {
                return (login != null ? login.email : string.Empty);
            }
        }

        public bool isDefault { get; set; }
    }

    public class AddAdministratorDto : AdministratorDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string name { get; set; }

        [Required(ErrorMessage = "User name is required")]
        public string userName { get; set; }

        public string emailAddress { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 6)]
        [HasLowerChar(ErrorMessage = "Password should contain at least one lower case letter")]
        [HasUpperChar(ErrorMessage = "Password should contain at least one upper case letter")]
        [HasNumber(ErrorMessage = "Password should contain at least one numeric value")]
        [HasSymbol(ErrorMessage = "Password should contain at least one special case characters")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "The password and confirmation password do not match")]
        public string confirmPassword { get; set; }

        [Required(ErrorMessage = "User role is required")]
        public string roleId { get; set; }
    }

    public class EditAdministratorDto : AdministratorDto
    {
        [Required(ErrorMessage = "User not found")]
        public string administratorId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string name { get; set; }

        [Required(ErrorMessage = "User name is required")]
        public string userName { get; set; }

        public string emailAddress { get; set; }

        [Required(ErrorMessage = "User role is required")]
        public string roleId { get; set; }
        public bool isDefault { get; set; }

    }
}