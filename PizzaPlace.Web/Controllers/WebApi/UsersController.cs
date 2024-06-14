using AutoMapper;
using PizzaPlace.Web.Core.Dtos.Model;
using PizzaPlace.Core;
using PizzaPlace.Core.Models;
using PizzaPlace.Core.Utilities;
using PizzaPlace.Persistence;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PizzaPlace.Web.Controllers.WebApi
{
    public class UsersController : ApiController
    {
        private static ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private static TextInfo _ti;
        private static UserStore<ApplicationUser> _userStore;
        private static UserManager<ApplicationUser> _userManager;

        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _context = new ApplicationDbContext();
            _ti = CultureInfo.CurrentCulture.TextInfo;
            _userStore = new UserStore<ApplicationUser>(_context);
            _userManager = new UserManager<ApplicationUser>(_userStore);
        }

        [HttpPost]
        [Route("api/Users/")]
        public IEnumerable<ListAdministratorDto> GetUsers()
        {
            var users = _unitOfWork.Administrators.GetAdministrators();

            return users.Select(Mapper.Map<Administrator, ListAdministratorDto>);
        }

        [HttpPost]
        [Route("api/Users/detail/")]
        public IHttpActionResult GetUser(string id)
        {
            try
            {
                var user = _unitOfWork.Administrators.GetAdministrator(Request.GetOwinContext().Authentication.User.Identity.GetUserId());

                var administrator = _unitOfWork.Administrators.GetAdministrator(id);
                if (administrator == null)
                    return Content(HttpStatusCode.NotAcceptable, "Administrator not found.");
                var curAdministrator = Mapper.Map<Administrator, ViewAdministratorDto>(administrator);

                curAdministrator.roleId = administrator.Login.Roles.FirstOrDefault().RoleId;
                var role = _unitOfWork.Roles.GetRole(curAdministrator.roleId);
                curAdministrator.role = Mapper.Map<IdentityRole, ViewRoleDto>(role);

                if (user.IsDefault == true)
                {
                    var roles = _unitOfWork.Roles.GetDefaultUserRoles();
                    curAdministrator.roles = roles.Select(Mapper.Map<IdentityRole, ListRoleDto>);
                }
                else
                {
                    var roles = _unitOfWork.Roles.GetOtherUserRoles();
                    curAdministrator.roles = roles.Select(Mapper.Map<IdentityRole, ListRoleDto>);
                }


                return Content(HttpStatusCode.OK, curAdministrator);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e.ToString());
            }
        }

        [HttpPost]
        [Route("api/Users/add/")]
        public IHttpActionResult Add(AddAdministratorDto dto)
        {
            List<string> errors = new List<string>();
            errors = ModelErrorChecker.Check(ModelState);

            if (errors.Count == 0)
            {
                try
                {
                    var role = _unitOfWork.Roles.GetRole(dto.roleId);
                    if (role == null)
                        return Content(HttpStatusCode.NotAcceptable, new List<string>() { "Role not found." });

                    var user = new ApplicationUser();
                    user.UserName = dto.userName;
                    user.Email = String.IsNullOrEmpty(dto.emailAddress) ? null : dto.emailAddress;
                    var chkUser = _userManager.Create(user, dto.password);
                    if (chkUser.Succeeded)
                    {
                        var result1 = _userManager.AddToRole(user.Id, role.Name);

                        var administrator = new Administrator()
                        {
                            AdministratorId = user.Id,
                            Name = dto.name,
                            EmailAddress = dto.emailAddress,
                            ApiKey = GetUniqueCode.ForApiKey(25),
                            IsActive = true,
                            IsDefault = false,
                        };
                        _unitOfWork.Administrators.Add(administrator);
                        _unitOfWork.Complete();


                        return Ok();
                    }
                    else
                    {
                        return Content(HttpStatusCode.NotAcceptable, GetErrorResult(chkUser));
                    }
                }
                catch (Exception e)
                {
                    return Content(HttpStatusCode.NotAcceptable, e);
                }
            }
            else
                return Content(HttpStatusCode.NotAcceptable, errors.ToList());

        }

        [HttpPost]
        [Route("api/Users/update/")]
        public IHttpActionResult Update(EditAdministratorDto dto)
        {

            var userId = User.Identity.GetUserId();
            List<string> errors = new List<string>();
            errors = ModelErrorChecker.Check(ModelState);

            if (errors.Count == 0)
            {
                if (userId == dto.administratorId)
                    return Content(HttpStatusCode.NotAcceptable, new List<string>() { "You are not allowed to access this resources." });

                var user = _unitOfWork.Users.GetUser(dto.administratorId);

                var role = (from r in _context.Roles select r).FirstOrDefault();
                var oldRoleId = user.Roles.SingleOrDefault().RoleId;
                var oldRoleName = _context.Roles.SingleOrDefault(r => r.Id == oldRoleId).Name;
                var newRoleName = _context.Roles.SingleOrDefault(r => r.Id == dto.roleId).Name;


                using (var context = new ApplicationDbContext())
                {
                    var userStore = new UserStore<ApplicationUser>(context);
                    var userManager = new UserManager<ApplicationUser>(userStore);

                    if (oldRoleName != newRoleName)
                    {

                        IdentityResult addToRole = userManager.AddToRole(user.Id, newRoleName);
                        if (addToRole.Succeeded)
                        {
                            userManager.RemoveFromRole(user.Id, oldRoleName);
                            _unitOfWork.Complete();
                        }
                        else
                            return Content(HttpStatusCode.NotAcceptable, GetErrorResult(addToRole));
                    }

                    var login = userManager.FindById(user.Id);
                    var loginId = login.Id;
                    if (login != null)
                    {
                        login.Email = dto.emailAddress;
                        login.UserName = dto.userName;
                        var updateUser = userManager.Update(login);

                        if (!updateUser.Succeeded)
                        {
                            return Content(HttpStatusCode.NotAcceptable, GetErrorResult(updateUser));
                        }

                        var admin = _unitOfWork.Administrators.GetAdministrator(dto.administratorId);

                        admin.Name = dto.name;
                        admin.EmailAddress = dto.emailAddress;

                        _unitOfWork.Complete();
                    }
                    else
                    {
                        return Content(HttpStatusCode.NotAcceptable, new List<string>() { "User not found." });
                    }
                }
            }
            else
                return Content(HttpStatusCode.NotAcceptable, errors.ToList());

            return Ok();
        }

        [HttpPost]
        [Route("api/Users/profile/")]
        public IHttpActionResult GetProfile()
        {
            var userId = Request.GetOwinContext().Authentication.User.Identity.GetUserId();
            var user = _unitOfWork.Users.GetUser(userId);

            if (user == null)
                return Content(HttpStatusCode.NotFound, "User not found.");

            return Content(HttpStatusCode.OK, Mapper.Map<ApplicationUser, ViewUserDto>(user));
        }

        [HttpPost]
        [Route("api/Users/set-status/")]
        public IHttpActionResult SetStatus(string id)
        {
            var userId = User.Identity.GetUserId();

            if (userId != id)
            {
                var admin = _unitOfWork.Administrators.GetAdministrator(id);
                if (admin != null)
                {
                    admin.IsActive = admin.IsActive ? false : true;
                    _unitOfWork.Complete();
                    return Ok();
                }
                else
                    return Content(HttpStatusCode.NotAcceptable, "User not found.");
            }
            else
                return Content(HttpStatusCode.NotAcceptable, "Login user cannot change his own status.");
        }

        [HttpPost]
        [Route("api/Users/roles")]
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

        private List<string> GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return (new List<string>() { "An internal error occured." });
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    var errors = new List<string>();

                    foreach (string error in result.Errors)
                    {
                        errors.Add(error);
                    }

                    return errors;
                }

                if (ModelState.IsValid)
                {
                    return (new List<string>() { "An object is not valid." });
                }
            }

            return (new List<string>() { "An internal error occured." });
        }
    }
}