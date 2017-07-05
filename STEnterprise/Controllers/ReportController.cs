using System;
using System.Collections.Generic;
using System.Web.Mvc;
using STEnterprise.Areas.Inventory.BLL;
using STEnterprise.Areas.Inventory.Models;
using STEnterprise.Areas.Purchase.BLL;
using STEnterprise.Areas.Purchase.Models;
using STEnterprise.Areas.Sale.BLL;
using STEnterprise.Areas.Sale.Models;
using STEnterprise.AuthData;
using STEnterprise.CommonObject;
using STEnterprise.Models;

namespace STEnterprise.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        [AuthenticationFilter]
        public ActionResult Index()
        {
            SupplierBLL objSupplierBLL = new SupplierBLL();
            List<Supplier> SupplierList = objSupplierBLL.GetAllSupplier();
            ViewBag.supplierList = SupplierList;
            ProductBLL objProductBll = new ProductBLL();
            List<Product> ProductList = objProductBll.GetAllProduct();
            ViewBag.productList = ProductList;
            CustomerDetailBLL objCustomerBLL = new CustomerDetailBLL();
            ViewBag.customerList = objCustomerBLL.GetAllCustomerInfo();
            CountryBLL objCountryBLL = new CountryBLL();
            ViewBag.countryList = objCountryBLL.GetAllCountry();

            return View();
        }

        [HttpPost]
        public void Index(ReportModel objReportModel)
        {
            if (ModelState.IsValid)
            {
                
                ReportObject.ReportType = objReportModel.ReportType;
                ReportObject.ConsignmentNumber = objReportModel.ConsignmentNumber;
                ReportObject.FromDate = Convert.ToString(objReportModel.FromDate);
                ReportObject.ToDate = Convert.ToString(objReportModel.ToDate);
                ReportObject.SupplierId = Convert.ToInt32(objReportModel.SupplierId);
                ReportObject.CustomerId = Convert.ToInt32(objReportModel.CustomerId);
                ReportObject.CountryId = Convert.ToInt32(objReportModel.CountryId);
                ReportObject.TruckDetailId = Convert.ToInt32(objReportModel.TruckDetailId);
                ReportObject.ProductId = Convert.ToInt32(objReportModel.ProductId);

                Response.Redirect("~/ReportViewer/ReportsViewer.aspx");
            }
            //return View();
        }
    }
}