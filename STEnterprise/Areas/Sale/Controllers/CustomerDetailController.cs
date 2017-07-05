using System;
using System.Collections.Generic;
using System.Web.Mvc;
using STEnterprise.Areas.Sale.BLL;
using STEnterprise.Areas.Sale.Models;
using STEnterprise.AuthData;

namespace STEnterprise.Areas.Sale.Controllers
{
    // created by shovon
    [AuthenticationFilter]
    public class CustomerDetailController : Controller
    {
        public ActionResult Index()
        {
            CustomerDetailBLL objCustomerBLL = new CustomerDetailBLL();
            List<CustomerDetail> customerList = objCustomerBLL.GetAllCustomerInfo();
            return View(customerList);
        }


        [HttpGet]
        public ActionResult Save()
        {
            CustomerDetailBLL objCustomerBLL = new CustomerDetailBLL();
            var model = new CustomerDetail
            {
                CountryInfo = objCustomerBLL.GetCountryName()
            };
                return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Save(CustomerDetail objCustomerInfo)
        {

            objCustomerInfo.CountryId = Convert.ToInt16(Request.Form["CountryId"]);

            if (ModelState.IsValid)
            {
                CustomerDetailBLL objCustomerBLL = new CustomerDetailBLL();

                objCustomerBLL.SaveCustomerInfo(objCustomerInfo);

            }

            return RedirectToAction("Index", "CustomerDetail");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            CustomerDetailBLL objCustomerBLL = new CustomerDetailBLL();
            var customerInfo = objCustomerBLL.GetCustomerInfo(id);
            ViewBag.CountryInfo = objCustomerBLL.GetCountryName();
            return View(customerInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Edit(CustomerDetail objCustomerInfo)
        {

            if (ModelState.IsValid)
            {
                CustomerDetailBLL objCustomerBLL = new CustomerDetailBLL();

                objCustomerBLL.UpdateCustomerInfo(objCustomerInfo);

            }
            return RedirectToAction("Index", "CustomerDetail");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            CustomerDetailBLL objCustomerBLL = new CustomerDetailBLL();
            var customerInfo = objCustomerBLL.GetCustomerInfo(id);
            ViewBag.CountryInfo = objCustomerBLL.GetCountryName();
            return View(customerInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [ActionName("Delete")]
        public ActionResult DeleteCustomerInfo(int id)
        {

            if (ModelState.IsValid)
            {
                CustomerDetailBLL objCustomerBLL = new CustomerDetailBLL();
                objCustomerBLL.DeleteCustomerInfo(id);
            }
            return RedirectToAction("Index", "CustomerDetail");
        }
    }
}