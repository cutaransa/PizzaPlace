using System.Web.Mvc;

namespace PizzaPlace.Web.Controllers
{
    public class AppController : Controller
    {
        [Authorize]
        public ActionResult Categories()
        {
            return PartialView();
        }
        [Authorize]
        public ActionResult CategoriesEditor()
        {
            return PartialView();
        }
        [Authorize]
        public ActionResult ChangePassword()
        {
            return PartialView();
        }
        [Authorize]
        public ActionResult Dashboard()
        {
            return PartialView();
        }
        public ActionResult ForgotPassword()
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
        public ActionResult Pizzas()
        {
            return PartialView();
        }
        [Authorize]
        public ActionResult PizzasEditor()
        {
            return PartialView();
        }
        [Authorize]
        public ActionResult PizzaTypes()
        {
            return PartialView();
        }
        [Authorize]
        public ActionResult PizzaTypesEditor()
        {
            return PartialView();
        }
        public ActionResult ResetPassword()
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
        public ActionResult SignIn()
        {
            return PartialView();
        }
        [Authorize]
        public ActionResult UserProfile()
        {
            return PartialView();
        }
        [Authorize]
        public ActionResult Users()
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