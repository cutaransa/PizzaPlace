using PizzaPlace.Core.Models;
using PizzaPlace.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PizzaPlace.Persistence.Repositories
{
    public class AdministratorRepository : IAdministratorRepository
    {
        private ApplicationDbContext _context;

        public AdministratorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Administrator> GetAdministrators()
        {
            return _context.Administrators.Include(x => x.Login)
                .Where(x => !(x.Login.Roles.Select(r => r.RoleId).Contains(_context.Roles.FirstOrDefault(r => r.Id == "95488c21-017a-491b-83f3-4eba8aa5d4a6").Id) || x.Login.Roles.Select(r => r.RoleId).Contains(_context.Roles.FirstOrDefault(r => r.Id == "19080cb3-9ce7-4c26-a3cd-4e758c796db0").Id) || x.Login.Roles.Select(r => r.RoleId).Contains(_context.Roles.FirstOrDefault(r => r.Id == "747289c3-433d-4746-bcbb-366ae566721b").Id)))
                .ToList();
        }

        public IEnumerable<Administrator> GetAdministratorsExceptDefault()
        {
            return _context.Administrators.Include(x => x.Login)
                .Where(x => x.IsDefault == false && !(x.Login.Roles.Select(r => r.RoleId).Contains(_context.Roles.FirstOrDefault(r => r.Id == "95488c21-017a-491b-83f3-4eba8aa5d4a6").Id) || x.Login.Roles.Select(r => r.RoleId).Contains(_context.Roles.FirstOrDefault(r => r.Id == "19080cb3-9ce7-4c26-a3cd-4e758c796db0").Id) || x.Login.Roles.Select(r => r.RoleId).Contains(_context.Roles.FirstOrDefault(r => r.Id == "747289c3-433d-4746-bcbb-366ae566721b").Id)))
                .ToList();
        }

        public Administrator GetAdministrator(string userId)
        {
            return _context.Administrators.Include(x => x.Login)
                 .SingleOrDefault(mb => mb.AdministratorId == userId);
        }

        public void Add(Administrator administrator)
        {
            _context.Administrators.Add(administrator);
        }

        public IEnumerable<Administrator> GetUsersDetails()
        {
            return _context.Administrators.Where(a => !a.Login.Roles.Select(r => r.RoleId).Contains(_context.Roles.FirstOrDefault(r => r.Id == "95488c21-017a-491b-83f3-4eba8aa5d4a6").Id)).ToList();
        }

        public Administrator GetAdministratorByApiKey(string apiKey)
        {
            return _context.Administrators
                .SingleOrDefault(m => m.ApiKey == apiKey);
        }

        public Administrator GetAdministratorByEmailAddress(string emailAddress)
        {
            return _context.Administrators
                .SingleOrDefault(m => m.EmailAddress == emailAddress);
        }
    }
}
