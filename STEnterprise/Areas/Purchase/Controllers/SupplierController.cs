using System.Collections.Generic;
using System.Web.Mvc;
using STEnterprise.Areas.Purchase.BLL;
using STEnterprise.Areas.Purchase.Models;
using STEnterprise.AuthData;

namespace STEnterprise.Areas.Purchase.Controllers
{
    //Created By Shahadat
    [AuthenticationFilter]
    public class SupplierController : Controller
    {
        // GET: Admin/Supplier
        public ActionResult Index()
        {
            SupplierBLL objSupplierBLL = new SupplierBLL();
            List<Supplier> SupplierList = objSupplierBLL.GetAllSupplier();
            return View(SupplierList);
        }

        
        [HttpGet]
        public ActionResult Save()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Save(Supplier objSupplier)
        {
            if (ModelState.IsValid)
            {
                SupplierBLL objSupplierBLL = new SupplierBLL();

                objSupplierBLL.SaveSupplier(objSupplier);
            }
            return RedirectToAction("Index", "Supplier");
        }

        public JsonResult GetSupplierNameIsExist(string supplierName)
        {
            SupplierBLL objSupplierBLL = new SupplierBLL();
            bool exsits = objSupplierBLL.GetSupplierNameIsExist(supplierName);
            if (exsits) { return Json(false, JsonRequestBehavior.AllowGet); }
            else { return Json(true, JsonRequestBehavior.AllowGet); }
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            SupplierBLL objSupplierBLL = new SupplierBLL();
            var supplier = objSupplierBLL.GetAllSupplierById(id);
            return View(supplier);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Supplier objSupplier)
        {
            if (ModelState.IsValid)
            {
                SupplierBLL objSupplierBLL = new SupplierBLL();

                objSupplierBLL.UpdateSupplier(objSupplier);
            }
            return RedirectToAction("Index", "Supplier");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            SupplierBLL objSupplierBLL = new SupplierBLL();
            var supplier = objSupplierBLL.GetSupplier(id);
            return View(supplier);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteMeasurement(int id)
        {
            if (ModelState.IsValid)
            {
                SupplierBLL objSupplierBLL = new SupplierBLL();
                objSupplierBLL.DeleteSupplier(id);
            }
            return RedirectToAction("Index", "Supplier");
        }


    }
}