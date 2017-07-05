using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using STEnterprise.Areas.Purchase.BLL;
using STEnterprise.Areas.Purchase.Models;
using STEnterprise.Areas.Sale.BLL;
using STEnterprise.Areas.Sale.Models;
using STEnterprise.AuthData;

namespace STEnterprise.Areas.Purchase.Controllers
{
    [AuthenticationFilter]
    public class ReturnPurchaseController : Controller
    {
        // GET: Purchase/ReturnPurchase
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult GetPurchaseDetilByConsignment(string consignmentNumber)
        {
            PurchaseBLL objPurchaseBLL = new PurchaseBLL();
            var purchaseDetail = objPurchaseBLL.GetPurchaseDetail(consignmentNumber);
            return Json(purchaseDetail, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSuppliersByConsignment(string consignment)
        {
            PurchaseBLL objPurchaseBLL = new PurchaseBLL();
            var purchaseDetail = objPurchaseBLL.GetSuppliersByConsignment(consignment);
            return Json(purchaseDetail, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTotalAmountByConsignment(string consignmentNumber)
        {
            PurchaseBLL objPurchaseBLL = new PurchaseBLL();
            var data = objPurchaseBLL.GetTotalAmountByConsignment(consignmentNumber);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUnitBagKgPriceByProdcutName(int productId, string consignmentNumber)
        {
            PurchaseBLL objPurchaseBLL = new PurchaseBLL();
            var productDetail = objPurchaseBLL.GetUnitBagKgPriceByProdcutName(productId, consignmentNumber);
            //var productDetail = objPurchaseBLL.GetAllPurchaseInfo().Where(x => x.ConsignmentNumber == consignmentNumber && x.ProductId == productId);
            return Json(productDetail, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetProductNameByConsignment(string consignmentNumber,string truck)
        {
            PurchaseBLL objPurchaseBLL = new PurchaseBLL();
            var data = objPurchaseBLL.GetProductNameByConsignment(consignmentNumber,truck);
            return Json(data, JsonRequestBehavior.AllowGet); 

        }

        [HttpPost]
        public ActionResult SaveReturnPurchase(PurchaseModel objPurchase)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                PurchaseBLL objPurchaseBLL = new PurchaseBLL();

                PurchaseModel objPurchaseModel = new PurchaseModel();

                PurchaseDetail objPurchaseDetail = new PurchaseDetail();

                objPurchaseDetail.Purchase.ConsignmentNumber = objPurchase.ConsignmentNumber;
                //objPurchaseDetail.Purchase.TruckNumber = objPurchase.TruckNumber;
                objPurchaseDetail.Purchase.SupplierId = objPurchase.SupplierId;
                objPurchaseDetail.Purchase.PurchaseDate = objPurchase.PurchaseDate;
                objPurchaseDetail.Purchase.ReturnAmount = objPurchase.ReturnAmount;
                //objPurchaseDetail.Purchase.ProductId = objPurchase.ProductId;
                objPurchaseDetail.Purchase.Remarks = objPurchase.Remarks;


                foreach (PurchaseDetail pd in objPurchase.PurchaseDetails)
                {
                    objPurchaseDetail.ProductId = pd.ProductId;
                    objPurchaseDetail.TruckNumber = pd.TruckNumber;
                    objPurchaseDetail.ReturnPurchaseUnitBag = pd.ReturnPurchaseUnitBag;
                    objPurchaseDetail.ReturnPurchaseUnitKG = pd.ReturnPurchaseUnitKG;
                    objPurchaseDetail.PurchasePrice = pd.PurchasePrice;
                    objPurchaseBLL.SaveReturnPurchase(objPurchaseDetail);

                    objPurchaseDetail.Purchase.SupplierId = 0;
                    status = true;
                }

            }
            else
            {
                status = false;
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}