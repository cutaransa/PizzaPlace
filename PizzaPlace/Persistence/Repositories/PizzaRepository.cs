using PizzaPlace.Core.Models;
using PizzaPlace.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace PizzaPlace.Persistence.Repositories
{
    public class PizzaRepository : IPizzaRepository
    {
        private ApplicationDbContext _context;

        public PizzaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Pizza> GetPizzas()
        {
            return _context.Pizzas
                .ToList();
        }

        public Pizza GetPizza(int pizzaId)
        {
            return _context.Pizzas
                .SingleOrDefault(m => m.PizzaId == pizzaId);
        }

        public void Add(Pizza pizza)
        {
            _context.Pizzas.Add(pizza);
        }

        public void Remove(Pizza pizza)
        {
            _context.Pizzas.Remove(pizza);
        }
    }
}
