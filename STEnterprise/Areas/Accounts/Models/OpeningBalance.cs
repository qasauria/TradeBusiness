using STEnterprise.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace STEnterprise.Areas.Accounts.Models
{
    public class OpeningBalance:CommonModel
    {
        [Key]
        public short OpeningBalanceId { get; set; }
        [Display(Name = "Opening Balance Date")]
        [DisplayFormat(DataFormatString = "{0:dd/M/yy}", ApplyFormatInEditMode = true)]
        public DateTime OpeningBalanceDate { get; set; }

        public string OpeningBalanceDateShow { get; set; }
        [Display(Name = "Account Type")]
        public string AccountType { get; set; }
        [Display(Name = "Party Name")]
        public short PartyId { get; set; }
        public string PartyName { get; set; }
        public string CustomerName { get; set; }

        public string SupplierName { get; set; }
        public string ExpenseType { get; set; }
        [Display(Name = "Amount")]
        [Required(ErrorMessage ="Amount Is requird")]
        public decimal Amount { get; set; }
        

    }
}