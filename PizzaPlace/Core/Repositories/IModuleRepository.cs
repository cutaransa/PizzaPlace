using PizzaPlace.Core.Models;
using System.Collections.Generic;

namespace PizzaPlace.Core.Repositories
{
    public interface IModuleRepository
    {
        IEnumerable<Module> GetModules();
        Module GetModule(int moduleId);
        void Add(Module module);
        void Remove(Module module);
    }
}
