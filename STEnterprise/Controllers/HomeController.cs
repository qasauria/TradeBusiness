using System;
using System.Web.Mvc;
using STEnterprise.AuthData;
using STEnterprise.BLL;

namespace STEnterprise.Controllers
{
    [AuthenticationFilter]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}