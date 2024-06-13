using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PizzaPlace.Core.PasswordValidations
{
    public class HasLowerChar : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            try
            {
                var input = Convert.ToString(value);
                var hasLowerChar = new Regex(@"[a-z\u00f1]+");

                if (hasLowerChar.IsMatch(input))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return (false);
            }
        }
    }
}
