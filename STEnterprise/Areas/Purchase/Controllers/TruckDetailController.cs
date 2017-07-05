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
    public class TruckDetailController : Controller
    {
        // GET: Purchase/TruckDetail
        public ActionResult Index()
        {
            TruckDetailBLL objTruckDetailBLL = new TruckDetailBLL();
            List<TruckDetail> truckDetailList = objTruckDetailBLL.GetAllTruckDetail();
            return View(truckDetailList);
        }
        

        [HttpGet]
        public ActionResult Save()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Save(TruckDetail objTruckDetail)
        {
            if (ModelState.IsValid)
            {
                TruckDetailBLL objTruckDetailBLL = new TruckDetailBLL();

                objTruckDetailBLL.SaveTruckDetail(objTruckDetail);
            }
            return RedirectToAction("Index", "TruckDetail");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            TruckDetailBLL objTruckDetailBLL = new TruckDetailBLL();

            var truckInfo = objTruckDetailBLL.GetAllTruckDetail().Where(a => a.TruckDetailId == id).FirstOrDefault();
            return View(truckInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Edit(TruckDetail objTruckDetail)
        {

            if (ModelState.IsValid)
            {
                TruckDetailBLL objTruckDetailBLL = new TruckDetailBLL();

                objTruckDetailBLL.UpdateTruckDetail(objTruckDetail);

            }
            return RedirectToAction("Index", "TruckDetail");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            TruckDetailBLL objTruckDetailBLL = new TruckDetailBLL();

            var truckInfo = objTruckDetailBLL.GetAllTruckDetail().Where(a => a.TruckDetailId == id).FirstOrDefault();
            return View(truckInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [ActionName("Delete")]
        public ActionResult DeleteTruckDetail(int id)
        {

            if (ModelState.IsValid)
            {
                TruckDetailBLL objTruckDetailBLL = new TruckDetailBLL();

                objTruckDetailBLL.DeleteTruckDetail(id);

            }
            return RedirectToAction("Index", "TruckDetail");
        }
    }
}