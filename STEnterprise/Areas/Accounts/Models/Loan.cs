using STEnterprise.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace STEnterprise.Areas.Accounts.Models
{
    public class Loan : CommonModel
    {
        [Key]
        public int LoanId { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd/M/yy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public string DateShow { get; set; }

        public short BankDetailId { get; set; }
        public string BankName { get; set; }

        public IEnumerable<Loan> BankNameList { get; set; }

        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }


        public decimal Amount { get; set; }

        public decimal LoanPaid { get; set; }

        public string TransactionNumber { get; set; }


    }
}