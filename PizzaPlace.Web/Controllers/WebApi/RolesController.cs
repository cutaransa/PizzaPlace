using AutoMapper;
using PizzaPlace.Web.Core.Dtos.Model;
using PizzaPlace.Core;
using PizzaPlace.Core.Models;
using PizzaPlace.Core.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PizzaPlace.Web.Controllers.WebApi
{
    [Authorize]
    public class RolesController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public RolesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("api/Roles/")]
        public IEnumerable<ListRoleDto> GetRoles()
        {
            IEnumerable<IdentityRole> roles = null;
            var user = _unitOfWork.Administrators.GetAdministrator(Request.GetOwinContext().Authentication.User.Identity.GetUserId());

            if (user.IsDefault == true)
                roles = _unitOfWork.Roles.GetDefaultUserRoles();
            else
                roles = _unitOfWork.Roles.GetOtherUserRoles();


            return roles.Select(Mapper.Map<IdentityRole, ListRoleDto>);
        }

        [HttpPost]
        [Route("api/Roles/detail/")]
        public IHttpActionResult GetRole(string id)
        {
            var role = _unitOfWork.Roles.GetRole(id);

            if (role == null)
                return Content(HttpStatusCode.NotFound, "Role not found.");

            return Content(HttpStatusCode.OK, Mapper.Map<IdentityRole, RoleDto>(role));
        }

        [HttpPost]
        [Route("api/Roles/add/")]
        public IHttpActionResult Add(AddRoleDto dto)
        {
            List<string> errors = new List<string>();
            errors = ModelErrorChecker.Check(ModelState);

            if (errors.Count == 0)
            {
                var roleManager = _unitOfWork.Roles.GetRoleManager();
                if (roleManager.RoleExists(dto.name))
                    return Content(HttpStatusCode.NotAcceptable, new string[] { "Role name already exist." });

                var role = new IdentityRole() { Name = dto.name };
                roleManager.Create(role);

                // List all modules and added it to role module for access management
                var modules = _unitOfWork.Modules.GetModules();
                foreach (var module in modules)
                {
                    var roleModule = new RoleModule()
                    {
                        RoleId = role.Id,
                        ModuleId = module.ModuleId
                    };
                    _unitOfWork.RoleModules.Add(roleModule);
                }
                _unitOfWork.Complete();

            }
            else
                return Content(HttpStatusCode.NotAcceptable, errors.ToList());

            return Ok();
        }

        [HttpPost]
        [Route("api/Roles/update/")]
        public IHttpActionResult Update(EditRoleDto dto)
        {
            List<string> errors = new List<string>();
            errors = ModelErrorChecker.Check(ModelState);

            if (errors.Count == 0)
            {
                var roleManager = _unitOfWork.Roles.GetRoleManager();
                if (roleManager.RoleExists(dto.name))
                    return Content(HttpStatusCode.NotAcceptable, new string[] { "Role name already exist." });

                var role = _unitOfWork.Roles.GetRole(dto.id);
                var isDefault = _unitOfWork.Roles.IsDefaultRole(dto.id);
                if (isDefault == true)
                    return Content(HttpStatusCode.NotAcceptable, new string[] { "Role cannot be updated." });
                role.Name = dto.name;
                _unitOfWork.Complete();
            }
            else
                return Content(HttpStatusCode.NotAcceptable, errors.ToList());

            return Ok();
        }
    }
}