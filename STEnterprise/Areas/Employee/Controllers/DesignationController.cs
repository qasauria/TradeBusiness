using System.Collections.Generic;
using System.Web.Mvc;
using STEnterprise.Areas.Employee.BLL;
using STEnterprise.Areas.Employee.Models;
using STEnterprise.AuthData;

namespace STEnterprise.Areas.Employee.Controllers
{
    [AuthenticationFilter]
    public class DesignationController : Controller
    {
        public ActionResult Index()
        {
            DesignationBLL objDesignationBll = new DesignationBLL();
            List<Models.Designation> designationList = objDesignationBll.GetAllDesignation();
            return View(designationList);
        }
       
        [HttpGet]
        public ActionResult Save()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Designation objDesignation)
        {
            if (ModelState.IsValid)
            {
                DesignationBLL objDesignationBll = new DesignationBLL();
                objDesignationBll.CreateDesignation(objDesignation);
            }
            return RedirectToAction("Index", "Designation");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            DesignationBLL objDesignationBll = new DesignationBLL();
            var designation = objDesignationBll.GetAllDesignation(id);
            return View(designation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Designation objDesignation)
        {
            if (ModelState.IsValid)
            {
                DesignationBLL objDesignationBll = new DesignationBLL();
                objDesignationBll.UpadateDesignation(objDesignation);
            }
            return RedirectToAction("Index", "Designation");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            DesignationBLL objDesignationBll = new DesignationBLL();
            var designation = objDesignationBll.GetAllDesignation(id);
            return View(designation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteDesignation(int id)
        {
           if (ModelState.IsValid)
            {
                DesignationBLL objDesignationBll=new DesignationBLL();
                objDesignationBll.DeleteDesignation(id);
            }
            return RedirectToAction("Index", "Designation");

        }

        public JsonResult GetDesignationNameIsExist(string DesignationName)
        {
            DesignationBLL objDesignationBLL = new DesignationBLL();
            bool exsits = objDesignationBLL.GetDesignationNameIsExist(DesignationName);
            if (exsits) { return Json(false, JsonRequestBehavior.AllowGet); }
            else { return Json(true, JsonRequestBehavior.AllowGet); }
        }
    }
}