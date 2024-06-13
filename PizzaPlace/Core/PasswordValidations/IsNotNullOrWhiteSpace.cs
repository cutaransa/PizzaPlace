using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlace.Core.PasswordValidations
{
    public class IsNotNullOrWhiteSpace : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            try
            {
                var input = Convert.ToString(value);

                if (!(string.IsNullOrWhiteSpace(input)))
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
