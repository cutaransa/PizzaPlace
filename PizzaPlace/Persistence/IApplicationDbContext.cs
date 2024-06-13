using Castle.Core.Resource;
using Microsoft.Owin.BuilderProperties;
using Ninject.Activation;
using Ninject.Planning.Bindings;
using PizzaPlace.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlace.Persistence
{
    public interface IApplicationDbContext
    {
        IDbSet<ApplicationUser> Users { get; set; }
        DbSet<Administrator> Administrators { get; set; }
        DbSet<Module> Modules { get; set; }
        DbSet<RoleModule> RoleModules { get; set; }
    }
}
