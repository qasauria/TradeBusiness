using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using STEnterprise.Models;

namespace STEnterprise.Areas.Purchase.Models
{
    //ataur
    public class PurchaseModel:CommonModel
    {
        [Key]
        public int PurchaseId { get; set; }

        //[Required(ErrorMessage = "Supplier Name Is Required")]
        [Display(Name = "Supplier Name")]
        public int SupplierId { get; set; }

        public string SupplierName { get; set; }

 
        //[Required (ErrorMessage = "Consignment Number Is Required")]
        [Display(Name = "Consignment Number")]
        public string ConsignmentNumber { get; set; }

        public int TruckDetailId { get; set; }


        [Display(Name = "Truck Number")]
        public string TruckNumber { get; set; }


        [Display(Name = "Purchase Date")]       
        public DateTime PurchaseDate { get; set; }

        public decimal PurchaseAmount { get; set; }


        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }


        [Display(Name = "Adjustment Amount")]
        public decimal? AdjustmentAmount { get; set; }


        [Display(Name = "Cheque Number")]
        public string ChequeNumber { get; set; }


        [Display(Name = "T.T Number")]
        public string TTNumber { get; set; }


        //[Required(ErrorMessage = "Paid Amount Is Required")]
        [Display(Name = "Paid Amount")]
        public decimal? PaidAmount { get; set; }

        //[Required(ErrorMessage = "Order Number Is Required")]
        [Display(Name ="Order Number")]
        public string OrderNumber { get; set; }

        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; }

        public byte PaymentMethod { get; set; }

        public decimal PreviousDueAmount { get; set; }

        public decimal ReturnAmount { get; set; }

        //[Required(ErrorMessage = "Product Name Is Required")]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
    }
}