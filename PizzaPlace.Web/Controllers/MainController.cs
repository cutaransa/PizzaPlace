using System.Web.Mvc;

namespace PizzaPlace.Web.Controllers
{
    public class MainController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}