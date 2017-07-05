using System;
using System.Web.Mvc;
using STEnterprise.Areas.Employee.BLL;
using STEnterprise.Areas.Employee.Models;
using STEnterprise.AuthData;

namespace STEnterprise.Areas.Employee.Controllers
{
    [AuthenticationFilter]
    public class EmployeeDetailController : Controller
    {
        // GET: Admin/EmployeeDetail
        public ActionResult Index()
        {
            EmployeeDetailBLL objEmployeeBLL = new EmployeeDetailBLL();
            var employeeList = objEmployeeBLL.GetAllEmployeeInfo();
            return View(employeeList);
        }


        [HttpGet]
        public ActionResult Save()
        {
            EmployeeDetailBLL objEmployeeBLL = new EmployeeDetailBLL();
            var model = new EmployeeDetail
            {
                DesignationInfo = objEmployeeBLL.GetDesignationName()
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Save(EmployeeDetail objEmployeeInfo)
        {

            objEmployeeInfo.DesignationId = Convert.ToByte(Request.Form["DesignationId"]);

            if (ModelState.IsValid)
            {
                EmployeeDetailBLL objEmployeeBLL = new EmployeeDetailBLL();

                objEmployeeBLL.SaveEmployeeInfo(objEmployeeInfo);

            }

            return RedirectToAction("Index", "EmployeeDetail");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            EmployeeDetailBLL objEmployeeBLL = new EmployeeDetailBLL();
            var employeeInfo = objEmployeeBLL.GetEmployeeInfo(id);
            ViewBag.DesignationInfo = objEmployeeBLL.GetDesignationName();
            return View(employeeInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Edit(EmployeeDetail objEmployeeInfo)
        {

            if (ModelState.IsValid)
            {
                EmployeeDetailBLL objEmployeeBLL = new EmployeeDetailBLL();

                objEmployeeBLL.UpdateEmployeeInfo(objEmployeeInfo);

            }
            return RedirectToAction("Index", "EmployeeDetail");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            EmployeeDetailBLL objEmployeeBLL = new EmployeeDetailBLL();
            var employeeInfo = objEmployeeBLL.GetEmployeeInfo(id);
            ViewBag.DesignationInfo = objEmployeeBLL.GetDesignationName();
            return View(employeeInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [ActionName("Delete")]
        public ActionResult DeleteEmployeeInfo(int id)
        {

            if (ModelState.IsValid)
            {
                EmployeeDetailBLL objEmployeeBLL = new EmployeeDetailBLL();
                objEmployeeBLL.DeleteEmployeeInfo(id);
            }
            return RedirectToAction("Index", "EmployeeDetail");
        }
    }
}