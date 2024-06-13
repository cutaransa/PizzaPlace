using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlace.Core.Utilities
{
    public static class ObjectValidation
    {
        public static bool objValid(this object obj)
        {
            var results = new List<ValidationResult>();
            var vc = new ValidationContext(obj, null, null);
            var value = Validator.TryValidateObject(obj, vc, results, true);

            return value;
        }

        public static string[] objErrors(this object obj)
        {
            var results = new List<ValidationResult>();
            var vc = new ValidationContext(obj, null, null);
            var isValid = Validator.TryValidateObject(obj, vc, results, true);

            var value = Array.ConvertAll(results.ToArray(), o => o.ErrorMessage);

            return value;
        }
    }
}
