using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject.Activation;
using PizzaPlace.Core.Models;
using PizzaPlace.Web.Core.Dtos.Model;


namespace PizzaPlace.Web.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Administrator, AdministratorDto>();
            CreateMap<IdentityRole, RoleDto>();
            CreateMap<ApplicationUser, UserDto>();


            CreateMap<Administrator, ListAdministratorDto>();
            CreateMap<IdentityRole, ListRoleDto>();
            CreateMap<ApplicationUser, ListUserDto>();


            CreateMap<Administrator, ViewAdministratorDto>();
            CreateMap<IdentityRole, ViewRoleDto>();
            CreateMap<ApplicationUser, ViewUserDto>();
        }
    }
}