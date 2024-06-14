using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject.Activation;
using PizzaPlace.Core.Models;
using PizzaPlace.Web.Core.Dtos.Model;
using PizzaPlace.Web.Core.Dtos.Report;


namespace PizzaPlace.Web.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Administrator, AdministratorDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Pizza, PizzaDto>();
            CreateMap<PizzaType, PizzaTypeDto>();
            CreateMap<IdentityRole, RoleDto>();
            CreateMap<ApplicationUser, UserDto>();


            CreateMap<Administrator, ListAdministratorDto>();
            CreateMap<Category, ListCategoryDto>();
            CreateMap<Pizza, ListPizzaDto>();
            CreateMap<PizzaType, ListPizzaTypeDto>();
            CreateMap<IdentityRole, ListRoleDto>();
            CreateMap<ApplicationUser, ListUserDto>();


            CreateMap<Administrator, ViewAdministratorDto>();
            CreateMap<Category, ViewCategoryDto>();
            CreateMap<Pizza, ViewPizzaDto>();
            CreateMap<PizzaType, ViewPizzaTypeDto>();
            CreateMap<IdentityRole, ViewRoleDto>();
            CreateMap<ApplicationUser, ViewUserDto>();

            CreateMap<OrderDetail, TransactionDto>();
        }
    }
}