using STEnterprise.Areas.Purchase.BLL;
using STEnterprise.Areas.Purchase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using STEnterprise.AuthData;

namespace STEnterprise.Areas.Purchase.Controllers
{
    [AuthenticationFilter]
    public class BankDetailController : Controller
    {
        // Created By Shahadat
        // GET: Purchase/BankDetail
        public ActionResult Index()
        {
            BankDetailBLL objBankDetailBLL = new BankDetailBLL();
            List<BankDetail> BankDetailList = objBankDetailBLL.GetAllBankDetail();
            return View(BankDetailList);
        }
       

        [HttpGet]
        public ActionResult Save()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Save(BankDetail objBankDetail)
        {
            if (ModelState.IsValid)
            {
                BankDetailBLL objBankDetailBLL = new BankDetailBLL();

                objBankDetailBLL.SaveBankDetail(objBankDetail);
            }
            return RedirectToAction("Index", "BankDetail");
        }
        
        public JsonResult GetBankNameIsExist(string bankName)
        {
            BankDetailBLL objBankDetailBLL = new BankDetailBLL();
            bool exsits = objBankDetailBLL.GetBankNameIsExist(bankName);
            if (exsits) { return Json(false, JsonRequestBehavior.AllowGet); }
            else { return Json(true, JsonRequestBehavior.AllowGet); }
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            BankDetailBLL objBankDetailBLL = new BankDetailBLL();
            var bankDetail = objBankDetailBLL.GetAllBankDetailById(id);
            return View(bankDetail);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BankDetail objBankDetail)
        {
            if (ModelState.IsValid)
            {
                BankDetailBLL objBankDetailBLL = new BankDetailBLL();                
                objBankDetailBLL.UpdateBankDetail(objBankDetail);
            }
            return RedirectToAction("Index", "BankDetail");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            BankDetailBLL objBankDetailBLL = new BankDetailBLL();
            var bankDetail = objBankDetailBLL.GetAllBankDetailById(id);
            return View(bankDetail);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteBankDetail(int id)
        {
            if (ModelState.IsValid)
            {
                BankDetailBLL objBankDetailBLL = new BankDetailBLL();
                objBankDetailBLL.DeleteBankDetail(id);
            }
            return RedirectToAction("Index", "BankDetail");
        }
    }
}