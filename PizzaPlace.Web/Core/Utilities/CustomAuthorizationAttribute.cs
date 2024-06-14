using PizzaPlace.Persistence;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace PizzaPlace.Web.Core.Utility
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (AuthorizeRequest(actionContext))
                return;

            HandleUnauthorizedRequest(actionContext);
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            if (((System.Web.HttpContext.Current.User).Identity).IsAuthenticated)
                actionContext.Response = new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.Forbidden,
                    Content = new StringContent("You are unauthorized to access this resource.")
                };
            else
                base.HandleUnauthorizedRequest(actionContext);
        }

        private bool AuthorizeRequest(HttpActionContext actionContext)
        {
            ApplicationDbContext _context = new ApplicationDbContext();
            // Get current user details
            var userId = actionContext.RequestContext.Principal.Identity.GetUserId();
            var currentUser = _context.Users.SingleOrDefault(u => u.Id == userId);
            var roleId = currentUser.Roles.SingleOrDefault().RoleId;

            // Get role module based on role, controller and action
            var roleModule = _context.RoleModules
                .Where(rm => rm.RoleId == roleId)
                .Where(rm => rm.Module.Controller == actionContext.ControllerContext.ControllerDescriptor.ControllerName)
                .Where(rm => rm.Module.Action == actionContext.ActionDescriptor.ActionName)
                .SingleOrDefault();

            // Return bool based on IsEnabled property
            return roleModule != null ? roleModule.IsEnabled : false;
        }
    }
}