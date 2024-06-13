using PizzaPlace.Core.Repositories;
using PizzaPlace.Core;
using PizzaPlace.Persistence.Repositories;

namespace PizzaPlace.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IAdministratorRepository Administrators { get; set; }
        public ICategoryRepository Categories { get; set; }
        public IModuleRepository Modules { get; set; }
        public IPizzaRepository Pizzas { get; set; }
        public IPizzaTypeRepository PizzaTypes { get; set; }
        public IRoleModuleRepository RoleModules { get; set; }
        public IRoleRepository Roles { get; set; }
        public IUserRepository Users { get; set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Administrators = new AdministratorRepository(context);
            Categories = new CategoryRepository(context);
            Modules = new ModuleRepository(context);
            Pizzas = new PizzaRepository(context);
            PizzaTypes = new PizzaTypeRepository(context);
            RoleModules = new RoleModuleRepository(context);
            Roles = new RoleRepository(context);
            Users = new UserRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
