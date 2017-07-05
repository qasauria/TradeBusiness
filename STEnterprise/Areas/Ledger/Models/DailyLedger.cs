using STEnterprise.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace STEnterprise.Areas.Ledger.Models
{
    public class DailyLedger
    {
        [Key]
        public int DailyLedgerId { get; set; }
        [Display(Name = "Transation Date")]
        public string TransactionId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/M/yy}", ApplyFormatInEditMode = true)]
        public DateTime TransactionDate { get; set; }
        [Display(Name = "Account Type")]
        public string TransactionDateShow { get; set; }
        public string AccountType { get; set; }
        [Display (Name ="Party Name")]
        public short PartyId { get; set; }
        public string PartyName { get; set; }     
        [Display(Name = "Ledger Entry Type")]


        public string CustomerName { get; set; }

        public string SupplierName { get; set; }
        //[Required(AllowEmptyStrings = true)]
        public string ExpenseType { get; set; }
        public string LedgerEntryType { get; set; }
        [Display(Name = "Debit Amount")]

        public decimal DebitAmount { get; set; }
        [Display(Name = "Credit Amount")]
        public decimal CreditAmount { get; set; }
        public string Remarks { get; set; }

    }
}