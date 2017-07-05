using STEnterprise.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace STEnterprise.Areas.Purchase.Models
{
    public class Supplier : CommonModel
    {
        [Key]
        public int SupplierId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Supplier name is required")]
        [DataType(DataType.Text)]
        [Display(Name = "Supplier Name")]
        [StringLength(maximumLength: 100, ErrorMessage = "Supplier Name cannot be longer than 100 characters.", MinimumLength = 3)]
        [RegularExpression("([a-zA-Z .&'-]+)", ErrorMessage = "Enter Valid Supplier Name !")]
        public string SupplierName { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Supplier Address is required")]
        [DataType(DataType.Text)]
        [Display(Name = "Supplier Address")]
        [StringLength(maximumLength: 500, ErrorMessage = "Supplier Address cannot be longer than 500 characters.", MinimumLength = 3)]
      //  [RegularExpression("([a-zA-Z .&'-]+)", ErrorMessage = "Enter Valid Supplier Address !")]
        public string SupplierAddress { get; set; }

        [Required]
        [Display(Name = "Supplier Contact No.")]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "Supplier Contact Number Must be at least 5 characters long.")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Digits Only.")]
        public string SupplierContactNo { get; set; }
        
        [Display(Name = "Supplier Email")]
        [StringLength(99, ErrorMessage = "Email cannot be longer than 50 characters.")]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
        public string SupplierEmail { get; set; }       
    }
}