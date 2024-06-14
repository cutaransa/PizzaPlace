using PizzaPlace.Persistence;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PizzaPlace.Core.Utilities
{

    public class GetUniqueCode
    {
        public static string ForApiKey(int count)
        {
            ApplicationDbContext _context = new ApplicationDbContext();
            var code = "";
            bool exist = true;

            do
            {
                code = CodeGenerator.Generate(count);
                exist = _context.Administrators.Where(cn => cn.ApiKey == code).Count() != 0;
            } while (exist);

            return code;
        }
    }
}
