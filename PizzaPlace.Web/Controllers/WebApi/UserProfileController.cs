using AutoMapper;
using PizzaPlace.Web.Core.Dtos.Model;
using PizzaPlace.Core;
using PizzaPlace.Persistence;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PizzaPlace.Web.Core.Dtos;
namespace PizzaPlace.Web.Controllers.WebApi
{
    public class UserProfileController : ApiController
    {
        private ApplicationDbContext _context;

        private readonly IUnitOfWork _unitOfWork;

        public UserProfileController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        [Route("api/UserProfile/detail/")]
        public IHttpActionResult GetUser()
        {
            var userId = Request.GetOwinContext().Authentication.User.Identity.GetUserId();
            var user = _unitOfWork.Users.GetUser(userId);
            var admin = _unitOfWork.Administrators.GetAdministrator(userId);
            if (user == null || admin == null)
                return Content(HttpStatusCode.NotFound, "User not found.");

            var role = user.Roles.SingleOrDefault().RoleId;
            var roleName = _context.Roles.SingleOrDefault(r => r.Id == role).Name;

            var roleDto = new ViewRoleDto()
            {
                id = role,
                name = roleName
            };

            var userDto = new GetUserDto()
            {
                id = user.Id,
                username = user.UserName,
                name = admin.Name,
                email = user.Email,
                role = roleDto,
                isDefault = admin.IsDefault,
            };

            return Json(userDto);
        }

    }
}