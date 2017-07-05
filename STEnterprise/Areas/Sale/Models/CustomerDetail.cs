using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using STEnterprise.Models;

namespace STEnterprise.Areas.Sale.Models
{
    // created by shovon
    public class CustomerDetail : CommonModel
    {
        [Key]
        public short CustomerId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Customer Name is Required")]
        [Display(Name = "Customer Name")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Customer Name cannot be longer than 100 characters.")]
        public string CustomerName { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Contact Person is Required")]
        [Display(Name = "Customer Contact Person")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Customer Name cannot be longer than 100 characters.")]
        public string CustomerContactPerson { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Address is Required")]
        [Display(Name = "Address")]
        [StringLength(500, MinimumLength = 2, ErrorMessage = "Address cannot be longer than 500 characters.")]
        public string CustomerAddress { get; set; }

        [Required]
        public short CountryId { get; set; }
        public IEnumerable<Country> CountryInfo { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Country Name")]
        //[Required(ErrorMessage = "Country Name is Required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Country Name cannot be longer than 100 characters.")]
        public string CountryName { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Contact Number is Required")]
        [Display(Name = "Contact Number")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Contact Number cannot be longer than 25 characters.")]
        public string CustomerContactNumber { get; set; }

        [Display(Name = "Email Address")]
        [StringLength(50, ErrorMessage = "Enter Valid Email Address !", MinimumLength = 3)]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "Enter Valid Email Address !")]
        public string CustomerEmail { get; set; }
    }
}