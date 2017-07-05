using System.Linq;
using System.Web.Mvc;
using STEnterprise.AuthData;
using STEnterprise.BLL;
using STEnterprise.Models;

namespace STEnterprise.Controllers
{
    [AuthenticationFilter]
    public class UserDetailController : Controller
    {
        // GET: Admin/UserDetail
        public ActionResult Index()
        {
            UserDetailBLL objUserDetailBll = new UserDetailBLL();
            var list = objUserDetailBll.GetAllUserInfo().ToList();
            return View(list);
        }


        [HttpGet]
        public ActionResult Save()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(UserDetail objUserDetail)
        {
            if (ModelState.IsValid)
            {
                UserDetailBLL objUserDetailBll = new UserDetailBLL();
                objUserDetailBll.CreateUserInfo(objUserDetail);
            }
            return RedirectToAction("Index", "UserDetail");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            UserDetailPartialBLL objUserDetailPartialBll = new UserDetailPartialBLL();
            var userInfo = objUserDetailPartialBll.GetUserInfo(id);

            return View(userInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserDetailPartial objUserDetailPartial)
        {

            if (ModelState.IsValid)
            {
                UserDetailPartialBLL objUserDetailPartialBll = new UserDetailPartialBLL();
                objUserDetailPartialBll.UpdateUserInfo(objUserDetailPartial);
            }
            return RedirectToAction("Index", "UserDetail");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            UserDetailPartialBLL objUserDetailPartialBll = new UserDetailPartialBLL();
            var userInfo = objUserDetailPartialBll.GetUserInfo(id);
            return View(userInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteUserInfo(int id)
        {
            if (ModelState.IsValid)
            {
                UserDetailPartialBLL objUserDetailPartialBll = new UserDetailPartialBLL();
                objUserDetailPartialBll.DeleteUserInfo(id);
            }
            return RedirectToAction("Index", "UserDetail");

        }
    }
}