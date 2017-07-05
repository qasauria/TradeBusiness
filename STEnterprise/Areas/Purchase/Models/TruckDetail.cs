using System.ComponentModel.DataAnnotations;
using STEnterprise.Models;

namespace STEnterprise.Areas.Purchase.Models
{
    /// <summary>
    /// created by shovon
    /// </summary>
    public class TruckDetail : CommonModel
    {
        [Key]
        public int TruckDetailId { get; set; }

        [Required(ErrorMessage = "Consignment Number is required")]
        [Display(Name = "Consignment Number")]
        public string ConsignmentNumber { get; set; }

        [Required(ErrorMessage = "Truck Number is required")]
        [Display(Name = "Truck Number")]
        public string TruckNumber { get; set; }

        [Required(ErrorMessage = "Truck Fare is required")]
        [Display(Name = "Truck Fare")]
        public decimal TruckFare { get; set; }

        [Display(Name = "Advance Payment")]
        public decimal AdvancePayment { get; set; }

        [Display(Name = "Loading Cost")]
        public decimal LoadingCost { get; set; }

        [Display(Name = "Unloading Cost")]
        public decimal UnloadingCost { get; set; }

        [Display(Name = "Other Cost")]
        public decimal OtherCost { get; set; }
    }
}