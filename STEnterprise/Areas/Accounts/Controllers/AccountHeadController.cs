using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using STEnterprise.Areas.Accounts.BLL;
using STEnterprise.Areas.Accounts.Models;
using STEnterprise.AuthData;

namespace STEnterprise.Areas.Accounts.Controllers
{
    //ataur
    [AuthenticationFilter]
    public class AccountHeadController : Controller
    {
        // GET: Accounts/AccountHead
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAccountHead()
        {
            AccountHeadBLL objAccountHeadBll = new AccountHeadBLL();
            var accountHeads = objAccountHeadBll.GetAccountHeadList().ToList();
            return Json(new { data = accountHeads }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Save()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Save(AccountHead objAccountHead)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                AccountHeadBLL objAccountHeadBll = new AccountHeadBLL();
                objAccountHeadBll.CreateAccountHead(objAccountHead);
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }
       
        [HttpGet]
        public ActionResult Edit(int id)
        {
            AccountHeadBLL objAccountHeadBll=new AccountHeadBLL();
            var accountHead= objAccountHeadBll.GetAccountHeadById(id);

            return View(accountHead);
        }

        [HttpPost]
        public ActionResult Edit(AccountHead objAccountHead)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                AccountHeadBLL objAccountHeadBll = new AccountHeadBLL();
                objAccountHeadBll.UpdateAccountHead(objAccountHead);
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            AccountHeadBLL objAccountHeadBll = new AccountHeadBLL();
            var accountHead = objAccountHeadBll.GetAccountHeadById(id);

            return View(accountHead);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteAccountHead(int id)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                AccountHeadBLL objAccountHeadBll = new AccountHeadBLL();
                objAccountHeadBll.DeleteAccountHead(id);
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}