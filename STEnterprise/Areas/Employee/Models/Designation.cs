using System.ComponentModel.DataAnnotations;
using STEnterprise.Models;

namespace STEnterprise.Areas.Employee.Models
{
    //created by ataur
    public class Designation: CommonModel
    {
        [Key]
        public byte DesignationId { get; set; }

        [Required]
        [Display(Name = "Designation Name")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Designation Name should between at least 2 to 100 characters long.")]
        public string DesignationName { get; set; }
    }
}