using STEnterprise.Areas.Inventory.Models;
using STEnterprise.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace STEnterprise.Areas.Sale.Models
{
    public class SaleModel:CommonModel
    {
        [Key]
        public int SaleId { get; set; }

        [Display(Name="Customer Name")]
        public short? CustomerId { get; set; }
        //public IEnumerable<CustomerDetail> Customers { get; set; }
        public string CustomerName { get; set; }


        [Display(Name = "Consignment Number")]
        public string ConsignmentNumber { get; set; }

        [Required(ErrorMessage = "Order Number Is Required")]
        [Display(Name = "Order Number")]
        public string OrderNumber { get; set; }

        [Display(Name = "Truck Number")]
        //public int TruckDetailId { get; set; }
        public string TruckNumber { get; set; }


        [Display(Name = "Selling Date")]
        public DateTime? SellingDate { get; set; }

        public decimal? SellingAmount { get; set; }

        [Display(Name = "Total Amount")]
        public decimal? TotalAmount { get; set; }


        [Display(Name = "Adjustment Amount")]
        public decimal? AdjustmentAmount { get; set; }


        [Display(Name = "Paid Amount")]
        public decimal? PaidAmount { get; set; }


        [Display(Name = "Payment Method")]
        public byte? PaymentMethod { get; set; }

        [Display(Name = "Cheque No")]
        public string ChequeNumber { get; set; }

        [Display(Name = "T.T Number")]
        public string TTNUmber { get; set; }

        public decimal? PreviousDueAmount { get; set; }

        public virtual ICollection<SaleDetail> SaleDetails { get; set; }

        [Display(Name = "Product Name")]
        public short? ProductId { get; set; }
        //public IEnumerable<Product> Products { get; set; }
        public string ProductName { get; set; }

        public decimal? TotalReturnAmount { get; set; }
    }
}