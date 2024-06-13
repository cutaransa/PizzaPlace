using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PizzaPlace.Core.PasswordValidations
{
    public class HasSymbol : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            try
            {
                var input = Convert.ToString(value);
                var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

                if (hasSymbols.IsMatch(input))
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
