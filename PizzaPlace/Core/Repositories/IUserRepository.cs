using PizzaPlace.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlace.Core.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<ApplicationUser> GetUsers();
        ApplicationUser GetUser(string Id);
        ApplicationUser GetDefaultUser();
        bool UserNameExists(string username, string userId);
        void Add(ApplicationUser user);
    }
}
