using STEnterprise.Areas.Purchase.BLL;
using STEnterprise.Areas.Purchase.Models;
using System.Web.Mvc;
using System.Collections.Generic;
using STEnterprise.AuthData;

namespace STEnterprise.Areas.Purchase.Controllers
{
    [AuthenticationFilter]
    public class ChequeDetailController : Controller
    {
        // GET: Purchase/ChequeDetail
        public ActionResult Index()
        {
            ChequeDetailBLL objChequeDetailBLL = new ChequeDetailBLL();
            List<ChequeDetail> chequeDetailList = objChequeDetailBLL.GetAllChequeDetail();
            return View(chequeDetailList);
        }

        [HttpGet]
        public ActionResult Save()
        {
            ChequeDetailBLL objChequeDetailBLL = new ChequeDetailBLL();
            var model = new ChequeDetail
            {
                BankNameInfo = objChequeDetailBLL.GetBankName()
            };

            return View(model);            
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Save(ChequeDetail objChequeDetail)
        {           
            if (ModelState.IsValid)
            {
                ChequeDetailBLL objChequeDetailBLL = new ChequeDetailBLL();

                objChequeDetailBLL.SaveChequeDetailInfo(objChequeDetail);

            }

            return RedirectToAction("Index", "ChequeDetail");
        }

        public JsonResult GetOwnerNameByChequeId(int chequeIssuedBy)
        {
            ChequeDetailBLL objChequeDetailBLL = new ChequeDetailBLL();
            var data = objChequeDetailBLL.GetOwnerNameByChequeId(chequeIssuedBy);
            
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ChequeDetailBLL objChequeDetailBLL = new ChequeDetailBLL();
            var chequeDetail = objChequeDetailBLL.GetChequeDetail(id);
            ViewBag.bankname = objChequeDetailBLL.GetBankName();
            return View(chequeDetail);
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Edit(ChequeDetail objChequeDetail)
        {

            if (ModelState.IsValid)
            {
                ChequeDetailBLL objChequeDetailBLL = new ChequeDetailBLL();

                objChequeDetailBLL.UpdateChequeDetail(objChequeDetail);

            }
            return RedirectToAction("Index", "ChequeDetail");
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            ChequeDetailBLL objChequeDetailBLL = new ChequeDetailBLL();
            var chequeDetail = objChequeDetailBLL.GetChequeDetail(id);
            ViewBag.bankname = objChequeDetailBLL.GetBankName();
            return View(chequeDetail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [ActionName("Delete")]
        public ActionResult DeleteChequeDetail(int id)
        {

            if (ModelState.IsValid)
            {
                ChequeDetailBLL objChequeDetailBLL = new ChequeDetailBLL();
                objChequeDetailBLL.DeleteChequeDetail(id);
            }
            return RedirectToAction("Index", "ChequeDetail");
        }
    }
}