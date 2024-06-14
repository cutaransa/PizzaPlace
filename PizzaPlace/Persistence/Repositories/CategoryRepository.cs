using PizzaPlace.Core.Models;
using PizzaPlace.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace PizzaPlace.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories
                .ToList();
        }

        public Category GetCategory(int categoryId)
        {
            return _context.Categories
                .SingleOrDefault(m => m.CategoryId == categoryId);
        }

        public void Add(Category category)
        {
            _context.Categories.Add(category);
        }

        public void Remove(Category category)
        {
            _context.Categories.Remove(category);
        }
    }
}
