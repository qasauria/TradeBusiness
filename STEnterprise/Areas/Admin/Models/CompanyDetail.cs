using System.ComponentModel.DataAnnotations;
using STEnterprise.Models;

namespace STEnterprise.Areas.Admin.Models
{
    //created by ataur
    public class CompanyDetail:CommonModel
    {
        [Key]
        public short CompanyId { get; set; }


        [Required]
        [Display(Name = "Company Name")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Company name should between atleast 3 to 100 characters.")]
        public string CompanyName { get; set; }



        //[Required]
        [Display(Name = "Logo")]
        public byte[] Logo { get; set; }



        [Required]
        [Display(Name = "Address")]
        [StringLength(150, MinimumLength = 4, ErrorMessage = "Address should between atleast 4 to 150 characters.")]
        public string Address { get; set; }




        [Required]
        [Display(Name = "Phone")]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "Phone Number should between at least 5 to 15 characters long.")]
        [RegularExpression("^[0-9,+,-]+$", ErrorMessage = "Digits Only.")]
        public string Phone { get; set; }



        [Display(Name = "Fax")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Fax Number should between at least 3 to 15 characters long.")]
        public string Fax { get; set; }




        [Display(Name = "Email")]
        [StringLength(99, ErrorMessage = "Email cannot be longer than 100 characters.")]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
        public string Email { get; set; }




        [Display(Name = "Tin Certificate")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "TIN Number should between at least 3 to 50 characters long.")]
        public string TinCertificate { get; set; }




        [Display(Name = "Vat Reg. Number")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Vat Reg. Number should between at least 3 to 100 characters long.")]
        public string VatRegNumber { get; set; }
    }
}