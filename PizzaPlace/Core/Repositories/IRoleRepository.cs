using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlace.Core.Repositories
{
    public interface IRoleRepository
    {
        IEnumerable<IdentityRole> GetDefaultUserRoles();
        IEnumerable<IdentityRole> GetOtherUserRoles();
        IdentityRole GetRole(string roleId);
        void Add(IdentityRole role);
        void Remove(IdentityRole role);
        bool IsDefaultRole(string roleId);
        RoleManager<IdentityRole> GetRoleManager();
    }
}
