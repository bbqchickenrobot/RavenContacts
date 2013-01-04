using System.Web.Mvc;

namespace Raven.Contacts.Controllers
{
    public class HomeController : BaseController
    {

        public ActionResult Index()
        {
            return View();
        }

    }
}
