using System;
using System.Collections.Generic;
using System.Web.Mvc;
using STEnterprise.Areas.Admin.BLL;
using STEnterprise.Areas.Admin.Models;
using STEnterprise.AuthData;

namespace STEnterprise.Areas.Admin.Controllers
{
    [AuthenticationFilter]
    public class DailyExpenseController : Controller
    {
        // GET: Admin/DailyExpense
        public ActionResult Index()
        {
            DailyExpenseBLL objDailyExpenseBLL = new DailyExpenseBLL();
            List<DailyExpense> dailyExpenseList = objDailyExpenseBLL.GetAllDailyExpenseInfo();
            return View(dailyExpenseList);
        }

        [HttpGet]
        public ActionResult Save()
        {
            ExpenseDetailBLL objExpenseDetailBLL = new ExpenseDetailBLL();
            var model = new DailyExpense
            {
                ExpenseDetailInfo = objExpenseDetailBLL.GetAllExpenseTypeInfo()
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Save(DailyExpense objDailyExpense)
        {

            objDailyExpense.ExpenseDetailId = Convert.ToInt16(Request.Form["ExpenseDetailId"]);

            if (ModelState.IsValid)
            {
                DailyExpenseBLL objDailyExpenseBLL = new DailyExpenseBLL();

                objDailyExpenseBLL.SaveDailyExpenseInfo(objDailyExpense);

            }

            return RedirectToAction("Index", "DailyExpense");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            DailyExpenseBLL objDailyExpenseBLL = new DailyExpenseBLL();
            var dailyExpenseInfo = objDailyExpenseBLL.GetDailyExpenseInfo(id);

            ExpenseDetailBLL objExpenseDetailBLL = new ExpenseDetailBLL();
            ViewBag.ExpenseDetailInfo = objExpenseDetailBLL.GetAllExpenseTypeInfo(); 
            return View(dailyExpenseInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Edit(DailyExpense objDailyExpenseInfo)
        {

            if (ModelState.IsValid)
            {
                DailyExpenseBLL objDailyExpenseBLL = new DailyExpenseBLL();

                objDailyExpenseBLL.UpdateDailyExpenseInfo(objDailyExpenseInfo);

            }
            return RedirectToAction("Index", "DailyExpense");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            DailyExpenseBLL objDailyExpenseBLL = new DailyExpenseBLL();
            var dailyExpenseInfo = objDailyExpenseBLL.GetDailyExpenseInfo(id);

            ExpenseDetailBLL objExpenseDetailBLL = new ExpenseDetailBLL();
            ViewBag.ExpenseDetailInfo = objExpenseDetailBLL.GetAllExpenseTypeInfo();
            return View(dailyExpenseInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [ActionName("Delete")]
        public ActionResult DeleteDailyExpenseInfo(int id)
        {

            if (ModelState.IsValid)
            {
                DailyExpenseBLL objDailyExpenseBLL = new DailyExpenseBLL();
                objDailyExpenseBLL.DeleteDailyExpenseInfo(id);
            }
            return RedirectToAction("Index", "DailyExpense");
        }
    }
}