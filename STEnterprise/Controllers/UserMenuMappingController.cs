using System.Collections.Generic;
using System.Web.Mvc;
using STEnterprise.BLL;

namespace STEnterprise.Controllers
{
    public class UserMenuMappingController : Controller
    {
        // GET: UserMenuMapping
        [HttpGet]
        public ActionResult Save(int userDetailId = 0, int roleId = 0)
        {
            UserDetailBLL objUserDetailBll = new UserDetailBLL();
            ViewBag.UserInfo = objUserDetailBll.GetAllUserInfo();

            RoleManagementBLL objRoleManagementBll = new RoleManagementBLL();
            ViewBag.RoleInfo = objRoleManagementBll.GetRoleListForDropDownForSave();

            NavigationBLL objNavigationBll = new NavigationBLL();
            var menu = objNavigationBll.GetAllMenuForUserMenuMapping(userDetailId, roleId);

            return View(menu);
        }

        [HttpPost]
        public JsonResult Save(IEnumerable<int> menuId, int roleId, int userId)
        {
            bool status = false;

            RoleManagementBLL objRoleManagementBll = new RoleManagementBLL();
            if (ModelState.IsValid)
            {
                objRoleManagementBll.ResetUserMenuMapping(roleId, userId);
                if (menuId != null)
                {
                    foreach (int item in menuId)
                    {
                        objRoleManagementBll.CreateUserMenuMapping(item, roleId, userId);
                    }
                }
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}