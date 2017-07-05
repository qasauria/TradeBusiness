using System.ComponentModel.DataAnnotations;
using STEnterprise.Models;

namespace STEnterprise.Areas.Accounts.Models
{
    //ataur
    public class AccountHead:CommonModel
    {
        [Key]
        public int AccountHeadId { get; set; }

        [Required]
        [Display(Name = "Account Code")]
        public string AccountCode { get; set; }


        [Required]
        [Display(Name = "Account Name")]
        public string AccountName { get; set; }



        [Display(Name = "Description")]
        public string Description { get; set; }


        [Required]
        [Display(Name = "Account Type")]
        public string AccountType { get; set; }
    }
}