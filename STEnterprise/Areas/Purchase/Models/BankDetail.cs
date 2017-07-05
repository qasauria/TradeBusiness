using STEnterprise.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace STEnterprise.Areas.Purchase.Models
{
    public class BankDetail :CommonModel
    {

        [Key]
        public byte BankDetailId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Bank Name  is required")]
        [DataType(DataType.Text)]
        [Display(Name = "Bank Name")]
        [StringLength(maximumLength: 100, ErrorMessage = "Bank Name cannot be longer than 100 characters.", MinimumLength = 3)]
        [RegularExpression("([a-zA-Z .&'-]+)", ErrorMessage = "Enter Valid Bank Name !")]
        public string BankName { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Branch Name is required")]
        [DataType(DataType.Text)]
        [Display(Name = "Branch Name")]
        [StringLength(maximumLength: 100, ErrorMessage = "Branch Name cannot be longer than 100 characters.", MinimumLength = 3)]
        [RegularExpression("([a-zA-Z .&'-]+)", ErrorMessage = "Enter Valid Branch Name !")]
        public string BranchName { get; set; }


        [Display(Name = "Account Number")]
        [StringLength(maximumLength: 50, ErrorMessage = "Account Number must be between 3 to 50 character long", MinimumLength = 3)]
        public string AccountNumber { get; set; }


        
        [Required]
        [Display(Name = "Address")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Address cannot be longer than 100 characters")]
        public string Address { get; set; }

        [Display(Name = "Contact Person")]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "Contact Person cannot be longer than 100 characters.")]        
        public string ContactPerson { get; set; }

    }
}