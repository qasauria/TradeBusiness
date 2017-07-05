using STEnterprise.Areas.Accounts.BLL;
using STEnterprise.Areas.Accounts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using STEnterprise.AuthData;

namespace STEnterprise.Areas.Accounts.Controllers
{
    [AuthenticationFilter]
    public class BankBookOpeningBalanceController : Controller
    {
        // GET: Accounts/BankBookOpeningBalance
        public ActionResult Save()
        {
            BankBookOpeningBalanceBLL objBankBookOpeningBalanceBLL = new BankBookOpeningBalanceBLL();
            List<BankBookOpeningBalance> bankName = objBankBookOpeningBalanceBLL.GetBankNameforOpeningBalnce();
            ViewBag.bankName = bankName;
            return View();
        }
        [HttpPost]
        public ActionResult Save(BankBookOpeningBalance objBankBookOpeningBalance)
        {
            if (ModelState.IsValid)
            {
                BankBookOpeningBalanceBLL objBankBookOpeningBalanceBLL = new BankBookOpeningBalanceBLL();
                objBankBookOpeningBalanceBLL.CreateBankBookOpeningBalance(objBankBookOpeningBalance);
                List<BankBookOpeningBalance> bankName = objBankBookOpeningBalanceBLL.GetBankNameforOpeningBalnce();
                ViewBag.bankName = bankName;
            }
            return View();
        }
        public JsonResult GetAccountNumberByBankName(int? BankName)
        {
            BankBookBLL objBankBookBLL = new BankBookBLL();
            var accountNumber = objBankBookBLL.GetAccountNumberByBankName(BankName);
            return Json(accountNumber, JsonRequestBehavior.AllowGet);
        }
    }
}