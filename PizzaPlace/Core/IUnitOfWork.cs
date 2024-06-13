using PizzaPlace.Core.Repositories;

namespace PizzaPlace.Core
{

    public interface IUnitOfWork
    {
        IAdministratorRepository Administrators { get; set; }
        ICategoryRepository Categories { get; set; }
        IModuleRepository Modules { get; set; }
        IPizzaRepository Pizzas { get; set; }
        IPizzaTypeRepository PizzaTypes { get; set; }
        IRoleModuleRepository RoleModules { get; set; }
        IRoleRepository Roles { get; set; }
        IUserRepository Users { get; set; }

        void Complete();
    }
}
