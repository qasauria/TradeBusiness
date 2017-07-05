using System.ComponentModel.DataAnnotations;
using STEnterprise.Models;

namespace STEnterprise.Models
{
    //ataur
    public class UserDetailPartial:CommonModel
    {
        [Key]
        public short UserDetailId { get; set; }

        [Display(Name = "Username")]
        [DataType(DataType.Text)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "User Name Is Required !")]
        [StringLength(50, ErrorMessage = "Must be between 3 and 50 characters long !", MinimumLength = 3)]
        [RegularExpression("([a-zA-Z .&'-]+)", ErrorMessage = "Enter Valid User Name !")]
        public string Username { get; set; }
    }
}