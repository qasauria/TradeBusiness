using System;
using System.Web.Mvc;
using STEnterprise.AuthData;
using STEnterprise.BLL;
using STEnterprise.Models;

namespace STEnterprise.Controllers
{
    [AuthenticationFilter]
    public class AssignUserRoleController : Controller
    {
        // GET: Admin/AssignUserRole
        public ActionResult Index()
        {
            RoleManagementBLL objRoleManagementBll = new RoleManagementBLL();

            var list = objRoleManagementBll.GetUserRole();
            return View(list);
        }

     

        [HttpGet]
        public ActionResult Get()
        {
            RoleManagementBLL objRoleManagementBll = new RoleManagementBLL();
            var model = new AssignUserRole
            {
                userDetail = objRoleManagementBll.GetUserListForDropDownForSave(),
                roleInfo = objRoleManagementBll.GetRoleListForDropDownForSave()
            };
            return View("UserPartial", model);
        }

        [HttpGet]
        public ActionResult Save()
        {
            //RoleManagementBLL objRoleManagementBll = new RoleManagementBLL();
            //var model = new AssignUserRole
            //{
            //    userDetail = objRoleManagementBll.GetUserListForDropDownForSave(),
            //    roleInfo = objRoleManagementBll.GetRoleListForDropDownForSave()
            //};
            //return View(model);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(AssignUserRole objAssignUserRole)
        {
            if (ModelState.IsValid)
            {
                RoleManagementBLL objRoleManagementBll = new RoleManagementBLL();
                objRoleManagementBll.AssignUserRole(objAssignUserRole);
            }
            return RedirectToAction("Index", "AssignUserRole");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            RoleManagementBLL objRoleManagementBll = new RoleManagementBLL();

            var model = new AssignUserRole
            {
                userDetail = objRoleManagementBll.GetUserListForDropDownForSave(),
                roleInfo = objRoleManagementBll.GetRoleListForDropDownForSave()
            };

            var assignRoleInfo = objRoleManagementBll.GetUserListForDropDownForEdit(id);
            Session["UrmId"] = assignRoleInfo.UrmId;
            ViewBag.roleId = assignRoleInfo.RoleId;
            ViewBag.UserDetailId = assignRoleInfo.UserDetailId;
            ViewBag.isActive = assignRoleInfo.IsActive;

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(AssignUserRole objAssignUserRole)
        {
            objAssignUserRole.UrmId = Convert.ToInt16(Session["UrmId"]);
            if (ModelState.IsValid)
            {
                RoleManagementBLL objRoleManagementBll = new RoleManagementBLL();
                objRoleManagementBll.UpdateUserRole(objAssignUserRole);

                Session.Remove("UrmId");
            }
            return RedirectToAction("Index", "AssignUserRole");
        }
    }
}