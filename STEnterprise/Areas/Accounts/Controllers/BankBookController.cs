using STEnterprise.Areas.Accounts.BLL;
using STEnterprise.Areas.Accounts.Models;
using STEnterprise.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using STEnterprise.AuthData;

namespace STEnterprise.Areas.Accounts.Controllers
{
       [AuthenticationFilter]
    public class BankBookController : Controller
    {
        // GET: BankBook/BankBook
        public ActionResult Save()
        {
            BankBookBLL objBankBookBLL =new BankBookBLL();
            List<BankBookModel> bankName= objBankBookBLL.GetBankName();
            ViewBag.bankName = bankName;
            return View();
        }
        [HttpPost]
        public ActionResult Save(List<BankBookModel> objbank)
        {
            bool status = false;
            BankBookBLL objBankBookBLL = new BankBookBLL();
            objBankBookBLL.SaveBankBook(objbank);
            status = true;
            var a = SessionUtility.STSessionContainer.UniqNumber;
            return new JsonResult { Data = new { status = status, a } };
        }

        public JsonResult GetAccountNumberByBankName(int? BankName)
        {
            BankBookBLL objBankBookBLL = new BankBookBLL();
            var accountNumber = objBankBookBLL.GetAccountNumberByBankName(BankName);
            return Json(accountNumber, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Privew(string TransactionId)
        {
            BankBookBLL objBankBookBLL = new BankBookBLL();
            //if (TransactionId >0)
            //{
                var data = objBankBookBLL.GetPrevieByTransactionId(TransactionId);
                ViewBag.data = data;
                ViewBag.TransactionId = TransactionId;
            //}
            //else
            //{
            //    TempData["msg"] = "<script>alert('No data Found');</script>";
            //    return Redirect("Save");
            //}
            return View();
        }
    }
}