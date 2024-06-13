
using PizzaPlace.Core.PasswordValidations;
using System.ComponentModel.DataAnnotations;

namespace PizzaPlace.Web.Core.Dtos
{
    public class AddExternalLoginBindingDto
    {
        [Required]
        [Display(Name = "External access token")]
        public string ExternalAccessToken { get; set; }
    }

    public class ChangePasswordBindingModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [HasLowerChar(ErrorMessage = "Password should contain at least one lower case letter")]
        [HasUpperChar(ErrorMessage = "Password should contain at least one upper case letter")]
        [HasNumber(ErrorMessage = "Password should contain at least one numeric value")]
        [HasSymbol(ErrorMessage = "Password should contain at least one special case characters")]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class RegisterBindingModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [HasLowerChar(ErrorMessage = "Password should contain at least one lower case letter")]
        [HasUpperChar(ErrorMessage = "Password should contain at least one upper case letter")]
        [HasNumber(ErrorMessage = "Password should contain at least one numeric value")]
        [HasSymbol(ErrorMessage = "Password should contain at least one special case characters")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "User Roles")]
        public string RoleName { get; set; }
    }

    public class RegisterExternalBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class RemoveLoginBindingModel
    {
        [Required]
        [Display(Name = "Login provider")]
        public string LoginProvider { get; set; }

        [Required]
        [Display(Name = "Provider key")]
        public string ProviderKey { get; set; }
    }

    public class SetPasswordBindingModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ForgotPasswordBindingModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }
    }

    public class ResetPasswordBindingModel
    {
        [Required]
        public string userId { get; set; }

        [Required]
        public string code { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [HasLowerChar(ErrorMessage = "Password should contain at least one lower case letter")]
        [HasUpperChar(ErrorMessage = "Password should contain at least one upper case letter")]
        [HasNumber(ErrorMessage = "Password should contain at least one numeric value")]
        [HasSymbol(ErrorMessage = "Password should contain at least one special case characters")]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string newPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("newPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string confirmPassword { get; set; }
    }
}