using PizzaPlace.Core.Models;
using PizzaPlace.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace PizzaPlace.Persistence.Repositories
{
    public class PizzaTypeRepository : IPizzaTypeRepository
    {
        private ApplicationDbContext _context;

        public PizzaTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<PizzaType> GetPizzaTypes()
        {
            return _context.PizzaTypes
                .ToList();
        }

        public PizzaType GetPizzaType(int typeId)
        {
            return _context.PizzaTypes
                .SingleOrDefault(m => m.TypeId == typeId);
        }

        public void Add(PizzaType pizzaType)
        {
            _context.PizzaTypes.Add(pizzaType);
        }

        public void Remove(PizzaType pizzaType)
        {
            _context.PizzaTypes.Remove(pizzaType);
        }
    }
}
