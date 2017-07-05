using STEnterprise.Areas.Employee.BLL;
using STEnterprise.Areas.Employee.Models;
using System.Web.Mvc;
using STEnterprise.AuthData;

namespace STEnterprise.Areas.Employee.Controllers
{
    //Created By Shahadat
    [AuthenticationFilter]
    public class SalaryDetailController : Controller
    {
        // GET: Employee/SalaryDetail
        public ActionResult Index()
        {
            SalaryDetailBLL objSalaryDetailBLL = new SalaryDetailBLL();
            var salaryDetailList = objSalaryDetailBLL.GetSalaryDetails();
            return View(salaryDetailList);
        }
      
        [HttpGet]
        public ActionResult Save()
        {
            SalaryDetailBLL objSalaryDetailBLL = new SalaryDetailBLL();
            var model = new SalaryDetail
            {
                EmployeeInfo = objSalaryDetailBLL.GetEmployeeDetailName()
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Save(SalaryDetail objSalaryDetail)
        {

            if (ModelState.IsValid)
            {
                SalaryDetailBLL objSalaryDetailBLL = new SalaryDetailBLL();

                objSalaryDetailBLL.SaveSalaryDetail(objSalaryDetail);

            }

            return RedirectToAction("Index", "SalaryDetail");
        }


        public JsonResult DesignationAndTotalSalaryById(int? EmployeeId)
        {
            
            SalaryDetailBLL objSalaryDetailBLL = new SalaryDetailBLL();
            if(EmployeeId !=null)
            {
                SalaryDetail salaryDetail = objSalaryDetailBLL.GetDesignationAndTotalSalaryById(EmployeeId);
                return Json(salaryDetail, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            SalaryDetailBLL objSalaryDetailBLL = new SalaryDetailBLL();
            var salaryDetail = objSalaryDetailBLL.GetSalaryDetailById(id);


             ViewBag.EmployeeInfo = objSalaryDetailBLL.GetEmployeeDetailName();

            return View(salaryDetail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SalaryDetail objSalaryDetail)
        {
            if (ModelState.IsValid)
            {
                SalaryDetailBLL objSalaryDetailBLL = new SalaryDetailBLL();

                objSalaryDetailBLL.UpdateSalaryDetail(objSalaryDetail);
            }
            return RedirectToAction("Index", "SalaryDetail");
        }

    }
}