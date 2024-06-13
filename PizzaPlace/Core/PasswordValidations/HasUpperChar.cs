using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PizzaPlace.Core.PasswordValidations
{
    public class HasUpperChar : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            try
            {
                var input = Convert.ToString(value);
                var hasUpperChar = new Regex(@"[A-Z\u00d1]+");

                if (hasUpperChar.IsMatch(input))
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
