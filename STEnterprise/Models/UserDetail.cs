using System.ComponentModel.DataAnnotations;
using STEnterprise.Models;

namespace STEnterprise.Models
{
    //ataur
    public class UserDetail:CommonModel
    {
        [Key]
        public short UserDetailId { get; set; }

        [Display(Name = "Username")]
        [DataType(DataType.Text)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "User Name Is Required !")]
        [StringLength(50, ErrorMessage = "Must be between 3 and 50 characters long !", MinimumLength = 3)]
        [RegularExpression("([a-zA-Z .&'-]+)", ErrorMessage = "Enter Valid User Name !")]
        public string Username { get; set; }


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password Is Required !")]
        [StringLength(130, ErrorMessage = "Must be between 3 and 130 characters long !", MinimumLength = 3)]
        [RegularExpression(@"^(?=.{3,})(?=.*?[A-Z])(?=.*?[a-z])(?=.*?\d).*$", ErrorMessage = "Enter at least 6 characters,1 uppercase,1 lowercase,1 number !")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        //[Required(ErrorMessage = "Confirm Password Is Required !")]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirm password must be same!")]
        public string ConfirmPassword { get; set; }

        //[Required(ErrorMessage = "Brand Name Is Required !")]
        //public short BranchId { get; set; }
        //public IEnumerable<BranchInfo> branchInfo { get; set; }

        //[Display(Name = "Branch Name")]
        //public string BranchName { get; set; }
    }
}