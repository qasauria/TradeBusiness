using System.Collections.Generic;
using System.Web.Mvc;
using STEnterprise.Areas.Inventory.BLL;
using STEnterprise.Areas.Inventory.Models;
using STEnterprise.AuthData;

namespace STEnterprise.Areas.Inventory.Controllers
{
    //Created By Shahadat
    [AuthenticationFilter]
    public class MeasurementController : Controller
    {
        // GET: Admin/Measurement
        public ActionResult Index()
        {
            MeasurementBLL objMeasurementBLL = new MeasurementBLL();
            List<Measurement> MeasurementList = objMeasurementBLL.GetAllMeasurement();
            return View(MeasurementList);
        }
       

        [HttpGet]
        public ActionResult Save()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Save(Measurement objMeasurement)
        {
            if (ModelState.IsValid)
            {
                MeasurementBLL objMeasurementBLL = new MeasurementBLL();

                objMeasurementBLL.SaveMeasurement(objMeasurement);
            }
            return RedirectToAction("Index","Measurement");
        }

        public JsonResult GetMeasurementNameIsExist(string measurementName)
         {
            MeasurementBLL objMeasurementBLL = new MeasurementBLL();
            bool exsits = objMeasurementBLL.GetMeasurementNameIsExist(measurementName);
            if (exsits) { return Json(false, JsonRequestBehavior.AllowGet); }
            else { return Json(true, JsonRequestBehavior.AllowGet); }
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            MeasurementBLL objMeasurementBLL = new MeasurementBLL();
            var measurement = objMeasurementBLL.GetAllMeasurementById(id);            
            return View(measurement);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Measurement objMeasurement)
        {           
            if (ModelState.IsValid)
            {
                MeasurementBLL objMeasurementBLL = new MeasurementBLL();

                objMeasurementBLL.UpdateMeasurement(objMeasurement);
            }
            return RedirectToAction("Index", "Measurement");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            MeasurementBLL objMeasurementBLL = new MeasurementBLL();
            var measurement = objMeasurementBLL.GetMeasurement(id);
            return View(measurement);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteMeasurement(int id)
        {
            if (ModelState.IsValid)
            {
                MeasurementBLL objMeasurementBLL = new MeasurementBLL();
                objMeasurementBLL.DeleteMeasurement(id);
            }
            return RedirectToAction("Index", "Measurement");
        }

    }
}