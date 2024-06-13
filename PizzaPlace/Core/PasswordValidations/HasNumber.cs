using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PizzaPlace.Core.PasswordValidations
{
    public class HasNumber : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            try
            {
                var input = Convert.ToString(value);
                var hasNumber = new Regex(@"[0-9]+");

                if (hasNumber.IsMatch(input))
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
