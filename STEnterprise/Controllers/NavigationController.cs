using System.Web.Mvc;
using STEnterprise.BLL;

namespace STEnterprise.Controllers
{
    public class NavigationController : Controller
    {
        // GET: Navigation
        public ActionResult Index()
        {
            NavigationBLL objNavigationBll = new NavigationBLL();

            var menu = objNavigationBll.GetAllMenu();
            return View(menu);
        }
    }
}