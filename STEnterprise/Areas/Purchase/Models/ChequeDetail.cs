using STEnterprise.Areas.Sale.Models;
using STEnterprise.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace STEnterprise.Areas.Purchase.Models
{
    public class ChequeDetail :CommonModel
    {
        [Key]
        public int ChequeDetailId { get; set; }

        [Display(Name = "Cheque Issued By")]        
        public byte ChequeIssuedBy { get; set; }

        public string ChequeIssuedByShow { get; set; }

        [Required(ErrorMessage = "Owner Name is required")]
        [Display(Name = "Owner Name")]        
        public short OwnerId { get; set; }

        public string OwnerName { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Cheque Number is required")]
        [Display(Name = "Cheque Number")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Cheque Number cannot be longer than 20 characters")]
        public string ChequeNumber { get; set; }


        [Required(ErrorMessage = "Bank Name is required")]
        [Display(Name = "Bank Name")]
        public byte BankDetailId { get; set; }

        public IEnumerable<BankDetail> BankNameInfo { get; set; }
        public string BankName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Cheque Amount is required")]
        [DataType(DataType.Text)]
        [Display(Name = "Cheque Amount")]
        public decimal ChequeAmount { get; set; }


        [Required(ErrorMessage = "Cheque Issue Date is required")]
        [Display(Name = "Cheque Issue Date")]
        [DataType(DataType.Date)]
        public DateTime ChequeIssueDate { get; set; }
        public string ChequeIssueDateShow { get; set; }


        [Display(Name = "Cheque Submit Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Cheque Submit Date is required")]
        public DateTime ChequeSubmitDate { get; set; }
        public string ChequeSubmitDateShow { get; set; }


        [Display(Name = "Cheque Matured Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Cheque Matured Date is required")]
        public DateTime ChequeMaturedDate { get; set; }
        public string ChequeMaturedDateShow { get; set; }

        [Display(Name = "Cheque Status")]
        public bool ChequeStatus { get; set; }
        public string ChequeStatusShow { get; set; }
    }
}