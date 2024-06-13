using PizzaPlace.Core.Models;
using PizzaPlace.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlace.Persistence.Repositories
{
    public class RoleModuleRepository : IRoleModuleRepository
    {
        private ApplicationDbContext _context;

        public RoleModuleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<RoleModule> GetRoleModules()
        {
            return _context.RoleModules
                .OrderBy(rm => rm.RoleId)
                .ToList();
        }

        public IEnumerable<RoleModule> GetRoleModulesByRoleId(string roleId)
        {
            return _context.RoleModules
                .Where(rm => rm.RoleId == roleId)
                .OrderBy(rm => rm.Module.Controller)
                .ToList();
        }

        public RoleModule GetRoleModuleByDetail(string roleId, string controller, string action)
        {
            return _context.RoleModules
                .Where(rm => rm.RoleId == roleId)
                .Where(rm => rm.Module.Controller == controller)
                .Where(rm => rm.Module.Action == action)
                .SingleOrDefault();
        }

        public RoleModule GetRoleModule(int roleModuleId)
        {
            return _context.RoleModules
                .SingleOrDefault(rm => rm.RoleModuleId == roleModuleId);
        }

        public void Add(RoleModule roleModule)
        {
            _context.RoleModules.Add(roleModule);
        }

        public void Remove(RoleModule roleModule)
        {
            _context.RoleModules.Remove(roleModule);
        }
    }
}
