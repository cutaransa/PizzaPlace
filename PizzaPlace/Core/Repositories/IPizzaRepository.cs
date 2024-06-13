using PizzaPlace.Core.Models;
using System.Collections.Generic;

namespace PizzaPlace.Core.Repositories
{
    public interface IPizzaRepository
    {
        IEnumerable<Pizza> GetPizzas();
        Pizza GetPizza(int pizzaId);
        void Add(Pizza pizza);
        void Remove(Pizza pizza);
    }
}
