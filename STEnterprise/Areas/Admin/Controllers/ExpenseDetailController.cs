using System.Collections.Generic;
using System.Web.Mvc;
using STEnterprise.Areas.Admin.BLL;
using STEnterprise.Areas.Admin.Models;
using STEnterprise.AuthData;

namespace STEnterprise.Areas.Admin.Controllers
{
    // created by shovon
    [AuthenticationFilter]
    public class ExpenseDetailController : Controller
    {
        // GET: Admin/ExpenseDetail
        public ActionResult Index()
        {

            ExpenseDetailBLL objExpenseDetailBLL = new ExpenseDetailBLL();
            List<ExpenseDetail> expenseTypeList = objExpenseDetailBLL.GetAllExpenseTypeInfo();
            return View(expenseTypeList);
        }


        [HttpGet]
        public ActionResult Save()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Save(ExpenseDetail objExpenseDetailInfo)
        {

            if (ModelState.IsValid)
            {
                ExpenseDetailBLL objExpenseDetailBLL = new ExpenseDetailBLL();

                objExpenseDetailBLL.SaveExpenseDetailInfo(objExpenseDetailInfo);

            }

            return RedirectToAction("Index", "ExpenseDetail");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ExpenseDetailBLL objExpenseDetailBLL = new ExpenseDetailBLL();
            var expenseDetailInfo = objExpenseDetailBLL.GetExpenseDetailInfo(id);
            return View(expenseDetailInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Edit(ExpenseDetail objExpenseDetailInfo)
        {

            if (ModelState.IsValid)
            {
                ExpenseDetailBLL objExpenseDetailBLL = new ExpenseDetailBLL();

                objExpenseDetailBLL.UpdateExpenseDetailInfo(objExpenseDetailInfo);

            }
            return RedirectToAction("Index", "ExpenseDetail");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            ExpenseDetailBLL objExpenseDetailBLL = new ExpenseDetailBLL();
            var expenseDetailInfo = objExpenseDetailBLL.GetExpenseDetailInfo(id);
            return View(expenseDetailInfo);
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        [ActionName("Delete")]
        public ActionResult DeleteExpenseDetailInfo(int id)
        {

            if (ModelState.IsValid)
            {
                ExpenseDetailBLL objExpenseDetailBLL = new ExpenseDetailBLL();
                objExpenseDetailBLL.DeleteExpenseDetailInfo(id);
            }
            return RedirectToAction("Index", "ExpenseDetail");
        }

        public JsonResult GetExpenseNameIsExist(string ExpenseName)
        {
            ExpenseDetailBLL objExpenseDetailBLL = new ExpenseDetailBLL();
            bool exsits = objExpenseDetailBLL.GetExpenseNameIsExist(ExpenseName);
            if (exsits) { return Json(false, JsonRequestBehavior.AllowGet); }
            else { return Json(true, JsonRequestBehavior.AllowGet); }
        }
    }
}