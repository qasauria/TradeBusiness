using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace STEnterprise.Areas.Purchase.Models
{
    //ataur
    public class PurchaseDetail
    {
        [Key]
        public int PurchaseDetailId { get; set; }
        public int TruckDetailId { get; set; }
        public string TruckNumber { get; set; }
        public int PurchaseId { get; set; }
        [ForeignKey("PurchaseId")]
        public virtual PurchaseModel Purchase { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal PurchaseUnitBag { get; set; }
        public decimal PurchaseUnitKg { get; set; }
        public decimal PurchasePrice { get; set; }

        public decimal ReturnPurchaseUnitBag { get; set; }
        public decimal ReturnPurchaseUnitKG { get; set; }
        public decimal ReturnPurchaseAmount { get; set; }
        public int TotalPrice { get; set; }
        public int SubTotal { get; set; }

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
        public PurchaseDetail()
        {
            Purchase = new PurchaseModel();
        }

    }
}