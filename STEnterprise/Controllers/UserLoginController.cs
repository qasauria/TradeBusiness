using System;
using System.Web.Mvc;
using System.Web.Security;
using STEnterprise.BLL;
using STEnterprise.DAL;
using STEnterprise.Models;

namespace STEnterprise.Controllers
{
    [AllowAnonymous]
    public class UserLoginController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(UserLogin userLogin, string returnUrl)
        {
            UserLoginBLL objUserLoginBll = new UserLoginBLL();

            if (ModelState.IsValid)
            {
                var loginInfo = objUserLoginBll.IsValid(userLogin);

                if (loginInfo != null)
                {
                    SessionUtility.STSessionContainer.UserID = loginInfo.UserDetailId;
                    SessionUtility.STSessionContainer.UserName = loginInfo.Username;

                    FormsAuthentication.SetAuthCookie(loginInfo.Username, true);

                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
            }
            return View("Login");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOff()
        {
            if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                FormsAuthentication.SignOut();
                Session.Clear();
                Session.Abandon();
            }
            return Redirect("~/UserLogin/Login");
        }
    }
}