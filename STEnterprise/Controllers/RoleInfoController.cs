using System.Web.Mvc;
using STEnterprise.AuthData;
using STEnterprise.BLL;
using STEnterprise.Models;

namespace STEnterprise.Controllers
{
    //created by ataur
    [AuthenticationFilter]
    public class RoleInfoController : Controller
    {
        public ActionResult Index()
        {
            RoleManagementBLL objRoleManagementBLL = new RoleManagementBLL();
            var roleInfoList = objRoleManagementBLL.GetAllRoleInfo();
            return View(roleInfoList);
        }


        [HttpGet]
        public ActionResult Save()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Save(RoleInfo objRoleInfo)
        {
           if (ModelState.IsValid)
            {
                RoleManagementBLL objRoleManagementBLL = new RoleManagementBLL();

               objRoleManagementBLL.CreateRole(objRoleInfo);
            }
            return RedirectToAction("Index", "RoleInfo");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            RoleManagementBLL objRoleManagementBLL = new RoleManagementBLL();
            var roleInfo = objRoleManagementBLL.GetRoleInfo(id);
            return View(roleInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Edit(RoleInfo objRoleInfo)
        {
            if (ModelState.IsValid)
            {
                RoleManagementBLL objRoleManagementBLL = new RoleManagementBLL();

                objRoleManagementBLL.UpdateRoleInfo(objRoleInfo);
            }
            return RedirectToAction("Index","RoleInfo");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            RoleManagementBLL objRoleManagementBLL = new RoleManagementBLL();
            var roleInfo = objRoleManagementBLL.GetRoleInfo(id);
            return View(roleInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [ActionName("Delete")]
        public ActionResult DeleteRoleInfo(int id)
        {
            if (ModelState.IsValid)
            {
                RoleManagementBLL objRoleManagementBLL = new RoleManagementBLL();
                objRoleManagementBLL.DeleteRoleInfo(id);
            }
            return RedirectToAction("Index","RoleInfo");
        }
    }
}