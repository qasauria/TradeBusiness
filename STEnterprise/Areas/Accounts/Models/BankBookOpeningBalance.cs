using STEnterprise.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace STEnterprise.Areas.Accounts.Models
{
    public class BankBookOpeningBalance:CommonModel
    {

        [Key]
        public int BankBookOpeningBalnceId { get; set; }

        [Display(Name = "Bank Book Opening Balnce Date")]
        [DisplayFormat(DataFormatString = "{0:dd/M/yy}", ApplyFormatInEditMode = true)]
        public DateTime BankBookOpeningBalnceDate { get; set; }

        [Display(Name = "Bank Name")]
        public string BankName { get; set; }
        public short BankDetailId { get; set; }


        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }       
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }
    }
}