using PizzaPlace.Core.Models;
using PizzaPlace.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace PizzaPlace.Persistence.Repositories
{
    public class ModuleRepository : IModuleRepository
    {
        private ApplicationDbContext _context;

        public ModuleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Module> GetModules()
        {
            return _context.Modules
                .OrderBy(m => m.Controller)
                .ToList();
        }

        public Module GetModule(int moduleId)
        {
            return _context.Modules
                .SingleOrDefault(m => m.ModuleId == moduleId);
        }

        public void Add(Module module)
        {
            _context.Modules.Add(module);
        }

        public void Remove(Module module)
        {
            _context.Modules.Remove(module);
        }
    }
}
