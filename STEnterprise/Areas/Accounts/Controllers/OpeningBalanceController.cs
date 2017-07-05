using STEnterprise.Areas.Accounts.BLL;
using STEnterprise.Areas.Accounts.Models;
using STEnterprise.Areas.Ledger.BLL;
using System.Web.Mvc;
using STEnterprise.AuthData;

namespace STEnterprise.Areas.Accounts.Controllers
{
    [AuthenticationFilter]
    public class OpeningBalanceController : Controller
    {
        // GET: Accounts/OpeningBalance
        public ActionResult Save()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(OpeningBalance objOpeningBalance)
        {
            if (ModelState.IsValid)
            {
                OpeningBalanceBLL objOpeningBalanceBLL = new OpeningBalanceBLL();
                objOpeningBalanceBLL.CreateOpeningBalnce(objOpeningBalance);
                
            }
            return Redirect("Save");
        }

        public JsonResult GetPartyNameByAccountType(string AccountType)
        {
            LedgerBLL objLedgerBLL = new LedgerBLL();
            var data = objLedgerBLL.GetPartyNameByAccountType(AccountType);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}