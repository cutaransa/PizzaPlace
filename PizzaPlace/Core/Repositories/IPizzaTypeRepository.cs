using PizzaPlace.Core.Models;
using System.Collections.Generic;

namespace PizzaPlace.Core.Repositories
{
    public interface IPizzaTypeRepository
    {
        IEnumerable<PizzaType> GetPizzaTypes();
        PizzaType GetPizzaType(int pizzaTypeId);
        void Add(PizzaType pizzaType);
        void Remove(PizzaType pizzaType);
    }
}
