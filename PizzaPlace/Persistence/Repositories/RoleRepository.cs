using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using PizzaPlace.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlace.Persistence.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<IdentityRole> GetDefaultUserRoles()
        {
            return _context.Roles.Where(r => r.Name != "Customer")
                .ToList();
        }

        public IEnumerable<IdentityRole> GetOtherUserRoles()
        {
            return _context.Roles.Where(r => r.Name != "Customer" && r.Name != "Administrator")
                .ToList();
        }

        public IdentityRole GetRole(string roleId)
        {
            return _context.Roles
                .SingleOrDefault(r => r.Id == roleId);
        }

        public void Add(IdentityRole role)
        {
            _context.Roles.Add(role);
        }

        public void Remove(IdentityRole role)
        {
            _context.Roles.Remove(role);
        }

        public bool IsDefaultRole(string roleId)
        {
            var defaultRoles = new string[] { "c7c06140-a28f-43d7-94ac-d0f6e3dd8c01", "19080cb3-9ce7-4c26-a3cd-4e758c796db0", "747289c3-433d-4746-bcbb-366ae566721b", "355c1a5e-1dd7-4272-9796-6d7c48fcdf62", "95488c21-017a-491b-83f3-4eba8aa5d4a6" };
            var role = _context.Roles.Where(u => defaultRoles.Contains(u.Id)).ToList();
            return role.Count != 0;
        }

        public RoleManager<IdentityRole> GetRoleManager()
        {
            return new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_context));
        }
    }
}
