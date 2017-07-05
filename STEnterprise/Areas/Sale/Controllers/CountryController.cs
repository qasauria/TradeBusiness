using System.Collections.Generic;
using System.Web.Mvc;
using STEnterprise.Areas.Sale.BLL;
using STEnterprise.Areas.Sale.Models;
using STEnterprise.AuthData;

namespace STEnterprise.Areas.Sale.Controllers
{
    //Created By Shahadat
    [AuthenticationFilter]
    public class CountryController : Controller
    {
        // GET: Admin/Country
        public ActionResult Index()
        {
            CountryBLL objCountryBLL = new CountryBLL();
            List<Country>  CountryList = objCountryBLL.GetAllCountry();
            return View(CountryList);
        }
       

        [HttpGet]
        public ActionResult Save()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Save(Country objCountry)
        {
            if (ModelState.IsValid)
            {
                CountryBLL objMeasurementBLL = new CountryBLL();

                objMeasurementBLL.SaveCountry(objCountry);
            }
            return RedirectToAction("Index", "Country");
        }

        public JsonResult GetCountryNameIsExist(string countryName)
        {
            CountryBLL objCountryBLL = new CountryBLL();
            bool exsits = objCountryBLL.GetCountryNameIsExist(countryName);
            if (exsits) { return Json(false, JsonRequestBehavior.AllowGet); }
            else { return Json(true, JsonRequestBehavior.AllowGet); }
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            CountryBLL objCountryBLL = new CountryBLL();
            var country = objCountryBLL.GetAllCountryById(id);
            return View(country);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Country objCountry)
        {
            if (ModelState.IsValid)
            {
                CountryBLL objCountryBLL = new CountryBLL();

                objCountryBLL.UpdateCountry(objCountry);
            }
            return RedirectToAction("Index", "Country");
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            CountryBLL objCountryBLL = new CountryBLL();
            var country = objCountryBLL.GetCountry(id);
            return View(country);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteMeasurement(int id)
        {
            if (ModelState.IsValid)
            {
                CountryBLL objCountryBLL = new CountryBLL();
                objCountryBLL.DeleteCountry(id);
            }
            return RedirectToAction("Index", "Country");
        }
    }
}