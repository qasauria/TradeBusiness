using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using STEnterprise.Areas.Admin.BLL;
using STEnterprise.Areas.Admin.Models;
using STEnterprise.AuthData;

namespace STEnterprise.Areas.Admin.Controllers
{
    //created by ataur
    [AuthenticationFilter]
    public class CompanyDetailController : Controller
    {

        public ActionResult Index()
        {
            CompanyDetailBLL objCompanyDetailBll = new CompanyDetailBLL();
            List<CompanyDetail> CompanyList = objCompanyDetailBll.GetCompanyInfo();
            return View(CompanyList);
        }


        [HttpGet]
        public ActionResult Save()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Save(CompanyDetail objCompanyDetail)
        {

            if (ModelState.IsValid)
            {
                CompanyDetailBLL objCompanyDetailBll = new CompanyDetailBLL();
                objCompanyDetailBll.SaveCompanyInfo(objCompanyDetail);

            }

            return RedirectToAction("Index", "CompanyDetail");
        }
        //public ActionResult Save(CompanyDetail objCompanyDetail, HttpPostedFileBase File)
        //{
        //    bool status = true;

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            string filename = "";
        //            byte[] bytes;
        //            int BytestoRead;
        //            int numBytesRead;
        //            if (File != null)
        //            {
        //                filename = Path.GetFileName(File.FileName);
        //                bytes = new byte[File.ContentLength];
        //                BytestoRead = (int)File.ContentLength;
        //                numBytesRead = 0;
        //                while (BytestoRead > 0)
        //                {
        //                    int n = File.InputStream.Read(bytes, numBytesRead, BytestoRead);
        //                    if (n == 0) break;
        //                    numBytesRead += n;
        //                    BytestoRead -= n;
        //                }
        //                objCompanyDetail.Logo = bytes;
        //                CompanyDetailBLL objCompanyDetailBll=new CompanyDetailBLL();
        //                objCompanyDetailBll.SaveCompanyInfo(objCompanyDetail);
        //            }
        //            else
        //            {
        //                status = false;
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            status = false;
        //        }
        //    }
        //    else
        //    {
        //        status = false;
        //    }
        //    if (status.Equals(true))
        //    {
        //        return RedirectToAction("Index","CompanyDetail");
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "CompanyDetail");
        //    }
        //}

        [HttpGet]
        public ActionResult Edit(int id)
        {
            CompanyDetailBLL objCompanyDetailBll=new CompanyDetailBLL();
            var company = objCompanyDetailBll.GetCompanyInfo(id);
            return View(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CompanyDetail objCompanyDetail)
        {
            CompanyDetailBLL objCompanyDetailBll=new CompanyDetailBLL();
            objCompanyDetailBll.UpdateCompanyDetail(objCompanyDetail);
            return RedirectToAction("Index", "CompanyDetail");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            CompanyDetailBLL objCompanyDetailBll = new CompanyDetailBLL();
            var company = objCompanyDetailBll.GetCompanyInfo(id);
            return View(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteCompany(int id)
        {
            CompanyDetailBLL objCompanyDetailBll = new CompanyDetailBLL();
            objCompanyDetailBll.DeleteCompanyDetail(id);
            return RedirectToAction("Index", "CompanyDetail");
        }

    }
}