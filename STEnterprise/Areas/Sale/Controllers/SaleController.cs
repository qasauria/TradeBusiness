using STEnterprise.Areas.Inventory.BLL;
using STEnterprise.Areas.Inventory.Models;
using STEnterprise.Areas.Purchase.BLL;
using STEnterprise.Areas.Purchase.Models;
using STEnterprise.Areas.Sale.BLL;
using STEnterprise.Areas.Sale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using STEnterprise.AuthData;

namespace STEnterprise.Areas.Sale.Controllers
{
    //created by shahadat
    [AuthenticationFilter]
    public class SaleController : Controller
    {
        // GET: Sale/Sale
        public ActionResult Index()
        {
            SaleBLL objSaleBLL = new SaleBLL();
            List<CustomerDetail> CustomerNameList = objSaleBLL.GetAllCustomerName();
            ViewBag.CustomerNameList = CustomerNameList;
            TruckDetailBLL objTruckDetailBLL = new TruckDetailBLL();
            List<TruckDetail> truckDetailList = objTruckDetailBLL.GetAllTruckDetail();
            ViewBag.truckList = truckDetailList;
            return View();
        }

        [HttpPost]
        public ActionResult Save(SaleModel objSales)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                SaleBLL objSaleBLL = new SaleBLL();

                SaleModel objSaleModel = new SaleModel();

                SaleDetail objSaleDetail = new SaleDetail();

                objSaleDetail.Sales.ConsignmentNumber = objSales.ConsignmentNumber;
                objSaleDetail.Sales.SellingDate = objSales.SellingDate;
                objSaleDetail.Sales.SellingAmount = objSales.SellingAmount;
                objSaleDetail.Sales.PaidAmount = objSales.PaidAmount;
                objSaleDetail.Sales.AdjustmentAmount = objSales.AdjustmentAmount;
                objSaleDetail.Sales.TotalAmount = objSales.TotalAmount;
                objSaleDetail.Sales.PaymentMethod = objSales.PaymentMethod;
                objSaleDetail.Sales.OrderNumber = objSales.OrderNumber;
                objSaleDetail.Sales.ChequeNumber = objSales.ChequeNumber;
                objSaleDetail.Sales.TTNUmber = objSales.TTNUmber;
                objSaleDetail.Sales.CustomerId = objSales.CustomerId;
                objSaleDetail.Sales.Remarks = objSales.Remarks;


                foreach (SaleDetail pd in objSales.SaleDetails)
                {

                    objSaleDetail.ProductId = pd.ProductId;
                    objSaleDetail.TruckNumber = pd.TruckNumber;
                    objSaleDetail.SalePrice = pd.SalePrice;
                    objSaleDetail.SaleUnitBag = pd.SaleUnitBag;
                    objSaleDetail.SaleUnitKG = pd.SaleUnitKG;

                    objSaleBLL.SaveSale(objSaleDetail);

                    objSaleDetail.Sales.CustomerId = 0;
                    status = true;
                }

            }
            else
            {
                status = false;
            }
            return new JsonResult { Data = new { status = status } };
        }

        public JsonResult GetProductByConsainmentNumberForSale(string consignmentNumber)
        {
            SaleBLL objSaleBLL = new SaleBLL();
            var product = objSaleBLL.GetProductByConsainmentNumberForSale(consignmentNumber);
            return Json(product, JsonRequestBehavior.AllowGet);

        }


        public JsonResult GetCustomerDueAmount(int id)
        {
            SaleBLL objSaleBLL = new SaleBLL();
            var data = objSaleBLL.GetCustomerDueAmount(id);

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetTruckNumberByConsainmentNumber(string consignmentNumber)
        {
            SaleBLL objSaleBLL = new SaleBLL();
            var truckNumber = objSaleBLL.GetTruckNumberByConsainmentNumber(consignmentNumber);
            return Json(truckNumber, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetCutomerNameByConsignment(string consignmentNumber)
        {
            SaleBLL objSaleBLL = new SaleBLL();
            var saleDetail = objSaleBLL.GetCutomerNameByConsignment(consignmentNumber);
            return Json(saleDetail, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTotalAmountByConsignment(string consignmentNumber)
        {
            SaleBLL objSaleBLL = new SaleBLL();
            var saleDetail = objSaleBLL.GetTotalAmountByConsignmentNumber(consignmentNumber);
            return Json(saleDetail, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUnitBagKgPriceByProdcutName(int productId, string consignmentNumber)
        {
            SaleBLL objSaleBLL = new SaleBLL();
            var productDetail = objSaleBLL.GetUnitBagKgPriceByProdcutName(productId, consignmentNumber);
            return Json(productDetail, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetProductNameByConsignment(string truck, string consignmentNumber)
        {
            SaleBLL objSaleBLL = new SaleBLL();
            var productName = objSaleBLL.GetProductNameByConsignment(truck, consignmentNumber);
            return Json(productName, JsonRequestBehavior.AllowGet);

        }



        // Start Return Sale Part //
        public ActionResult ReturnSale()
        {
            SaleBLL objSaleBLL = new SaleBLL();
            List<CustomerDetail> CustomerNameList = objSaleBLL.GetAllCustomerName();
            ViewBag.CustomerNameList = CustomerNameList;
            ProductBLL objProductBll = new ProductBLL();
            List<Product> ProductList = objProductBll.GetAllProduct();
            ViewBag.productList = ProductList;
            return View();
        }

        [HttpPost]
        public ActionResult SaveReturnSale(SaleModel objSales)
        {
            bool status = false;
            //if (ModelState.IsValid)
            //{
            SaleBLL objSaleBLL = new SaleBLL();

            SaleModel objSaleModel = new SaleModel();

            SaleDetail objSaleDetail = new SaleDetail();

            objSaleDetail.Sales.ConsignmentNumber = objSales.ConsignmentNumber;
            objSaleDetail.Sales.CustomerId = objSales.CustomerId;
            objSaleDetail.Sales.SellingDate = objSales.SellingDate;
            objSaleDetail.Sales.TotalReturnAmount = objSales.TotalReturnAmount;
            objSaleDetail.Sales.TotalAmount = objSales.TotalAmount;
            objSaleDetail.Sales.Remarks = objSales.Remarks;


            foreach (SaleDetail pd in objSales.SaleDetails)
            {
                objSaleDetail.ProductId = pd.ProductId;
                objSaleDetail.TruckNumber = pd.TruckNumber;
                objSaleDetail.ReturnSaleUnitBag = pd.ReturnSaleUnitBag;
                objSaleDetail.ReturnSaleUnitKG = pd.ReturnSaleUnitKG;
                objSaleDetail.SalePrice = pd.SalePrice;

                objSaleBLL.SaveReturnSale(objSaleDetail);

                objSaleDetail.Sales.CustomerId = 0;
                status = true;
            }

            //}
            //else
            //{
            //    status = false;
            //}
            return new JsonResult { Data = new { status = status } };
        }

        [HttpGet]
        public JsonResult AutoCompleteByConsignment(string term)
        {
            SaleBLL objSaleBLL = new SaleBLL();
            List<String> result = new List<String>();
            result = objSaleBLL.GetAllConsignmentNumber().Where(x => x.ConsignmentNumber.ToString().StartsWith(term)).Select(y => y.ConsignmentNumber.ToString()).Distinct().ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // End Return Sale Part //

        public ActionResult DemurageSale()
        {
            SaleBLL objSaleBLL = new SaleBLL();
            List<CustomerDetail> CustomerNameList = objSaleBLL.GetAllCustomerName();
            ViewBag.CustomerNameList = CustomerNameList;
            ProductBLL objProductBll = new ProductBLL();
            List<Product> ProductList = objProductBll.GetAllProduct();
            ViewBag.productList = ProductList;
            return View();
        }


        public ActionResult DemurrageSave(SaleModel objSales)
        {
            bool status = false;
            //if (ModelState.IsValid)
            //{
            SaleBLL objSaleBLL = new SaleBLL();

            SaleModel objSaleModel = new SaleModel();

            SaleDetail objSaleDetail = new SaleDetail();

            objSaleDetail.Sales.ConsignmentNumber = objSales.ConsignmentNumber;
            objSaleDetail.Sales.CustomerId = objSales.CustomerId;
            objSaleDetail.Sales.SellingDate = objSales.SellingDate;
            objSaleDetail.Sales.TotalReturnAmount = objSales.TotalReturnAmount;
            objSaleDetail.Sales.TotalAmount = objSales.TotalAmount;
            objSaleDetail.Sales.Remarks = objSales.Remarks;


            foreach (SaleDetail pd in objSales.SaleDetails)
            {
                objSaleDetail.ProductId = pd.ProductId;
                objSaleDetail.TruckNumber = pd.TruckNumber;
                objSaleDetail.ReturnSaleUnitBag = pd.ReturnSaleUnitBag;
                objSaleDetail.ReturnSaleUnitKG = pd.ReturnSaleUnitKG;
                objSaleDetail.SalePrice = pd.SalePrice;

                objSaleBLL.DemurrageSave(objSaleDetail);

                objSaleDetail.Sales.CustomerId = 0;
                status = true;
            }

            //}
            //else
            //{
            //    status = false;
            //}
            return new JsonResult { Data = new { status = status } };
            //return RedirectToAction("Index", "Purchase");
        }
        public JsonResult StockHandforBag(int? saleUnitBag, int? saleUnitKg, string productName)
        {
            SaleBLL objSaleBLL = new SaleBLL();
            if (saleUnitBag > 0)
            {
                var data = objSaleBLL.GetTotalBag(saleUnitBag, saleUnitKg, productName);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else if (saleUnitKg > 0)
            {
                var data = objSaleBLL.GetTotalBag(saleUnitBag, saleUnitKg, productName);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return Json("", JsonRequestBehavior.AllowGet);

        }
    }
}