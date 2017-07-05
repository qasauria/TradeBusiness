using System.Collections.Generic;
using System.Web.Mvc;
using STEnterprise.Areas.Inventory.BLL;
using STEnterprise.Areas.Inventory.Models;
using STEnterprise.AuthData;

namespace STEnterprise.Areas.Inventory.Controllers
{
    //created by ataur
    [AuthenticationFilter]
    public class ProductController : Controller
    {
        public ActionResult Index()
        {
            ProductBLL objProductBll = new ProductBLL();
            List<Product> productList = objProductBll.GetAllProduct();
            return View(productList);
        }


        [HttpGet]
        public ActionResult Save()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Product objProduct)
        {
            if (ModelState.IsValid)
            {
                ProductBLL objProductBll = new ProductBLL();
                objProductBll.CreateProduct(objProduct);
            }
            return RedirectToAction("Index", "Product");
        }

        public JsonResult GetProductNameIsExist(string productName)
        {
            ProductBLL objProductBll = new ProductBLL();
            bool exsits = objProductBll.GetProductNameIsExist(productName);
            if (exsits) { return Json(false, JsonRequestBehavior.AllowGet); }
            else { return Json(true, JsonRequestBehavior.AllowGet); }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ProductBLL objProductBll = new ProductBLL();
            var product = objProductBll.GetProduct(id);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product objProduct)
        {
            if (ModelState.IsValid)
            {
                ProductBLL objProductBll = new ProductBLL();
                objProductBll.UpdateProduct(objProduct);
            }
            return RedirectToAction("Index", "Product");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            ProductBLL objProductBll = new ProductBLL();
            var product = objProductBll.GetProduct(id);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteProduct(int id)
        {
            if (ModelState.IsValid)
            {
                ProductBLL objProductBll = new ProductBLL();
                objProductBll.DeleteProduct(id);
            }
            return RedirectToAction("Index", "Product");
        }

    }
}