using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using STEnterprise.Models;

namespace STEnterprise.Areas.Admin.Models
{
    // created by shovon
    public class DailyExpense : CommonModel
    {
        [Key]
        public int DailyExpenseId { get; set; }

        [Required]
        public decimal ExpenseDetailId { get; set; }
        public IEnumerable<ExpenseDetail> ExpenseDetailInfo { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Expense Type")]
        //[Required(ErrorMessage = "Country Name is Required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Expense Type cannot be longer than 100 characters.")]
        public string ExpenseType { get; set; }

        [Display(Name = "Total Amount")]
        [Required(ErrorMessage = "The Total Amount is required")]
        //[Range(2.00, 10.00, ErrorMessage = "The Total Amount must be between 2 and 100")]
        //[RegularExpression(@"^\d+\.\d{0,2}$")]
        [Range(0, 9999999999999999.99)]
        public double TotalAmount { get; set; }

        [Display(Name = "Paid Amount")]
        //[RegularExpression(@"^\d+\.\d{0,2}$")]
        [Range(0, 9999999999999999.99)]
        public double PaidAmount { get; set; }

        [Display(Name = "Due Amount")]
        //[RegularExpression(@"^\d+\.\d{0,2}$")]
        [Range(0, 9999999999999999.99)]
        public double DueAmount { get; set; }

        [Display(Name = "Date")]
      //  [DataType(DataType.Date)]
        public DateTime Date { get; set; }


    }
}