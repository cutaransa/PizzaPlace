using PizzaPlace.Core.Models;
using PizzaPlace.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlace.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ApplicationUser> GetUsers()
        {
            return _context.Users.Where(u => !(u.Roles.Select(r => r.RoleId).Contains(_context.Roles.FirstOrDefault(r => r.Id == "95488c21-017a-491b-83f3-4eba8aa5d4a6").Id) || u.Roles.Select(r => r.RoleId).Contains(_context.Roles.FirstOrDefault(r => r.Id == "19080cb3-9ce7-4c26-a3cd-4e758c796db0").Id) || u.Roles.Select(r => r.RoleId).Contains(_context.Roles.FirstOrDefault(r => r.Id == "747289c3-433d-4746-bcbb-366ae566721b").Id))).ToList();
        }

        public ApplicationUser GetUser(string Id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == Id);
        }

        public ApplicationUser GetDefaultUser()
        {
            return _context.Users.FirstOrDefault(u => u.UserName == "avanzait@avanza.ph" && u.Email == "avanzait@avanza.ph");
        }

        public bool UserNameExists(string username, string userId)
        {
            return _context.Users.Where(x => x.UserName == username && x.Id == userId).Count() != 0 ? true : false;
        }

        public void Add(ApplicationUser user)
        {
            _context.Users.Add(user);
        }
    }
}
