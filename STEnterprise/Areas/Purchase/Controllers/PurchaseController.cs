using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web.Mvc;
using STEnterprise.Areas.Inventory.BLL;
using STEnterprise.Areas.Inventory.Models;
using STEnterprise.Areas.Purchase.BLL;
using STEnterprise.Areas.Purchase.Models;


namespace STEnterprise.Areas.Purchase.Controllers
{
    //ataur
    public class PurchaseController : Controller
    {

        // GET: Purchase/Purchase
        public ActionResult Index()
        {
            SupplierBLL objSupplierBLL = new SupplierBLL();
            List<Supplier> SupplierList = objSupplierBLL.GetAllSupplier();
            ViewBag.supplierList = SupplierList;
            ProductBLL objProductBll = new ProductBLL();
            List<Product> ProductList = objProductBll.GetAllProduct();
            ViewBag.productList = ProductList;
            return View();
        }

        [HttpPost]
        public ActionResult Save(PurchaseModel objPurchase)
        {
            bool status = false;
            //if (ModelState.IsValid)
            //{
                PurchaseBLL objPurchaseBll = new PurchaseBLL();

                PurchaseDetail objPurchaseDetail = new PurchaseDetail();

                objPurchaseDetail.Purchase.ConsignmentNumber = objPurchase.ConsignmentNumber;
                objPurchaseDetail.Purchase.PurchaseDate = objPurchase.PurchaseDate;
                objPurchaseDetail.Purchase.PurchaseAmount = objPurchase.PurchaseAmount;
                objPurchaseDetail.Purchase.PaidAmount = objPurchase.PaidAmount;
                objPurchaseDetail.Purchase.AdjustmentAmount = objPurchase.AdjustmentAmount;
                objPurchaseDetail.Purchase.TotalAmount = objPurchase.TotalAmount;
                objPurchaseDetail.Purchase.SupplierId = objPurchase.SupplierId;
                objPurchaseDetail.Purchase.PaymentMethod = objPurchase.PaymentMethod;
                objPurchaseDetail.Purchase.OrderNumber = objPurchase.OrderNumber;
                objPurchaseDetail.Purchase.ChequeNumber = objPurchase.ChequeNumber;
                objPurchaseDetail.Purchase.TTNumber = objPurchase.TTNumber;
                objPurchaseDetail.Purchase.Remarks = objPurchase.Remarks;


                foreach (PurchaseDetail pd in objPurchase.PurchaseDetails)
                {                   
                    objPurchaseDetail.ProductId = pd.ProductId;
                    objPurchaseDetail.PurchasePrice = pd.PurchasePrice;
                    objPurchaseDetail.PurchaseUnitBag = pd.PurchaseUnitBag;
                    objPurchaseDetail.PurchaseUnitKg = pd.PurchaseUnitKg;
                    objPurchaseDetail.TruckNumber = pd.TruckNumber;
                    objPurchaseDetail.TruckFare = pd.TruckFare;
                    objPurchaseDetail.AdvancePayment = pd.AdvancePayment;
                    objPurchaseDetail.LoadingCost = pd.LoadingCost;
                    objPurchaseDetail.UnloadingCost = pd.UnloadingCost;
                    objPurchaseDetail.OtherCost = pd.OtherCost;

                    objPurchaseBll.SavePurchase(objPurchaseDetail);

                    objPurchaseDetail.Purchase.SupplierId = 0;
                    status = true;
                }

            ////}
            //else
            //{
            //    status = false;
            //}

            return new JsonResult { Data = new { status = status } };
            //return RedirectToAction("Index", "Purchase");
        }

        [HttpGet]
        public JsonResult AutoCompleteConsignment(string term)
        {
            TruckDetailBLL objTruckDetailBLL = new TruckDetailBLL();
            List<String> result = new List<String>();
            result = objTruckDetailBLL.GetAllTruckDetail().Where(x => x.ConsignmentNumber.ToString().StartsWith(term)).Select(y => y.ConsignmentNumber.ToString()).Distinct().ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTruckByConsignment(string consignmentNumber)
        {
            TruckDetailBLL objTruckDetailBLL = new TruckDetailBLL();
            var truck =
                objTruckDetailBLL.GetAllTruckDetail()
                    .Where(x => x.ConsignmentNumber == consignmentNumber)
                    .ToList();
            return Json(truck, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetSupplierDueAmount(int id)
        {
            PurchaseBLL objPurchaseBLL = new PurchaseBLL();
            var data = objPurchaseBLL.GetSupplierDueAmount(id);
            
            return Json(data, JsonRequestBehavior.AllowGet);
            
        }

    }
}