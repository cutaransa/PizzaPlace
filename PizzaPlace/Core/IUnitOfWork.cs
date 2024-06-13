using PizzaPlace.Core.Repositories;

namespace PizzaPlace.Core
{

    public interface IUnitOfWork
    {
        IAdministratorRepository Administrators { get; set; }
        IModuleRepository Modules { get; set; }
        IRoleModuleRepository RoleModules { get; set; }
        IRoleRepository Roles { get; set; }
        IUserRepository Users { get; set; }

        void Complete();
    }
}
