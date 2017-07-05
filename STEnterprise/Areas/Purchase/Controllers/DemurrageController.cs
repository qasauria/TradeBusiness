using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using STEnterprise.Areas.Purchase.BLL;
using STEnterprise.Areas.Purchase.Models;
using STEnterprise.AuthData;

namespace STEnterprise.Areas.Purchase.Controllers
{
    [AuthenticationFilter]
    public class DemurrageController : Controller
    {
        // GET: Purchase/Demurrage
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Save(PurchaseModel objPurchase)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                PurchaseBLL objPurchaseBLL = new PurchaseBLL();

                PurchaseModel objPurchaseModel = new PurchaseModel();

                PurchaseDetail objPurchaseDetail = new PurchaseDetail();

                objPurchaseDetail.Purchase.ConsignmentNumber = objPurchase.ConsignmentNumber;               
                objPurchaseDetail.Purchase.SupplierId = objPurchase.SupplierId;
                objPurchaseDetail.Purchase.PurchaseDate = objPurchase.PurchaseDate;
                objPurchaseDetail.Purchase.ReturnAmount = objPurchase.ReturnAmount;               
                objPurchaseDetail.Purchase.Remarks = objPurchase.Remarks;


                foreach (PurchaseDetail pd in objPurchase.PurchaseDetails)
                {
                    objPurchaseDetail.ProductId = pd.ProductId;
                    objPurchaseDetail.TruckNumber = pd.TruckNumber;
                    objPurchaseDetail.ReturnPurchaseUnitBag = pd.ReturnPurchaseUnitBag;
                    objPurchaseDetail.ReturnPurchaseUnitKG = pd.ReturnPurchaseUnitKG;
                    objPurchaseDetail.PurchasePrice = pd.PurchasePrice;

                    objPurchaseBLL.SavePurchaseDemurrage(objPurchaseDetail);

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