using PizzaPlace.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlace.Core.Repositories
{
    public interface IRoleModuleRepository
    {
        IEnumerable<RoleModule> GetRoleModules();
        IEnumerable<RoleModule> GetRoleModulesByRoleId(string roleId);
        RoleModule GetRoleModule(int roleModuleId);
        RoleModule GetRoleModuleByDetail(string roleId, string controller, string action);
        void Add(RoleModule roleModule);
        void Remove(RoleModule roleModule);
    }
}
