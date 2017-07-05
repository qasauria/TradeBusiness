using STEnterprise.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace STEnterprise.Areas.Accounts.Models
{
    public class BankBookModel:CommonModel
    {

        [Key]
        public int BankBookId { get; set; }
        
        public string TransactionId { get; set; }

        [Display(Name = "Transation Date")]
        [DisplayFormat(DataFormatString = "{0:dd/M/yy}", ApplyFormatInEditMode = true)]
        public DateTime TransactionDate { get; set; }

        public string TransactionDateShow { get; set; }

        [Display(Name = "Bank Name")]
        public string BankName { get; set; }
        public short BankDetailId { get; set; }

        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }
        public string PartyName { get; set; }

        [Display(Name = "Entry Type")]
        public string BankEntryType { get; set; }
        [Display(Name = "Debit Amount")]

        public decimal DebitAmount { get; set; }
        [Display(Name = "Credit Amount")]
        public decimal CreditAmount { get; set; }
    }
}