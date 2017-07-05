using STEnterprise.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace STEnterprise.Areas.Employee.Models
{
    public class SalaryDetail : CommonModel
    {
        [Key]
        public short SalaryDetailId { get; set; }

        [Required(ErrorMessage = "Employee Name is required")]
        [Display(Name ="Employee Name")]
        public short EmployeeId { get; set; }

        public IEnumerable<EmployeeDetail> EmployeeInfo { get; set; }
        public string EmployeeName { get; set; }



        [Required(ErrorMessage = "Month to be paid field is required")]
        [Display(Name ="Month To Be Paid")]        
        public string MonthToBePaid { get; set; }


        [Display(Name ="Advanced Payment")]
        public decimal AdvancedPayment { get; set; }


        [Display(Name ="Working Days")]
        [Required(ErrorMessage = "Working Days is required")]
        public byte WorkingDays { get; set; }


        [Display(Name = "Absent Days")]
        public byte AbsentDays { get; set; }


        [Required(ErrorMessage = "Paid Salary is required")]
        [Display(Name = "Paid Salary")]
        public decimal PaidSalary { get; set; }


        //extara for calculation
        public string DesignationName { get; set; }

        [Display(Name ="Total Salary")]
        public decimal MonthlySalary { get; set; }

        [Display(Name = "Per Day Salary")]
        public decimal PerDaySalary { get; set; }

        [Display(Name = "Absent Wise Per Day Amount")]
        public decimal AbsentWisePerDayAmount { get; set; }

        [Display(Name = "Payable Salary")]
        public decimal PayableSalary { get; set; }

        [Display(Name = "Due Salary")]
        public decimal DueSalary { get; set; }
    }
}