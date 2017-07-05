using System.Web.Mvc;
using System.Web.Mvc.Filters;
using STEnterprise.BLL;
using STEnterprise.DAL;

namespace STEnterprise.AuthData
{
    public class AuthenticationFilter : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            NavigationBLL objNavigationBll = new NavigationBLL();
            string actionName = filterContext.ActionDescriptor.ActionName;
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var getRequestedMenuId = objNavigationBll.GetRequestedMenuId(controllerName, actionName);
            int id = getRequestedMenuId != null ? getRequestedMenuId.MenuId : 0;
            int userId = SessionUtility.STSessionContainer.UserID;

            bool getAuthenticMenuId = objNavigationBll.GetAuthenticMenuId(id, userId);

            if (!getAuthenticMenuId)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            //throw new System.NotImplementedException();
        }
    }
}