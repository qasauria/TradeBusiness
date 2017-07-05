using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using STEnterprise.Areas.Admin.Models;

namespace STEnterprise.Models
{
    public class AssignUserRole:CommonModel
    {

        [Key]
        public int UrmId { get; set; }

        [Required(ErrorMessage = "Username Is Required !")]
        [Display(Name = "Username")]
        public short UserDetailId { get; set; }
        public string Username { get; set; }
        public List<UserDetail> userDetail { get; set; }

        [Required(ErrorMessage = "Role Name Is Required !")]
        [Display(Name = "Role Name")]
        public byte RoleId { get; set; }
        public string RoleName { get; set; }
        public List<RoleInfo> roleInfo { get; set; }
    }
}