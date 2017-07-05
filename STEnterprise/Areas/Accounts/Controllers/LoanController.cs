using STEnterprise.Areas.Accounts.BLL;
using STEnterprise.Areas.Accounts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using STEnterprise.AuthData;

namespace STEnterprise.Areas.Accounts.Controllers
{
    [AuthenticationFilter]
    public class LoanController : Controller
    {
        // GET: Accounts/Loan
        public ActionResult LoanEntry()
        {
            LoanBL objLoanBL = new LoanBL();
            var model = new Loan
            {
                BankNameList = objLoanBL.GetBankNameforLoan()
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult LoanEntry(Loan objLoan)
        {
            LoanBL objLoanBL = new LoanBL();
            try
            {
                if (ModelState.IsValid)
                {

                    int IsRowAffected = objLoanBL.CreateLoan(objLoan);
                    if (IsRowAffected > 0)
                    {
                        ViewBag.Message = "Save Succesfully";
                    }
                    else
                    {
                        ViewBag.Message = "Save Failed";
                    }

                }

            }
            catch (Exception ex)
            {
                ViewBag.error = ("Error : " + ex.Message);
            }
            var model = new Loan
            {
                BankNameList = objLoanBL.GetBankNameforLoan()
            };
            return View(model);
        }
        public ActionResult LoanPaid()
        {
            LoanBL objLoanBL = new LoanBL();
            List<Loan> bankName = objLoanBL.GetBankNameforLoan();
            ViewBag.bankName = bankName;
            return View();
        }
        [HttpPost]
        public ActionResult LoanPaid(Loan objLoan)
        {
            try
            {
                LoanBL objLoanBL = new LoanBL();

                int IsRowAffected = objLoanBL.PaidLoan(objLoan);
                if (IsRowAffected > 0)
                {
                    ViewBag.Message = "Save Succesfully";
                }
                else
                {
                    ViewBag.Message = "Save Failed";
                }
                List<Loan> bankName = objLoanBL.GetBankNameforLoan();
                ViewBag.bankName = bankName;
            }
            catch (Exception ex)
            {
                ViewBag.error = ("Error : " + ex.Message);
            }
            return View();
        }

        public JsonResult AutoCompleteTruncationNumber(string term)
        {
            LoanBL objLoanBL = new LoanBL();
            List<String> result = new List<String>();
            result = objLoanBL.GetAllTruncationNumber().Where(x => x.TransactionNumber.ToString().StartsWith(term)).Select(y => y.TransactionNumber.ToString()).Distinct().ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBankNameAndTotalAmountByTruncationNumber(string TruncationNumber)
        {
            LoanBL objLoanBL = new LoanBL();
            var data = objLoanBL.GetBankNameAndTotalAmountByTruncationNumber(TruncationNumber);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBankNameByTruncationNumber(string TruncationNumber)
        {
            LoanBL objLoanBL = new LoanBL();
            var data = objLoanBL.GetBankNameByTruncationNumber(TruncationNumber);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}