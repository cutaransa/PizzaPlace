using PizzaPlace.Core.Models;
using System.Collections.Generic;

namespace PizzaPlace.Core.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories();
        Category GetCategory(int categoryId);
        void Add(Category category);
        void Remove(Category category);
    }
}
