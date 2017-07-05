using STEnterprise.Areas.Ledger.BLL;
using STEnterprise.Areas.Ledger.Models;
using STEnterprise.Areas.Purchase.BLL;
using STEnterprise.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace STEnterprise.Areas.Ledger.Controllers
{
    public class DailyLedgerController : Controller
    {
        // GET: Ledger/DailyLedger
        public ActionResult Save()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Save(List<DailyLedger> objLedger)
        {
            bool status = false;
            LedgerBLL objLedgerBLL = new LedgerBLL();
            objLedgerBLL.SaveLadger(objLedger);
            status = true;
            var a = SessionUtility.STSessionContainer.UniqNumber;
            return new JsonResult { Data = new { status = status,a } };
        }
        public JsonResult GetPartyNameByAccountType(string AccountType)
        {
            LedgerBLL objLedgerBLL = new LedgerBLL();
            var data = objLedgerBLL.GetPartyNameByAccountType(AccountType);            
            return Json(data,JsonRequestBehavior.AllowGet);
        }
        public ActionResult Privew(int? TransactionId)
        {
            LedgerBLL objLedgerBLL = new LedgerBLL();
            if (TransactionId > 0)
            {
                var data = objLedgerBLL.GetPrevieByTransactionId(TransactionId);
                ViewBag.data = data;
                ViewBag.TransactionId = TransactionId;
            }
            else
            {
                TempData["msg"] = "<script>alert('No data Found');</script>";
                return Redirect("Save");
            }
            return View();
        }

    }
}