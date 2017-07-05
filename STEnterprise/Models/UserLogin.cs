using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace STEnterprise.Models
{
    public class UserLogin
    {
        [Key]
        public int UserDetailId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "User Name Is Required")]
        [Display(Name = "Username")]
        [StringLength(50, ErrorMessage = "Must be between 3 and 50 characters long !", MinimumLength = 3)]
        [RegularExpression("([a-zA-Z .&'-]+)", ErrorMessage = "Enter Valid User Name !")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(130, ErrorMessage = "Must be between 3 and 130 characters long !", MinimumLength = 3)]
        public string Password { get; set; }

        public int UserRole { get; set; }
    }
}