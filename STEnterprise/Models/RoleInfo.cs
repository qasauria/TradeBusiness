using System.ComponentModel.DataAnnotations;
using STEnterprise.Models;

namespace STEnterprise.Models
{
    public class RoleInfo:CommonModel
    {
        [Key]
        public byte RoleId { get; set; }

        [Display(Name = "Role Name")]
        [DataType(DataType.Text)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Role Name Is Required !")]
        [StringLength(100, ErrorMessage = "Must be between 3 and 100 characters long !", MinimumLength = 3)]
        [RegularExpression("([a-zA-Z  ]+)", ErrorMessage = "Enter Valid Role Name !")]
        public string RoleName { get; set; }
    }
}