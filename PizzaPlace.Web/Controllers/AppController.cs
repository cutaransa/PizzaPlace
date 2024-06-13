using System.Web.Mvc;

namespace PizzaPlace.Web.Controllers
{
    public class AppController : Controller
    {
        public ActionResult SignIn()
        {
            return PartialView();
        }
        public ActionResult ForgotPassword()
        {
            return PartialView();
        }
        public ActionResult ResetPassword()
        {
            return PartialView();
        }
        [Authorize]
        public ActionResult ChangePassword()
        {
            return PartialView();
        }
        [Authorize]
        public ActionResult CustomerDetails()
        {
            return PartialView();
        }
        [Authorize]
        public ActionResult Dashboard()
        {
            return PartialView();
        }
        [Authorize]
        public ActionResult Modules()
        {
            return PartialView();
        }
        [Authorize]
        public ActionResult ModulesEditor()
        {
            return PartialView();
        }
        [Authorize]
        public ActionResult Roles()
        {
            return PartialView();
        }
        [Authorize]
        public ActionResult RolesEditor()
        {
            return PartialView();
        }
        [Authorize]
        public ActionResult Users()
        {
            return PartialView();
        }
        [Authorize]
        public ActionResult UserProfile()
        {
            return PartialView();
        }
        [Authorize]
        public ActionResult UsersEditor()
        {
            return PartialView();
        }
    }
}