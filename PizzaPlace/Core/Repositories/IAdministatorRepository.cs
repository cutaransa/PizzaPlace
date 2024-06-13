using PizzaPlace.Core.Models;
using System.Collections.Generic;

namespace PizzaPlace.Core.Repositories
{
    public interface IAdministratorRepository
    {
        IEnumerable<Administrator> GetAdministrators();
        IEnumerable<Administrator> GetAdministratorsExceptDefault();
        Administrator GetAdministrator(string userId);
        void Add(Administrator administrator);
        IEnumerable<Administrator> GetUsersDetails();
        Administrator GetAdministratorByApiKey(string apiKey);
        Administrator GetAdministratorByEmailAddress(string emailAddress);
    }
}
