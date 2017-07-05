using System;
using System.ComponentModel.DataAnnotations;

namespace STEnterprise.Models
{
    public class ReportModel
    {

        [Required(ErrorMessage = "Please Select Report Type")]
        [Display(Name = "Report Type")]
        public string ReportType { get; set; }

        [Display(Name = "Consignment Number")]
        public string ConsignmentNumber { get; set; }

        [Display(Name = "Date From")]
        [Required(ErrorMessage = "Please Select From Date")]
        public DateTime? FromDate { get; set; }

        [Display(Name = "To")]
        [Required(ErrorMessage = "Please Select To Date")]
        public DateTime? ToDate { get; set; }

        [Display(Name = "Supplier")]
        public int? SupplierId { get; set; }

        [Display(Name = "Customer")]
        public int? CustomerId { get; set; }

        [Display(Name = "Product")]
        public int? ProductId { get; set; }

        [Display(Name = "Truck")]
        public int? TruckDetailId { get; set; }

        [Display(Name = "Country")]
        public int? CountryId { get; set; }

    }
}