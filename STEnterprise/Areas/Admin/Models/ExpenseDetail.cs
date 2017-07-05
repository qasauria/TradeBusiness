using System.ComponentModel.DataAnnotations;
using STEnterprise.Models;

namespace STEnterprise.Areas.Admin.Models
{
    public class ExpenseDetail : CommonModel
    {
        [Key]
        public short ExpenseDetailId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Expense Type is required")]
        [DataType(DataType.Text)]
        [Display(Name = "Expense Type")]
        [StringLength(100, ErrorMessage = "Expense Type cannot be longer than 100 characters.", MinimumLength = 1)]
        public string ExpenseType { get; set; }
    }
}