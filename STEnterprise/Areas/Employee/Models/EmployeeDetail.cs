using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using STEnterprise.Models;

namespace STEnterprise.Areas.Employee.Models
{
    // created by shovon
    public class EmployeeDetail : CommonModel
    {
        [Key]
        public short EmployeeId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Employee Name is Required")]
        [Display(Name = "Employee Name")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Employee Name cannot be longer than 100 characters.")]
        public string EmployeeName { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Present Address is Required")]
        [Display(Name = "Present Address")]
        [StringLength(300, MinimumLength = 2, ErrorMessage = "Address cannot be longer than 300 characters.")]
        public string PresentAddress { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Permanent Address is Required")]
        [Display(Name = "Permanent Address")]
        [StringLength(300, MinimumLength = 2, ErrorMessage = "Address cannot be longer than 300 characters.")]
        public string PermanentAddress { get; set; }

        [Required(ErrorMessage = "Designation is Required")]
        public byte DesignationId { get; set; }
        
        public IEnumerable<Designation> DesignationInfo { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Designation")]
        //[Required(ErrorMessage = "Country Name is Required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Designation Name cannot be longer than 100 characters.")]
        public string DesignationName { get; set; }


        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Phone Number is Required")]
        [Display(Name = "Phone Number")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Phone Number cannot be longer than 25 characters.")]
        public string Phone { get; set; }

        [Display(Name = "Email Address")]
        [StringLength(50, ErrorMessage = "Enter Valid Email Address !", MinimumLength = 3)]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "Enter Valid Email Address !")]
        public string Email { get; set; }

        //[DataType(DataType.PhoneNumber)]
        //[StringLength(5, ErrorMessage = "Enter Numeric Value !", MinimumLength = 2)]
        //[RegularExpression("^(0?[1-9]|[1-9][0-9]|[1][1-9][1-9]|200)$", ErrorMessage = "Digits Only.")]
        public byte Age {get ; set; }

        [Display(Name = "Hire Date")]
        public string HireDateShow { get; set; }

        public DateTime HireDate { get; set; }


        [DataType(DataType.Text)]
        [Required(ErrorMessage = "NId is Required")]
        [Display(Name = "NID Number")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "NID cannot be longer than 30 characters.")]
        public string NationalId { get; set; }

        [Display(Name = "Monthly Salary")]
        [Required(ErrorMessage = "Salary is Required")]
        [Range(0, 9999999999999999.99)]
        public decimal MonthlySalary { get; set; }
    }
}