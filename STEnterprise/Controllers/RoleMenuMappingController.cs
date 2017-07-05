using System.Collections.Generic;
using System.Web.Mvc;
using STEnterprise.AuthData;
using STEnterprise.BLL;

namespace STEnterprise.Controllers
{
    [AuthenticationFilter]
    public class RoleMenuMappingController : Controller
    {
        // GET: RoleMenuMapping
        [HttpGet]
        public ActionResult Save(int roleId = 0)
        {
            RoleManagementBLL objRoleManagementBll = new RoleManagementBLL();
            ViewBag.RoleInfo = objRoleManagementBll.GetRoleListForDropDownForSave();

            NavigationBLL objNavigationBll = new NavigationBLL();
            var menu = objNavigationBll.GetAllMenuForRoleMenuMapping(roleId);
            
            return View(menu);
        }

        [HttpPost]
        public JsonResult Save(IEnumerable<int> menuId, int roleId)
        {
            bool status = false;

            RoleManagementBLL objRoleManagementBll = new RoleManagementBLL();
            if (ModelState.IsValid)
            {
                objRoleManagementBll.ResetRoleMenuMapping(roleId);
                if (menuId != null)
                {
                    foreach (int item in menuId)
                    {
                        objRoleManagementBll.CreateRoleMenuMapping(item, roleId);
                    }
                }
                status = true;
            }
            return new JsonResult {Data = new { status = status }};
        }
    }
}